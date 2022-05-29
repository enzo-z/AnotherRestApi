using System.ComponentModel.DataAnnotations;

namespace WebApiTest.Controllers.DTOs.Cinema
{
    public class UpdateCinemaDto
    {
        [Required(ErrorMessage = "Campo 'Nome' é obrigatório!")]
        public string Nome { get; set; } = null!;
    }
}
