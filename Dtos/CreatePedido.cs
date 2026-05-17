using System.ComponentModel.DataAnnotations;

namespace Pedidos_ASP.Dtos
{
    public class CreatePedido
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres")]
        public string nombre { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "El precio debe ser mayor que 0")]
        public int precio { get; set; }

        public bool estado { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El cliente es obligatorio")]
        public int ClienteId { get; set; }
    }
}