using KingFashion.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KingFashion.Models.Carts
{
    public class Cart
    {
        public int Id { get; set; }
        [ForeignKey("UsersRefId")]
        public int UserId { get; set; }
        public string SessionId { get; set; }
        public string Token { get; set; }
        public CartStatus Status { get; set; }
        public string FirstName { get; set; } 
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Content { get; set; }
        public User User { get; set; }
    }
}
