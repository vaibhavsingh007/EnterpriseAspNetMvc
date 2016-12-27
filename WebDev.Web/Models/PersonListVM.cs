using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebDev.Models;

namespace WebDev.Web.Models
{
    public class PersonListVM
    {
        public Person Person { get; set; }
        public int AddressCount{ get; set; }

        // Other 'View' related properties
    }
}