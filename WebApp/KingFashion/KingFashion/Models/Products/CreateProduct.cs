using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KingFashion.Models.Products
{
    public class CreateProduct
    {
        public int? UserId { get; set; }
        [Required(ErrorMessage = "Tiêu đề là bắt buộc")]
        [Display(Name ="Tiêu Đề")]
        public string Title { get; set; }
        [Display(Name = "Tiêu Đề Meta")]
        public string MetaTitle { get; set; }
        [Required(ErrorMessage = "Slug là bắt buộc")]
        public string Slug { get; set; }
        [Display(Name = "Tóm lượt")]
        public string Summary { get; set; }
        [Required(ErrorMessage = "Type là bắt buộc")]
        public int Type { get; set; }
        [StringLength(maximumLength:10,MinimumLength =0)]
        public string SKU { get; set; }
        [Display(Name = "Giá")]
        public float Price { get; set; }
        [Required(ErrorMessage = "Giảm giá là bắt buộc")]
        [Display(Name ="Giảm giá")]
        public float Discount { get; set; }
        [Required(ErrorMessage = "Số Lượng là bắt buộc")]
        [Display(Name ="Số Lượng")]
        public int Quantity { get; set; }
        [Required]
        [Display(Name ="Có mặt tại cửa hàng")]
        public bool Shop { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? PublishedAt { get; set; }
        public DateTime? StartsAt { get; set; }
        public DateTime? EndsAt { get; set; }
        public string Content { get; set; }
        public int CategoryId { get; set; }
        public List<IFormFile> Photo { get; set; }

    }
}
