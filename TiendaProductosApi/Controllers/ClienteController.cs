using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using TiendaProductosApi.Data;
using TiendaProductosApi.Models;

namespace TiendaProductosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ClienteController(ApplicationDbContext context)
        {
            _context= context;
        }
        [HttpGet]
        public IActionResult GetCLiente() 
        {
            var cliente = _context.Clientes.ToList();
            return Ok(cliente);
        }
        [HttpPost]
        public IActionResult CreateCliente([FromBody] DtoAgregarCliente cliente)
        {
            if(cliente == null)
            {
                return BadRequest("No ingreso cliente");
            }
            Cliente cliente1 = new Cliente();
            cliente1.Nombre=cliente.Nombre;
            cliente1.Direccion=cliente.Direccion;
            cliente1.Telefono=cliente.Telefono;
            _context.Clientes.Add(cliente1);
            _context.SaveChanges();
            return Ok(cliente1);
        }
        [HttpGet("byid")]
        public IActionResult GetClienteById(int id)
        {
            if(id == 0)
            {
                return BadRequest("No se encontro cliente");
            }
            var cliente = _context.Clientes.Find(id);
            return Ok(cliente);
        }

        [HttpPut]
        public IActionResult ActualizarCliente(int id,DtoActualizarCliente cliente)
        {
            if(id == 0)
            {
                return BadRequest("No se encontro cliente");
            }
            
            Cliente cliente1 = _context.Clientes.Find(id);
            if(cliente1 == null)
            {
                return BadRequest("No existe el id para actualizar");
            }
            cliente1.Nombre = cliente.Nombre;
            cliente1.Direccion= cliente.Direccion;
            cliente1.Telefono = cliente.Telefono;
           
            _context.SaveChanges();
            return Ok(cliente1);
        }

        [HttpDelete]
        public IActionResult DeleteCliente(int id)
        {
            var cliente = _context.Clientes.Find(id);
            _context.Clientes.Remove(cliente);
            _context.SaveChanges();
            return Ok("Cliente eliminado....");
        }

    }
}
