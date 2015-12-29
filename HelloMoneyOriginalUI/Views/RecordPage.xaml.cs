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
          //  rh = new RecordHelper();
            datePicker.MaxYear = DateTime.Now;
            IncomeType.SelectedValue = "Salary";
            RecordSource.SelectedValue = "Cash";
            ExpendType.SelectedValue = "Food";
            ExpendRecordSource.SelectedValue = "Cash";
        }

       // private RecordHelper rh;

        // Income types
        private List<string> _incomeCategory = new List<string>()
        {
            "Salary",
            "Bonus",
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
        // Record sources
        private List<string> _recordSources = new List<string>()
        {
            "Cash",
            "Credit Card",
            "Alipay" ,
            "Others"
        };
        public List<string> RecordSources
        {
            get
            {
                return _recordSources;
            }
        }
        // expend category
        private List<string> _expendCategories = new List<string>()
        {
            "Food",
            "Shopping",
            "Others"
        };
        public List<string> ExpendCategories
        {
            get
            {
                return this._expendCategories;
            }
        }

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
                               amount = (PivotIndex.SelectedIndex == 0) ? 
                                    double.Parse(RecordAmount.Text) 
                                    : double.Parse(ExpendsRecordAmount.Text);
                            }
                            catch (Exception ex)
                            {
                                await new MessageDialog(ex.Message).ShowAsync();
                                break;
                            }

                            Record rec = new Record()
                            {
                                Amount = amount,
                                Type = PivotIndex.SelectedIndex,
                                Category = (PivotIndex.SelectedIndex == 0) ? (string)IncomeType.SelectedValue 
                                    : (string)ExpendType.SelectedValue,
                                RecordSource = (PivotIndex.SelectedIndex == 0) ? (string)RecordSource.SelectedValue 
                                    : (string)ExpendRecordSource.SelectedValue,
                                RecordTime = (PivotIndex.SelectedIndex == 0) ? 
                                    new DateTime(datePicker.Date.Year,datePicker.Date.Month,datePicker.Date.Day,
                                        timePicker.Time.Hours,timePicker.Time.Minutes,timePicker.Time.Seconds)
                                    : new DateTime(ExpendDatePicker.Date.Year, ExpendDatePicker.Date.Month, ExpendDatePicker.Date.Day,
                                        ExpendTimePicker.Time.Hours, ExpendTimePicker.Time.Minutes, ExpendTimePicker.Time.Seconds) ,
                                RecordNotes = (PivotIndex.SelectedIndex == 0) ?
                                    RecordNotes.Text : ExpendRecordNotes.Text
                            };

                            App.recordHelper.AddNewRecord(rec);
                            await new MessageDialog("Save success!").ShowAsync();
                            Frame.Navigate(typeof(BillPage));
                        }
                        break;
                    case "RecordCancelButton":
                        {
                            Frame.Navigate(typeof(BillPage));
                        }
                        break;
                }
            }
        }        
    }
}
