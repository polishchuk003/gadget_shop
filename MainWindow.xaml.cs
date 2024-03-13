using gadget_shop.data;
using Microsoft.SqlServer.Server;
using System;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Media.Media3D;

namespace gadget_shop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Random random = new Random();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ViewProduct(object sender, RoutedEventArgs e)
        {
            using (gadgetShopEntities dbContext = new gadgetShopEntities())
            {
 
                dbContext.Product.Add(new Product { maker = "a", model = Convert.ToString(random.Next(1000,9999)), type = "laptop" });
                dbContext.SaveChanges();

                var list = dbContext.Product.ToList();

                //dataGrid.ItemsSource = list.Select(x => new { x.model, x.type, x.maker }).ToList();
                dataGrid.ItemsSource = from item in list
                                       select item;

            }

        }
        private void ViewPrinter(object sender, RoutedEventArgs e)
        {
            using (gadgetShopEntities dbContext = new gadgetShopEntities())
            {
                // var existingModel = dbContext.Product.Select(x=>x.model).FirstOrDefault();

                var list = dbContext.Printer.ToList();

                dataGrid.ItemsSource = list.Select(x => new { x.code, x.price, x.color, x.model }).ToList();
            }


        }

        private void ViewPC(object sender, RoutedEventArgs e)
        {
            using (gadgetShopEntities dbContext = new gadgetShopEntities())
            {
                var list = dbContext.PC.ToList();

                dataGrid.ItemsSource = list.Select(x => new { x.code, x.hd, x.cd, x.speed, x.ram, x.price }).ToList();
            }
        }

        private void ViewLaptop(object sender, RoutedEventArgs e)
        {
            using (gadgetShopEntities dbContext = new gadgetShopEntities())
            {

                var list = dbContext.Laptop.ToList();

                dataGrid.ItemsSource = list.Select(x => new { x.code, x.hd, x.ram, x.screen, x.price }).ToList();

            }
        }


    }
}
