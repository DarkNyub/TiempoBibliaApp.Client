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
    }
}