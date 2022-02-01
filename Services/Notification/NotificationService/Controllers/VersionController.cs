using Integrations.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace NotificationService.Controllers
{
    public class VersionController : BaseVersionController
    {
        public override string ServiceName => "Notification";
    }
}
