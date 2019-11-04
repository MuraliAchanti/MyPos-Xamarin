using MyPOS.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace MyPOS.Views
{
    [DesignTimeVisible(false)]
    public partial class ReceiptDetailPage : ContentPage
    {
        public ReceiptDetailViewModel viewModel;

        public ReceiptDetailPage(ReceiptDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
            this.viewModel.LoadBilledItemsCommand.Execute(null);
        }

        private void EmailReceipt_Clicked(object sender, System.EventArgs e)
        {
            //TODO
            DisplayAlert("Success", "Email Sent", "Ok");
        }

        private void PrintReceipt_Clicked(object sender, System.EventArgs e)
        {
            //TODO
            return;
        }
    }
}