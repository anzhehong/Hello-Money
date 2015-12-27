using System;
using System.Collections.Generic;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace NavigationMenuSample.Views
{
    /// <summary>
    /// The record page
    /// </summary>
    public sealed partial class RecordPage : Page
    {
        public RecordPage()
        {
            this.InitializeComponent();
            rh = new RecordHelper();
            datePicker.MaxYear = DateTime.Now;
        }

        private RecordHelper rh;

        // Income types
        private List<string> _incomeCategory = new List<string>()
        {
            "Salary",
            "Bonux",
            "Investments",
            "Interests",
            "Others"
        };
        public List<string> IncomeCategory
        {
            get
            {
                return _incomeCategory;
            }
        }

        // Record types
        private string _recordType = "";

        // Perform corresponding confirm & cancel actions to click event
        private async void RecordButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Button btn = sender as Button;

            if (btn != null)
            {
                string btn_name = btn.Name;
                switch (btn_name)
                {
                    case "RecordConfirmButton":
                        {
                            double amount = 0;
                            try
                            {
                               // amount = double.Parse(RecordAmount.Text);
                            }
                            catch (Exception ex)
                            {
                                await new MessageDialog(ex.Message).ShowAsync();
                                break;
                            }

                            Record rec = new Record()
                            {
                                Amount = amount,
                                Type = 0,
                                Category = this._recordType,
                                RecordTime = new DateTime(datePicker.Date.Year, datePicker.Date.Month, datePicker.Date.Day,
                                timePicker.Time.Hours, timePicker.Time.Minutes, timePicker.Time.Seconds)
                            };

                            rh.AddNewRecord(rec);
                            await new MessageDialog("Save success!").ShowAsync();
                            
                        }
                        break;
                    case "RecordCancelButton":
                        {

                        }
                        break;
                }
            }
        }

        // Get record type
        private void IncomeType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string iType = e.AddedItems[0].ToString();
            switch (iType)
            {
                case "Salary":
                    this._recordType = "Salary";
                    break;
                case "Bonus":
                    this._recordType = "Bonus";
                    break;
                case "Investments":
                    this._recordType = "Investments";
                    break;
                case "Others":
                    this._recordType = "Others";
                    break;
                default:
                    this._recordType = "Others";
                    break;
            }
        }
    }
}
