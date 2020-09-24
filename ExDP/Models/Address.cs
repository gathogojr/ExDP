using System.Collections.Generic;

namespace ServiceNS.Models
{
    // Address open complex type
    public class Address
    {
        public string AddressLine { get; set; }
        public string City { get; set; }
        public IDictionary<string, object> DynamicProperties { get; set; }
    }
}
