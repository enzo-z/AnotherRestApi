using System.ComponentModel.DataAnnotations;

namespace WebApiTest.Controllers.DTOs.Endereco
{
    public class CreateEnderecoDto
    {
        [Required(ErrorMessage = "Campo 'Logradouro' obrigatório!")]
        public string Logradouro { get; set; } = null!;

        [Required(ErrorMessage = "Campo 'Bairro' obrigatório!")]
        public string Bairro { get; set; } = null!;

        [Required(ErrorMessage = "Campo 'Numero' obrigatório!")]
        public int Numero { get; set; }
    }
}
