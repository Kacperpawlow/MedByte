using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medbyte.Models
{
    public class Rezerwacja
    {
        public int Id { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [Required]
        [ForeignKey("Tomograf")]
        public int TomografId { get; set; }

        public Tomograf Tomograf { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string Imie { get; set; }

        [Required]
        public string Nazwisko { get; set; }

        [Required]
        public string Pesel { get; set; }
    }
}
