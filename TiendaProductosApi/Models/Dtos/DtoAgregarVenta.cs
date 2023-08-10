namespace TiendaProductosApi.Models.Dtos
{
    public class DtoAgregarVenta
    {
        public List<int> ProductoId { get; set; }
       // public List<int>? Productos { get; set; }
        public int ClienteId { get; set; }
        public int Cantidad { get; set; }
       

    }
}
