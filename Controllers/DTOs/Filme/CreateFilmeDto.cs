using System.ComponentModel.DataAnnotations;

namespace WebApiTest.Controllers.DTOs.Filme
{
    public class CreateFilmeDto
    {
        [Required(ErrorMessage = "Título no meu DTO não informado!")]
        public string Titulo { get; set; } = null!;

        [Required(ErrorMessage = "Diretor não informado!")]
        public string Diretor { get; set; } = null!;

        [StringLength(30, ErrorMessage = $"Campo genero excede o limite de caracteres permitido (30)")]
        public string? Genero { get; set; }

        [Range(1, 5100, ErrorMessage = "Duração não se encontra no limite permitido (1-5100) min")]
        public short Duracao { get; set; }
    }
}
