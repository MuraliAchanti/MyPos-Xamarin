using MyPOS.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace MyPOS.Views
{
    [DesignTimeVisible(false)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsViewModel viewModel;
        
        public SettingsPage(SettingsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
            
        }

        private void Log_Out_Clicked(object sender, EventArgs e)
        {
            this.viewModel.LogOut();
            App.GoToLoginPage();
        }
    }
}