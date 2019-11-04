using MyPOS.Models;
using MyPOS.ViewModels;
using SQLite;
using System;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

namespace MyPOS.Views
{
    [DesignTimeVisible(false)]
    public partial class CustomersPage : ContentPage
    {
        CustomerViewModel viewModel;

        public CustomersPage(CustomerViewModel customerViewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = customerViewModel;
        }

        async void OnCustomerSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var customer = args.SelectedItem as Customer;
            if (customer == null)
                return;

            // holds the customer information
            this.viewModel.LinkedCustomer = customer;

            await Navigation.PopModalAsync();

            // Manually deselect the item
            CustomersListView.SelectedItem = null;
        }

        async void NewCustomer_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewCustomerPage(new CustomerDetailViewModel(new Customer()))));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Customers.Count == 0)
                viewModel.LoadCustomersCommand.Execute(null);
            viewModel.IsBusy = false;
        }

        public async void OnEdit(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            await Navigation.PushModalAsync(new NavigationPage(new CustomerDetailPage(new CustomerDetailViewModel(               
                mi.CommandParameter as Customer))));
        }

        public async void OnDelete(object sender, EventArgs e)
        {
            try
            {
                var mi = ((MenuItem)sender);
                await this.viewModel.DeleteCustomer(mi.CommandParameter as Customer);
            }
            catch (SQLiteException)
            {
                await DisplayAlert("Alert", "Failed to delete the customer information", "OK");
            }
        }

        private void CustomerSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = CustomerSearchBar.Text;
            CustomersListView.ItemsSource = viewModel.Customers.Where(
                Customer => Customer.Name.ToLower().Contains(keyword.ToLower()));
        }
    }
}