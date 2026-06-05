using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Netflix.API.Modules.Content.DTOs.Episode;
using Netflix.API.Modules.Content.Models;
using Netflix.API.Modules.Content.Repositories.Interfaces;
using Netflix.API.Modules.Content.Services.Interfaces;

namespace Netflix.API.Modules.Content.Services
{
    public class EpisodeService : IEpisodeService
    {
        private readonly IEpisodeRepository _episodeRepository;

        public EpisodeService(IEpisodeRepository episodeRepository)
        {
            _episodeRepository = episodeRepository;
        }

        private EpisodeResponseDto MapToDto(Episode episode)
        {
            if (episode == null) return null;
            return new EpisodeResponseDto
            {
                Id = episode.Id,
                SeasonId = episode.SeasonId,
                EpisodeNumber = episode.EpisodeNumber,
                Title = episode.Title,
                Description = episode.Description,
                DurationMinutes = episode.DurationMinutes,
                VideoUrl = episode.VideoUrl
            };
        }

        public async Task<EpisodeResponseDto> GetByIdAsync(int id)
        {
            var episode = await _episodeRepository.GetByIdAsync(id);
            return MapToDto(episode);
        }

        public async Task<IEnumerable<EpisodeResponseDto>> GetBySeasonIdAsync(int seasonId)
        {
            var episodes = await _episodeRepository.GetBySeasonIdAsync(seasonId);
            return episodes.Select(MapToDto);
        }

        public async Task<EpisodeResponseDto> CreateAsync(CreateEpisodeDto dto)
        {
            var episode = new Episode
            {
                SeasonId = dto.SeasonId,
                EpisodeNumber = dto.EpisodeNumber,
                Title = dto.Title,
                Description = dto.Description,
                DurationMinutes = dto.DurationMinutes,
                VideoUrl = dto.VideoUrl
            };

            var created = await _episodeRepository.AddAsync(episode);
            return MapToDto(created);
        }

        public async Task UpdateAsync(int id, UpdateEpisodeDto dto)
        {
            var episode = await _episodeRepository.GetByIdAsync(id);
            if (episode != null)
            {
                episode.EpisodeNumber = dto.EpisodeNumber;
                episode.Title = dto.Title;
                episode.Description = dto.Description;
                episode.DurationMinutes = dto.DurationMinutes;
                episode.VideoUrl = dto.VideoUrl;

                await _episodeRepository.UpdateAsync(episode);
            }
        }

        public async Task DeleteAsync(int id)
        {
            await _episodeRepository.DeleteAsync(id);
        }
    }
}
