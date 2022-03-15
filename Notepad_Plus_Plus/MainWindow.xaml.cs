using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Notepad_Plus_Plus
{
    public partial class MainWindow : Window
    {
        private List<string> filePath;
        private List<string> clipboard;
        private string text;
        private int caretPosition;
        private int count;
        public MainWindow()
        {
            count = 1;
            InitializeComponent();
            initialize();
            filePath= new List<string>();
            clipboard= new List<string>();
        }

        private void initialize()
        {
            Lenght.Content="Lenght: 0";
            Line.Content = "Line: 0";
            Column.Content = "Column: 0";
            Lines.Content = "Lines: 0";
            Position.Content = "Position: 0";
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            int i=TextTabs.SelectedIndex;
            if (i != -1)
            {
                int index = TextTabs.SelectedIndex;
                TabItem tabItem = TextTabs.Items[index] as TabItem;
                var data = (tabItem.Content as TextBox).Text;
                if (filePath[index] != null)
                {
                    File.WriteAllText(filePath[index], data);

                }
                else
                {
                    SaveFileDialog dialog = new SaveFileDialog()
                    {
                        Filter = "Text Files(*.txt)|*.txt|All(*.*)|*"
                    };

                    if (dialog.ShowDialog() == true)
                    {
                        File.WriteAllText(dialog.FileName, data);
                        filePath[index] = dialog.FileName;
                    }
                }
                string title = fileName(filePath[index]);
                tabItem.Header = title;
                /*string newTitle = null;
                for (int j = 0; j < title.Length - 1; j++)
                    newTitle += title[j];
                tabItem.Header = newTitle;*/
            }
            else
            {
                MessageBox.Show("You don't have selected a file to save");
            }
        }

        private void SaveAll(object sender, RoutedEventArgs e)
        {
            int tabsCount=TextTabs.Items.Count;
            string title = null;
            if (tabsCount > 0)
            {
                for (int i = 0; i < tabsCount; i++)
                {
                    TabItem tabItem = TextTabs.Items[i] as TabItem;
                    var data = (tabItem.Content as TextBox).Text;
                    if (data == null)
                        MessageBox.Show("Empty File");
                    else
                    {
                        SaveFileDialog dialog = new SaveFileDialog()
                        {
                            Filter = "Text Files(*.txt)|*.txt|All(*.*)|*"
                        };

                        if (dialog.ShowDialog() == true)
                        {
                            File.WriteAllText(dialog.FileName, data);
                            title = dialog.FileName;
                        }
                    }
                    string newTitle = fileName(title);
                    tabItem.Header = newTitle;
                }
            }
            else
            {
                MessageBox.Show("You don't have any file to save");
            }
        }

        private void SaveFile(object sender, RoutedEventArgs e)
        {
            int index=TextTabs.SelectedIndex;
            string title=null;
            if (index != -1)
            {
                var tabItem = TextTabs.SelectedItem as TabItem;
                var data = (tabItem.Content as TextBox).Text;
                
                if (data == null)
                    MessageBox.Show("Empty File");
                else
                {
                    SaveFileDialog dialog = new SaveFileDialog()
                    {
                        Filter = "Text Files(*.txt)|*.txt|All(*.*)|*"
                    };

                    if (dialog.ShowDialog() == true)
                    {
                        File.WriteAllText(dialog.FileName, data);
                        title=dialog.FileName;
                    }
                }
                string newTitle = fileName(title);
                tabItem.Header = newTitle;
            }
            else
            {
                MessageBox.Show("You don't have selected a file to save");
            }
        }

        private void Content_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tabItem = TextTabs.SelectedItem as TabItem;
            var data = (tabItem.Content as TextBox).Text;
            TextBox textBox = tabItem.Content as TextBox;
            int lenght=data.Length+1;
            Lenght.Content = "Lenght: " + data.Length.ToString();
            Position.Content ="Position: "+lenght.ToString();
            Lines.Content = "Lines: " + textBox.LineCount.ToString();
            string title = tabItem.Header.ToString();
            if (!title.EndsWith("*"))
            {
                title = title + "*";
                tabItem.Header = title;
                
            }
        }

        private void NewFile(object sender, RoutedEventArgs e)
        {
            TabItem it = new TabItem();
            TextBox txt = new TextBox();
            txt.AcceptsReturn = true;
            txt.AcceptsTab = true;
            it.Header = "new "+count.ToString();
            count++;
            it.Content = txt;
            txt.TextChanged+=Content_TextChanged;
            txt.SelectionChanged += TextBox_SelectionChanged;
            TextTabs.Items.Add(it);
            filePath.Add(null);
        }


        private string fileName(string name)
        {
            string newName = null;
            int c=name.LastIndexOf('\\');
            name=name.Remove(0,c+1);
            
            return name;
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string FileToOpen in openFileDialog.FileNames)
                {
                    TabItem it = new TabItem();
                    TextBox txt = new TextBox();
                    txt.AcceptsReturn = true;
                    txt.AcceptsTab = true;
                    txt.Text = File.ReadAllText(FileToOpen);
                    txt.TextChanged += Content_TextChanged;
                    txt.SelectionChanged += TextBox_SelectionChanged;
                    it.Header = fileName(FileToOpen);
                    filePath.Add(FileToOpen);
                    it.Content = txt;
                    TextTabs.Items.Add(it);
                }
            }
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CloseFiles(object sender, RoutedEventArgs e)
        {
            int index = TextTabs.SelectedIndex;
            if (index != -1)
            {
                TabItem tabItem = TextTabs.Items[index] as TabItem;
                var data = (tabItem.Content as TextBox).Text;
                filePath.RemoveAt(index);
                TextTabs.Items.RemoveAt(index);
            }
            else
            {
                MessageBox.Show("You don't any file to close");
            }
        }



        private void Cut(object sender, RoutedEventArgs e)
        {
            Copy(sender, e);
            Delete(sender, e);
        }

        private void Copy(object sender, RoutedEventArgs e)
        {
            clipboard.Add(text);
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            int index = TextTabs.SelectedIndex;
            TabItem tabItem = TextTabs.Items[index] as TabItem;
            TextBox textBox= tabItem.Content as TextBox;
            string content = (tabItem.Content as TextBox).Text.ToString();
            content=content.Remove(caretPosition, text.Length);
            textBox.Text = content;
            tabItem.Content = textBox;
        }

        private void TextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            text = textBox.SelectedText;
            caretPosition = textBox.SelectionStart;

        }

        private void SelectAll(object sender, RoutedEventArgs e)
        {
            int index = TextTabs.SelectedIndex;
            TabItem tabItem = TextTabs.Items[index] as TabItem;
            TextBox textBox = tabItem.Content as TextBox;
            textBox.SelectAll();
        }

        private void Paste(object sender, RoutedEventArgs e)
        {
            int index=TextTabs.SelectedIndex;
            if (index != -1)
            {
                if (clipboard.Count != 0)
                {
                    string text = clipboard[clipboard.Count - 1];
                    TabItem tabItem = TextTabs.Items[index] as TabItem;
                    TextBox textBox=tabItem.Content as TextBox;
                    string textContent = textBox.Text.ToString();
                    textContent=textContent.Insert(caretPosition, text);
                    textBox.Text= textContent;
                }
                else
                {
                    MessageBox.Show("You don't have nothing to paste");
                }
            }
            else
            {
                MessageBox.Show("You don't any file selected");
            }
        }

        private void Uppercase(object sender, RoutedEventArgs e)
        {
            int index = TextTabs.SelectedIndex;
            if (index != -1)
            {
                TabItem tabItem = TextTabs.Items[index] as TabItem;
                TextBox textBox = tabItem.Content as TextBox;
                string content = (tabItem.Content as TextBox).Text.ToString();
                string newText = text.ToUpper();
                content = content.Remove(caretPosition, text.Length);
                content = content.Insert(caretPosition, newText);
                textBox.Text = content;
                tabItem.Content = textBox;
            }
            else
            {
                MessageBox.Show("You don't any file selected");
            }
        }

        private void Lowercase(object sender, RoutedEventArgs e)
        {
            int index = TextTabs.SelectedIndex;
            if (index != -1)
            {
                TabItem tabItem = TextTabs.Items[index] as TabItem;
                TextBox textBox = tabItem.Content as TextBox;
                string content = (tabItem.Content as TextBox).Text.ToString();
                string newText = text.ToLower();
                content = content.Remove(caretPosition, text.Length);
                content = content.Insert(caretPosition, newText);
                textBox.Text = content;
                tabItem.Content = textBox;
            }
            else
            {
                MessageBox.Show("You don't any file selected");
            }
        }

        private void RemoveLines(object sender, RoutedEventArgs e)
        {
            int index = TextTabs.SelectedIndex;
            if (index != -1)
            {
                TabItem tabItem = TextTabs.Items[index] as TabItem;
                TextBox textBox = tabItem.Content as TextBox;
                string newContent=null;
                string content = (tabItem.Content as TextBox).Text.ToString();
                int line = textBox.LineCount;
                for(int i=0;i<line;i++)
                {
                    bool blank = true;
                    string text=textBox.GetLineText(i);

                    for (int j = 0; j < text.Length; j++)
                    {
                        if (text[j]>=33&&text[j]<=126)
                        {
                            blank = false;
                        }
                    }
                    if (blank==false)
                        newContent += text;
                    
                }
                textBox.Text = newContent;
                tabItem.Content = textBox;
                //MessageBox.Show(newContent);
            }
            else
            {
                MessageBox.Show("You don't any file selected");
            }
        }

        private void Linkedin(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Find(object sender, RoutedEventArgs e)
        {
            
        }

        private void Replace(object sender, RoutedEventArgs e)
        {
            int index=TextTabs.SelectedIndex;
            if (index != -1)
            {
                TabItem tabItem = TextTabs.SelectedItem as TabItem;
                TextBox textBox = tabItem.Content as TextBox;
                string content=textBox.Text;
                Replace r = new Replace(content,this);
                r.Show();
            }
        }

        public void setTextBoxContent(string text)
        {
            TabItem tabItem = TextTabs.SelectedItem as TabItem;
            TextBox textBox = tabItem.Content as TextBox;
            textBox.Text = text;
        }
    }
}
