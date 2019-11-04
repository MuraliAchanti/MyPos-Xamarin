using MyPOS.Models;
using MyPOS.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace MyPOS.Views
{
    [DesignTimeVisible(false)]
    public partial class SalesPage : ContentPage
    {
        public SalesViewModel viewModel;
        CustomerViewModel customerViewModel;

        public SalesPage()
        {
            InitializeComponent();

            if (this.viewModel == null)
                this.viewModel = new SalesViewModel();

            BindingContext = this.viewModel;

            if (customerViewModel == null)
                customerViewModel = CustomerViewModel.GetInstance();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (this.viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
            ItemsListView.ItemsSource = viewModel.Items;
            viewModel.IsBusy = false;

            if (this.viewModel.Categories.Count == 0)
                viewModel.LoadCategoriesCommand.Execute(null);

            TotalCost.SetBinding(Label.TextProperty, new Binding("TotalCost"));
            viewModel.UpdateTotalCost();
            CategoryListInSales.SelectedIndex = 0;
            MainSearchBar.Text = string.Empty;

            CustomerInfo.IconImageSource = customerViewModel.LinkedCustomer == null
                ? "CustomerInfo"
                : "CustomerInfoChecked";
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Item;
            if (item == null)
                return;

            await Navigation.PushModalAsync(new ItemQuantityPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void LinkCustomer_Clicked(object sender, EventArgs e)
        {            
            await Navigation.PushModalAsync(new NavigationPage(new CustomersPage(customerViewModel)));
        }

        void ClearSale_Clicked(object sender, EventArgs e)
        {
            customerViewModel.LinkedCustomer = null;
            CustomerInfo.IconImageSource = "CustomerInfo";
            TotalCost.Text = "0";
            foreach (Item item in this.viewModel.Items)
                item.Quantity = 0;
        }

        async void Charge_Clicked(object sender, EventArgs e)
        {
            List<BilledItem> billedItems = new List<BilledItem>();
            foreach (Item itemToBeBilled in this.viewModel.Items)
            {
                BilledItem billedItem = new BilledItem();
                if (itemToBeBilled.Quantity > 0)
                {
                    billedItem.Name = itemToBeBilled.Name;
                    billedItem.Price = itemToBeBilled.Price;
                    billedItem.Quantity = itemToBeBilled.Quantity;
                    billedItems.Add(billedItem);
                }
            }

            if (customerViewModel.LinkedCustomer == null)
            {
                await DisplayAlert("Alert", "Please add customer information", "OK");
                await Navigation.PushModalAsync(new NavigationPage(new CustomersPage(customerViewModel)));
                return;
            }
            if (double.Parse(TotalCost.Text) == 0)
            {
                await DisplayAlert("Alert", "No items selected", "OK");
                return;
            }
            else
            {
                await Navigation.PushModalAsync(new NavigationPage(new PaymentPage(new PaymentViewModel(billedItems, customerViewModel.LinkedCustomer))));
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private void MainSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = MainSearchBar.Text;
            ItemsListView.ItemsSource = viewModel.Items.Where(
                Item => Item.Name.ToLower().Contains(keyword.ToLower()));
        }

        private void CategoryListInSales_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            if (selectedIndex >= 1)
            {
                //viewModel.CategoryName = picker.Items[selectedIndex];
                ItemsListView.ItemsSource = viewModel.Items.Where(
                    Item => Item.Category.ToLower().Contains(picker.Items[selectedIndex].ToLower()));
            }
            else
            {
                //viewModel.CategoryName = "All Items";
                ItemsListView.ItemsSource = viewModel.Items;
            }
        }

        async void ScanItem_Clicked(object sender, EventArgs e)
        {
            var scanPage = new ZXingScannerPage();
            // Navigate to our scanner page
            await Navigation.PushAsync(scanPage);
            scanPage.OnScanResult += (result) =>
            {
                // Stop scanning
                scanPage.IsScanning = false;
                // Pop the page and show the result
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync(); //popping scanpage
                    foreach (Item item in this.viewModel.Items)
                    {
                        if (result.Text.Equals(item.Barcode))
                        {
                            await Navigation.PushModalAsync(new ItemQuantityPage(new ItemDetailViewModel(item)));
                            return;
                        }
                    }
                    await DisplayAlert("Alert", "Item Not Found", "OK");
                });
            };
        }
    }
}