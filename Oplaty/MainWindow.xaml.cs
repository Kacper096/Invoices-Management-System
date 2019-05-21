using Oplaty.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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

namespace Oplaty
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainViewModel _account;
        public MainWindow()
        {
            InitializeComponent();
            Application.Current.MainWindow = this;
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }
        public MainWindow(int accountID)
        {
            _account = new MainViewModel(accountID);
            InitializeComponent(); 
            this.DataContext = _account;
            Application.Current.MainWindow = this;
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }
             
        //Close the app
        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // You can moves the window.
        private void ColorZone_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        /// <summary>
        /// It minimalizes window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Minimalize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Minimized;
            } 
        }

        /// <summary>
        /// It switchs the theme.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Light_Click(object sender, RoutedEventArgs e)
        {
            var _dictionarySource = Application.Current.Resources.MergedDictionaries.Select(x => x.Source).FirstOrDefault();

            if (_dictionarySource.ToString().Contains("Dark.xaml"))
            {
                //It's renames source to app.xaml
                Uri _newSource = new Uri(_dictionarySource.ToString().Replace("Dark.xaml", "Light.xaml"));
                Application.Current.Resources.MergedDictionaries.FirstOrDefault().Source = _newSource;
            }
            else
            {
                Uri _newSource = new Uri(_dictionarySource.ToString().Replace("Light.xaml", "Dark.xaml"));
                Application.Current.Resources.MergedDictionaries.FirstOrDefault().Source = _newSource;
            }

        }

        /// <summary>
        /// It maximizes and minimizes window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
                Maximize.Kind = MaterialDesignThemes.Wpf.PackIconKind.ArrowCollapseAll;
            }
            else
            {
                this.WindowState = WindowState.Normal;
                Maximize.Kind = MaterialDesignThemes.Wpf.PackIconKind.ArrowExpandAll;
            }
        }

        /// <summary>
        /// The user logouts from app.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            var _loginWin = new LoginWindow();
            Application.Current.MainWindow = _loginWin;
            this.Close();
            _loginWin.Show();
        }
    }
}
