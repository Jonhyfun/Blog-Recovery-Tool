using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blog_Recovery_Tool
{
    public class Dialog
    {
        static OpenFileDialog file = null;
        private static void CallDialog()
        {
            file = new OpenFileDialog();
            file.Filter = "Text Files|*.txt";
            file.DefaultExt = "txt";
            file.Title = "Please select your txt list";
            file.InitialDirectory = Directory.GetCurrentDirectory();
            file.ShowDialog();
        } 
        public static string[] GetList()
        {
            CallDialog();
            return File.ReadLines(file.FileName).ToArray();

        }
    }
}
