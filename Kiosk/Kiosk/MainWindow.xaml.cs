﻿using System;
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

namespace Kiosk
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetTime();
        }

        private void SetTime()
        {
            Time.Text = DateTime.Now.ToString("yyyy-MM-dd HH:ss");
        }

        private void Order_Btn_Click(object sender, RoutedEventArgs e)
        {
            OrderPage page = new OrderPage();
            this.Content = page;
        }
    }
}
