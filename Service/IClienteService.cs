using Pedidos_ASP.Dtos;
using Pedidos_ASP.Models;

namespace Pedidos_ASP.Service
{
    public interface IClienteService
    {
        Task<List<Cliente>> GetAllClientes();

        Task<Cliente?> GetClienteById(int id);

        Task<Cliente> CreateCliente(CreateCliente cliente);
    }
}