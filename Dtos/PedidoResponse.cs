namespace Pedidos_ASP.Dtos
{
    public class PedidoResponse
    {
        public int Id { get; set; }

        public string nombre { get; set; } = string.Empty;

        public int precio { get; set; }

        public bool estado { get; set; }

        public ClienteResponse? Cliente { get; set; }
    }
}