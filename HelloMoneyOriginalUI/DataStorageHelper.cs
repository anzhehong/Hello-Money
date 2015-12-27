using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace NavigationMenuSample
{
    class DataStorageHelper
    {
        // Folder name of system data
        private const string FolderName = "Records";
        // Folder Object of system data
        private static StorageFolder DataFolder = null;

        // Fetch the folder object
        private static async Task<StorageFolder> GetDataFolder()
        {
            // Fetch folder
            if (null == DataFolder)
            {
                DataFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(FolderName, CreationCollisionOption.OpenIfExists);
            }
            return DataFolder;
        }

        // Read file contents under data folder
        public static async Task<string> ReadFileAsync(string fileName)
        {
            string text;
            try
            {
                // Fetch data folder
                StorageFolder applicationFolder = await GetDataFolder();
                // Read file coressponding to fileName
                StorageFile storageFile = await applicationFolder.GetFileAsync(fileName);
                // Get data stream from the file
                IRandomAccessStream accessStream = await storageFile.OpenReadAsync();
                // Read file contents
                using (StreamReader streamReader = new StreamReader(accessStream.AsStreamForRead((int)accessStream.Size)))
                {
                    text = streamReader.ReadToEnd();
                }

            }
            catch (Exception ex)
            {
                text = "Read File Error : " + ex.Message;
            }
            return text;

        }

        // Write contents into file
        public static async Task WriteFileAsync(string fileName, string content)
        {
            // Fetch data folder
            StorageFolder applicationFolder = await GetDataFolder();
            // Create file in the folder , replace old one if same file already exists
            StorageFile storageFile = await applicationFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
            // Write contents into the file
            await FileIO.WriteTextAsync(storageFile, content);

        }

        // Serialize entity objects into xml format and stored into given file
        public static async Task WriteAsync<T>(T data, string fileName)
        {
            // Fetch data folder
            StorageFolder applicationFolder = await GetDataFolder();
            StorageFile file = await applicationFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
            using (IRandomAccessStream accessStream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                using (IOutputStream outStream = accessStream.GetOutputStreamAt(0))
                {
                    // Create serialized object
                    DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                    serializer.WriteObject(outStream.AsStreamForWrite(), data);
                    await outStream.FlushAsync();
                }
            }
        }

        // Deserialize xml files
        public static async Task<T> ReadAsync<T>(string fileName)
        {
            // Fetch object
            T sessionState_ = default(T);
            // Fetch data folder
            StorageFolder applicationFolder = await GetDataFolder();
            StorageFile file = await applicationFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
            if (null == file)
            {
                return sessionState_;
            }
            try
            {
                using (IInputStream inStream = await file.OpenSequentialReadAsync())
                {
                    // Deserialize xml data
                    DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                    sessionState_ = (T)serializer.ReadObject(inStream.AsStreamForRead());

                }
            }
            catch (Exception ex)
            {

            }

            return sessionState_;

        }

    }
}
