using System.Threading.Tasks;
using static Integrations.Versioning.BaseVersionController;

namespace GatewayAPI.Interfaces
{
    public interface IVersionableService
    {
        Task<BaseVersionDto> GetVersion();
    }
}
