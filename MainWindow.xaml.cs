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
            using (var db = new gadgetShopEntities())
            {
                var list = db.Product.ToList();

                //dataGrid.ItemsSource = list.Select(x => new { x.model, x.type, x.maker }).ToList();
                dataGrid.ItemsSource = from item in list
                                       select new { item.model, item.maker, item.type };

            }

        }
        private void ViewPrinter(object sender, RoutedEventArgs e)
        {
            using (var db = new gadgetShopEntities())
            {
                // var existingModel = dbContext.Product.Select(x=>x.model).FirstOrDefault();

                var list = db.Printer.ToList();

                dataGrid.ItemsSource = list.Select(x => new { x.code, x.model, x.color, x.price }).ToList();
            }


        }

        private void AddNewProduct(object sender, RoutedEventArgs e)
        {
            using (var db = new gadgetShopEntities())
            {
                var newProduct = new Product()
                {
                    maker = "t",
                    type = "telephone",
                    model = random.Next(1000, 6000).ToString()
                };

                db.Product.Add(newProduct);
                db.SaveChanges();
            }
        }

        private void ChangeTypeProduct(object sender, RoutedEventArgs e)
        {
            using (var db = new gadgetShopEntities())
            {

                var telephone = db.Product.SingleOrDefault(x => x.type == "telephone");
                telephone.type = "smartphone";
                db.SaveChanges();
            }
        }

        private void LeftJoinProductPrinter(object sender, RoutedEventArgs e)
        {
            using (var db = new gadgetShopEntities())
            {

                var list = db.Product.ToList();

                //dataGrid.ItemsSource = list.Join(pri)

                var testObject = from prod in db.Product        //left join
                                 join print in db.Printer
                                 on prod.model equals print.model into modelGroup
                                 from left in modelGroup.DefaultIfEmpty()
                                 select new
                                 {
                                     model = prod.model,
                                     maker = prod.maker,
                                     type = prod.type,
                                     typeProduct = left.model
                                 };

                dataGrid.ItemsSource = testObject.ToList();


            }

        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            using (var db = new gadgetShopEntities())
            {

                var deletePrinter = db.Product.FirstOrDefault(x => x.type == "Printer");
                if (deletePrinter != null)
                {
                    var deletefromPrinter = db.Printer.Where(x => x.model == deletePrinter.model).ToList();
                    db.Printer.RemoveRange(deletefromPrinter);
                    db.Product.Remove(deletePrinter);
                    db.SaveChanges();
                }

            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new gadgetShopEntities())
            {
                var product = db.Product.FirstOrDefault(p => p.model == "1434");
                if (product != null)
                {
                    var newPrinter = new Printer()
                    {
                        color = "red",
                        price = 100,
                        type = "modern",
                        model = "1434"
                    };

                    db.Printer.Add(newPrinter);
                    db.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Unable to save file, try again.");
                }




            }

        }
    }
}
