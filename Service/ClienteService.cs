using Microsoft.EntityFrameworkCore;
using Pedidos_ASP.Data;
using Pedidos_ASP.Dtos;
using Pedidos_ASP.Models;

namespace Pedidos_ASP.Service
{
    public class ClienteService : IClienteService
    {
        private readonly AppDbContext _context;

        public ClienteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Cliente>> GetAllClientes()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente?> GetClienteById(int id)
        {
            return await _context.Clientes
                .FirstOrDefaultAsync(cliente => cliente.Id == id);
        }

        public async Task<Cliente> CreateCliente(CreateCliente cliente)
        {
            Cliente nuevoCliente = new()
            {
                Nombre = cliente.Nombre,
                Email = cliente.Email
            };

            _context.Clientes.Add(nuevoCliente);

            await _context.SaveChangesAsync();

            return nuevoCliente;
        }
    }
}