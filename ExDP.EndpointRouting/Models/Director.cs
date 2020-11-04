using System.Collections.Generic;

namespace ServiceNS.Models
{
    // Director open entity type
    public class Director
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IDictionary<string, object> DynamicProperties { get; set; }
    }
}
