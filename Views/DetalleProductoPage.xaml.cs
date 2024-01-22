using productoApp.Models;
using productoApp.Services;
using productoApp.ViewModels;
namespace productoApp
{
    public partial class DetalleProductoPage : ContentPage
    {
        private readonly DetalleProductoViewModel _viewModel;

        public DetalleProductoPage(APIService apiservice)
        {
            InitializeComponent();
            _viewModel = new DetalleProductoViewModel(apiservice);
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.Producto = BindingContext as Producto;
        }

        private void ClickEliminarProducto(object sender, EventArgs e)
        {
            _viewModel.EliminarProducto();
            Navigation.PopAsync();
        }

        private async void ClickEditarProducto(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NuevoProductoEditar(_viewModel._APIService)
            {
                BindingContext = _viewModel.Producto
            });
        }
    }
}
