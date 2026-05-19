using Microsoft.EntityFrameworkCore;
using Pedidos_ASP.Data;
using Pedidos_ASP.Dtos;
using Pedidos_ASP.Models;

namespace Pedidos_ASP.Service
{
    public class PedidoService : IPedidoService

    {
        private readonly AppDbContext _context;
        public PedidoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pedido>> GetAllPedidos()
        {
            return await _context.Pedidos
                .Include(pedido => pedido.Cliente)
                .ToListAsync();
        }
        public async Task<Pedido?> CreatePedido(CreatePedido pedido)
        {
            Cliente? cliente = await _context.Clientes
                .FirstOrDefaultAsync(c => c.Id == pedido.ClienteId);

            if (cliente == null)
            {
                return null;
            }

            Pedido nuevoPedido = new()
            {
                nombre = pedido.nombre,
                precio = pedido.precio,
                estado = pedido.estado,
                ClienteId = pedido.ClienteId
            };

            _context.Pedidos.Add(nuevoPedido);
            await _context.SaveChangesAsync();

            return nuevoPedido;
        }



        public async Task<string> GetEstadoById(int id)
        {
            Pedido? pedido1 = await _context.Pedidos.FirstOrDefaultAsync(pedido => pedido.Id == id);
            if (pedido1?.estado == true)
            {
                return "El pedido está vendido";
            }
            return "No está vendido";
        }

        public async Task<Pedido?> GetPedidoById(int id)
        {
            return await _context.Pedidos
                .Include(pedido => pedido.Cliente)
                .FirstOrDefaultAsync(pedido => pedido.Id == id);
        }

        public async Task<int> GetPrecioById(int id)
        {
            Pedido? ped = await _context.Pedidos.FirstOrDefaultAsync(pedido => pedido.Id == id);
    
            return ped?.precio ?? 0 ;
        }

        public async Task<int> GetPrecioByNombre(string nombre)
        {
            Pedido? ped = await _context.Pedidos.FirstOrDefaultAsync(pedido => pedido.nombre == nombre);
            return ped?.precio ?? 0;
        }

        public async Task<bool> UpdatePedido(int id, UpdatePedido actualizar)
        {
            Pedido? pedido = await _context.Pedidos
                .FirstOrDefaultAsync(ped => ped.Id == id);

            if (pedido == null)
            {
                return false;
            }

            bool clienteExiste = await _context.Clientes
                .AnyAsync(cliente => cliente.Id == actualizar.ClienteId);

            if (!clienteExiste)
            {
                return false;
            }

            pedido.nombre = actualizar.nombre;
            pedido.precio = actualizar.precio;
            pedido.estado = actualizar.estado;
            pedido.ClienteId = actualizar.ClienteId;

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> DeletePedido(int id)
        {
            Pedido? ped = await _context.Pedidos.FirstOrDefaultAsync(pedido => pedido.Id == id);

            if (ped == null)
            {
                return false;
            }

            _context.Pedidos.Remove(ped);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Pedido>> GetPedidosCaros(int precio)
        {
            return await _context.Pedidos
                .Where(pedido => pedido.precio > precio)
                .ToListAsync();
        }

        public async Task<List<Pedido>> GetPedidoByname(string nombre)
        {
            return await _context.Pedidos
                .Where(pedido => pedido.nombre == nombre)
                .ToListAsync();
        }

        public async Task<List<Pedido>> GetPedidosOrdenadosPorPrecio()
        {
            return await _context.Pedidos
                .OrderBy(pedido => pedido.precio)
                .ToListAsync();
        }

        public async Task<List<Pedido>> BuscarPedidos(string nombre)
        {
            return await _context.Pedidos
                .Where(pedido => pedido.nombre.Contains(nombre))
                .ToListAsync();
        }

        public async Task<int> ContarPedidos()
        {
            return await _context.Pedidos.CountAsync();
        }

        public async Task<bool> ExistePedidoPorNombre(string nombre)
        {
            return await _context.Pedidos
                .AnyAsync(pedido => pedido.nombre == nombre);
        }
        public async Task<PagedResponse<Pedido>> GetPedidosPaginados(int page, int pageSize)
        {
            int totalItems = await _context.Pedidos.CountAsync();

            List<Pedido> pedidos = await _context.Pedidos
                .OrderBy(pedido => pedido.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResponse<Pedido>
            {
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                Items = pedidos
            };
        }

        public async Task<PagedResponse<Pedido>> FiltrarPedidos(PedidoFilterRequest filtro)
        {
            IQueryable<Pedido> query = _context.Pedidos;
            if (filtro.Page < 1)
            {
                filtro.Page = 1;
            }

            if (filtro.PageSize < 1)
            {
                filtro.PageSize = 10;
            }

            if (filtro.PageSize > 50)
            {
                filtro.PageSize = 50;
            }

            if (!string.IsNullOrWhiteSpace(filtro.Nombre))
            {
                query = query.Where(pedido => pedido.nombre.Contains(filtro.Nombre));
            }

            if (filtro.PrecioMinimo.HasValue)
            {
                query = query.Where(pedido => pedido.precio >= filtro.PrecioMinimo.Value);
            }

            if (filtro.Estado.HasValue)
            {
                query = query.Where(pedido => pedido.estado == filtro.Estado.Value);
            }

            int totalItems = await query.CountAsync();

            bool descendente = filtro.Direccion == "desc";

            if (filtro.Orden == "precio")
            {
                query = descendente
                    ? query.OrderByDescending(pedido => pedido.precio)
                    : query.OrderBy(pedido => pedido.precio);
            }
            else if (filtro.Orden == "nombre")
            {
                query = descendente
                    ? query.OrderByDescending(pedido => pedido.nombre)
                    : query.OrderBy(pedido => pedido.nombre);
            }
            else
            {
                query = descendente
                    ? query.OrderByDescending(pedido => pedido.Id)
                    : query.OrderBy(pedido => pedido.Id);
            }

            List<Pedido> pedidos = await query
                .Skip((filtro.Page - 1) * filtro.PageSize)
                .Take(filtro.PageSize)
                .ToListAsync();

            return new PagedResponse<Pedido>
            {
                Page = filtro.Page,
                PageSize = filtro.PageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)filtro.PageSize),
                Items = pedidos
            };
        }




    }
}
