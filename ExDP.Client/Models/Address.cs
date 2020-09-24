using System;
using System.Collections.Generic;
using System.Text;

namespace ClientNS.Models
{
    public class Address
    {
        public string AddressLine { get; set; }
        public string City { get; set; }
        [Microsoft.OData.Client.ContainerProperty]
        public IDictionary<string, object> DynamicProperties { get; set; }
    }
}
