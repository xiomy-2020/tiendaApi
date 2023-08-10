using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaProductosApi.Data;
using TiendaProductosApi.Models;
using TiendaProductosApi.Models.Dtos;

namespace TiendaProductosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VentaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetVentas()
        {
           var venta = _context.Ventas.ToList();
           return Ok(_context.Ventas.OrderBy(v => v.Id)               
               .Include(c=>c.Cliente)
               .Include(p=>p.Productos).Select(c=>new {
                c.Id,
                nombreCliente = c.Cliente.Nombre,
                nombreProducto = c.Productos.Select(p=>p.Nombre),
                cantidad= c.CantidadVendida,
                                
               })
               .ToList());
        }

        [HttpGet("ById")]
        public IActionResult GetVentaxId(int id)
        {
            var venta = _context.Ventas.Find(id);
            return Ok(_context.Ventas.Where(c => c.Cliente.Id == venta.ClienteId).Select(c =>
            new
            {
                NombreCliente = c.Cliente.Nombre,
                NombreProducto = c.Productos.Select(p => p.Nombre),
                Cantidad = venta.CantidadVendida,
                
            }).ToList());
        }

        [HttpPost]
        public IActionResult AgregarVenta([FromBody] DtoAgregarVenta venta)
        {
            if (venta == null)
            {
                return BadRequest("No Ingreso datos");
            }
            
            VentaProducto ventaProducto = new VentaProducto();
            ventaProducto.ClienteId= venta.ClienteId;
            ventaProducto.Productos = new List<Producto>();
            
            
                foreach (int productoId in venta.ProductoId)
                {
                Producto producto = _context.Productos.Find(productoId);
                    if (producto == null)
                    {
                        return BadRequest("No existe producto con ID: " + productoId);
                    }

                if (venta.Cantidad > producto.Cantidad)
                {
                    return BadRequest("La cantidad solicitada del producto con ID" + venta.ProductoId + " no se encuentra disponible");
                }
                producto.Cantidad -= venta.Cantidad;
                ventaProducto.Productos.Add(producto);

            }
                
                ventaProducto.CantidadVendida = venta.Cantidad;
                _context.Ventas.Add(ventaProducto);
                _context.SaveChanges();
                return Ok(ventaProducto);
           
        }
        [HttpPut]
        public IActionResult ActualizarVenta([FromForm]DtoActualizarVenta venta, int id)
        {

            VentaProducto venta1 = _context.Ventas.Find(id);
            venta.ProductoId.Remove(id);
            
            if (venta1 == null)
            {
                return BadRequest("No existe la venta");
            }
            venta1.ClienteId = venta.ClienteId;
            foreach (int productoId in venta.ProductoId)
            {
                Producto producto = _context.Productos.Find(productoId);
                venta1.Productos = new List<Producto>();
                venta1.Productos.Add(producto);
            {
            };
                venta1.CantidadVendida = venta.Cantidad;
            }
           
            _context.SaveChanges();
            return Ok(venta1);
        }
        [HttpDelete]
        public IActionResult DeleteVenta(int id) 
        {
            VentaProducto venta = _context.Ventas.Find(id);
            if (venta == null)
            {
                return NotFound();
            }
            _context.Remove(venta);
            _context.SaveChanges();
            return Ok("Eliminado correctamente");
        }
    }
}
