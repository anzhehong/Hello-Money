using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationMenuSample
{
    class RecordHelper
    {
        // Record list
        private List<Record> _records;

        // Add new record
        public async void AddNewRecord(Record record)
        {
            await GetData();
            record.ID = Guid.NewGuid();
            this._records.Add(record);
            this.SaveToFile();
        }

        // Read record list
        public async Task<bool> LoadFromFile()
        {
            this._records = await DataStorageHelper.ReadAsync<List<Record>>("Records.dat");
            return (null != this._records);
        }

        // Save records list
        public async void SaveToFile()
        {
            if(_records!=null)
            await DataStorageHelper.WriteAsync<List<Record>>(this._records, "Records.dat");
        }

        // Fetch records list
        public async Task<List<Record>> GetData()
        {
            if (null == this._records)
            {
                bool isExist = await LoadFromFile();
                if (!isExist)
                {
                    this._records = new List<Record>();
                }
            }
            return this._records;
        }
    }
}
