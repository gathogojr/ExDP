using System;
using System.Collections.Generic;
using System.Text;

namespace ClientNS.Models
{
    [Microsoft.OData.Client.Key("Id")]
    public class Director
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Microsoft.OData.Client.ContainerProperty]
        public IDictionary<string, object> DynamicProperties { get; set; }
    }
}
