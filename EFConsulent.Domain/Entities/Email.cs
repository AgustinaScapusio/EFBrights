using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFConsulent.Domain.Entities
{
    public class Email
    {
        public int Id { get; set; }

        public string EmailAddress { get; set; } = string.Empty;

        public int ConsulentID { get; set; }

        public Consulent? Consulent { get; set; }
    }
}
