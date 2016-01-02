using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace NavigationMenuSample.Views
{
    public sealed partial class DetailsPage : Page
    {
        public DetailsPage()
        {
            this.InitializeComponent();
            DetailDatePicker.MaxYear = DateTime.Now;

        }

        private Record _item;

        // enter process
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            this.RequestedTheme = await App.walletHelper.GetTheme();

            base.OnNavigatedTo(e);

            this._item = e.Parameter as Record;

            // set item sources
            if (_item.Type == 0)
            {
                DetailType.ItemsSource = App.IncomeCategory;
            }
            else
            {
                DetailType.ItemsSource = App.ExpendCategories;
            }
            DetailSource.ItemsSource = App.RecordSources;


            // Update details view
            DetailAmount.Text = Convert.ToString(_item.Amount);
            DetailType.SelectedValue = _item.Category;
            DetailType.PlaceholderText = _item.Category;
            DetailSource.SelectedValue = _item.RecordSource;
            DetailSource.SelectedValue = _item.RecordSource;
            DetailDatePicker.Date = new DateTime(_item.RecordTime.Year, _item.RecordTime.Month, _item.RecordTime.Day);
            DetailTimePicker.Time = new TimeSpan(_item.RecordTime.Hour, _item.RecordTime.Minute, _item.RecordTime.Second);
            if (_item.RecordNotes == null)
                _item.RecordNotes = "";
            DetailRecordNotes.Text = _item.RecordNotes;

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
                    case "RecordSaveButton":
                        {
                            var rec_tmp = (from c in await App.recordHelper.GetData()
                                       where (c.ID == _item.ID)
                                       select c);

                            Record rec = rec_tmp.Last();

                            rec.Amount = Convert.ToDouble(DetailAmount.Text);
                            rec.Category = (string)DetailType.SelectedValue;
                            rec.RecordSource = (string)DetailSource.SelectedValue;
                            rec.RecordTime = new DateTime(DetailDatePicker.Date.Year, DetailDatePicker.Date.Month, DetailDatePicker.Date.Day,
                                        DetailTimePicker.Time.Hours, DetailTimePicker.Time.Minutes, DetailTimePicker.Time.Seconds);
                            rec.RecordNotes = DetailRecordNotes.Text;

                            // update records
                            App.recordHelper.SaveToFile();
                            // redirect
                            Frame.Navigate(typeof(BillPage));
                        }
                        break;
                    case "RecordDeleteButton":
                        {
                            var rec_tmp = (from c in await App.recordHelper.GetData()
                                           where (c.ID == _item.ID)
                                           select c);

                            Record rec = rec_tmp.Last();

                            // delete record
                            App.recordHelper.DeleteRecord(rec);
                            // redirect
                            Frame.Navigate(typeof(BillPage));
                        }
                        break;
                }
            }
        }
    }

}

