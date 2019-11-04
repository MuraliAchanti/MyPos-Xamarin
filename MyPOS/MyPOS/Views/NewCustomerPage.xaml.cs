using MyPOS.ViewModels;
using SQLite;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace MyPOS.Views
{
    [DesignTimeVisible(false)]
    public partial class NewCustomerPage : ContentPage
    {
        CustomerDetailViewModel viewModel;

        public NewCustomerPage(CustomerDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.viewModel.Customer.Name))
                {
                    await DisplayAlert("Alert", "Name cannot be empty", "OK");
                    return;
                }
                MessagingCenter.Send(this, "AddCustomer", this.viewModel.Customer);
                await Navigation.PopModalAsync();
            }
            catch (SQLiteException)
            {
                await DisplayAlert("Alert", "Customer already exists", "OK");
            }
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}