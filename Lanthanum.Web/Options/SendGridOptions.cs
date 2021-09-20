namespace Lanthanum.Web.Options
{
    public class SendGridOptions
    {
        public string ApiKey { get; set; }
        public string SenderEmail { get; set; }
        public string SenderName { get; set; }
        public string WelcomeMailTemplateId { get; set; }
        public string BaseUrl { get; set; }
    }
}