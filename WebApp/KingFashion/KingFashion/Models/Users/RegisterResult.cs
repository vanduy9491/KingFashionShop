using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingFashion.Models.Users
{
    public class RegisterResult
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public bool Success => !string.IsNullOrEmpty(Id);
        public string Message { get; set; }
        public RegisterResult()
        {
            Id = string.Empty;
            Email = string.Empty;
            Message = "";
        }
    }
}
