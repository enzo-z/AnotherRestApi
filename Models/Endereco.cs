using System.ComponentModel.DataAnnotations;

namespace WebApiTest.Models
{
    public class Endereco
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo 'Logradouro' obrigatório!")]
        public string Logradouro { get; set; } = null!;

        [Required(ErrorMessage = "Campo 'Bairro' obrigatório!")]
        public string Bairro { get; set; } = null!;
        
        [Required(ErrorMessage = "Campo 'Numero' obrigatório!")]
        public int Numero { get; set; }

        public Cinema Cinema { get; set; }
    }
}
