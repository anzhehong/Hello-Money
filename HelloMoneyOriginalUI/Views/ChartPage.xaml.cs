using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class ChartPage : Page
    {
        private RecordHelper rh;
        public ChartPage()
        {
            this.InitializeComponent();
            rh = new RecordHelper();
            //this.Loaded += PhoneApplicationPage_Loaded;

           // this.ShowBlockInfo();
        }

        // 导航进入界面的事件处理程序
        protected async override void OnNavigatedTo (NavigationEventArgs e)
        {
            // 创建图表的数据源对象
            ObservableCollection<ChartData> collecion = new ObservableCollection<ChartData>();
            // 获取所有的记账记录
            IEnumerable<Record> allRecords = await LINQ.GetAllRecords();
            // 获取所有记账记录里面的类别
            IEnumerable<string> enumerable2 = (from c in allRecords select c.Category).Distinct<string>();
            // 按照类别来统计记账的数目
            foreach (var item in enumerable2)
            {
                // 获取该类别下的钱的枚举集合
                IEnumerable<double> enumerable3 = from c in allRecords.Where<Record>(c => c.Category == item) select c.Amount;
                // 添加一条图表的数据
                ChartData data = new ChartData
                {
                    Account = enumerable3.Sum(),
                    Type = item
                };
                collecion.Add(data);
            }
            // 设置柱形图形的数据源
            barChart.DataSource = collecion;

            pieChart.DataSource = collecion;
        }

        //    private async void ShowBlockInfo()
        //    {
        //        double sumIncome = 0;
        //        double sumOutcome = 0;
        //        foreach(Record rec in await rh.GetData())
        //        {
        //            if(rec.Type == 0)
        //            {
        //                sumIncome += rec.Amount;
        //            }
        //            else if(rec.Type == 1)
        //            {
        //                sumOutcome += rec.Amount;
        //            }
        //        }
        //      //  ShowBlock.Text = "Income:" + sumIncome + "\nOutcome:" + sumOutcome;

        //    }

        //    //pie
        //    public ObservableCollection<PData> PData = new ObservableCollection<PData>()
        //    {
        //        new PData() { title = "#1", value = 30 },
        //        new PData() { title = "#2", value = 60 },
        //        new PData() { title = "#3", value = 40 },
        //        new PData() { title = "#4", value = 10 },
        //    };

        //    private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        //    {
        //        pie1.DataSource = PData;
        //        this.DataContext = this;
        //    }

        //    //bar
        //    private ObservableCollection<SData> _data = new ObservableCollection<SData>()
        //    {
        //        new SData() { cat1 = "cat1", val1=1, val2=15, val3=12},
        //        new SData() { cat1 = "cat2", val1=4, val2=1.5, val3=2.1M},
        //        new SData() { cat1 = "cat3", val1=25, val2=5, val3=2},
        //        new SData() { cat1 = "cat4", val1=8.1, val2=1, val3=8},
        //        new SData() { cat1 = "cat5", val1=8.1, val2=1, val3=4},
        //        new SData() { cat1 = "cat6", val1=8.1, val2=1, val3=10},
        //    };

        //    public ObservableCollection<SData> SData { get { return _data; } }
        //}
        //public class PData
        //{
        //    public string title { get; set; }
        //    public double value { get; set; }
        //}
        //public class SData
        //{
        //    public string cat1 { get; set; }
        //    public double val1 { get; set; }
        //    public double val2 { get; set; }
        //    public decimal val3 { get; set; }
        //}
    }
}
