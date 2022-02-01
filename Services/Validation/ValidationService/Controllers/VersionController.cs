using Integrations.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace ValidationService.Controllers
{
    public class VersionController : BaseVersionController
    {
        public override string ServiceName => "Validation";
    }
}
