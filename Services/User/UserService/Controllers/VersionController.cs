using Integrations.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace UserService.Controllers
{
    public class VersionController : BaseVersionController
    {
        public override string ServiceName => "User";
    }
}
