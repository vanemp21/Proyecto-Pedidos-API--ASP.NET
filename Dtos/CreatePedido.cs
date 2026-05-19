using System.ComponentModel.DataAnnotations;

namespace Pedidos_ASP.Dtos
{
    public class CreatePedido
    {
        [Required]
        [MaxLength(100)]
        public string nombre { get; set; } = string.Empty;

        [Range(1, 999999)]
        public int precio { get; set; }

        public bool estado { get; set; }

        [Range(1, int.MaxValue)]
        public int ClienteId { get; set; }
    }
}