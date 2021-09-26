
using KingFashion.Models.Categorys;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KingFashion.Models.Products
{
    public class ProductResult
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string MetaTitle { get; set; }
        public string Slug { get; set; }
        public string Summary { get; set; }
        public float Price { get; set; }
        public float Discount { get; set; }
        public bool Shop { get; set; }
        public DateTime StartsAt { get; set; }
        public DateTime EndsAt { get; set; }
        public string Content { get; set; }
        public string CategorySlug { get; set; }
        public string Photo { get; set; }
        public string MainPhoto
        {
            get
            {
                if (!String.IsNullOrEmpty(Photo.Trim()))
                {
                    var images = Photo.Split(" ");
                    if (images.Length > 0)
                        return images[1];
                }
                return null;
            }
        }


    }
}
