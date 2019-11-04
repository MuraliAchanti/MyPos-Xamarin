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
    public partial class CategoriesPage : ContentPage
    {
        CategoryViewModel viewModel;

        public CategoriesPage(CategoryViewModel categoryViewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = categoryViewModel;
        }

        async void OnCategorySelected(object sender, SelectedItemChangedEventArgs args)
        {
            var category = args.SelectedItem as Category;
            if (category == null)
                return;

            await Navigation.PushModalAsync(new NavigationPage(new CategoryDetailPage(new CategoryDetailViewModel(category))));

            // Manually deselect the item
            CategoriesListView.SelectedItem = null;
        }

        async void NewCategory_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewCategoryPage(new CategoryDetailViewModel(
                new Category()))));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Categories.Count == 0)
                viewModel.LoadCategoriesCommand.Execute(null);
        }

        public async void OnEdit(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            await Navigation.PushModalAsync(new NavigationPage(new CategoryDetailPage(new CategoryDetailViewModel(
                mi.CommandParameter as Category))));
        }

        public async void OnDelete(object sender, EventArgs e)
        {
            try
            {
                var mi = ((MenuItem)sender);
                await this.viewModel.DeleteCategory(mi.CommandParameter as Category);
            }
            catch (SQLiteException)
            {
                await DisplayAlert("Alert", "Failed to delete the customer information", "OK");
            }
        }

        private void CategorySearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = CategorySearchBar.Text;
            CategoriesListView.ItemsSource = viewModel.Categories.Where(
                Category => Category.Name.ToLower().Contains(keyword.ToLower()));
        }
    }
}