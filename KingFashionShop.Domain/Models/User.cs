using System;
using System.Collections.Generic;
using System.Text;

namespace KingFashionShop.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LaststName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool Admin { get; set; }
        public bool Vendor { get; set; }
        public DateTime RegisteredAt { get; set; }
        public DateTime LastLogin { get; set; }
        public string Intro { get; set; }
        public string Profile { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<Cart> Carts { get; set; }




    }
}
