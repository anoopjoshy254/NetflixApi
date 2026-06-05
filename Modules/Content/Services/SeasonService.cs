using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Netflix.API.Modules.Content.DTOs.Season;
using Netflix.API.Modules.Content.Models;
using Netflix.API.Modules.Content.Repositories.Interfaces;
using Netflix.API.Modules.Content.Services.Interfaces;

namespace Netflix.API.Modules.Content.Services
{
    public class SeasonService : ISeasonService
    {
        private readonly ISeasonRepository _seasonRepository;

        public SeasonService(ISeasonRepository seasonRepository)
        {
            _seasonRepository = seasonRepository;
        }

        private SeasonResponseDto MapToDto(Season season)
        {
            if (season == null) return null;
            return new SeasonResponseDto
            {
                Id = season.Id,
                SeriesId = season.SeriesId,
                SeasonNumber = season.SeasonNumber,
                Title = season.Title,
                ReleaseDate = season.ReleaseDate
            };
        }

        public async Task<SeasonResponseDto> GetByIdAsync(int id)
        {
            var season = await _seasonRepository.GetByIdAsync(id);
            return MapToDto(season);
        }

        public async Task<IEnumerable<SeasonResponseDto>> GetBySeriesIdAsync(int seriesId)
        {
            var seasons = await _seasonRepository.GetBySeriesIdAsync(seriesId);
            return seasons.Select(MapToDto);
        }

        public async Task<SeasonResponseDto> CreateAsync(CreateSeasonDto dto)
        {
            var season = new Season
            {
                SeriesId = dto.SeriesId,
                SeasonNumber = dto.SeasonNumber,
                Title = dto.Title,
                ReleaseDate = dto.ReleaseDate
            };

            var created = await _seasonRepository.AddAsync(season);
            return MapToDto(created);
        }
    }
}
