using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Integrations.Versioning
{
    public abstract class BaseVersionController : ControllerBase
    {
        public abstract string ServiceName { get; }

        [HttpGet]
        [Route("api/version")]
        [ResponseCache(Duration = 9999)]
        public BaseVersionDto GetVersionService()
        {
            return new BaseVersionDto
            {
                Version = Assembly.GetAssembly(this.GetType()).GetName().Version.ToString(),
                Name = this.ServiceName
            };
        }

        public class BaseVersionDto
        {
            public string Version { get; set; }
            public string Name { get; set; }
        }

    }
}
