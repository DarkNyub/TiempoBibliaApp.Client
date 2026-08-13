namespace TiempoBiblia.Client.Models
{
    public class CategoriaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Orden { get; set; }
    }

    public class TagDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
public class ProductoTagDto
    {
        public int TagId { get; set; } // 🔥 LÍNEA NUEVA: Leemos el ID directo de la tabla intermedia
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
        
        // 🔥 CAMPOS NUEVOS AGREGADOS PARA LA EDICIÓN
        public decimal Descuento { get; set; }
        public string? PdfUrl { get; set; }
        public string? VideoUrl { get; set; }
        public bool Activo { get; set; } = true;
        
        public bool EsGratuito { get; set; }
        public string ImagenUrl { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        
        public int CategoriaId { get; set; }
        public CategoriaDto Categoria { get; set; } = new();
        
        // Relaciones completas (Para leer de la base de datos)
        public List<ProductoCategoriaSecundariaDto> CategoriasSecundarias { get; set; } = new();
        public List<ProductoTagDto> ProductoTags { get; set; } = new();
        public List<ProductoRelacionadoDto> ProductosRelacionadosOrigen { get; set; } = new();

        // 🔥 CORRECCIÓN PARA MUDBLAZOR: Usar IReadOnlyCollection
        public IReadOnlyCollection<int> CategoriasSecundariasIds { get; set; } = new List<int>();
        public IReadOnlyCollection<int> TagsIds { get; set; } = new List<int>();
        public IReadOnlyCollection<int> ProductosRelacionadosIds { get; set; } = new List<int>();
    }
    // 🔥 NUEVO: DTO para Crear/Editar desde el Frontend
    public class ProductoMutacionDto
    {
        public int Id { get; set; } // Lo usamos internamente en Blazor para saber si es Editar o Crear
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
        public bool EsGratuito { get; set; }
        public string? ImagenUrl { get; set; }
        public string Tipo { get; set; } = "Imprimible"; // Imprimible, Serie, Miniserie
        public string? PdfUrl { get; set; }
        public string? VideoUrl { get; set; }
        public bool Activo { get; set; } = true;
        
        public int CategoriaId { get; set; }

        // MudBlazor maneja las selecciones múltiples con IEnumerable
        public IEnumerable<int> CategoriasSecundariasIds { get; set; } = new HashSet<int>();
        public IEnumerable<int> TagsIds { get; set; } = new HashSet<int>();
        public IEnumerable<int> ProductosRelacionadosIds { get; set; } = new HashSet<int>();
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
    // DTO para enviar la petición de creación de link
    public class GenerarLinkRequestDto
    {
        public int ProductoId { get; set; }
        public string CorreoCliente { get; set; } = string.Empty;
    }

    // DTO para recibir el link seguro del backend
    public class GenerarLinkResponseDto
    {
        public string UrlSegura { get; set; } = string.Empty;
        public DateTime ExpiraEn { get; set; }
    }
    // Nueva clase para manejar el Producto + Su Cantidad
    public class CarritoItem
    {
        public ProductoDto Producto { get; set; } = new();
        public int Cantidad { get; set; } = 1;
    }
    // DTO para enviar el total a cobrar
    public class SolicitudPagoDto
    {
        public string Titulo { get; set; } = "Recursos de Tiempo Biblia";
        public decimal TotalAPagar { get; set; }
    }

    // DTO para recibir el link de Mercado Pago
    public class RespuestaPagoDto
    {
        public string UrlPago { get; set; } = string.Empty;
    }
    // DTOs para recibir el pago directo desde Checkout Bricks
    public class BrickPagoRequestDto
    {
        public string Token { get; set; } = string.Empty; // La tarjeta encriptada
        public string IssuerId { get; set; } = string.Empty;
        public string PaymentMethodId { get; set; } = string.Empty;
        public decimal TransactionAmount { get; set; }
        public int Installments { get; set; }
        public PayerDto Payer { get; set; } = new();
    }

    public class PayerDto
    {
        public string Email { get; set; } = string.Empty;
        public IdentificationDto Identification { get; set; } = new();
    }

    public class IdentificationDto
    {
        public string Type { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
    }
    
    public class RespuestaPagoBrickDto
    {
        public bool Aprobado { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string IdPago { get; set; } = string.Empty;
        public string Mensaje { get; set; } = string.Empty;
    }
} // Fin del namespace