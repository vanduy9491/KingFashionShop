using System;
using System.Collections.Generic;
using System.Text;

namespace KingFashionShop.Commons.Messages
{
    public static class ResponseMessage
    {
        public static string Fail = "Đã xảy ra lỗi. Vui lòng thử lại";
        public static class Category
        {
            public static string Create = "Danh mục đã được tạo thành công!";
            public static string Update = "Danh mục đã được cập nhật thành công!";
            public static string Delete = "Danh mục đã được xóa thành công!";
            public static string Exits = "Danh mục đã tồn tại!";
            public static string NotFound = "Danh mục đã không được tìm thấy!";
            public static string ChangeStatus = "Danh mục đã được thay đổi trạng thái thành công!";
        }
        public static class Product
        {
            public static string Create = "Sản phẩm đã được tạo thành công!";
            public static string Update = "Sản phẩm đã được cập nhật thành công!";
            public static string Delete = "Sản phẩm đã được xóa thành công!";
            public static string Exits = "Sản phẩm đã tồn tại!";
            public static string NotFound = "Sản phẩm đã không được tìm thấy!";
            public static string ChangeStatus = "Sản phẩm đã được thay đổi trạng thái thành công!";
        }
        public static class Contact
        {
            public static string Create = "Email đã gửi thành công";
        }
    }
}
