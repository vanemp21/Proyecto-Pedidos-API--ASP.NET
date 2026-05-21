using System.ComponentModel.DataAnnotations;

namespace Pedidos_ASP.Dtos
{
    public class CreateCliente
    {
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(20)]
        public string Telefono { get; set; } = string.Empty;
    }
}