using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingFashion.Models.Contacts
{
    public class Contact
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public bool Status { get; set; }
    }
}
