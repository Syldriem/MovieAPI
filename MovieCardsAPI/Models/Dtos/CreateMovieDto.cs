using MovieCardsAPI.Models.Entities;

namespace MovieCardsAPI.Models.Dtos
{
    public class CreateMovieDto
    {
        public string Title { get; set; }
        public string Rating { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public int DirectorId { get; set; }
        public IEnumerable<int> GenreIds{ get; set; }
        public IEnumerable<string> ActorNames { get; set; }
    }
}
