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

namespace Oplaty.Controls
{
    /// <summary>
    /// Interaction logic for ButtImage.xaml
    /// </summary>
    public partial class ButtImage : UserControl
    {
        public ButtImage()
        {
            InitializeComponent();
        }

        //Text Property
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(ButtImage), new FrameworkPropertyMetadata(string.Empty));

        public string Text
        {
            get
            {
                return GetValue(TextProperty).ToString();
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }

        //Image Property
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(ImageSource),
                typeof(ButtImage), new FrameworkPropertyMetadata(null));

        public ImageSource ImageSource
        {
            get
            {
                return GetValue(ImageSourceProperty) as ImageSource;
            }
            set
            {
                SetValue(ImageSourceProperty, value);
            }
        }

        //Click Event
        public static readonly RoutedEvent ClickEvent =
            EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble,
                typeof(RoutedEventHandler), typeof(ButtImage));

        public event RoutedEventHandler Click
        {
            add
            {
                AddHandler(ClickEvent, value);
            }
            remove
            {
                RemoveHandler(ClickEvent, value);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(ClickEvent));
        }
    }
}
