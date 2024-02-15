using Polly;
using Polly.Retry;

namespace RoyalLifeSavings.Services
{
    public static class Policies
    {
        public static AsyncRetryPolicy Retry => Policy.Handle<Exception>().WaitAndRetryAsync(new[]
            {
                 TimeSpan.FromSeconds(1),
                 TimeSpan.FromSeconds(3),
                 TimeSpan.FromSeconds(5),
                 TimeSpan.FromSeconds(10)
            });

        public static AsyncRetryPolicy<HttpResponseMessage> RetryFulfillment => Policy
               .Handle<Exception>()
               .OrResult<HttpResponseMessage>(x => x.StatusCode == System.Net.HttpStatusCode.InternalServerError)
               .WaitAndRetryAsync(new[]
               {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(3),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10)
               });
    }
}
