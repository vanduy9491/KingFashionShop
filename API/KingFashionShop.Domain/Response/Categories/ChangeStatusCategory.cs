using System;
using System.Collections.Generic;
using System.Text;

namespace KingFashionShop.Domain.Response.Categories
{
    public class ChangeStatusCategory
    {
        public int ParentId { get; set; }
        public bool Status { get; set; }
    }
}
