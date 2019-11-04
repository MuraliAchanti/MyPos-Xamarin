using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MyPOS.Models;
using MyPOS.ViewModels;

namespace MyPOS.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
        {
            InitializeComponent();
            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemType.Sales, (NavigationPage)Detail);
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.Sales:
                        MenuPages.Add(id, new NavigationPage(new SalesPage()));
                        break;
                    case (int)MenuItemType.Receipts:
                        MenuPages.Add(id, new NavigationPage(new ReceiptsPage(new ReceiptsViewModel())));
                        break;
                    case (int)MenuItemType.Items:
                        MenuPages.Add(id, new NavigationPage(new ItemsPage(new ItemsViewModel())));
                        break;
                    case (int)MenuItemType.Categories:
                        MenuPages.Add(id, new NavigationPage(new CategoriesPage(new CategoryViewModel())));
                        break;
                    case (int)MenuItemType.Settings:
                        MenuPages.Add(id, new NavigationPage(new SettingsPage(new SettingsViewModel())));
                        break;
                    case (int)MenuItemType.Reports:
                        MenuPages.Add(id, new NavigationPage(new ReportsPage(new ReportViewModel())));
                        break;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}