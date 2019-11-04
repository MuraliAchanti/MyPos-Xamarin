using MyPOS.Models;
using MyPOS.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyPOS.ViewModels
{
    public class ReportViewModel : BaseViewModel
    {
        public IDataStore<Receipt> DataStore => DependencyService.Get<IDataStore<Receipt>>() ?? new ReceiptDataStore();
        public List<Receipt> Receipts { get; set; }
        public Command LoadReceiptsCommand { get; set; }
        public ReportViewModel()
        {
            Title = "Sales Summary";
            Receipts = new List<Receipt>();
            LoadReceiptsCommand = new Command(async () => await ExecuteLoadReceiptsCommand());
        }

        async Task ExecuteLoadReceiptsCommand()
        {
            try
            {
                Receipts.Clear();
                var receipts = await DataStore.GetItemsAsync();
                
                foreach (var receipt in receipts)
                {
                    Receipts.Insert(0, receipt);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
