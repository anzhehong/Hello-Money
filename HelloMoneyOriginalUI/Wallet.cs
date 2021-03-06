﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationMenuSample.Models
{
    
    public class Wallet
    {
        // 账户名称
        public string walletName { get; set; }
        // 账户描述
        public string walletDescription { get; set; }
        // 账户余额
        public double walletValue { get; set; }
        // 账户图片
        public string walletImg { get; set; }
        // TODO:未完待续
    }

    public class WalletHelper
    {
        public List<Wallet> _walletData;
        public double balance = 123; //{ get; set; }
        public List<Record> _recordData;
        public RecordHelper recordHelper;

        public double buget = -999;
        public Windows.UI.Xaml.ElementTheme themeStyle = Windows.UI.Xaml.ElementTheme.Default;

        // load record.dat to read data


        // load wallet.dat
        public async Task<bool> LoadFromFile()
        {
            this._walletData = await DataStorageHelper.ReadAsync<List<Wallet>>("Wallet.dat");
            return (this._walletData != null);
        }
        public async Task<double> LoadBugetFromFile()
        {
            this.buget = await DataStorageHelper.ReadAsync<double>("Buget.dat");
            return (this.buget);
        }
        public async Task<Windows.UI.Xaml.ElementTheme> LoadTheme()
        {
            this.themeStyle = await DataStorageHelper.ReadAsync<Windows.UI.Xaml.ElementTheme>("Theme.dat");
            return (this.themeStyle);
        }


        // save to wallet.dat
        public async void SaveToFile()
        {
            await DataStorageHelper.WriteAsync<List<Wallet>>(this._walletData, "Wallet.dat");
        }

        public async void SaveBugetToFile()
        {
            await DataStorageHelper.WriteAsync<double>(this.buget, "Buget.dat");
        }
        public async void SaveThemeToFile()
        {
            await DataStorageHelper.WriteAsync<Windows.UI.Xaml.ElementTheme>(this.themeStyle, "Theme.dat");
        }

        // data initial func
        internal static List<Wallet> getWallets()
        {
            List<Wallet> list = new List<Wallet>();
            list.Add(new Wallet { walletName = "Alipay", walletDescription = "Supported by Ali Inc.", walletImg = "/Assets/zhifubao.png", walletValue = 0 });
            list.Add(new Wallet { walletName = "Cash", walletDescription = "Money in Pocket", walletImg = "/Assets/cash.png", walletValue = 0 });
            list.Add(new Wallet { walletName = "Credit Card", walletDescription = "All Bank Cards", walletImg = "/Assets/creditcard.png", walletValue = 0 });
            list.Add(new Wallet { walletName = "Others", walletDescription = "All Money Except Things Above", walletImg = "/Assets/others.png", walletValue = 0 });
            return list;
        }

        // func to return list
        public async Task<List<Wallet>> GetData()
        {
            if (this._walletData == null)
            {
                bool isExist = await LoadFromFile();
                if (!isExist)
                {
                    this._walletData = getWallets();
                    IEnumerable<Record> allRecords = await LINQ.GetAllRecords();
                    this._recordData = (await LINQ.GetAllRecords()).ToList();
                    addFunc();
                }
                else
                {
                    // TODO
                }

            }
            else
            {
                this._walletData = getWallets();
                this._recordData = (await LINQ.GetAllRecords()).ToList();
                addFunc();
            }

            return this._walletData;
        }
        // func to return buget
        public async Task<double> GetBuget()
        {
           if( -999 == this.buget)
            {
                double fileBuget = await LoadBugetFromFile();
                if (fileBuget.Equals(null)||fileBuget.Equals(0))
                {
                    this.buget = -999;
                }
                else
                {
                    this.buget = fileBuget;
                }
            }
            return this.buget;
        }
        // func to set buget
        public void SetBuget(double newBuget)
        {
            this.buget = newBuget;
            this.SaveBugetToFile();
        }

        //func to get theme
        public async Task<Windows.UI.Xaml.ElementTheme> GetTheme()
        {

            
            if (this.themeStyle.Equals(null))
            {
                Windows.UI.Xaml.ElementTheme fileTheme = await LoadTheme();
                if(fileTheme.Equals(null))
                {
                    this.themeStyle = Windows.UI.Xaml.ElementTheme.Light;
                }
                this.themeStyle = fileTheme;
            }
            else
            {
                //Windows.UI.Xaml.ElementTheme fileTheme = await LoadTheme();
                //this.themeStyle = fileTheme;
            }
            return this.themeStyle;
        }
        public void SetTheme(Windows.UI.Xaml.ElementTheme newTheme)
        {
            this.themeStyle = newTheme;
            this.SaveThemeToFile();
        }

        public void addFunc()
        {

            foreach (var item in this._recordData)
            {
                // 0 means income
                if (item.Type == 0)
                {
                    for (int i = 0; i < this._walletData.Count; i++)
                    {
                        if (this._walletData[i].walletName.Equals(item.RecordSource))
                            this._walletData[i].walletValue += item.Amount;
                    }

                }
                else
                {
                    for (int i = 0; i < this._walletData.Count; i++)
                    {
                        if (this._walletData[i].walletName.Equals(item.RecordSource))
                            this._walletData[i].walletValue -= item.Amount;
                    }
                }

            }
        }

    }

    public class WalletManager
    {

        // read all wallet 
        public static async Task<IEnumerable<Wallet>> GetAllWallets()
        {
            return (from c in await App.walletHelper.GetData() select c);
        }

        //read wallet record by wallet name
        public static async Task<IEnumerable<Record>> getAllWalletRecordByName(int thisIndex)
        {
            List<Wallet> tempWalletList = (from c in await App.walletHelper.GetData() select c).ToList();
            string name = tempWalletList[thisIndex].walletName;
            return (from c in await App.recordHelper.GetData()
                    where c.RecordSource == name
                    select c);

        }

        // set value according to wallet name
        public async Task<string> setValueByName(string walletName, double newValue)
        {
            IEnumerable<Wallet> wallets = (from c in await App.walletHelper.GetData() select c);

            List<Wallet> tempList = wallets.ToList();
            for (int i = 0; i < tempList.Count(); i++)
            {
                if (tempList[i].walletName.Equals(walletName))
                {
                    tempList[i].walletValue = newValue;
                    App.walletHelper._walletData = tempList;
                    App.walletHelper.SaveToFile();
                    return "Set Value Successfully!";
                }
            }
            return "Something Wrong!";
        }

        // add wallet type
        public async Task<string> addNewWallet(string name, double value, string description)
        {
            IEnumerable<Wallet> wallets = (from c in await App.walletHelper.GetData() select c);

            List<Wallet> tempList = wallets.ToList();
            tempList.Add(new Wallet
            {
                walletDescription = description,
                walletName = name,
                walletValue = value,
                walletImg = "/Assets/others"
            });
            App.walletHelper._walletData = tempList;
            App.walletHelper.SaveToFile();

            return "Add Successfully";
        }

    }


}
