using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alquilar.Models
{
    public class SendEmailRequest
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public Dictionary<string, string> Tags { get; set; } = new Dictionary<string, string>();
    }
}
