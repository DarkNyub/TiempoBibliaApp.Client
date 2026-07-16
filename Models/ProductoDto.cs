namespace TiempoBiblia.Client.Models
{
    public class ProductoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
        public bool EsGratuito { get; set; }
        public string ImagenUrl { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
    }
}