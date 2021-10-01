using System;
using System.Collections.Generic;
using System.Text;

namespace KingFashionShop.Domain.Response.ContactRespone
{
    public class CreateContact
    {
        public string Email { get; set; }
        public string Content { get; set; }
        public bool Status { get; set; }
    }
}
