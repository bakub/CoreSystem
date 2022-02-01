using GatewayAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static Integrations.Versioning.BaseVersionController;

namespace Gateway.Controllers
{
    [ApiController]
    [Route("api")]
    public class VersionController : ControllerBase
    {

        private IEnumerable<IVersionableService> _services { get; set; }

        public VersionController(IEnumerable<IVersionableService> services)
        {
            _services = services;
        }

        [HttpGet]
        [Route("versions")]
        public async Task<VersionsDto> GetVersions()
        {
            try
            {
                var listTasks = _services.Select(x => x.GetVersion()).ToList();
                await Task.WhenAll(listTasks);
                return new VersionsDto
                {
                    Version = null,
                    Modules = listTasks.Select(x => x.Result)
                };
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return new VersionsDto { Version = ex.Message };
            }
        }

        public class VersionsDto
        {
            public string Version { get; set; }
            public IEnumerable<BaseVersionDto> Modules { get; set; }
        }
    }
}