﻿namespace MovieCardsAPI.Models.Entities
{
    public class Director
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? Birthdate { get; set; }
        // Navigation property

        public ContactInformation ContactInformation { get; set; }

        public ICollection<Movie> Movies { get; set; } 
    }
}
