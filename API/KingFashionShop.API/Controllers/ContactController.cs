using KingFashionShop.Domain.Models;
using KingFashionShop.Domain.Response.ContactRespone;
using KingFashionShop.Service.ContactService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingFashionShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService contactService;
        public ContactController(IContactService contactService)
        {
            this.contactService = contactService;
        }
        [HttpGet]
        public async Task<IEnumerable<Contact>> Get()
        {
            return await contactService.GetContacts();
        }
        [HttpPost]
        public async Task<CreateContactResult> Create(CreateContact contact)
        {
            return await contactService.Create(contact);
        }

        [HttpGet("Get/{id}")]
        public async Task<Contact> GetById(int id)
        {
            return await contactService.GetById(id);
        }
        [HttpPut]
        [Route("ChangStatus/{id}")]
        public async Task<Contact> ChangStatus(int id)
        {
            return await contactService.ChangeStatus(id);
        }

    }
}
