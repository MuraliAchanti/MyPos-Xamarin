using MyPOS.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace MyPOS.Views
{
    [DesignTimeVisible(false)]
    public partial class PaymentPage : ContentPage
    {
        PaymentViewModel viewModel;
              
        public PaymentPage(PaymentViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        //async void SplitPayment_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushModalAsync(new NavigationPage(new SplitPaymentPage()));
        //}

        //async void CardPayment_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushModalAsync(new NavigationPage(new PaymentPage()));
        //}

        //async void CashPayment_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushModalAsync(new NavigationPage(new PaymentPage()));
        //}

        async void NewSale_Clicked(object sender, EventArgs e)
        {
            // insert the records into billeditems and receipts tables
            this.viewModel.InsertReceipt();

            App.GoToMainPage();
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}