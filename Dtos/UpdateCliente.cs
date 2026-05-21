using System.ComponentModel.DataAnnotations;

namespace Pedidos_ASP.Dtos
{
    public class UpdateCliente
    {
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [MaxLength(20)]
        public string Telefono { get; set; } = string.Empty;
    }
}