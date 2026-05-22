using Pedidos_ASP.Dtos;
using Pedidos_ASP.Models;

namespace Pedidos_ASP.Service
{
    public interface IPedidoService
    {
        Task<List<Pedido>> GetAllPedidos();

        Task<Pedido?> GetPedidoById(int id);

        Task<int> GetPrecioById(int id);

        Task<int> GetPrecioByNombre(string nombre);

        Task<string> GetEstadoById(int id);

        Task<bool> UpdatePedido(int id, UpdatePedido actualizar);

        Task<PedidoResponse?> CreatePedido(CreatePedido pedido);

        Task<bool> DeletePedido(int id);

        Task<List<Pedido>> GetPedidosCaros(int precio);

        Task<List<Pedido>> GetPedidoByname(string nombre);

        Task<List<Pedido>> GetPedidosOrdenadosPorPrecio();

        Task<List<Pedido>> BuscarPedidos(string nombre);

        Task<int> ContarPedidos();

        Task<bool> ExistePedidoPorNombre(string nombre);

        Task<PagedResponse<Pedido>> GetPedidosPaginados(int page, int pageSize);

        //Task<List<Pedido>> FiltrarPedidos(PedidoFilterRequest filtro);

        Task<PagedResponse<Pedido>> FiltrarPedidos(PedidoFilterRequest filtro);
    }


}
