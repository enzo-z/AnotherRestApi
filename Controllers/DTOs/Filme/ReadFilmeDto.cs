using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiTest.Controllers.DTOs.Filme
{
    public class ReadFilmeDto
    {
        [Key]
        [Required]
        public short Id { get; set; }

        [Required(ErrorMessage = "Título não informado!")]
        public string Titulo { get; set; } = null!;

        [Required(ErrorMessage = "Diretor não informado!")]
        public string Diretor { get; set; } = null!;

        [StringLength(30, ErrorMessage = $"Campo genero excede o limite de caracteres permitido (30)")]

        public string? Genero { get; set; }

        [Range(1, 5100, ErrorMessage = "Duração não se encontra no limite permitido (1-5100) min")]
        public short Duracao { get; set; }

        public DateTime HorarioConsulta { get; set; } = DateTime.Now;
    }
}
