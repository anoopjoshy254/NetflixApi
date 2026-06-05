using System.Threading.Tasks;
using Netflix.API.Modules.Content.DTOs.Search;

namespace Netflix.API.Modules.Content.Services.Interfaces
{
    public interface ISearchService
    {
        Task<SearchResponseDto> SearchAsync(string query);
    }
}
