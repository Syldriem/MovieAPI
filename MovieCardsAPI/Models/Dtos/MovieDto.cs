namespace MovieCardsAPI.Models.Dtos
{
    public record MovieDto(int Id, string Title, string Rating, DateTime ReleaseDate, string Description, int DirectorId);
}
