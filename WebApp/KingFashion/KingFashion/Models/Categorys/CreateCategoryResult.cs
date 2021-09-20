using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingFashion.Models.Categorys
{
    public class CreateCategoryResult
    {
        public Category Category { get; set; }
        public bool IsExitst { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
