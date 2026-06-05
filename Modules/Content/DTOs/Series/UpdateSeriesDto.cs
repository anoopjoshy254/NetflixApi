namespace Netflix.API.Modules.Content.DTOs.Series
{
    public class UpdateSeriesDto : CreateSeriesDto
    {
        public bool IsActive { get; set; }
    }
}
