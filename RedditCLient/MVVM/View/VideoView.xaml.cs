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

namespace RedditCLient.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для VideoView.xaml
    /// </summary>
    public partial class VideoView : Window
    {
        public VideoView()
        {
            InitializeComponent();
        }
        private void Mouse_move(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
