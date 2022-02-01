namespace GatewayAPI.Configuration
{
    public class UrlsConfig
    {
        public string NotificationService { get; set; }
        public string UserService { get; set; }
        public string ValidationService { get; set; }
        public static string VersionApi => "/api/version";
    }
}
