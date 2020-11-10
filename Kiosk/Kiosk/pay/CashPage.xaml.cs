﻿using Kiosk.model;
using Kiosk.remote;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Kiosk.pay
{
    /// <summary>
    /// CashPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CashPage : Page
    {
        public CashPage()
        {
            InitializeComponent();
            barcode_Text.Focus();

            orderTotalPrice();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PayPage());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CompletePage());
        }

        private void UserBarCodeSearch(String barcode)
        {
            UserRemote userRemote = new UserRemote();
            OrderRemote orderRemote = new OrderRemote();
            List<User> users = userRemote.GetAllUser();

            foreach (User item in users)
            {
                if (item.barCode == barcode)
                {
                    orderRemote.SetOrderList(App.selectFoodList, App.tableIdx, App.payType);
                    orderRemote.SetOrderInfo(App.selectFoodList);

                    NavigationService.Navigate(new CompletePage());
                }
                else
                {
                    MessageBox.Show("존재하지 않는 유저입니다");
                }
            }
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            barcode.Content = "인식된 카드번호:" + barcode_Text.Text;

            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                UserBarCodeSearch(barcode_Text.Text);
            }
        }

        private void orderTotalPrice()
        {
            ObservableCollection<Food> foodList = App.selectFoodList;

            int totalPrice = 0;

            foreach (Food item in foodList)
            {
                totalPrice += item.totalPrice;
            }

            order_price.Content = "총 주문 금액 : " + totalPrice;
        }
    }
}
