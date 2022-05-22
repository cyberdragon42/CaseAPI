using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseAPI.Domain.Models
{
    public class Account
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string ContactId { get; set; }
        public Contact Contact { get; set; }

        public ICollection<Incident> Incidents { get; set; }
    }
}
