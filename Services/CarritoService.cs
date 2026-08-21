using Microsoft.JSInterop;
using System.Text.Json;
using TiempoBiblia.Client.Models;

namespace TiempoBiblia.Client.Services
{
    public class CarritoService
    {
        private readonly IJSInProcessRuntime _js;
        public List<CarritoItem> Items { get; private set; } = new();
        public event Action? OnCarritoCambiado;

        // Inyectamos el puente con el navegador web
        public CarritoService(IJSRuntime js)
        {
            _js = (IJSInProcessRuntime)js;
        }

        // ==========================================
        // LA MAGIA DE LA MEMORIA (Local Storage)
        // ==========================================
        public void CargarCarritoGuardado()
        {
            var json = _js.Invoke<string>("localStorage.getItem", "tiempobiblia_carrito");
            if (!string.IsNullOrEmpty(json))
            {
                Items = JsonSerializer.Deserialize<List<CarritoItem>>(json) ?? new();
                NotificarEstadoCambiado();
            }
        }

        private void GuardarCarrito()
        {
            var json = JsonSerializer.Serialize(Items);
            _js.InvokeVoid("localStorage.setItem", "tiempobiblia_carrito", json);
        }

        // ==========================================
        // MÉTODOS DEL CARRITO (Con guardado automático)
        // ==========================================
        public void AgregarAlCarrito(ProductoDto producto)
        {
            var itemExistente = Items.FirstOrDefault(i => i.Producto.Id == producto.Id);
            if (itemExistente != null)
            {
                itemExistente.Cantidad++;
            }
            else
            {
                Items.Add(new CarritoItem { Producto = producto, Cantidad = 1 });
            }
            
            GuardarCarrito(); // ¡Guardamos el cambio en el navegador!
            NotificarEstadoCambiado();
        }

        public void ModificarCantidad(int productoId, int nuevaCantidad)
        {
            var item = Items.FirstOrDefault(i => i.Producto.Id == productoId);
            if (item != null)
            {
                if (nuevaCantidad <= 0)
                {
                    Items.Remove(item);
                }
                else
                {
                    item.Cantidad = nuevaCantidad;
                }
                
                GuardarCarrito(); // ¡Guardamos el cambio!
                NotificarEstadoCambiado();
            }
        }

        public void EliminarDelCarrito(int productoId)
        {
            var item = Items.FirstOrDefault(i => i.Producto.Id == productoId);
            if (item != null)
            {
                Items.Remove(item);
                GuardarCarrito(); // ¡Guardamos el cambio!
                NotificarEstadoCambiado();
            }
        }

        public void VaciarCarrito()
        {
            Items.Clear();
            GuardarCarrito(); // Guardamos el carrito vacío
            NotificarEstadoCambiado();
        }

        public decimal ObtenerTotal(bool esColombia) => Items.Where(i => !i.Producto.EsGratuito).Sum(i => (esColombia ? i.Producto.Precio : i.Producto.PrecioUsd) * i.Cantidad);
        
        public int ObtenerCantidadItems() => Items.Sum(i => i.Cantidad);

        private void NotificarEstadoCambiado() => OnCarritoCambiado?.Invoke();
    }
}