using MyPOS.Models;
using MyPOS.ViewModels;
using SQLite;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace MyPOS.Views
{
    [DesignTimeVisible(false)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;
        public ItemDetailPage(ItemDetailViewModel itemDetailViewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = itemDetailViewModel;
        }

        async void SaveItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                MessagingCenter.Send(this, "UpdateItem", viewModel.Item);
                await Navigation.PopModalAsync();
            }
            catch (SQLiteException)
            {
                await DisplayAlert("Alert", "Item already exists", "OK");
            }
        }

        async void CancelItem_Clicked(object sender, EventArgs e)
        {
            this.viewModel.Item = await this.viewModel.GetItemById(this.viewModel.Item.ItemId);
            await Navigation.PopModalAsync();
        }

        async void DeleteItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                MessagingCenter.Send(this, "DeleteItem", this.viewModel.Item);
                await Navigation.PopModalAsync();
            }
            catch (SQLiteException)
            {
                await DisplayAlert("Alert", "Failed to delete the item", "OK");
            }
        }

        private void CategoryList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = sender as Picker;
            int selectedIndex = picker.SelectedIndex;
            if (selectedIndex != -1)
            {
                this.viewModel.Item.Category = picker.Items[selectedIndex];
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Categories.Count == 0)
                viewModel.LoadCategoriesCommand.Execute(null);

            int index = viewModel.FindCategoryIndex();
            CategoryList.SelectedIndex = index ;
        }
    }
}