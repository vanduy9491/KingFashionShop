using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingFashion.Models.Contacts
{
    public class EmailModel
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string FromEmail = "kingstartloving@gmail.com";
        public string FromPassword = "Loveemyeu2";
        public int ContactId { get; set; }
    }
}
