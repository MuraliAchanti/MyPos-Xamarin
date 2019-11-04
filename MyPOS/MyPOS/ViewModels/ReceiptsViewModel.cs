using MyPOS.Models;
using MyPOS.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyPOS.ViewModels
{
    public class ReceiptsViewModel : BaseViewModel
    {
        public IDataStore<Receipt> DataStore => DependencyService.Get<IDataStore<Receipt>>() ?? new ReceiptDataStore();
        public ObservableCollection<Receipt> Receipts { get; set; }
        public Command LoadReceiptsCommand { get; set; }
        public ReceiptsViewModel()
        {
            Title = "Receipts";
            Receipts = new ObservableCollection<Receipt>();
            LoadReceiptsCommand = new Command(async () => await ExecuteLoadReceiptsCommand());
        }

        async Task ExecuteLoadReceiptsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Receipts.Clear();
                var receipts = await DataStore.GetItemsAsync();
                foreach (var receipt in receipts)
                {
                    Receipts.Insert(0,receipt);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime date = (DateTime)value;            
            return date.Day.ToString().PadLeft(2, '0') + @"/" + date.Month.ToString().PadLeft(2, '0') + "-" + date.Year;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
