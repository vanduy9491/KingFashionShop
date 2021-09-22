using Dapper;
using KingFashionShop.Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using KingFashionShop.Domain.Response.Categories;

namespace KingFashionShop.Service.CategoryService
{
    public class CategoryService : BaseService, ICategoryService
    {
        public CategoryService(IConfiguration configuration) : base(configuration)
        {

        }

        public async Task<IEnumerable<CategoryRespone>> Get()
        {
            var categories = await SqlMapper.QueryAsync<CategoryRespone>(
                                 cnn: connection,
                                 sql: "sp_getallCategory",
                                 commandType: CommandType.StoredProcedure);
            return categories;
        }

        public async Task<IEnumerable<CategoryRespone>> GetByParentId(int CatId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@catParentId", CatId);
            var categories = await SqlMapper.QueryAsync<CategoryRespone>(
                                 cnn: connection,
                                 param: parameters,
                                 sql: "sp_getCategoryByParentId",
                                 commandType: CommandType.StoredProcedure);
            return categories;
        }
        public async Task<IEnumerable<CategoryRespone>> GetCategoryById(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", id);  
            var categories = await SqlMapper.QueryAsync<CategoryRespone>(
                                 cnn: connection,
                                 param: parameters,
                                 sql: "sp_getCategorybyId",
                                 commandType: CommandType.StoredProcedure);
            return categories;
        }
        public async Task<CreateCategoryResult> Create(CreateCategory create)
        {
            try
            {
                var foundCategory = await GetCategoryByName(create.Title);

                if (foundCategory == null)
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@title", create.Title);
                    parameters.Add("@metaTitle", create.MetaTitle);
                    parameters.Add("@slug", create.Slug);
                    parameters.Add("@content", create.Content);
                    parameters.Add("@parentId", create.ParentId);
                    parameters.Add("@status", create.Status);
                    var category = await SqlMapper.QueryFirstOrDefaultAsync<Category>(
                                            cnn: connection,
                                            sql: "sp_CreateCategory",
                                            param: parameters,
                                            commandType: CommandType.StoredProcedure
                                        );
                    return new CreateCategoryResult()
                    {
                        IsExitst = false,
                        Category = category
                    };
                }
                return new CreateCategoryResult()
                {
                    Category = foundCategory,
                    IsExitst = true
                };
            }
            catch (Exception ex)
            {
                return new CreateCategoryResult()
                {
                    Category = new Category()
                };
            }
        }

        public async Task<Category> GetCategoryByName(string title, int id = 0)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@title", title);
            parameters.Add("@id", id);
            var category = await SqlMapper.QueryFirstOrDefaultAsync<Category>(
                                cnn: connection,
                                sql: "sp_GetCategoryByName",
                                param: parameters,
                                commandType: CommandType.StoredProcedure);
            return category;
        }

        public async Task<UpdateCategoryResult> Update(UpdateCategory update)
        {
            try
            {
                var foundCategory = await GetCategoryByName(update.Title, update.Id);

                if (foundCategory == null)
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@id", update.Id);
                    parameters.Add("@title", update.Title);
                    parameters.Add("@slug", update.Slug);
                    parameters.Add("@metaTitle", update.MetaTitle);
                    parameters.Add("@content", update.Content);
                    parameters.Add("@status", update.Status);
                    var category = await SqlMapper.QueryFirstOrDefaultAsync<Category>(
                                            cnn: connection,
                                            sql: "sp_UpdateCategory",
                                            param: parameters,
                                            commandType: CommandType.StoredProcedure
                                        );
                    return new UpdateCategoryResult()
                    {
                        IsExist = false,
                        Category = category
                    };
                }
                return new UpdateCategoryResult()
                {
                    Category = foundCategory,
                    IsExist = true
                };
            }
            catch (Exception ex)
            {
                return new UpdateCategoryResult()
                {
                    Category = new Category()
                };
            }
        }

        public async Task<ChangeStatusCategoryResult> ChangeStatus(ChangeStatusCategory changeStatus)
        {
            try
            {
                var foundCategory = await GetByParentId(changeStatus.Id);

                if (foundCategory != null)
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@id", changeStatus.Id);
                    parameters.Add("@status", changeStatus.Status);

                    var categoryId = await SqlMapper.QueryFirstOrDefaultAsync<int>(
                                            cnn: connection,
                                            sql: "sp_ChangeStatusCategory",
                                            param: parameters,
                                            commandType: CommandType.StoredProcedure
                                        );
                    return new ChangeStatusCategoryResult()
                    {
                        Success = categoryId > 0
                    };
                }
                return new ChangeStatusCategoryResult()
                {
                    Success = false
                };
            }
            catch (Exception)
            {
                return new ChangeStatusCategoryResult()
                {
                    Success = false
                };
            }
        }
    }
}
