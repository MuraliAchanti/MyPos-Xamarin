using MyPOS.Models;
using MyPOS.Services;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyPOS.ViewModels
{
    public class ReceiptDetailViewModel : BaseViewModel
    {
        public Receipt Receipt { get; set; }
        public Command LoadBilledItemsCommand { get; set; }
        public string BilledItems { get; set; }

        public BilledItemDataStore BilledItemDataStore = new BilledItemDataStore();
        
        public ReceiptDetailViewModel(Receipt receipt = null)
        {
            Title = '#' + receipt?.ReceiptId.ToString();
            Receipt = receipt;
            LoadBilledItemsCommand = new Command(async () => await ExecuteLoadBilledItemsCommand());
        }
        async Task ExecuteLoadBilledItemsCommand()
        {
            try
            {
                //var billedItems = await BilledItemDataStore.GetItemsAsync(Receipt.ReceiptId);
                //foreach (var item in billedItems)
                //{
                //    Receipt.Note += item.Quantity + "\t x " + item.ItemName + "\n";
                //}
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
