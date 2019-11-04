using Microcharts;
using MyPOS.Models;
using MyPOS.ViewModels;
using SkiaSharp;
using System.Collections.Generic;
using Xamarin.Forms;
using Entry = Microcharts.Entry;

namespace MyPOS.Views
{
    public partial class ReportsPage : ContentPage
    {
        ReportViewModel viewModel;
        public ReportsPage(ReportViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Receipts.Count == 0)
                this.viewModel.LoadReceiptsCommand.Execute(null);

            List<Entry> entries = new List<Entry>();

            foreach (Receipt receipt in viewModel.Receipts)
            {
                Entry entry = new Entry((float)receipt.Total);
                entry.Label = receipt.TransactionDate.ToString();
                entry.ValueLabel = receipt.Total.ToString();
                entry.Color = SKColor.Parse("#00CED1");
                entries.Add(entry);
            }

            SalesChart.Chart = new LineChart { Entries = entries };
        }
    }
}