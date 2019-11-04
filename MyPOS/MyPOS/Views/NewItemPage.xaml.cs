using MyPOS.Models;
using MyPOS.ViewModels;
using SQLite;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace MyPOS.Views
{
    [DesignTimeVisible(false)]
    public partial class NewItemPage : ContentPage
    {
        ItemDetailViewModel viewModel;
        public NewItemPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }

        async void SaveItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.viewModel.Item.Name))
                {
                    await DisplayAlert("Alert", "Name cannot be empty", "OK");
                    return;
                }
                else if (this.CategoryList.Items.Count == 0)
                {
                    var answer = await DisplayAlert("Alert", "Do you want to add new category?", "Yes", "No");
                    if (answer)
                    {
                        await Navigation.PushModalAsync(new NavigationPage(new NewCategoryPage(
                            new CategoryDetailViewModel(new Category()))));
                    }
                    return;
                }
                else if (this.CategoryList.Items.Count > 1 && this.CategoryList.SelectedIndex == -1)
                {
                    await DisplayAlert("Alert", "Category not selected", "OK");
                    return;
                }
                else if (this.Price.Text.Equals("0"))
                {
                    await DisplayAlert("Alert", "Price cannot be zero", "OK");
                    return;
                }
                else if (this.Cost.Text.Equals("0"))
                {
                    await DisplayAlert("Alert", "Cost cannot be zero", "OK");
                }
                else
                {
                    MessagingCenter.Send(this, "AddItem", this.viewModel.Item);
                    await Navigation.PopModalAsync();
                }
            }
            catch (SQLiteException)
            {
                await DisplayAlert("Alert", "Item already exists", "OK");
            }
        }

        async void CancelItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
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
            viewModel.LoadCategoriesCommand.Execute(null);
        }
    }
}