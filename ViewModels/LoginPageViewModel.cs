using CommunityToolkit.Maui.Core;
using productoApp.Models;
using productoApp.Services;
using Microsoft.Maui.Controls;
using System;
using System.Linq;
using System.ComponentModel;

namespace productoApp.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private User _usuario;
        private readonly APIService _APIService;

        public LoginViewModel(APIService apiservice)
        {
            _APIService = apiservice;
            LoginCommand = new Command(OnLoginClicked);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string UserName
        {
            get { return _usuario?.UserMail; }
            set
            {
                if (_usuario == null)
                    _usuario = new User();

                if (_usuario.UserMail != value)
                {
                    _usuario.UserMail = value;
                    OnPropertyChanged(nameof(UserName));
                }
            }
        }

        public string Password
        {
            get { return _usuario?.UserPassword; }
            set
            {
                if (_usuario == null)
                    _usuario = new User();

                if (_usuario.UserPassword != value)
                {
                    _usuario.UserPassword = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        public Command LoginCommand { get; }

        private async void OnLoginClicked()
        {
            bool isValidUser = await _APIService.VerificarUsuario(_usuario);

            if (isValidUser)
            {
                // Usuario válido, puedes realizar la navegación a la siguiente página
                // Aquí asumí que tu aplicación tiene una clase App que hereda de Application
                // y tiene la propiedad MainPage para realizar la navegación a la nueva página
                Application.Current.MainPage = new NavigationPage(new ProductoPage(_APIService));
            }
            else
            {
                // Muestra una alerta u otro mensaje indicando que el inicio de sesión falló
                await Application.Current.MainPage.DisplayAlert("Error", "Inicio de sesión fallido/Credenciales incorrectas", "OK");
            }
        }
    }
}
