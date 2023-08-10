using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TiendaProductosApi.Data;
using TiendaProductosApi.Models;
using TiendaProductosApi.Models.Dtos;
using System;
using System.IO;

namespace TiendaProductosApi.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProductoController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetProducto() 
        {
            var producto = _context.Productos.OrderBy(p => p.Id).Include(pr=> pr.proveedor).ToList();
            return Ok(producto);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductosPorProveedor(int id) 
        {
            var producto = _context.Productos.Where(p=>p.Id==id).ToArray();
           return Ok(producto);
        }
        [HttpPost]
        public IActionResult CreateProducto([FromForm]DtoAgregarProducto producto)
        {
            if(producto == null)
            {
                return BadRequest("No se ingreso producto");
            }
            if (_context.Productos.Any(p => p.Nombre == producto.Nombre && p.ProveedorId == producto.ProveedorId))
            {             
                 return BadRequest("Producto repetido");              
            }
            
            Producto producto1 = new Producto();
            producto1.Nombre = producto.Nombre;
            producto1.ProveedorId= producto.ProveedorId;
            producto1.Cantidad = producto.Cantidad;
            if(producto.ImagenProducto!=null && producto.ImagenProducto.Length > 0)
            {
                string nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(producto.ImagenProducto.FileName);
                string carpetaImagen = "Images\\imagenes";
                string rutaImagen = Path.Combine(carpetaImagen, nombreArchivo);
                Directory.CreateDirectory(carpetaImagen);
                using var stream = new FileStream(rutaImagen, FileMode.Create);
                producto.ImagenProducto.CopyTo(stream);
                string rutaDirecto = $"/images/{nombreArchivo}";
                producto1.ImagenProducto = rutaDirecto;
            }
            
            _context.Productos.Add(producto1);
            _context.SaveChanges();
            return Ok(producto);
        }
        [HttpPut]
        public IActionResult UpdateProducto(int id,[FromForm] DtoActualizarProducto producto)
        {
            Producto producto1 = _context.Productos.Find(id);
            if (producto1 == null)
            {
                BadRequest("No hay producto para modificar");
            }
            producto1.Nombre = producto.Nombre;
            producto1.Cantidad = producto.Cantidad;
            Proveedor proveedor = _context.Proveedores.Find(producto.proveedorId);
            if (proveedor == null)
            {
                return BadRequest("Proveedor no Encontrado..");
            }
            producto1.proveedor = proveedor;

            if(producto.ImagenUrl!= null)
            {

                string nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(producto.ImagenUrl.FileName);
                string carpetaImagen = "Images\\imagenes";
                string rutaImagen = Path.Combine(carpetaImagen, nombreArchivo);
                Directory.CreateDirectory(carpetaImagen);
                using var stream = new FileStream(rutaImagen, FileMode.Create);
                producto.ImagenUrl.CopyTo(stream);
                string rutaDirecto = $"/images/{nombreArchivo}";
                producto1.ImagenProducto = rutaDirecto;
                //producto1.ImagenProducto = producto.rutaDirecto;
            }
            _context.SaveChanges();
            return Ok(producto1);
        }
        [HttpDelete]
        public IActionResult DeleteProducto(int id) 
        {
            var producto = _context.Productos.FirstOrDefault(p=>p.Id==id);
            if(producto!=null) 
            {
                if (!string.IsNullOrEmpty(producto.ImagenProducto))
                {

                    var imagen = Path.GetFileNameWithoutExtension(producto.ImagenProducto);
                    var imagenPatch = Path.Combine("Images\\imagenes", imagen + ".jpg");
                    
                    if (System.IO.File.Exists(imagenPatch))
                    {
                        System.IO.File.Delete(imagenPatch);
                    }
                    else
                    {
                        return BadRequest(imagenPatch);
                    }
                }
                
                _context.Productos.Remove(producto);
                _context.SaveChanges();
            }
                return Ok(producto);
           
        }
    }
}
