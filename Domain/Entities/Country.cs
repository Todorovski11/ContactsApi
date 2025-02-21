using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }

        [Required, MaxLength(100)]
        public string CountryName { get; set; } = string.Empty;

        public List<Contact> Contacts { get; set; } = new();

        public Dictionary<string, int> GetCompanyStatistics()
        {
            return Contacts
                .Where(c => c.Company != null)
                .GroupBy(c => c.Company!.CompanyName) 
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}
