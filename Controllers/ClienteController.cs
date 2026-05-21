using Microsoft.AspNetCore.Mvc;
using Pedidos_ASP.Dtos;
using Pedidos_ASP.Models;
using Pedidos_ASP.Service;

namespace Pedidos_ASP.Controllers
{
    [ApiController]
    [Route("api/cliente")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ClienteResponse>>> GetAll()
        {
            List<ClienteResponse> clientes = (await _clienteService.GetAllClientes())
                .Select(cliente => ToResponse(cliente))
                .ToList();

            return Ok(clientes);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ClienteResponse>> GetById(int id)
        {
            Cliente? cliente = await _clienteService.GetClienteById(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(ToResponse(cliente));
        }

        [HttpPost]
        public async Task<ActionResult<ClienteResponse>> Create(CreateCliente cliente)
        {
            Cliente nuevoCliente = await _clienteService.CreateCliente(cliente);

            ClienteResponse response = ToResponse(nuevoCliente);

            return CreatedAtAction(
                nameof(GetById),
                new { id = response.Id },
                response
            );
        }

        
        [HttpGet("{id:int}/pedidos")]
        public async Task<ActionResult<ClienteResponse>> ObtenerPedidosDeCliente(int id) {

            Cliente cliente = await _clienteService.ObtenerPedidosCliente(id);


            if (cliente == null)
            {
                return NotFound();
            }

            ClienteResponse response = ToResponse(cliente);

            return Ok(response);
        }


        public static ClienteResponse ToResponse(Cliente cliente)
        {
            return new ClienteResponse
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Email = cliente.Email,
                Pedidos = cliente.Pedidos
                    .Select(pedido => new PedidoSimpleResponse
                    {
                        Id = pedido.Id,
                        nombre = pedido.nombre,
                        precio = pedido.precio,
                        estado = pedido.estado
                    })
                    .ToList()
            };
        }

    }
}