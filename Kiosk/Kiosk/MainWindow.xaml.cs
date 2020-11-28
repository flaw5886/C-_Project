﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using Kiosk.auth;
using Kiosk.intro;
using Kiosk.repository;
using Kiosk.repositoryImpl;
using Kiosk.util;

namespace Kiosk
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly AuthRepository repository;

        public MainWindow()
        {
            InitializeComponent();
            repository = new AuthRepositoryImpl();
            SetTime();

            StartState();
            StartGetServerMessage();
        }

        private void StartState()
        {
            Thread thread = new Thread(ConnectState);
            thread.IsBackground = true;
            thread.Start();
        }

        private void StartGetServerMessage()
        {
            Thread thread = new Thread(App.serverManager.GetServerMessage);
            thread.IsBackground = true;
            thread.Start();
        }

        private void ConnectState()
        {
            while (true)
            {
                if (App.serverManager.isConnected)
                {
                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                    {
                        ConnectButton.Content = "연결 중";
                        ConnectButton.Background = new SolidColorBrush(Colors.LightGreen);
                    }));
                }
                else
                {
                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                    {
                        ConnectButton.Content = "연결 실패";
                        ConnectButton.Background = new SolidColorBrush(Colors.Red);
                    }));

                    App.serverManager.ServerConnect();
                    if (App.serverManager.isConnected)
                    {
                        repository.SetLogin();
                        StartGetServerMessage();
                    }
                }
            }
        }

        private void SetTime()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Time.Text = DateTime.Now.ToString();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {

            if (App.selectFoodList.Count > 0)
            {
                if (MessageBox.Show("홈으로 이동", "홈으로 이동하실건가요?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    App.selectFoodList.Clear();
                    FrameLayout.NavigationService.Navigate(new IntroPage());
                }
            } else
            {
                FrameLayout.NavigationService.Navigate(new IntroPage());
            }
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            repository.SetLogin();
        }
    }
}
