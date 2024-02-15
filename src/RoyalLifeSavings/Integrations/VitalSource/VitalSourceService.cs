using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using System.Xml.Serialization;
using RoyalLifeSavings.Data;
using RoyalLifeSavings.Integrations.VitalSource.Responses;
using static RoyalLifeSavings.Services.Policies;

namespace RoyalLifeSavings.Integrations.VitalSource
{
    public class VitalSourceService
    {
        private readonly HttpClient _client;
        

        public VitalSourceService(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> VerifyUserAsync(ApplicationUser user)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.vitalsource.com/v3/credentials.xml");
            var element = new XElement("credentials", new XElement("credential", new XAttribute("reference", user.Email)));
            request.Content = CreateContent(element);
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var serializer = new XmlSerializer(typeof(Credentials));
            var credentialsResponse = serializer.Deserialize(await response.Content.ReadAsStreamAsync());
            var credentials = credentialsResponse as Credentials;
            if (credentials.Credential == null)
            {
                var errorSerializer = new XmlSerializer(typeof(CredentialsError));
                var errorResponse = errorSerializer.Deserialize(await response.Content.ReadAsStreamAsync());
                var errors = errorResponse as CredentialsError;
                if (errors.Error.Code == 601)
                {
                    //reset token an fetch again
                    await ResetAccessTokenAsync(user.VitalSourceAccessToken);
                    response = await _client.SendAsync(request);
                    credentialsResponse = serializer.Deserialize(await response.Content.ReadAsStreamAsync());
                    credentials = credentialsResponse as Credentials;
                    user.VitalSourceAccessToken = credentials.Credential.AccessToken;
                }
                else
                {
                    throw new VitalSourceException(string.Join("|", errors));
                }
            }

            return credentials.Credential.AccessToken;
        }

        //this returns empty body
        private async Task ResetAccessTokenAsync(string accessToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.vitalsource.com/v3/reset_access.xml");
            request.Headers.Add("X-VitalSource-Access-Token", accessToken);
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task CreateReferenceUserAsync(ApplicationUser user)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.vitalsource.com/v3/users.xml");


            //first we'll check if there is already an existing user in Vitalsource with this email token.
            var existingUser = await GetUserDetailsFullAsync(user);

            if (existingUser is not null)
            {
                //This means that we have an existing user with this email address
                user.VitalSourceReferenceString = existingUser.ReferralGuid;
            }
            else
            {
                var data = new Dictionary<string, string>()
                {
                    { "reference", user.Email },
                    { "first-name", "#####"},
                    { "last-name", "####" },
                };
                var xmlContent = CreateXElement("user", data);
                request.Content = CreateContent(xmlContent);
                var response = await _client.SendAsync(request);

                response.EnsureSuccessStatusCode();


                var serializer = new XmlSerializer(typeof(User), new XmlRootAttribute("user"));
                var userResponse = serializer.Deserialize(await response.Content.ReadAsStreamAsync()) as User;
                if (userResponse is null)
                {
                    throw new VitalSourceException("User is empty");
                }

                user.VitalSourceReferenceString = userResponse.Guid;
                user.VitalSourceAccessToken = userResponse.Accesstoken;
            }
            
        }

        //this throws 500 (don't know why) but add license to the user 
        public async Task FullfilmentAsync(ApplicationUser user, string accessToken, string? ebookId)
        {
            var response = await RetryFulfillment.ExecuteAsync(async () =>
               {
                   var request = new HttpRequestMessage(HttpMethod.Post, "https://api.vitalsource.com/v4/fulfillments");
                   request.Headers.Add("X-VitalSource-Access-Token", accessToken);
                   var model = new
                   {
                       fulfillment = new
                       {
                           sku = ebookId,
                           term = "Perpetual",
                           tag = $"fulfillment_{user.Email}"
                       }
                   };

                   request.Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
                   return await _client.SendAsync(request);

               });
            //the user has been alredy added to the ebook
            if ((int)response.StatusCode == 422)
            {
                user.UserLicences.Add(new ApplicationUserEBook { FulfillmentAdded = true, EBookId = ebookId });
                return;
            }
            if (!response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var errors = JsonSerializer.Deserialize<ErrorResponse>(responseContent);
                throw new VitalSourceException(string.Join('|', errors.errors));
            }
            response.EnsureSuccessStatusCode();

            var fulfillment = JsonSerializer.Deserialize<Fulfillment>(await response.Content.ReadAsStringAsync());
            user.UserLicences.Add(new ApplicationUserEBook { VitalSourceFulfillmetCode = fulfillment?.code, EBookId = ebookId });
        }


        private async Task<UserDetailsFull?> GetUserDetailsFullAsync(ApplicationUser user)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.vitalsource.com/v3/users.xml/{user.Email}?full=true");
            var response = await _client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            var serializer = new XmlSerializer(typeof(UserDetailsFull), new XmlRootAttribute("user"));
            var userResponse = serializer.Deserialize(await response.Content.ReadAsStreamAsync());
            var userdetails = userResponse as UserDetailsFull;
            if (userdetails is null) { return null; }
            return userdetails;
        }



        public async Task<string?> GetOnlineAccessAsync(string userAccessToken, string bookId)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.vitalsource.com/v3/redirects.xml");
            request.Headers.Add("X-VitalSource-Access-Token", userAccessToken);
            var data = new Dictionary<string, string>()
            {
                // Destination URL for Bookshelf online eReader. (Note: Branded versions of Bookshelf have different URLs.)
                { "destination", $"https://bookshelf.vitalsource.com/#/books/{bookId}" },
            };
            var xmlContent = CreateXElement("redirect", data);
            request.Content = CreateContent(xmlContent);
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var serializer = new XmlSerializer(typeof(Redirect));

            var redirect = serializer.Deserialize(await response.Content.ReadAsStreamAsync()) as Redirect;

            return redirect?.AutoSignin;
        }

        private XElement CreateXElement(string root, Dictionary<string, string> valuePairs)
        {
            var element = new XElement(
                root,
                valuePairs.Select(x => new XElement(x.Key, x.Value))
                );
            return element;
        }

        private StringContent CreateContent(object model) => new StringContent(model.ToString() ?? string.Empty, Encoding.UTF8, "text/xml");
    }
}
