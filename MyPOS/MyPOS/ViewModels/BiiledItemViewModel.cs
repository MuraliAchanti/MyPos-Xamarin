using MyPOS.Models;
using MyPOS.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyPOS.ViewModels
{
    public class BilledItemViewModel : BaseViewModel
    {
        public ObservableCollection<BilledItem> BilledItems { get; set; }
        public IDataStore<BilledItem> DataStore => DependencyService.Get<IDataStore<BilledItem>>() ?? new BilledItemDataStore();
        public Command GetBilledItemsCommand { get; set; }
        public BilledItemViewModel()
        {
            BilledItems = new ObservableCollection<BilledItem>();
            GetBilledItemsCommand = new Command(async () => await ExecuteGetBilledItemsCommand());
        }
        public async Task<int> ExecuteGetBilledItemsCommand()
        {
            try
            {
                BilledItems.Clear();
                var billedItems = await DataStore.GetItemsAsync();
                foreach (var item in BilledItems)
                {
                    BilledItems.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return 1;
        }
    }
}
