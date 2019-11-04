using MyPOS.Models;
using MyPOS.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyPOS.ViewModels
{
    public class PaymentViewModel : BaseViewModel
    {
        public IDataStore<Receipt> DataStore => DependencyService.Get<IDataStore<Receipt>>() ?? new ReceiptDataStore();
        public IDataStore<BilledItem> BilledItemDataStore => DependencyService.Get<IDataStore<BilledItem>>() ?? new BilledItemDataStore();

        public List<BilledItem> BilledItems = new List<BilledItem>();
        public Customer Customer { get; set; }
        public double TotalCost { get; set; }
        public PaymentViewModel(List<BilledItem> billedItems, Customer customer)
        {
            Title = "Charge";
            Customer = customer;
            TotalCost = 0;

            foreach (BilledItem item in billedItems)
            {
                BilledItems.Add(item);
                TotalCost += item.Price * item.Quantity;
            }
        }

        public async void InsertReceipt()
        {
            string billedItemsList = string.Empty;
            foreach (var billedItem in BilledItems)
            {
                billedItemsList += billedItem.Quantity + "\tx\t " + billedItem.Name + "\n";
            }

            Receipt receipt = new Receipt {
                CustomerId = Customer.Id,
                Name = Customer.Name,
                Email = Customer.Email,
                Total = TotalCost,
                ItemsList = billedItemsList,
                Note = string.Empty,
                TransactionDate = DateTime.Now
            };
            AddReceipt(receipt);

            foreach (var billedItem in BilledItems)
            {
                BilledItem ItemBilled = new BilledItem();                
                ItemBilled.Name = billedItem.Name;
                ItemBilled.Price = billedItem.Price;
                ItemBilled.Quantity = billedItem.Quantity;
                ItemBilled.ReceiptId = receipt.ReceiptId;
                AddBilledItem(ItemBilled);
            }
        }

        public async Task<int> AddBilledItem(BilledItem newBilled)
        {
            return await BilledItemDataStore.AddItemAsync(newBilled);
        }

        public async Task<int> AddReceipt(Receipt newReceipt)
        {
            return await DataStore.AddItemAsync(newReceipt);
        }

    }
}
