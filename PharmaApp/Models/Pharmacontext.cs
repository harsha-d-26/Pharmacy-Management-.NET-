using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PharmaApp.Models
{
    public class Users
    {
        [Key]
        public int user_id { get; set; }
        [Display(Name = "Name")]
        public String user_name { get; set; }

        public String password { get; set; }

    }
    public class Medicines
    {
        [Key]
        public int med_id { get; set; }
        [Display(Name = "Medicine Name")]
        public String med_name { get; set; }
        [Display(Name = "Category")]
        public String category { get; set; }
        [Display(Name = "Price")]
        public double price { get; set; }
        [Display(Name = "Quantity")]
        public int quantity { get; set; }
        [Display(Name = "Rack")]
        public int rack { get; set; }
        [Display(Name = "Manufactured Date")]
        public DateTime mfg_date { get; set; }
        [Display(Name = "Expiry Date")]
        public DateTime exp_date { get; set; }
    }
    public class Supplier
    {
        [Key]
        public int sup_id { get; set; }
        [Display(Name = "Supplier Name")]
        public String sup_name { get; set; }
        [Display(Name = "Phone No.")]
        public String phone_no { get; set; }
        [Display(Name = "Address")]
        public String address { get; set; }

        [Display(Name = "Email")]
        public String email { get; set; }
        
    }
    public class Sales
    {
        [Key]
        public int sale_id { get; set; }

        [ForeignKey("FK1")]
        [Display(Name = "Medicine Name")]
        public int med_id { get; set; }
        public Medicines FK1 { get; set; }

        [Display(Name = "Date")]
        public DateTime date_time { get; set; }
        [Display(Name = "Total")]
        public double total_amt { get; set; }
        //[ForeignKey("FK0")]
        //[Display(Name = "Customer id")]
        //public int c_id { get; set; }
        //public Customer FK0 { get; set; }
        
        [ForeignKey("FK2")]
        [Display(Name = "Sold By")]
        public int user_id { get; set; }
        public Users FK2 { get; set; }
        
    }
    public class Sales_items
    {
        [ForeignKey("FK5")]
        public int med_id { get; set; }
        [Display(Name = "Medicine Id")]
        public Medicines FK5 { get; set; }
        [ForeignKey("FK6")]
        [Display(Name = "Medicine Name")]
        public int sale_id { get; set; }        
        public Sales FK6 { get; set; }
        [Display(Name = "Sale Id")]

        public int sale_qty { get; set; }
        [Display(Name = "Sale Quantity")]

        
        public int tot_price { get; set; }
        

    }
    public class Purchase
    {
        [Key]
        public int purchase_id { get; set; }
        //[Display(Name = "Purchase Id")]
        
        [ForeignKey("FK3")]        
        public int med_id { get; set; }
        [Display(Name = "Medicine Id")]
        public Medicines FK3 { get; set; }

        
        [ForeignKey("FK4")]
        public int sup_id { get; set; }
        [Display(Name = "Supplier Id")]

        public Supplier FK4 { get; set; }

        [Display(Name = "Purchase Quantity")]
        public int purchase_qty { get; set; }
        [Display(Name = "Purchase Amount")]
        public double purchase_amt { get; set; }
        [Display(Name = "Purchase Date")]
        public DateTime purchase_date { get; set; }
        

        
        
        
        
    }

    //public class Customer
    //{
    //    [Key]
    //    public int c_id { get; set; }
    //    public String c_name { get; set; }
    //    public int c_age { get; set; }
    //    public String c_sex { get; set; }
    //    public String c_phno { get; set; }

    //}
    public class PharmaContext: DbContext
    {
        public PharmaContext(DbContextOptions<PharmaContext> options) : base(options)
        {

        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Medicines> Medicines { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<Purchase> Purchase { get; set; }
    }
}
