using MyPOS.ViewModels;
using SQLite;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace MyPOS.Views
{
    [DesignTimeVisible(false)]
    public partial class CustomerDetailPage : ContentPage
    {
        CustomerDetailViewModel viewModel;
        public CustomerDetailPage(CustomerDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }

        async void SaveCustomer_Clicked(object sender, EventArgs e)
        {
            try
            {
                MessagingCenter.Send(this, "UpdateCustomer", viewModel.Customer);
                await Navigation.PopModalAsync();
            }
            catch (SQLiteException)
            {
                await DisplayAlert("Alert", "Customer already exists", "OK");
            }
        }

        async void CancelCustomer_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void DeleteCustomer_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "DeleteCustomer", this.viewModel.Customer);
            await Navigation.PopModalAsync();
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}