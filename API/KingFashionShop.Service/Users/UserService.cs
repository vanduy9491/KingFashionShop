using Dapper;
using KingFashionShop.Domain.Response.Users;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace KingFashionShop.Service.Users
{
    public class UserService : BaseService,IUserService
    {
        public UserService(IConfiguration configunation) : base(configunation){

        }

        public async Task<ChangeIsDeletedUserResult> ChangeIsDeleted(ChangeIsDeletedUser changeIsDeleted)
        {
            try
            {
                var foundUser = await GetByUserId(changeIsDeleted.Id);
                if (foundUser != null)
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@id", changeIsDeleted.Id);
                    parameters.Add("@isDeleted", changeIsDeleted.IsDeleted);

                    var UserId = await SqlMapper.QueryFirstOrDefaultAsync<string>(
                                            cnn: connection,
                                            sql: "sp_ChangeIsDeletedUser",
                                            param: parameters,
                                            commandType: CommandType.StoredProcedure
                                        );
                    return new ChangeIsDeletedUserResult()
                    {
                        Success = true
                    };
                }
                return new ChangeIsDeletedUserResult()
                {
                    Success = false
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ChangeIsDeletedUserResult()
                {
                    Success = false
                };
            }
        }

        public async Task<IEnumerable<UsersRespone>> getallUserBand()
        {
            var users = await SqlMapper.QueryAsync<UsersRespone>(
                cnn: connection, sql: "sp_GetUserBand", commandType: CommandType.StoredProcedure);
            return users;
        }

        public async Task<IEnumerable<UsersRespone>> getallUserNoBand()
        {
            var users = await SqlMapper.QueryAsync<UsersRespone>(
                cnn: connection, sql: "sp_GetUserNoBand", commandType: CommandType.StoredProcedure);
            return users;
        }

        public async Task<UsersRespone> GetByUserId(string userId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", userId);
            var users = await SqlMapper.QueryFirstOrDefaultAsync<UsersRespone>(
                                 cnn: connection,
                                 param: parameters,
                                 sql: "sp_GetUserById",
                                 commandType: CommandType.StoredProcedure);
            return users;
        }
    }
}
