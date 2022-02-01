using GatewayAPI.Configuration;
using GatewayAPI.Interfaces;
using Microsoft.Extensions.Options;
using Utils;
using static Integrations.Versioning.BaseVersionController;

namespace Gateway.Implementations
{
    public class ValidationConnectionService : IVersionableService
    {
        private readonly UrlsConfig _urls;

        public ValidationConnectionService(IOptions<UrlsConfig> config)
        {
            _urls = config.Value;
        }

        public async Task<BaseVersionDto> GetVersion()
        {
            return await RestUtils.SendRequestAsync<BaseVersionDto>(_urls.ValidationService + UrlsConfig.VersionApi, HttpMethod.Get);
        }

    }
}
