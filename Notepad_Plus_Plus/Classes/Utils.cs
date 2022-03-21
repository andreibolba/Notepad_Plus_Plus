using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Notepad_Plus_Plus.Classes
{
    internal class Utils
    {
        #region variables and constructor

        public Utils()
        {
        }
        #endregion

        public  string fileName(string name)
        {
            string newName = null;
            int c = name.LastIndexOf('\\');
            name = name.Remove(0, c + 1);

            return name;
        }
    }
}
