using MyPOS.Models;
using MyPOS.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace MyPOS.Views
{
    [DesignTimeVisible(false)]
    public partial class ReceiptsPage : ContentPage
    {
        public ReceiptsViewModel viewModel;
        public Receipt Receipt { get; }

        public ReceiptsPage(ReceiptsViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        async void OnReceiptSelected(object sender, SelectedItemChangedEventArgs args)
        {
           Receipt receipt = args.SelectedItem as Receipt;
            if (receipt == null)
                return;

            await Navigation.PushModalAsync(new NavigationPage(new ReceiptDetailPage(new ReceiptDetailViewModel(receipt))));

            // Manually deselect item.
            ReceiptsListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Receipts.Count == 0)
                viewModel.LoadReceiptsCommand.Execute(null);
        }
    }
}