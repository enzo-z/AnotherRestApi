using System.ComponentModel.DataAnnotations;

namespace WebApiTest.Models
{
    public class Cinema
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo 'Nome' é obrigatório!")]
        public string Nome { get; set; } = null!;

        public Endereco Endereco { get; set; } = null!;

        public int EnderecoId { get; set; }
    }
}
