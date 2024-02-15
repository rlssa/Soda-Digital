using System.Text;
using RoyalLifeSavings.Data;
using RoyalLifeSavings.Models;

namespace RoyalLifeSavings.Extensions;

public static class ModelExtensions
{
    public static (string Subject, string Body) BuyOrRentEmailBody(this BuyOrRentModel entity)
    {
        var keywords = new[]
        {
            ("[Option]", entity.BuyOrRentOptions.GetDescription()),
            ("[Ebooks]", string.Join(", ", entity.GetEbooks())), ("[OtherInformation]", entity.OtherInformation),
            ("[FirstName]", entity.FirstName), ("[LastName]", entity.LastName), ("[Email]", entity.Email),
            ("[State]", entity.State), ("[PhoneNumber]", entity.PhoneNumber), ("[SchoolOrOrg]", entity.SchoolOrg)
        };
        var body = keywords.Aggregate(
            @"<!doctype html><html xmlns='http://www.w3.org/1999/xhtml' xmlns:v='urn:schemas-microsoft-com:vml' xmlns:o='urn:schemas-microsoft-com:office:office'><head><title></title><!--[if !mso]><!--><meta http-equiv='X-UA-Compatible' content='IE=edge'><!--<![endif]--><meta http-equiv='Content-Type' content='text/html; charset=UTF-8'><meta name='viewport' content='width=device-width,initial-scale=1'><style type='text/css'>#outlook a { padding:0; }
          body { margin:0;padding:0;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%; }
          table, td { border-collapse:collapse;mso-table-lspace:0pt;mso-table-rspace:0pt; }
          img { border:0;height:auto;line-height:100%; outline:none;text-decoration:none;-ms-interpolation-mode:bicubic; }
          p { display:block;margin:13px 0; }</style><!--[if mso]>
        <noscript>
        <xml>
        <o:OfficeDocumentSettings>
          <o:AllowPNG/>
          <o:PixelsPerInch>96</o:PixelsPerInch>
        </o:OfficeDocumentSettings>
        </xml>
        </noscript>
        <![endif]--><!--[if lte mso 11]>
        <style type='text/css'>
          .mj-outlook-group-fix { width:100% !important; }
        </style>
        <![endif]--><!--[if !mso]><!--><link href='https://fonts.googleapis.com/css?family=Ubuntu:300,400,500,700' rel='stylesheet' type='text/css'><style type='text/css'>@import url(https://fonts.googleapis.com/css?family=Ubuntu:300,400,500,700);</style><!--<![endif]--><style type='text/css'>@media only screen and (min-width:480px) {
        .mj-column-per-100 { width:100% !important; max-width: 100%; }
      }</style><style media='screen and (min-width:480px)'>.moz-text-html .mj-column-per-100 { width:100% !important; max-width: 100%; }</style><style type='text/css'></style></head><body style='word-spacing:normal;'><div><!--[if mso | IE]><table align='center' border='0' cellpadding='0' cellspacing='0' class='' style='width:600px;' width='600' ><tr><td style='line-height:0px;font-size:0px;mso-line-height-rule:exactly;'><![endif]--><div style='margin:0px auto;max-width:600px;'><table align='center' border='0' cellpadding='0' cellspacing='0' role='presentation' style='width:100%;'><tbody><tr><td style='direction:ltr;font-size:0px;padding:20px 0;text-align:center;'><!--[if mso | IE]><table role='presentation' border='0' cellpadding='0' cellspacing='0'><tr><td class='' style='vertical-align:top;width:600px;' ><![endif]--><div class='mj-column-per-100 mj-outlook-group-fix' style='font-size:0px;text-align:left;direction:ltr;display:inline-block;vertical-align:top;width:100%;'><table border='0' cellpadding='0' cellspacing='0' role='presentation' style='vertical-align:top;' width='100%'><tbody><tr><td align='left' style='font-size:0px;padding:10px 25px;word-break:break-word;'><table cellpadding='0' cellspacing='0' width='100%' border='0' style='color:#000000;font-family:Ubuntu, Helvetica, Arial, sans-serif;font-size:13px;line-height:22px;table-layout:auto;width:100%;border:none;'><tr><td style='padding: 0 15px 0 0;'><strong>Option</strong></td><td style='padding: 0 15px;'>[Option]</td></tr><tr><td style='padding: 0 15px 0 0;'><strong>Ebook(s)</strong></td><td style='padding: 0 15px;'>[Ebooks]</td></tr><tr><td style='padding: 0 15px 0 0;'><strong>Other Information</strong></td><td style='padding: 0 15px;'>[OtherInformation]</td></tr><tr><td style='padding: 0 15px 0 0;'><strong>First Name</strong></td><td style='padding: 0 15px;'>[FirstName]</td></tr><tr><td style='padding: 0 15px 0 0;'><strong>Last Name</strong></td><td style='padding: 0 15px;'>[LastName]</td></tr><tr><td style='padding: 0 15px 0 0;'><strong>Email</strong></td><td style='padding: 0 15px;'>[Email]</td></tr><tr><td style='padding: 0 15px 0 0;'><strong>State or Territory</strong></td><td style='padding: 0 15px;'>[State]</td></tr><tr><td style='padding: 0 15px 0 0;'><strong>Phone Number</strong></td><td style='padding: 0 15px;'>[PhoneNumber]</td></tr><tr><td style='padding: 0 15px 0 0;'><strong>School or Org</strong></td><td style='padding: 0 15px;'>[SchoolOrOrg]</td></tr></table></td></tr></tbody></table></div><!--[if mso | IE]></td></tr></table><![endif]--></td></tr></tbody></table></div><!--[if mso | IE]></td></tr></table><![endif]--></div></body></html>",
            (current, keyword) => current.Replace(keyword.Item1, keyword.Item2));

        return ($"Buying Multiple Copies or Renting Request from {entity.FirstName} {entity.LastName}", body);
    }

    private static IEnumerable<string> GetEbooks(this BuyOrRentModel entity)
    {
        var ebooks = new List<string>();
        if (entity.LifeguardingManual)
            ebooks.Add("Lifeguarding Manual");
        if (entity.SwimmingAndWaterSafetyManual)
            ebooks.Add("Swimming and Water Safety Manual");
        if (entity.FirstAidManual)
            ebooks.Add("First Aid Manual");
        return ebooks;
    }
}
