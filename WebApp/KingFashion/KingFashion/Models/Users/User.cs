using KingFashion.Models.Orders;
using KingFashion.Models.Transactions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingFashion.Models.Users
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LaststName { get; set; }
        public string Mobile { get; set; }
        public bool Admin { get; set; }
        public bool Vendor { get; set; }
        public DateTime RegisteredAt { get; set; }
        public DateTime LastLogin { get; set; }
        public string Intro { get; set; }
        public string Profile { get; set; }
        public ICollection<Transactions.Transaction> Transaction { get; set; }
        public ICollection<Order> Order { get; set; }
        public ICollection<Models.Carts.Cart> Carts { get; set; }
    }
}
