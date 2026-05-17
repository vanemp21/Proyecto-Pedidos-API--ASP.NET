namespace Pedidos_ASP.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        // Un cliente tiene muchos pedidos
        public List<Pedido> Pedidos { get; set; } = new();
    }
}