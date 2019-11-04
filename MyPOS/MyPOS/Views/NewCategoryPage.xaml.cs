using MyPOS.Models;
using MyPOS.ViewModels;
using SQLite;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;

namespace MyPOS.Views
{
    [DesignTimeVisible(false)]
    public partial class NewCategoryPage : ContentPage
    {
        CategoryDetailViewModel viewModel;
        
        public NewCategoryPage(CategoryDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }

        async void SaveCategory_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.viewModel.Category.Name))
                {
                    await DisplayAlert("Alert", "Name cannot be empty", "OK");
                    return;
                }
                MessagingCenter.Send(this, "AddCategory", this.viewModel.Category);
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
    }
}