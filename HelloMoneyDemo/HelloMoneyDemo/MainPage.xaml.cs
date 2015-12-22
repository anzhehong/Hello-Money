using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace HelloMoneyDemo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            rh = new RecordHelper();
        }

        private RecordHelper rh;

        public object DatePickedIncome { get; private set; }

        private async void btnIncomeConfirm_Click(object sender, RoutedEventArgs e)
        {
            
            await SaveRecords(0);
        }

        private async void btnOutcomeConfirm_Click(object sender, RoutedEventArgs e)
        {
            
            await SaveRecords(1);
        }

        // Save records
        private async Task<bool> SaveRecords(int type)
        {
            string erro = "";
           
            try
            {
                if ( 0 == type) // income
                {
                    if ( "" == this.textRecordAmount.Text.Trim())
                    {
                        await new MessageDialog("Empty input!").ShowAsync();
                        return false;
                    }
                    else
                    {
                        Record rec = new Record
                        {
                            Amount = double.Parse(this.textRecordAmount.Text),
                            Type = 0,
                            RecordTime = DateTime.Now,
                            Category = "Income"
                        };
                        
                        rh.AddNewRecord(rec);
                        
                    }
                }
                else
                {
                    if ("" == this.textRecordAmount.Text.Trim())
                    {
                        await new MessageDialog("Empty input!").ShowAsync();
                        return false;
                    }
                    else
                    {
                        Record rec = new Record
                        {
                            Amount = double.Parse(this.textRecordAmount.Text),
                            Type = 1,
                            RecordTime = DateTime.Now,
                            Category = "Outcome"
                        };
                        
                        rh.AddNewRecord(rec);
                        
                    }
                }
            }
            catch(Exception ex)
            {
                erro = ex.Message;
            }

            if ("" != erro)
            {
                await new MessageDialog(erro).ShowAsync();
                return false;
            }
            else
            {
                rh.SaveToFile();
                await new MessageDialog("Save Success!").ShowAsync();
                return true;
            }
        }

        private async void btnReadOutcome_Click(object sender, RoutedEventArgs e)
        {
            //await rh.LoadFromFile();
            double sum = 0;
            foreach (Record rec in await rh.GetData())
            {
                if (0 == rec.Type)
                {
                    sum += rec.Amount;
                }
            }
            await new MessageDialog(sum.ToString()).ShowAsync();

        }

        private async void btnReadIncome_Click(object sender, RoutedEventArgs e)
        {
            await rh.LoadFromFile();
            double sum = 0;
            foreach (Record rec in await rh.GetData())
            {
                if (1 == rec.Type)
                {
                    sum += rec.Amount;
                }
            }
            await new MessageDialog(sum.ToString()).ShowAsync();

        }
    }
}
