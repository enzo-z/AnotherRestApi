using System.ComponentModel.DataAnnotations;

namespace WebApiTest.Controllers.DTOs.Cinema
{
    public class ReadCinemaDto
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo 'Nome' é obrigatório!")]
        public string Nome { get; set; } = null!;
    }
}
