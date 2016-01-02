using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace NavigationMenuSample.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SearchPage : Page
    {
        public SearchPage()
        {
            this.InitializeComponent();
            listReport.IsItemClickEnabled = true;
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // fetch search restriction
            DateTime startDate = new DateTime(DatePickerBegin.Date.Year, DatePickerBegin.Date.Month, DatePickerBegin.Date.Day);
            DateTime endDate = new DateTime(DatePickerEnd.Date.Year, DatePickerEnd.Date.Month, DatePickerEnd.Date.Day);
            string keywords = (keyWords.Text == null) ? "" : keyWords.Text.ToLower();

            // find records that match the given restriction
            var allRecords = await LINQ.GetAllRecords();
            var dateResult = allRecords.Where(r => (r.RecordTime <= endDate) &&
                                (r.RecordTime >= startDate));

            var queryResult = dateResult;
            if(keywords != null)
            {
                if(keywords != "")
                {
                    queryResult = dateResult.Where(r => (r.Category.ToLower().Contains(keywords) ||
                                        r.RecordNotes.ToLower().Contains(keywords) ||
                                        r.RecordSource.ToLower().Contains(keywords)));
                }
                
            }
            listReport.ItemsSource = queryResult;

        }

        // redirect to details page
        private void NavToItemDetails(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(DetailsPage), e.ClickedItem);
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            this.RequestedTheme = await App.walletHelper.GetTheme();

            base.OnNavigatedFrom(e);
        }
    }
}
