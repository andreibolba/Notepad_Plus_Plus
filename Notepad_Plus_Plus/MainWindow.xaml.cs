using Microsoft.Win32;
using Notepad_Plus_Plus.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Notepad_Plus_Plus
{
    public partial class MainWindow : Window
    {
        #region variables and constructor
        private string text;
        private int caretPosition;
        private Edit edit;
        private Utils utils;
        private FileAction fileAction;
        public MainWindow()
        {
            InitializeComponent();
            initialize();
            edit = new Edit();
            utils = new Utils();
            fileAction = new FileAction();
        }

        private void initialize()
        {
            Lenght.Content = "Lenght: 0";
            Line.Content = "Line: 0";
            Column.Content = "Column: 0";
            Lines.Content = "Lines: 0";
            Position.Content = "Position: 0";
            FileRead.Content = "Is not readonly!";
        }
        #endregion

        #region utils

        void setChanges()
        {
            int count=TextTabs.Items.Count;
            TabItem tabItem=TextTabs.Items[count-1] as TabItem;
            TextBox textBox=tabItem.Content as TextBox;
            textBox.TextChanged += Content_TextChanged;
            tabItem.Content = textBox;
        }

        #endregion

        #region file action
        private void NewFile(object sender, RoutedEventArgs e)
        {
            TextTabs.Items.Add(fileAction.newFile());
            setChanges();
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string FileToOpen in openFileDialog.FileNames)
                {
                    TextTabs.Items.Add(fileAction.openFile(utils.fileName(FileToOpen), File.ReadAllText(FileToOpen),FileToOpen));
                    setChanges();
                }
            }
        }
        private void Save(object sender, RoutedEventArgs e)
        {
            int i = TextTabs.SelectedIndex;
            if (i != -1)
            {
                TabItem tabItem = TextTabs.Items[i] as TabItem;
                string data = (tabItem.Content as TextBox).Text.ToString();
                string title = utils.fileName(fileAction.save(i,data));
                tabItem.Header = title;
            }
            else
            {
                MessageBox.Show("You don't have selected a file to save");
            }
        }
        private void SaveFile(object sender, RoutedEventArgs e)
        {
            int index = TextTabs.SelectedIndex;
            if (index != -1)
            {
                TabItem tabItem = TextTabs.SelectedItem as TabItem;
                string data = (tabItem.Content as TextBox).Text.ToString();
                string newTitle = utils.fileName(fileAction.saveFile(index,data));
                tabItem.Header = newTitle;
            }
            else
            {
                MessageBox.Show("You don't have selected a file to save");
            }
        }
        private void SaveAll(object sender, RoutedEventArgs e)
        {
            int tabsCount = TextTabs.Items.Count;
            if (tabsCount > 0)
            {
                for (int i = 0; i < tabsCount; i++)
                {
                    TabItem tabItem = TextTabs.Items[i] as TabItem;
                    string data = (tabItem.Content as TextBox).Text.ToString();
                    string newTitle = utils.fileName(fileAction.save(i,data));
                    tabItem.Header = newTitle;
                }
            }
            else
            {
                MessageBox.Show("You don't have any file to save");
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
                fileAction.closeFile(index);
                TextTabs.Items.RemoveAt(index);
            }
            else
            {
                MessageBox.Show("You don't any file to close");
            }
        }



        #endregion

        #region textBox changes
        private void TextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            text = textBox.SelectedText;
            caretPosition = textBox.SelectionStart;
            edit.setSelectionChange(textBox.Text, text, caretPosition);
        }
        public void Content_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tabItem = TextTabs.SelectedItem as TabItem;
            var data = (tabItem.Content as TextBox).Text;
            TextBox textBox = tabItem.Content as TextBox;
            int lenght = data.Length + 1;
            Lenght.Content = "Lenght: " + data.Length.ToString();
            Position.Content = "Position: " + lenght.ToString();
            Lines.Content = "Lines: " + textBox.LineCount.ToString();
            string title = tabItem.Header.ToString();
            if (!title.EndsWith("*"))
            {
                title = title + "*";
                tabItem.Header = title;
            }
            edit.Content = textBox.Text;
        }
        private void SelectAll(object sender, RoutedEventArgs e)
        {
            int index = TextTabs.SelectedIndex;
            TabItem tabItem = TextTabs.Items[index] as TabItem;
            TextBox textBox = tabItem.Content as TextBox;
            textBox.SelectAll();
        }
        public void setTextBoxContent(string text)
        {
            TabItem tabItem = TextTabs.SelectedItem as TabItem;
            TextBox textBox = tabItem.Content as TextBox;
            textBox.Text = text;
        }
        public void setTextBox(TextBox textBoxUpdate)
        {
            TabItem tabItem = TextTabs.SelectedItem as TabItem;
            TextBox textBox = tabItem.Content as TextBox;
            textBox = textBoxUpdate;
            tabItem.Content = textBox;
        }
        #endregion

        #region edit text
        private void Cut(object sender, RoutedEventArgs e)
        {
            int index = TextTabs.SelectedIndex;
            TabItem tabItem = TextTabs.Items[index] as TabItem;
            TextBox textBox = tabItem.Content as TextBox;
            string content = (tabItem.Content as TextBox).Text.ToString();
            textBox.Text = edit.cut();
            tabItem.Content = textBox;
        }
        private void Copy(object sender, RoutedEventArgs e)
        {
            edit.copy();
        }
        private void Delete(object sender, RoutedEventArgs e)
        {
            int index = TextTabs.SelectedIndex;
            TabItem tabItem = TextTabs.Items[index] as TabItem;
            TextBox textBox = tabItem.Content as TextBox;
            string content = (tabItem.Content as TextBox).Text.ToString();
            textBox.Text = edit.delete();
            tabItem.Content = textBox;
        }
        private void Paste(object sender, RoutedEventArgs e)
        {
            int index = TextTabs.SelectedIndex;
            if (index != -1)
            {
                if (edit.paste(caretPosition) != null)
                {
                    TabItem tabItem = TextTabs.Items[index] as TabItem;
                    TextBox textBox = tabItem.Content as TextBox;
                    textBox.Text = edit.paste(caretPosition);
                    tabItem.Content = textBox;
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
        private void Lowercase(object sender, RoutedEventArgs e)
        {
            int index = TextTabs.SelectedIndex;
            if (index != -1)
            {
                TabItem tabItem = TextTabs.Items[index] as TabItem;
                TextBox textBox = tabItem.Content as TextBox;
                textBox.Text = edit.capsLock(caretPosition, 1);
                tabItem.Content = textBox;
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
                textBox.Text = edit.capsLock(caretPosition, 2);
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
                string newContent = null;
                string content = (tabItem.Content as TextBox).Text.ToString();
                int line = textBox.LineCount;
                for (int i = 0; i < line; i++)
                {
                    bool blank = true;
                    string text = textBox.GetLineText(i);

                    for (int j = 0; j < text.Length; j++)
                    {
                        if (text[j] >= 33 && text[j] <= 126)
                        {
                            blank = false;
                        }
                    }
                    if (blank == false)
                        newContent += text;

                }
                textBox.Text = newContent;
                tabItem.Content = textBox;
            }
            else
            {
                MessageBox.Show("You don't any file selected");
            }
        }
        private void readOnly(object sender, RoutedEventArgs e)
        {
            int index = TextTabs.SelectedIndex;
            TabItem tabItem = TextTabs.Items[index] as TabItem;
            TextBox textBox = tabItem.Content as TextBox;
            if (textBox.IsReadOnly == true)
            {
                textBox.IsReadOnly = false;
                FileRead.Content = "Is not readonly!";
            }
            else
            {
                textBox.IsReadOnly = true;
                FileRead.Content = "Is readonly!";
            }
        }
        private void goToLine(object sender, RoutedEventArgs e)
        {
            int index = TextTabs.SelectedIndex;
            TabItem tabItem = TextTabs.Items[index] as TabItem;
            TextBox textBox = tabItem.Content as TextBox;
            Line line = new Line(this, textBox);
            line.Show();
        }
        #endregion

        #region social
        private void Linkedin(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.linkedin.com/in/bolba-mateescu-andrei/");
        }
        private void Facebook(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.facebook.com/andreibolbamateescu");
        }
        private void Instagram(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.instagram.com/andreibolba/");
        }
        private void Author(object sender, RoutedEventArgs e)
        {
            Author author = new Author();
            author.Show();
        }
        #endregion

        #region search
        private void Find(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = TextTabs.SelectedItem as TabItem;
            TextBox textBox = tabItem.Content as TextBox;
            string content = textBox.Text;
            Find find = new Find(content, this, textBox);
            find.Show();
        }

        private void Replace(object sender, RoutedEventArgs e)
        {
            int index = TextTabs.SelectedIndex;
            if (index != -1)
            {
                TabItem tabItem = TextTabs.SelectedItem as TabItem;
                TextBox textBox = tabItem.Content as TextBox;
                string content = textBox.Text;
                Replace replace = new Replace(content, this);
                replace.Show();
            }
        }
        #endregion

    }
}