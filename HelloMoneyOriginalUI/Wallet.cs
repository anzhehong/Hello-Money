using System;
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

        // load record.dat to read data


        // load wallet.dat
        public async Task<bool> LoadFromFile()
        {
            this._walletData = await DataStorageHelper.ReadAsync<List<Wallet>>("Wallet.dat");
            return (this._walletData != null);
        }


        // save to wallet.dat
        public async void SaveToFile()
        {
            await DataStorageHelper.WriteAsync<List<Wallet>>(this._walletData, "Wallet.dat");
        }

        // data initial func
        internal static List<Wallet> getWallets()
        {
            List<Wallet> list = new List<Wallet>();
            list.Add(new Wallet { walletName = "Alipay", walletDescription = "阿里公司支付宝", walletImg = "/Assets/zhifubao.png", walletValue = 0 });
            list.Add(new Wallet { walletName = "Cash", walletDescription = "口袋money", walletImg = "/Assets/cash.png", walletValue = 0 });
            list.Add(new Wallet { walletName = "Credit Card", walletDescription = "各种银行卡", walletImg = "/Assets/creditcard.png", walletValue = 0 });
            list.Add(new Wallet { walletName = "Others", walletDescription = "除了上面的所有money", walletImg = "/Assets/others.png", walletValue = 0 });
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
            List<Wallet> tempWalletList =( from c in await App.walletHelper.GetData() select c).ToList();
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
