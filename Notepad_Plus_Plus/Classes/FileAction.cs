using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
            filePath.Add(null);
            return tabItem;
        }

        public TabItem openFile(string title, string content, string fileP)
        {
            TabItem tabItem = new TabItem();
            TextBox textBox = new TextBox();
            textBox.AcceptsReturn = true;
            textBox.AcceptsTab = true;
            tabItem.Header = title;
            textBox.Text = content;
            tabItem.Content = textBox;
            filePath.Add(fileP);
            return tabItem;
        }

        public string save(int index, string content)
        {
            if (filePath[index] != null)
            {
                File.WriteAllText(filePath[index], content);
            }
            else
            {
                SaveFileDialog dialog = new SaveFileDialog()
                {
                    Filter = "Text Files(*.txt)|*.txt|All(*.*)|*"
                };

                if (dialog.ShowDialog() == true)
                {
                    File.WriteAllText(dialog.FileName, content);
                    filePath[index] = dialog.FileName;
                }
            }
            return filePath[index];
        }

        public string saveFile(int index, string content)
        {
            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = "Text Files(*.txt)|*.txt|All(*.*)|*"
            };

            if (dialog.ShowDialog() == true)
            {
                File.WriteAllText(dialog.FileName, content);
                filePath[index] = dialog.FileName;
            }
            return filePath[index];
        }

        public void closeFile(int index)
        {
            filePath.RemoveAt(index);
        }
    }
}
