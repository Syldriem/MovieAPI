namespace MovieCardsAPI.Models.Dtos
{
    public class MovieDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string DirectorName { get; set; }
        public string ContactInformationEmail { get; set; }
        public IEnumerable<int> GenreIds { get; set; }
        public IEnumerable<int> ActorIds { get; set; }
    }
}
