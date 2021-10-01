using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingFashion.Models.Transaction
{
    public class TransactionResult
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
