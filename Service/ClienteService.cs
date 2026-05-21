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
                Email = cliente.Email,
                Telefono = cliente.Telefono,
            };

            _context.Clientes.Add(nuevoCliente);

            await _context.SaveChangesAsync();

            return nuevoCliente;
        }

        public async Task<Cliente?> ObtenerPedidosCliente(int id)
        {
            Cliente? cliente = await _context.Clientes
                .Include(cliente => cliente.Pedidos)
                .FirstOrDefaultAsync(cliente => cliente.Id == id);

            return cliente;
        }

        public async Task<bool> UpdateCliente(int id, UpdateCliente actualizar)
        {
            Cliente? cliente = await _context.Clientes
                .FirstOrDefaultAsync(cliente => cliente.Id == id);

            if (cliente == null)
            {
                return false;
            }

            cliente.Nombre = actualizar.Nombre;
            cliente.Email = actualizar.Email;
            cliente.Telefono = actualizar.Telefono;

            await _context.SaveChangesAsync();

            return true;
        }

    }


}