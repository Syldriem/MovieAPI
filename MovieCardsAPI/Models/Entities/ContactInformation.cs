﻿namespace MovieCardsAPI.Models.Entities
{
    public class ContactInformation
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int phoneNumber { get; set; }


        public int DirectorId { get; set; }

        public Director Director { get; set; }
    }
}
