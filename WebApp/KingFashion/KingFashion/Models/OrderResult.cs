using System;
using System.Collections.Generic;
using System.Text;

namespace KingFashionShop.Domain.Response
{
   public class OrderResult
    {
        public int id { get; set; }
        public string sessionId { get; set; }
        public float subTotal { get; set; }
        public float itemDiscount { get; set; }
        public float tax { get; set; }
        public float shipping { get; set; }
        public float total { get; set; }
        public string promo { get; set; }
        public float discount { get; set; }
        public float grandTotal { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string line1 { get; set; }
        public string line2 { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public DateTime createdAt { get; set; }
      
    }
}
