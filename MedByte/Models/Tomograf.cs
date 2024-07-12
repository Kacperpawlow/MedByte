using System.ComponentModel.DataAnnotations;

namespace Medbyte.Models
{
    public class Tomograf
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa jest wymagana.")]
        public string Nazwa { get; set; }

        public string? Opis { get; set; }

        [Required(ErrorMessage = "Telefon jest wymagany.")]
        [Phone(ErrorMessage = "Niepoprawny format numeru telefonu.")]
        public string Telefon { get; set; }

        [Required(ErrorMessage = "Ulica jest wymagana.")]
        public string Ulica { get; set; }

        [Required(ErrorMessage = "Miasto jest wymagane.")]
        public string Miasto { get; set; }

        [Required(ErrorMessage = "Kod pocztowy jest wymagany.")]
        [RegularExpression(@"\d{2}-\d{3}", ErrorMessage = "Niepoprawny format kodu pocztowego.")]
        public string KodPocztowy { get; set; }

        public string? ImagePath { get; set; }

        public string? UserId { get; set; }
    }
}
