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
        private string text;
        public string content
        {
            get { return text; }
            set { text = value; }
        }

        MainWindow mainWindow;

        private string replacingWord;
        private string replacedWord;
        public Find()
        {
            InitializeComponent();
            words = new List<int>();
        }

        public Find(string content, MainWindow main)
        {
            InitializeComponent();
            this.content = content;
            this.mainWindow = main;
            words = new List<int>();
        }

        private void FindPrevious_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FindNext_Click(object sender, RoutedEventArgs e)
        {

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
            string wordToFind = WordInput.Text;
            words=getDictionary(text,wordToFind);
            MessageBox.Show(words.Count.ToString());

        }
    }
}
