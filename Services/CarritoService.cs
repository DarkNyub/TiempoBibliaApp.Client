using TiempoBiblia.Client.Models;

namespace TiempoBiblia.Client.Services
{

    public class CarritoService
    {
        public List<CarritoItem> Items { get; private set; } = new();
        public event Action? OnCarritoCambiado;

        public void AgregarAlCarrito(ProductoDto producto)
        {
            var itemExistente = Items.FirstOrDefault(i => i.Producto.Id == producto.Id);
            if (itemExistente != null)
            {
                itemExistente.Cantidad++; // Si ya existe, le sumamos 1 a la cantidad
            }
            else
            {
                Items.Add(new CarritoItem { Producto = producto, Cantidad = 1 });
            }
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
                NotificarEstadoCambiado();
            }
        }

        public void EliminarDelCarrito(int productoId)
        {
            var item = Items.FirstOrDefault(i => i.Producto.Id == productoId);
            if (item != null)
            {
                Items.Remove(item);
                NotificarEstadoCambiado();
            }
        }

        // Calcula el total, ignorando el precio si el producto es gratuito
        public decimal ObtenerTotal()
        {
            return Items.Where(i => !i.Producto.EsGratuito)
                        .Sum(i => i.Producto.Precio * i.Cantidad);
        }

        // Cuenta la cantidad total de artículos físicos (no solo tipos de productos)
        public int ObtenerCantidadItems() => Items.Sum(i => i.Cantidad);

        public void VaciarCarrito()
        {
            Items.Clear();
            NotificarEstadoCambiado();
        }

        private void NotificarEstadoCambiado() => OnCarritoCambiado?.Invoke();
    }
}