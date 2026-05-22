namespace Pedidos_ASP.Dtos
{
    public class PedidoResponse
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public int Precio { get; set; }

        public bool Estado { get; set; }

        public ClienteResponse? Cliente { get; set; }
    }
}