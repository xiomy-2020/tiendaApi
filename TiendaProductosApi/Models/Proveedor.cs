using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TiendaProductosApi.Models
{
    public class Proveedor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string NombreProveedor { get; set; }
        [JsonIgnore]
        public List<Producto> Productos { get;set;}
        
    }
}
