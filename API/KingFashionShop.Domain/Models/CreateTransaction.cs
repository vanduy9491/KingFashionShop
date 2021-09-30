using System;
using System.Collections.Generic;
using System.Text;

namespace KingFashionShop.Domain.Models
{
    public class CreateTransaction
    {
        public int UserId { get; set; }
        public int OrderId { get; set; }
        public string Code { get; set; }
        public int Type { get; set; }
        public int Mode { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Content { get; set; }
    }
}
