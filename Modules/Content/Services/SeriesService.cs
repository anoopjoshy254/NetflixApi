using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Netflix.API.Modules.Content.DTOs.Series;
using Netflix.API.Modules.Content.Models;
using Netflix.API.Modules.Content.Repositories.Interfaces;
using Netflix.API.Modules.Content.Services.Interfaces;

namespace Netflix.API.Modules.Content.Services
{
    public class SeriesService : ISeriesService
    {
        private readonly ISeriesRepository _seriesRepository;

        public SeriesService(ISeriesRepository seriesRepository)
        {
            _seriesRepository = seriesRepository;
        }

        private SeriesResponseDto MapToDto(Series series)
        {
            if (series == null) return null;
            return new SeriesResponseDto
            {
                Id = series.Id,
                Title = series.Title,
                Description = series.Description,
                ReleaseYear = series.ReleaseYear,
                MaturityRating = series.MaturityRating,
                PosterUrl = series.PosterUrl,
                IsActive = series.IsActive
            };
        }

        public async Task<SeriesResponseDto> GetByIdAsync(int id)
        {
            var series = await _seriesRepository.GetByIdAsync(id);
            return MapToDto(series);
        }

        public async Task<IEnumerable<SeriesResponseDto>> GetAllAsync()
        {
            var series = await _seriesRepository.GetAllAsync();
            return series.Select(MapToDto);
        }

        public async Task<SeriesResponseDto> CreateAsync(CreateSeriesDto dto)
        {
            var series = new Series
            {
                Title = dto.Title,
                Description = dto.Description,
                ReleaseYear = dto.ReleaseYear,
                MaturityRating = dto.MaturityRating,
                PosterUrl = dto.PosterUrl,
                IsActive = true
            };

            var created = await _seriesRepository.AddAsync(series);
            return MapToDto(created);
        }

        public async Task UpdateAsync(int id, UpdateSeriesDto dto)
        {
            var series = await _seriesRepository.GetByIdAsync(id);
            if (series != null)
            {
                series.Title = dto.Title;
                series.Description = dto.Description;
                series.ReleaseYear = dto.ReleaseYear;
                series.MaturityRating = dto.MaturityRating;
                series.PosterUrl = dto.PosterUrl;
                series.IsActive = dto.IsActive;

                await _seriesRepository.UpdateAsync(series);
            }
        }

        public async Task DeleteAsync(int id)
        {
            await _seriesRepository.DeleteAsync(id);
        }
    }
}
