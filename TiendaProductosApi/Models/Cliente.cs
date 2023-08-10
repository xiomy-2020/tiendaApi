using System.Text.Json.Serialization;

namespace TiendaProductosApi.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set;}
        public string Telefono { get; set;}
        [JsonIgnore]
        public List<VentaProducto>? Ventas { get; set; }
    }
}
