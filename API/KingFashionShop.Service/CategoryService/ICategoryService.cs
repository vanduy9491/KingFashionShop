﻿using KingFashionShop.Domain.Models;
using KingFashionShop.Domain.Response.Categories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace KingFashionShop.Service.CategoryService
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryRespone>> Get();
        Task<IEnumerable<CategoryRespone>> GetByParentId(int CatId);
        Task<CreateCategoryResult> Create(CreateCategory create);
        Task<UpdateCategoryResult> Update(UpdateCategory update);
        Task<ChangeStatusCategoryResult> ChangeStatus(ChangeStatusCategory changeStatus);
        Task<CategoryRespone> GetCategoryById(int id);

    }
}
