﻿
using KingFashion.Models.Categorys;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KingFashion.Models.Categorys
{
    public class Category
    { 
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Title { get; set; }
    }
}
