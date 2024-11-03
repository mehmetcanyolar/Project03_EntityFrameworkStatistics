using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project03_EntityFrameworkStatistics
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       Db3Project20Entities db =new Db3Project20Entities();

        private void Form1_Load(object sender, EventArgs e)
        {
            // toplam kategori sayısı 

            int categoryCount = db.TblCategories.Count();

            lblCategoryCount.Text = categoryCount.ToString();

            //toplam ürün sayısı

            int productCount = db.TblProducts.Count();

            lblProductCount.Text = productCount.ToString();

            // toplam müşteri sayısı

            int customerCount = db.TblCustomers.Count();
            lblCustomerCount.Text = customerCount.ToString();

            // toplam Sipariş sayısı

            int orderCount = db.TblOrders.Count();
            lblOrderCount.Text = orderCount.ToString();

            //toplam stok sayısı

            var totalProductStockCount = db.TblProducts.Sum(x => x.ProductStock);
            lblProductTotalStock.Text = totalProductStockCount.ToString();

            //Ortalama Ürün Fiyatı

            var averageProductPrice =  db.TblProducts.Average(x => x.ProductPrice);
            lblProductAveragePrice.Text = averageProductPrice.ToString() + " ₺";   

            //Toplam Meyve Stoğu

            var totalProductCountByCategoryIsFruit = db.TblProducts.Where(x=>x.CategoryId== 1).Sum(y=>y.ProductStock);
            lblProductCountByCategoryIsFruit.Text= totalProductCountByCategoryIsFruit.ToString();

            // Gazozun Toplam İşlem Hacmi || stok sayısı ile fiyatının çarpımı

            var totalPriceByProductNameIsGazozGetProductStock = db.TblProducts.Where(x=>x.ProductName== "Gazoz").Select(x=>x.ProductStock).FirstOrDefault();

            var totalPriceByProductNameIsGazozGetProductPrice = db.TblProducts.Where(x=>x.ProductName=="Gazoz").Select(x=>x.ProductPrice).FirstOrDefault();

            var totalPriceByProductNameIsGazoz = totalPriceByProductNameIsGazozGetProductStock * totalPriceByProductNameIsGazozGetProductPrice;

            lblTotalPriceByProductNameIsGazoz.Text = totalPriceByProductNameIsGazoz.ToString() + " ₺";

            // Stok sayısı 100'den az olan ürün sayısı

            var productCountByStockCountSmallerThan100  = db.TblProducts.Where(x => x.ProductStock < 100).Count();
            lblProductStockSmallerThan100.Text = productCountByStockCountSmallerThan100.ToString();

            // KAtegorisi sebze ve durumu aktif(true) olan ürün stok toplamı
            var productStockCountByCategoryNameIsSebzeAndStatusIsTrue = db.TblProducts.Where(x => x.CategoryId == (db.TblCategories.Where(y=>y.CategoryName=="Sebze").Select(z => z.CategoryId).FirstOrDefault()) && x.ProductStatus == true).Sum(t=>t.ProductStock);
            lblProductCountByCategoryNameIsSebzeAndStatusIsTrue.Text = productStockCountByCategoryNameIsSebzeAndStatusIsTrue.ToString();

            // Türkiye'den yapılan siparişler (SQL Query yöntemiyle)
         
           // var customerId = db.TblCustomers.Where(x => x.CustomerCountry == "Türkiye").Select(y => y.CustomerId).ToList();

          //  lblOrderCountFromTurkey.Text = customerId.ToString();

            var sq = db.Database.SqlQuery<int>("Select Count(*) From TblOrder Where CustomerId In (Select CustomerId From TblCustomer Where\r\nCustomerCountry = 'Türkiye')").FirstOrDefault();

            lblOrderCountFromTurkey.Text = sq.ToString();

            //TURKIYE DEN VERILEN SIPARISLER (EF METHODUYLA) 

            var turkishCustomerIds = db.TblCustomers.Where(x=>x.CustomerCountry=="Türkiye").Select(y=>y.CustomerId).ToList();

            var orderCountFromTurkiyeWithEF = db.TblOrders.Count(z => turkishCustomerIds.Contains(z.CustomerId.Value));

            lblOrderCountFromTurkeyByEF.Text=orderCountFromTurkiyeWithEF.ToString();

            //MEYVE SATISLARI KAZANCI

            var orderTotalPriceByCategoryIsMeyve = db.Database.SqlQuery<decimal>("Select Sum(o.TotalPrice) From TblOrder o Join TblProduct p On o.ProductId = p.ProductId Join TblCategory c On p.CategoryId=c.CategoryId Where c.CategoryName='Meyve'").FirstOrDefault();

            lblOrderTotalPriceByCategoryIsMeyve.Text=orderTotalPriceByCategoryIsMeyve.ToString();
        }






       
    }
}
