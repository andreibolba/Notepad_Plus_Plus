using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Notepad_Plus_Plus.Classes
{
    internal class FileAction
    {
        #region variables and constructor 
        private int count;
        private List<string> filePath;
        public FileAction()
        {
            count = 1;
            filePath = new List<string>();
        }

        #endregion

        public TabItem newFile()
        {
            TabItem tabItem = new TabItem();
            TextBox textBox = new TextBox();
            textBox.AcceptsReturn = true;
            textBox.AcceptsTab = true;
            tabItem.Header = "new " + count.ToString();
            count++;
            tabItem.Content = textBox;
            return tabItem;
        }
    }
}
