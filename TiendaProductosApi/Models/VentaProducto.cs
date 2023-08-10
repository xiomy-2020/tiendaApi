using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaProductosApi.Models
{
    public class VentaProducto
    {
        public int Id { get; set; }
        
        public List<Producto>? Productos { get; set; }

        public int CantidadVendida { get; set; }

        public int ClienteId { get; set; }
        [ForeignKey("ClienteId")]
        public Cliente? Cliente { get; set; }
       
    }
}
