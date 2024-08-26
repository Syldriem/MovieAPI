namespace MovieCardsAPI.Models.Entities
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }

        public ICollection<Movie> Movies { get; set; }
    }


}
