using Pedidos_ASP.Dtos;
using Pedidos_ASP.Models;
using Pedidos_ASP.Service;
using Microsoft.AspNetCore.Mvc;
namespace Pedidos_ASP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }
        [HttpGet]
        public async Task<ActionResult<List<TaskResponse>>> GetAll()
        {
            List<TaskResponse> pedidos = (await _pedidoService.GetAllPedidos())
                .Select(ped => ToResponse(ped))
                .ToList();

            return Ok(pedidos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskResponse>> ObtenerPorId(int id)
        {
            Pedido? pedido = await _pedidoService.GetPedidoById(id);
            if (pedido == null)
            {
                return NotFound();
            }

            return Ok(ToResponse(pedido));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarPedido(int id, UpdatePedido actualizar)
        {

            bool realizado = await _pedidoService.UpdatePedido(id, actualizar);
            if (!realizado)
            {
                return NotFound();
            }
            return NoContent();

        }

        [HttpPost]
        public async Task<ActionResult> CrearPedido(CreatePedido pedido)
        {
            Pedido nuevoPedido = await _pedidoService.CreatePedido(pedido);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevoPedido.Id }, nuevoPedido);

        }

        [HttpGet("buscar/name/{nombre}")]
        public async Task<ActionResult<List<Pedido>>> GetPedidosNombre(string nombre)
        {
            List<Pedido> pedidos = await _pedidoService.GetPedidoByname(nombre);

            return Ok(pedidos);
        }
        public async Task<ActionResult<int>> GetPrecioPorNombre(string nombrePedido)
        {
            int precio = await _pedidoService.GetPrecioByNombre(nombrePedido);
            if (precio == 0)
            {
                return NotFound();
            }
            return Ok(precio);

        }

        [HttpGet("precio/{id}")]
        public async Task<ActionResult<int>> GetPrecioPorId(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            int precio = await _pedidoService.GetPrecioById(id);

            return Ok(precio);
        }



        //Task<bool> GetEstadoById(int id);
        [HttpGet("estado/{id}")]
        public async Task<ActionResult<string>> GetEstadoPorId(int id)
        {

            string? estado = await _pedidoService.GetEstadoById(id);
            if (id <= 0 || estado == null)
            {
                return BadRequest();
            }
            return Ok(estado);
        }


        public static TaskResponse ToResponse(Pedido pedido)
        {
            return new TaskResponse
            {
                Id = pedido.Id,
                nombre = pedido.nombre,
                precio = pedido.precio,
            };

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> EliminarPedido(int id)
        {
            bool resultado = await _pedidoService.DeletePedido(id);

            if (!resultado)
            {
                return NotFound(resultado);
            }

            return NoContent();
        }
        [HttpGet("caros/{precio}")]
        public async Task<ActionResult<List<TaskResponse>>> GetPedidosCaros(int precio)
        {
            List<TaskResponse> pedidos = (await _pedidoService.GetPedidosCaros(precio))
                .Select(ped => ToResponse(ped))
                .ToList();

            return Ok(pedidos);
        }

        [HttpGet("ordenados/precio")]
        public async Task<ActionResult<List<Pedido>>> GetPedidosOrdenadosPorPrecio()
        {
            List<Pedido> pedidos = await _pedidoService.GetPedidosOrdenadosPorPrecio();

            return Ok(pedidos);
        }

        [HttpGet("buscar/{nombre}")]
        public async Task<ActionResult<List<Pedido>>> BuscarPedidos(string nombre)
        {
            List<Pedido> pedidos = await _pedidoService.BuscarPedidos(nombre);

            return Ok(pedidos);
        }

        [HttpGet("total")]
        public async Task<ActionResult<int>> ContarPedidos()
        {
            int total = await _pedidoService.ContarPedidos();

            return Ok(total);
        }

        [HttpGet("existe/{nombre}")]
        public async Task<ActionResult<bool>> ExistePedidoPorNombre(string nombre)
        {
            bool existe = await _pedidoService.ExistePedidoPorNombre(nombre);

            return Ok(existe);
        }

        [HttpGet("filtrar")]
        public async Task<ActionResult<PagedResponse<Pedido>>> FiltrarPedidos([FromQuery] PedidoFilterRequest filtro)
        {
            PagedResponse<Pedido> resultado = await _pedidoService.FiltrarPedidos(filtro);

            return Ok(resultado);
        }
    }


















}
