using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxMatching
{
    public class Reader
    {
        public string Read()
        {
            using (FileStream fstream = File.OpenRead(AppDomain.CurrentDomain.BaseDirectory + @"in4.txt"))
            {
                byte[] array = new byte[fstream.Length];
                fstream.Read(array, 0, array.Length);
                string textFromFile = System.Text.Encoding.Default.GetString(array);
                return textFromFile;
            }
        }

        public void Write(string text)
        {
            System.IO.StreamWriter textFile =
                new System.IO.StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"out4.txt");
            textFile.Write(text);
            textFile.Close();
        }
    }
}