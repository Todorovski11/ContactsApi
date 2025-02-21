using ContactsApi.Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IContactRepository : IBaseRepository<Contact>
    {
        Task<List<Contact>> GetContactsAsync(int? countryId, int? companyId, int skip, int take);
    }
}
