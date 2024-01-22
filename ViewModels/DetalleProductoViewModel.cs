using System;
using System.ComponentModel;
using productoApp.Models;
using productoApp.Services;

namespace productoApp.ViewModels
{
    public class DetalleProductoViewModel : INotifyPropertyChanged
    {
        private Producto _producto;
        public APIService _APIService;

        public DetalleProductoViewModel(APIService apiservice)
        {
            _APIService = apiservice;
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
                if (_producto != null && _producto.Nombre != value)
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
                if (_producto != null && _producto.Descripcion != value)
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
                if (_producto != null && _producto.CtdenStock != value)
                {
                    _producto.CtdenStock = value;
                    OnPropertyChanged(nameof(CtdenStock));
                }
            }
        }

        

        public int ProveedorId
        {
            get { return _producto?.ProveedorId ?? 0; }
            set
            {
                if (_producto != null && _producto.ProveedorId != value)
                {
                    _producto.ProveedorId = value;
                    OnPropertyChanged(nameof(ProveedorId));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void EliminarProducto()
        {
            // Eliminar lógica aquí
            await _APIService.DeleteProducto(_producto.ProductoId);
        }

    }
}
