namespace Pedidos_ASP.Models
{
    public class Pedido
    {
        public string nombre { get; set; } = string.Empty;

        public int Id { get; set; }

        public int precio { get; set; } = 0;


        public bool estado { get; set; } = false;
    }
}
