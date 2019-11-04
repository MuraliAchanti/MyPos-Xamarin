using MyPOS.Models;
using MyPOS.ViewModels;
using SQLite;
using System;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace MyPOS.Views
{
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;

        public ItemsPage(ItemsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Item;
            if (item == null)
                return;

            await Navigation.PushModalAsync(new NavigationPage(new ItemDetailPage(new ItemDetailViewModel(item))));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }
        private void MainSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = MainSearchBar.Text;
            ItemsListView.ItemsSource = viewModel.Items.Where(
                Item => Item.Name.ToLower().Contains(keyword.ToLower()));
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage(new ItemDetailViewModel(new Item()
            { Price = 0, Cost = 0, Quantity = 0 }))));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        async void AddItemUsingBarcode_Clicked(object sender, EventArgs e)
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
                    await Navigation.PushModalAsync(new NavigationPage(new NewItemPage(new ItemDetailViewModel(new Item()
                    { Price = 0, Cost = 0, Barcode = result.Text, Quantity = 0 }))));
                });
            };
        }
        public async void OnEdit(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            await Navigation.PushModalAsync(new NavigationPage(new ItemDetailPage(new ItemDetailViewModel(mi.CommandParameter as Item))));
        }

        public async void OnDelete(object sender, EventArgs e)
        {
            try
            {
                var mi = ((MenuItem)sender);
                await this.viewModel.DeleteItem(mi.CommandParameter as Item);
            }
            catch (SQLiteException)
            {
                await DisplayAlert("Alert", "Failed to delete the item", "OK");
            }
        }
    }
}