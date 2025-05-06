using System.Collections.Generic;
using System.Threading.Tasks;

namespace Azure.Interfaces
{
    public interface IOneriService
    {
        Task<string> GetDiziOnerisiAsync(List<string> userPreferences);
        Task<string> GetFilmOnerisiAsync(List<string> userPreferences);
    }
}