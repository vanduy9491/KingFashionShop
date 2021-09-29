using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KingFashion.Models.Users
{
    public class Register
    {
        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Email không phải là một địa chỉ e-mail hợp lệ")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Mật khẩu là bắc buộc")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Xác nhận Mật khẩu là bắt buộc")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Xác nhận mật khẩu không khớp")]
        public string ConfirmPassword { get; set; }
    }
}
