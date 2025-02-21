using ContactsApi.Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICountryRepository : IBaseRepository<Country>
    {
        Task<Dictionary<string, int>> GetCompanyStatisticsByCountryId(int countryId);
    }
}
