namespace Pedidos_ASP.Dtos
{
    public class ClienteResponse
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public List<PedidoSimpleResponse> Pedidos { get; set; } = new();

        public string Telefono { get; set; } = string.Empty;
    }
}