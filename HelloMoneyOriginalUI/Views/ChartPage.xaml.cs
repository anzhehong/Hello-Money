using System.Collections.ObjectModel;
using Windows.UI.Xaml;
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
            this.Loaded += PhoneApplicationPage_Loaded;

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
            //ShowBlock.Text = "Income:" + sumIncome + "\nOutcome:" + sumOutcome;

        }

        //pie
        public ObservableCollection<PData> PData = new ObservableCollection<PData>()
        {
            new PData() { title = "#1", value = 30 },
            new PData() { title = "#2", value = 60 },
            new PData() { title = "#3", value = 40 },
            new PData() { title = "#4", value = 10 },
        };

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            pie1.DataSource = PData;
            this.DataContext = this;
        }

        //bar
        private ObservableCollection<SData> _data = new ObservableCollection<SData>()
        {
            new SData() { cat1 = "cat1", val1=1, val2=15, val3=12},
            new SData() { cat1 = "cat2", val1=4, val2=1.5, val3=2.1M},
            new SData() { cat1 = "cat3", val1=25, val2=5, val3=2},
            new SData() { cat1 = "cat4", val1=8.1, val2=1, val3=8},
            new SData() { cat1 = "cat5", val1=8.1, val2=1, val3=4},
            new SData() { cat1 = "cat6", val1=8.1, val2=1, val3=10},
        };

        public ObservableCollection<SData> SData { get { return _data; } }
    }
    public class PData
    {
        public string title { get; set; }
        public double value { get; set; }
    }
    public class SData
    {
        public string cat1 { get; set; }
        public double val1 { get; set; }
        public double val2 { get; set; }
        public decimal val3 { get; set; }
    }
}
