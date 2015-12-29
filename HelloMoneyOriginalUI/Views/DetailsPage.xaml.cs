using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this._item = e.Parameter as Record;

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
        private void RecordButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Button btn = sender as Button;

            if (btn != null)
            {
                string btn_name = btn.Name;
                switch (btn_name)
                {
                    case "RecordSaveButton":
                        {
                            
                        }
                        break;
                    case "RecordDeleteButton":
                        {
                            
                        }
                        break;
                }
            }
        }
    }

}

