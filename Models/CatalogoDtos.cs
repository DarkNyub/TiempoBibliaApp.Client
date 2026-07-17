namespace TiempoBiblia.Client.Models
{
    public class CategoriaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }

    public class TagDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }

    public class ProductoTagDto
    {
        public TagDto Tag { get; set; } = new();
    }

    // El DTO para nuestra nueva tabla de categorías secundarias
    public class ProductoCategoriaSecundariaDto
    {
        public int CategoriaId { get; set; }
    }

    public class ProductoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public bool EsGratuito { get; set; }
        public string ImagenUrl { get; set; } = string.Empty;
        
        // Relación Principal
        public int CategoriaId { get; set; }
        public CategoriaDto Categoria { get; set; } = new();
        
        // Relaciones Múltiples
        public List<ProductoCategoriaSecundariaDto> CategoriasSecundarias { get; set; } = new();
        public List<ProductoTagDto> ProductoTags { get; set; } = new();
        public List<ProductoRelacionadoDto> ProductosRelacionadosOrigen { get; set; } = new();
    }
    // 1. NUEVO: DTO para el Paquete
    public class PaqueteDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public string ImagenUrl { get; set; } = string.Empty;
    }
    // 2. NUEVO: DTO para los Productos Relacionados
    public class ProductoRelacionadoDto
    {
        public int ProductoRelacionadoId { get; set; }
        // Traemos el producto destino para poder dibujar su tarjeta en el Pop-up
        public ProductoDto ProductoRelacionadoDestino { get; set; } = new(); 
    }
}