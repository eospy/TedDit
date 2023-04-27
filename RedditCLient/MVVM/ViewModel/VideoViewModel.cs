using RedditCLient.Core;
using RedditCLient.MVVM.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RedditCLient.MVVM.ViewModel
{
    public class VideoViewModel:ViewModelBase
    {
        public RelayCommand MinimizeButtonCommand { get; set; }
        public RelayCommand MaximixeButtonCommand { get; set; }
        public RelayCommand CloseWindowCommand { get; set; }
        public string Videouri { get; set; } = "https://v.redd.it/8i2ek2uan9wa1/DASH_1080.mp4?source=fallback";
        public VideoViewModel()
        {
            //CloseWindowCommand = new RelayCommand(o => Application.Current.Shutdown());
            MinimizeButtonCommand = new RelayCommand(o => MinimizeWindow());
            MaximixeButtonCommand = new RelayCommand(o => MaximizeWindow());
        }

        static void MinimizeWindow()
        {
            //Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }
        static void MaximizeWindow()
        {
            //if (Application.Current.MainWindow.WindowState != WindowState.Maximized)
              //  Application.Current.MainWindow.WindowState = WindowState.Maximized;
            //else Application.Current.MainWindow.WindowState = WindowState.Normal;
        }
    }
}
