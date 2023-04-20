using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class About
    {
        public int Id { get; set; }
        public string Heading { get; set; }
        public string Content { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string? Address { get; set; }
    }
}
