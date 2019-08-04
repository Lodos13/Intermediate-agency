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

namespace intermediate_agency
{
    /// <summary>
    /// Interaction logic for EditMerchandiseWindow.xaml
    /// </summary>
    public partial class EditMerchandiseWindow : Window
    {
        public EditMerchandiseWindow()
        {
            InitializeComponent();

            MerchNameTextBox.Focus();
        }

        public EditMerchandiseWindow(string name) : this()
        {
            this.MerchNameTextBox.Text = name;
        }

        public string GetMerchaName()
        { return this.MerchNameTextBox.Text; }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
