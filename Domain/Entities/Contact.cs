using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }

        [Required, MaxLength(100)]
        public string ContactName { get; set; } = string.Empty;

        [ForeignKey("Company")]
        public int CompanyId { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }

        public Company? Company { get; set; }
        public Country? Country { get; set; }
    }
}
