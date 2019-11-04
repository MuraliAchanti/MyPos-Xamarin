using MyPOS.Helpers;
using MyPOS.Models;
using MyPOS.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyPOS.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public IDataStore<User> DataStore => DependencyService.Get<IDataStore<User>>() ?? new UserDataStore();

        public LoginViewModel()
        {
            Title = "Sign in";
        }

        public async Task<int> Login(string userName, string password)
        {
            int userId = -1;
            var users = await DataStore.GetItemsAsync();
            foreach (var user in users)
            {
                if (user.UserName.Equals(userName) && user.Password.Equals(password))
                {
                    userId = user.Id;
                    Settings.UserId = userId.ToString();
                    Settings.UserType = user.Type;
                    Settings.Email = user.Email;
                    App.GoToMainPage();
                    break;
                }
            }

            return userId;
        }
    }
}
