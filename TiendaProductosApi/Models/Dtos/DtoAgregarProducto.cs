namespace TiendaProductosApi.Models.Dtos
{
    public class DtoAgregarProducto
    {
        public string Nombre { get; set; }
        public int ProveedorId { get; set; }
        public int Cantidad { get; set; }
        public IFormFile ImagenProducto { get; set; }
    }
}
