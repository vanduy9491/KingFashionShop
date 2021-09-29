using System;
using System.Collections.Generic;
using System.Text;

namespace KingFashionShop.Domain.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public bool Status { get; set; }
    }
}
