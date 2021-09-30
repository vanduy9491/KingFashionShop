using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingFashion.Models.Users
{
    public class ChangeIsDeletedUserResult
    {
        public ViewUsers User { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
