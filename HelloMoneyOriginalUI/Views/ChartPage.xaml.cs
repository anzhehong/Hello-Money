using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace NavigationMenuSample.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ChartPage : Page
    {
        private RecordHelper rh;
        public ChartPage()
        {
            this.InitializeComponent();
            rh = new RecordHelper();

            this.ShowBlockInfo();
        }

        private async void ShowBlockInfo()
        {
            double sumIncome = 0;
            double sumOutcome = 0;
            foreach(Record rec in await rh.GetData())
            {
                if(rec.Type == 0)
                {
                    sumIncome += rec.Amount;
                }
                else if(rec.Type == 1)
                {
                    sumOutcome += rec.Amount;
                }
            }
            ShowBlock.Text = "Income:" + sumIncome + "\nOutcome:" + sumOutcome;
            
        }
    }
}
