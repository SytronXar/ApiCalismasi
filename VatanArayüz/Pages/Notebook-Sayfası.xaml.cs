﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using VatanArayüz.UserControls2;

namespace VatanArayüz
{
    /// <summary>
    /// Notebook_Sayfası.xaml etkileşim mantığı
    /// </summary>
    public partial class Notebook_Sayfası : Page
    {
        string FilterMarkaCurrent = "", FilterFiyatCurrent = "";
        string min = "";
        string max = "";
        public Notebook_Sayfası()
        {
            InitializeComponent();
            foreach (var item in Enum.GetValues(typeof(EMarka)).Cast<EMarka>())
            {
                RadioButton RB = new RadioButton();
                RB.Tag = item;
                RB.Content = item.ToString();
                RB.Click += MarkaFiltresi_Click;
                Thickness thickness = new Thickness(5, 0, 5, 0);
                RB.Margin = thickness;
                markafiltrePaneli.Children.Add(RB);
            }
            AddProductButtons();
        }
        void AddProductButtons()
        {

            if (FilterFiyatCurrent != "")
            {
                min = FilterFiyatCurrent.Substring(0, FilterFiyatCurrent.IndexOf('-'));
                max = FilterFiyatCurrent.Substring(0, FilterFiyatCurrent.IndexOf('-') + 1);

            }
            pGrid.Children.Clear();
            var window = (MainWindow)Application.Current.MainWindow;
            Numberofproducts.Text = window.Products.Count.ToString();
            foreach (var item in window.Products)
            {
                ProductButton pb = new ProductButton();
                ProductButton2 productButtons2 = new ProductButton2();
                ProductButton3 productButtons3 = new ProductButton3();
                pb.productName.Text = item.Name;
                if (item.Cost < item.PreviousCost)
                {
                    pb.ProductPreviousCost.Text = Convert.ToDecimal(item.PreviousCost).ToString();
                }
                else pb.ProductPreviousCost.Text = "";
                if (item.KargoFiyatı != 0)
                {
                    pb.KargoDurumuBelirteci.Visibility = Visibility.Hidden;
                }
                pb.productInfo.Text = item.Info;
                if (item.NumberInStock <= 20)
                {
                    pb.productCount.Text = "Son " + item.NumberInStock.ToString() + " Ürün";
                }
                else pb.OzelUrunBelirteci.Visibility = Visibility.Hidden;
                pb.productCost.Text = Convert.ToDecimal(item.Cost).ToString();
                pb.productImage.Source = new BitmapImage(new Uri(item.ImageUrl));
                pb.ProductButton1.Tag = item.Id;
                pb.ProductButton1.Click += Product_Click;
                if (FilterMarkaCurrent != "")
                {
                    if (FilterFiyatCurrent != "")
                    {
                        if (item.Marka.ToString() == FilterMarkaCurrent && item.Cost <= Convert.ToInt32(min))
                        {
                            pGrid.Children.Add(pb);
                        }
                    }
                    else
                    {
                        if (FilterFiyatCurrent != "")
                        {
                            if (item.Marka.ToString() == FilterMarkaCurrent)
                            {
                                pGrid.Children.Add(pb);
                            }
                        }
                    }
                }
                else
                {
                    if (FilterFiyatCurrent != "")
                    {
                        if (item.Cost <= Convert.ToInt32(min))
                        {
                            pGrid.Children.Add(pb);
                        }
                    }
                    else
                    {
                        pGrid.Children.Add(pb);
                    }
                }
            }
        }
        private void Product_Click(object sender, RoutedEventArgs e)
        {
            int PId = Convert.ToInt32((sender as Button).Tag);
            var window = (MainWindow)Application.Current.MainWindow;
            ÜrünSayfası ürünSayfası = new ÜrünSayfası();
            foreach (var item in window.Products)
            {
                if (PId == item.Id)
                {
                    ürünSayfası.productInfo.Text = item.Info;
                    ürünSayfası.productName.Content = item.Name;
                    ürünSayfası.swiperImage.Source = new BitmapImage(new Uri(item.ImageUrl));
                    ürünSayfası.productCost.Content = item.Cost.ToString();
                    ürünSayfası.MinTaksitTutarı.Text = (item.Cost / 12).ToString("C") + "TL";
                    ürünSayfası.productSayfaIsim.Content = item.Name;
                    break;
                }
            }


            window.Main.Content = ürünSayfası;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ÜrünSayfası ürünSayfası = new ÜrünSayfası();
            var window = (MainWindow)Application.Current.MainWindow;
            window.Main.Content = ürünSayfası;
        }

        private void MarkaFiltresi_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as RadioButton).IsChecked == true)
            {
                FilterMarkaCurrent = (sender as RadioButton).Content.ToString();
                AddProductButtons();
            }
        }
        private void FiyatFiltresi_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as RadioButton).IsChecked == true)
            {
                FilterFiyatCurrent = (sender as RadioButton).Content.ToString();
                AddProductButtons();
            }
        }
    }
}
