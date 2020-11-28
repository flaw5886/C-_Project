﻿using Kiosk.mananger;
using Kiosk.repository;
using Kiosk.repositoryImpl;
using Kiosk.util;
using MySql.Data.MySqlClient.Memcached;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Kiosk.auth
{
    /// <summary>
    /// LoginPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly AuthRepository repository;

        public LoginWindow()
        {
            InitializeComponent();
            App.serverManager.ServerConnect();

            if (!App.serverManager.isConnected)
            {
                ToastMessage toast = new ToastMessage();
                toast.ShowNotification("서버 에러", "서버가 연결되어 있지 않습니다");
            }

            repository = new AuthRepositoryImpl();

            if (repository.IsAutoLogin()) {
                repository.SetLogin();
                this.ShowMainWindow();
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            this.SetLogin();
        }

        public void SetLogin()
        {
            if (userId.Text == "2210" && userPw.Text == "123")
            {
                repository.SetLogin();
                if (AutoCheck.IsChecked == true)
                {
                    repository.SetAutoLogin();
                }
                
                this.ShowMainWindow();
            }
            else
            {
                MessageBox.Show("아이디 또는 비밀번호가 틀렸습니다");
            }
        }

        private void ShowMainWindow()
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }
    }
}
