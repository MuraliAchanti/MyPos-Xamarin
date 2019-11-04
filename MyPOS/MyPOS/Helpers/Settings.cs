using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace MyPOS.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants
        const string UserIdKey = "userid";
        static readonly string UserIdDefault = string.Empty;

        const string UserTypeKey = "usertype";
        static readonly int UserTypeDefault = 2;

        const string EmailKey = "email";
        static readonly string DefaultEmail = string.Empty;
        #endregion

        public static bool IsLoggedIn => !string.IsNullOrWhiteSpace(UserId);
        public static string UserId
        {
            get
            {
                return AppSettings.GetValueOrDefault(UserIdKey, UserIdDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserIdKey, value);
            }
        }

        public static int UserType
        {
            get
            {
                return AppSettings.GetValueOrDefault(UserTypeKey, UserTypeDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserTypeKey, value);
            }
        }

        public static string Email
        {
            get
            {
                return AppSettings.GetValueOrDefault(EmailKey, DefaultEmail);
            }
            set
            {
                AppSettings.AddOrUpdateValue(EmailKey, value);
            }
        }
    }
}
