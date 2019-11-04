using MyPOS.ViewModels;
using SQLite;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace MyPOS.Views
{
    [DesignTimeVisible(false)]
    public partial class CategoryDetailPage : ContentPage
    {
        CategoryDetailViewModel viewModel;
        public CategoryDetailPage(CategoryDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }

        async void SaveCategory_Clicked(object sender, EventArgs e)
        {
            try
            {
                MessagingCenter.Send(this, "UpdateCategory", viewModel.Category);
                await Navigation.PopModalAsync();
            }
            catch (SQLiteException)
            {
                await DisplayAlert("Alert", "Category already exists", "OK");
            }
        }

        async void CancelCategory_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}