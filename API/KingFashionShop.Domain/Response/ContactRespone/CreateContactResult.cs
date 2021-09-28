using KingFashionShop.Commons.Messages;
using KingFashionShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KingFashionShop.Domain.Response.ContactRespone
{
    public class CreateContactResult
    {
        public Contact Contact { get; set; }
        public string Message => ResponseMessage.Contact.Create;
    }
}
