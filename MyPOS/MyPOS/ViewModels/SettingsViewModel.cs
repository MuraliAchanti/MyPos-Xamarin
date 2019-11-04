using MyPOS.Helpers;

namespace MyPOS.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        public string Email { get; set; }
        public string Role { get; set; }
        public SettingsViewModel()
        {
            Title = "Settings";
            Email = Settings.Email;
            Role = Settings.UserType == 1 ? "Manager" : "Cashier";
        }

        public void LogOut()
        {
            Settings.UserId = string.Empty;
        }
    }
}