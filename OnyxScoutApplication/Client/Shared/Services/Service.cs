using System.Threading.Tasks;

namespace OnyxScoutApplication.Client.Shared;

public interface IService
{
    Task OnInit(string eventKey);
}