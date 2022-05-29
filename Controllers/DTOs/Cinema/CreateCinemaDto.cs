using System.ComponentModel.DataAnnotations;

namespace WebApiTest.Controllers.DTOs.Cinema
{
    public class CreateCinemaDto
    {
        [Required(ErrorMessage = "Campo 'Nome' é obrigatório!")]
        public string Nome { get; set; } = null!;
    }
}
