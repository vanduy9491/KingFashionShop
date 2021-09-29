using KingFashionShop.Domain.Models;
using KingFashionShop.Domain.Response.ContactRespone;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KingFashionShop.Service.ContactService
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetContacts();
        Task<CreateContactResult> Create(CreateContact createContact);
        Task<Contact> GetById(int id);
        Task<Contact> ChangeStatus(int id);
    }
}
