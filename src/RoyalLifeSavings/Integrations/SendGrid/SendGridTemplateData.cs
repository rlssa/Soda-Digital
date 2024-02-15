namespace RoyalLifeSavings.Integrations.SendGrid;

public class SendGridTemplateData
{
    public static object LoginLink(string url)
    {
        return new { loginlink = url };
    }
}
