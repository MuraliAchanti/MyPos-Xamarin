using MyPOS.Models;
using MyPOS.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyPOS.ViewModels
{
    public class CustomerDetailViewModel : BaseViewModel
    {
        public IDataStore<Customer> DataStore => DependencyService.Get<IDataStore<Customer>>() ?? new CustomerDataStore();
        public Customer Customer { get; set; }

        public CustomerDetailViewModel(Customer customer = null)
        {
            Title = customer?.Name;
            Customer = customer;
        }

        public async Task<int> AddCustomer(Customer newCustomer)
        {
            return await DataStore.AddItemAsync(newCustomer);
        }
    }
}
