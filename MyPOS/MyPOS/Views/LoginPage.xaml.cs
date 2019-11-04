using MyPOS.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace MyPOS.Views
{
    [DesignTimeVisible(false)]
    public partial class LoginPage : ContentPage
    {
        public LoginViewModel viewModel;

        public LoginPage(LoginViewModel viewModel)
		{
			InitializeComponent ();
            BindingContext = this.viewModel = viewModel;
		}

        private async void Login_Clicked(object sender, EventArgs e)
        {
            int userId = await viewModel.Login(this.Username.Text, this.Password.Text);
            if (userId == -1)
            {
                await DisplayAlert("Alert", "Invalid username or password", "OK");                
            }
        }
    }
}