using System;
using System.ComponentModel.DataAnnotations;

namespace movie.Data.Entities
{
    public class Movie
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
    }
}
