using System;
using System.ComponentModel;
using System.Linq;
using productoApp.Models;
using productoApp.Services;
using Microsoft.Maui.Controls;

namespace productoApp.ViewModels
{
    public class NuevoProductoViewModel : INotifyPropertyChanged
    {
        private Producto _producto;
        private readonly APIService _APIService;

        public NuevoProductoViewModel(APIService apiservice)
        {
            _APIService = apiservice;
            GuardarProductoCommand = new Command(OnGuardarNuevoProductoClicked);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Producto Producto
        {
            get { return _producto; }
            set
            {
                if (_producto != value)
                {
                    _producto = value;
                    OnPropertyChanged(nameof(Producto));
                }
            }
        }

        public string Nombre
        {
            get { return _producto?.Nombre; }
            set
            {
                if (_producto == null)
                    _producto = new Producto();

                if (_producto.Nombre != value)
                {
                    _producto.Nombre = value;
                    OnPropertyChanged(nameof(Nombre));
                }
            }
        }

        public string Descripcion
        {
            get { return _producto?.Descripcion; }
            set
            {
                if (_producto == null)
                    _producto = new Producto();

                if (_producto.Descripcion != value)
                {
                    _producto.Descripcion = value;
                    OnPropertyChanged(nameof(Descripcion));
                }
            }
        }

        public int CtdenStock
        {
            get { return _producto?.CtdenStock ?? 0; }
            set
            {
                if (_producto == null)
                    _producto = new Producto();

                if (_producto.CtdenStock != value)
                {
                    _producto.CtdenStock = value;
                    OnPropertyChanged(nameof(CtdenStock));
                }
            }
        }

        public double Precio
        {
            get { return _producto?.Precio ?? 0; }
            set
            {
                if (_producto == null)
                    _producto = new Producto();

                if (_producto.Precio != value)
                {
                    _producto.Precio = value;
                    OnPropertyChanged(nameof(Precio));
                }
            }
        }

        public int ProveedorId
        {
            get { return _producto?.ProveedorId ?? 0; }
            set
            {
                if (_producto == null)
                    _producto = new Producto();

                if (_producto.ProveedorId != value)
                {
                    _producto.ProveedorId = value;
                    OnPropertyChanged(nameof(ProveedorId));
                }
            }
        }

        public Command GuardarProductoCommand { get; }

        private async void OnGuardarNuevoProductoClicked()
        {
            try
            {
                if (_producto != null)
                {
                    await _APIService.PutProducto(_producto.ProductoId, _producto);
                }
                else
                {
                    Producto nuevoProducto = new Producto
                    {
                        Nombre = Nombre,
                        Descripcion = Descripcion,
                        CtdenStock = CtdenStock,
                        Precio = Precio,
                        ProveedorId = ProveedorId
                    };

                    // Agrega a la lista local 
                    Utils.Utils.ListaProductos.Add(nuevoProducto);

                    // Envia la solicitud al servicio web
                    await _APIService.PostProducto(nuevoProducto);
                }

                // Navegar hacia atrás después de realizar las operaciones
                // Aquí asumí que tu aplicación tiene una clase App que hereda de Application
                // y tiene la propiedad MainPage para realizar la navegación a la nueva página
                Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                // Manejar la excepción, mostrar mensaje al usuario, realizar el registro, etc.
                Console.WriteLine($"Error al guardar el producto: {ex.Message}");
            }
        }

        public void OnCantidadChanged(string newTextValue)
        {
            if (!newTextValue.All(char.IsDigit))
            {
                CtdenStock = int.Parse(new string(newTextValue.Where(char.IsDigit).ToArray()));
            }
        }

        public void OnPrecioChanged(string newTextValue)
        {
            if (!newTextValue.All(char.IsDigit) && !newTextValue.EndsWith("."))
            {
                Precio = float.Parse(new string(newTextValue.Where(c => char.IsDigit(c) || c == '.').ToArray()));
            }
        }
    }
}
