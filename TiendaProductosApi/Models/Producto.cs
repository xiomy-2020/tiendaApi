using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TiendaProductosApi.Models
{
    /*Tienda de productos donde se pueda vender... 
    Crud
    Guardar historial de ventas...*/

    public class Producto
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public int ProveedorId { get; set; }
        [ForeignKey("ProveedorId")]
        public Proveedor? proveedor { get; set; }

        public int Cantidad { get; set; }
        [JsonIgnore]
        public List<VentaProducto>? Ventas { get; set; } 

        public string ImagenProducto { get; set; }

    }
}
