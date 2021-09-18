﻿using System;
using System.Collections.Generic;
using System.Text;

namespace KingFashionShop.Commons.Messages
{
    public static class ResponseMessage
    {
        public static string Fail = "Something went wrong, please try again.";
        public static class Category
        {
            public static string Create = "Category has been created successful!";
            public static string Update = "Category has been updated successful!";
            public static string Delete = "Category has been deleted successful!";
            public static string Exits = "Category has been existed";
            public static string NotFound = "Category has been not found!";
            public static string ChangeStatus = "Category has been change status successful!";
        }
    }
}
