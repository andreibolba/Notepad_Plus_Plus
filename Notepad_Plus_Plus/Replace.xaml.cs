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
    /// Interaction logic for Replace.xaml
    /// </summary>
    public partial class Replace : Window
    {
        private string text;
        public string content
        {
            get { return text; }
            set { text = value; }
        }
        public Replace()
        {
            InitializeComponent();
        }

        public Replace(string content)
        {
            InitializeComponent();
            this.content = content;
        }

        private void ReplaceAll(object sender, RoutedEventArgs e)
        {

        }

        private void ReplaceWord(object sender, RoutedEventArgs e)
        {

        }
    }
}
