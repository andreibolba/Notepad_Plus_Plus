using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Notepad_Plus_Plus
{
    /// <summary>
    /// Interaction logic for Find.xaml
    /// </summary>
    public partial class Find : Window
    {
        private List<int> words;
        private int index;
        private string text;
        private string replacingWord;
        private string replacedWord;
        MainWindow mainWindow;
        TextBox textBox;

        public string content
        {
            get { return text; }
            set { text = value; }
        }

        public Find()
        {
            InitializeComponent();
            words = new List<int>();
            index = 0;
        }

        public Find(string content, MainWindow main,TextBox textBox)
        {
            InitializeComponent();
            this.content = content;
            this.mainWindow = main;
            words = new List<int>();
            index = 0;
            this.textBox = textBox;
        }

        private void FindPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (index == 0)
            {
                MessageBox.Show("Last word found in this file");
            }
            else
            {
                textBox.Select(words[index], WordInput.Text.Length);
                index--;
                mainWindow.setTextBox(textBox);
            }
        }

        private void FindNext_Click(object sender, RoutedEventArgs e)
        {
            if(index==words.Count)
            {
                MessageBox.Show("Last word found in this file");
                index--;
            }
            else {

          
                index++;
                mainWindow.setTextBox(textBox);
            }
        }

        private List<int> getDictionary(string text,string word)
        {
            List<int> list = new List<int>();
            for(int i=0;i<text.Length-word.Length;i++)
            {
                bool equal = true;
                for(int j=0;j<word.Length;j++)
                    if(word[j]!=text[i+j])
                        equal=false;
                if(equal)
                    list.Add(i);
            }
            return list;
        }

        private void FindAll_Click(object sender, RoutedEventArgs e)
        {

        }

        private void WordInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            string wordToFind = WordInput.Text;
            words = getDictionary(text, wordToFind);
        }
    }
}
