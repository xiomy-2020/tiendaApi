namespace TiendaProductosApi.Models.Dtos
{
    public class DtoActualizarProducto
    {
       
        public string Nombre { get; set; }
        public int proveedorId { get; set; }
        public IFormFile ImagenUrl { get; set; }
        public int Cantidad { get; set; }
    }
}
