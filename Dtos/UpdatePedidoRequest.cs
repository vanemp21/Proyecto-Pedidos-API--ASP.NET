public class UpdatePedidoRequest
{
    public string Nombre { get; set; } = string.Empty;

    public decimal Precio { get; set; }

    public bool Estado { get; set; }

    public int ClienteId { get; set; }
}