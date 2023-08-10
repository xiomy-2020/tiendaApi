using Microsoft.AspNetCore.Mvc;
using TiendaProductosApi.Data;
using TiendaProductosApi.Models;
using TiendaProductosApi.Models.Dtos;


namespace TiendaProductosApi.Controllers
{
    [Route("Api/[Controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly ApplicationDbContext _context; 

        public ProveedorController(ApplicationDbContext context)
        {
            _context= context;
        }
        [HttpGet]
        public IActionResult GetProveedor() 
        {
            return Ok(_context.Proveedores.OrderBy(p => p.Id).ToList());
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var proveedor = _context.Proveedores.Where(p=>p.Id == id).ToArray();
            return Ok(proveedor);
        }
        [HttpPost]
        public IActionResult AgregarProveedor([FromBody] DtoAgregarProveedor proveedor)
        {
            if(proveedor == null)
            {
                return BadRequest("No ingreso datos");
            }
            Proveedor proveedor1 = new Proveedor();
            proveedor1.NombreProveedor = proveedor.Nombre;
            _context.Proveedores.Add(proveedor1);
            _context.SaveChanges();
            return Ok(proveedor1);
        }
        [HttpDelete]
        public IActionResult DeleteProveedor(int id)
        {
            var proveedor = _context.Proveedores.Find(id);
            if (proveedor == null)
            {
                return BadRequest("No existe el proveedor");
            }
            _context.Remove(proveedor);
            _context.SaveChanges();
            return Ok("Proveedor Eliminado");
        }
    }
}
