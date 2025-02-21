using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }

        [Required, MaxLength(100)]
        public string CompanyName { get; set; } = string.Empty;

        public List<Contact> Contacts { get; set; } = new();
    }
}
