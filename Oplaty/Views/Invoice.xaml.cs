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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewModel;

namespace Oplaty.Views
{
    /// <summary>
    /// Interaction logic for Faktury.xaml
    /// </summary>
    public partial class Faktury : UserControl
    {
        public Faktury()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Shows the right content to pay invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pay_Click(object sender, RoutedEventArgs e)
        {
            PayContent.Visibility = PayContent.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
