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

        }
    }
}
