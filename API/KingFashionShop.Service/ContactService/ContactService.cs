using Dapper;
using KingFashionShop.Domain.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KingFashionShop.Domain.Response.ContactRespone;

namespace KingFashionShop.Service.ContactService
{
    public class ContactService : BaseService, IContactService
    {
        public ContactService(IConfiguration configuration) : base(configuration)
        {

        }

        public async Task<Contact> ChangeStatus(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", id);
            var contact = await SqlMapper.QueryFirstOrDefaultAsync<Contact>(
                cnn: connection, sql: "sp_ChangeContactStatus", param: parameters, commandType: CommandType.StoredProcedure
                );
            return contact;
            
        }

        public async  Task<CreateContactResult> Create(CreateContact createContact)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@email", createContact.Email);
            parameters.Add("@content", createContact.Content);
            var contact = await SqlMapper.QueryFirstOrDefaultAsync<Contact>(
                                    cnn: connection,
                                    sql: "sp_CreateContact",
                                    param: parameters,
                                    commandType: CommandType.StoredProcedure
                                );
            return new CreateContactResult()
            {
                Contact = contact
            };
        }

        public async Task<Contact> GetById(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", id);
            var contact = await SqlMapper.QueryFirstOrDefaultAsync<Contact>(
                cnn: connection, sql: "sp_GetContactById", param:parameters, commandType: CommandType.StoredProcedure
                );
            return contact;
        }

        public async Task<IEnumerable<Contact>> GetContacts()
        {
            var contact = await SqlMapper.QueryAsync<Contact>(
                cnn: connection, sql: "sp_GetAllContact", commandType: CommandType.StoredProcedure
                );
            return contact;
        }
    }
}
