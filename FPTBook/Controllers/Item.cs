using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FPTBook.Models;
namespace FPTBook.Controllers
{
    public class Item
    {
        private Book p = new Book(); 
        private int quantity;
        public Book P
        {
            get { return p; }
            set { p = value; }
        }
        public int Quantity
        {
            get { return quantity; }    
            set { quantity = value; }
        }
        public Item() {
        }

        public Item(Book p, int quantity)
        {
            this.p = p;
            this.quantity = quantity;   
        }
    }
}