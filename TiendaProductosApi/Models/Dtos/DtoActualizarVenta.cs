namespace TiendaProductosApi.Models.Dtos
{
    public class DtoActualizarVenta
    {
        public List<int> ProductoId { get; set; }
        public int ClienteId { get; set; }
        public int Cantidad { get; set; }
        
    }
}
