namespace Pedidos_ASP.Dtos
{
    public class PedidoFilterRequest
    {
        public string? Nombre { get; set; }

        public int? PrecioMinimo { get; set; }

        public bool? Estado { get; set; }

        public string? Orden { get; set; }

        public string? Direccion { get; set; }

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}