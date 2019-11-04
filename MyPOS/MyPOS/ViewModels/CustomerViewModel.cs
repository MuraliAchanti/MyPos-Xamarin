using MyPOS.Models;
using MyPOS.Services;
using MyPOS.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyPOS.ViewModels
{
    public class CustomerViewModel : BaseViewModel
    {
        public IDataStore<Customer> DataStore => DependencyService.Get<IDataStore<Customer>>() ?? new CustomerDataStore();
        public ObservableCollection<Customer> Customers { get; set; }
        public Command LoadCustomersCommand { get; set; }
        public Customer LinkedCustomer { get; set; }
        public static  CustomerViewModel viewModel= new CustomerViewModel();
        public static CustomerViewModel GetInstance()
        {

            if (viewModel == null)
            {
                viewModel = new CustomerViewModel();
                
            }
            return viewModel;
        }
        private CustomerViewModel()
        {
            Customers = new ObservableCollection<Customer>();
            LoadCustomersCommand = new Command(async () => await ExecuteLoadCustomersCommand());
            
            MessagingCenter.Subscribe<NewCustomerPage, Customer>(this, "AddCustomer", async (obj, customer) =>
            {
                var newCustomer = customer as Customer;
                Customers.Add(newCustomer);
                await DataStore.AddItemAsync(newCustomer);
            });

            MessagingCenter.Subscribe<CustomerDetailPage, Customer>(this, "UpdateCustomer", async (obj, customer) =>
            {
                var updatedCustomer = customer as Customer;
                await DataStore.UpdateItemAsync(updatedCustomer);
            });

            MessagingCenter.Subscribe<CustomerDetailPage, Customer>(this, "DeleteCustomer", async (obj, customer) =>
            {
                var deletedCustomer = customer as Customer;
                Customers.Remove(deletedCustomer);
                await DataStore.DeleteItemAsync(deletedCustomer);
            });
        }

       

        public async Task<int> DeleteCustomer(Customer deletedCustomer)
        {
            Customers.Remove(deletedCustomer);
            return await DataStore.DeleteItemAsync(deletedCustomer);
        }
        
        async Task ExecuteLoadCustomersCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Customers.Clear();
                var customers = await DataStore.GetItemsAsync();
                foreach (var customer in customers)
                {
                    Customers.Add(customer);
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
}