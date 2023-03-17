﻿using RedditCLient.MVVM.ViewModel;
using System;
using System.Windows;
using System.Windows.Input;

namespace RedditCLient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var mainwindowmodel = new MainWindowModel();
            DataContext= mainwindowmodel;
            InitializeComponent();
        }
        private void Mouse_move(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
