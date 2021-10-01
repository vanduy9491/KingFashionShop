using KingFashionShop.Domain.Models;
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

        public List<string> Images
        {
            get
            {
                List<string> images = new List<string>();
                if (Photo != null && !String.IsNullOrEmpty(Photo.Trim()))
                {
                    var items = Photo.Split(" ");
                    foreach (var item in items)
                    {
                        if (!string.IsNullOrEmpty(item.Trim()))
                            images.Add(item);
                    }
                   
                }
                return images;
            }
            set
            {
            }
        }

        public string MainPhoto
        {
            get
            {
                if (Photo != null && !String.IsNullOrEmpty(Photo.Trim()))
                {
                    var images = Photo.Split(" ");
                    if (images.Length > 0)
                        return images[1];
                }
                return null;
            }
            set
            {
            }
        }
        public ProductResult()
        {
        }

        public ProductResult(Product product)
        {
            Id = product.Id;
            Title = product.Title;
            MetaTitle = product.MetaTitle;
            Slug = product.Slug;
            Summary = product.Summary;
            Price = product.Price;
            Discount = product.Discount;
            Shop = product.Shop;
            StartsAt = product.StartsAt;
            EndsAt = product.EndsAt;
            Content = product.Content;
            Photo = product.Photo;
        }
     
    }
}
