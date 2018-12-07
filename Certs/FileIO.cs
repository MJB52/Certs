using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Certs
{
    class FileIO
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Project2";

        public void WriteToDir(string name, IEnumerable<string> data)
        {

        }
        public void WriteToDir(string name, string data, string writeType)
        {
            string writePath = path + $"\\{name}{writeType}.txt";
            File.WriteAllText(writePath, data);
        }
        public void CreateDir(string name)
        {
            path += $"\\{name}";
            System.IO.Directory.CreateDirectory(path);
        }
        public object ReadFromDir(string name)
        {
            return new object();
        }

        public bool CheckRequests(string name)
        {
            throw new NotImplementedException();
        }
    }
}
