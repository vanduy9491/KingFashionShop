using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingFashion.Models.Categorys
{
    public class UpdateCategoryResult
    {
        public Category Category { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
