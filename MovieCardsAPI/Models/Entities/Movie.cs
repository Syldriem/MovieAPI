namespace MovieCardsAPI.Models.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Rating { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public int DirectorId { get; set; }
        public Director Director { get; set; }

    }
}
