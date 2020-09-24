using ClientNS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using ClientNS.Data;

namespace ClientNS
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceUri = new Uri("http://localhost:8302/odata");
            var dataServiceContext = new ExtendedDataServiceContext(serviceUri);

            dataServiceContext.ResolveType = name => {
                switch(name)
                {
                    case "ServiceNS.Models.Genre":
                        return typeof(Genre);
                    case "ServiceNS.Models.Address":
                        return typeof(Address);
                    case "ServiceNS.Models.NextOfKin":
                        return typeof(NextOfKin);
                    case "ServiceNS.Models.Director":
                        return typeof(Director);
                    default:
                        return null;
                }
            };

            dataServiceContext.ResolveName = type =>
            {
                // Lazy approach
                return string.Format("{0}.{1}", "ServiceNS.Models", type.Name);
            };

            // Query for entity by key
            var query = dataServiceContext.CreateQuery<Director>("Directors(1)");
            var queryResult = query.ExecuteAsync().Result;
            var director = queryResult.FirstOrDefault();

            var director2 = new Director { Id = 2, Name = "Director 2 " };

            director2.DynamicProperties = new Dictionary<string, object>
            {
                { "Title", "Prof." },
                { "YearOfBirth", 1971 },
                { "Salary", 800000m },
                { "BigInt", 7454777478706L },
                { "EulerConst", 0.5772156649d },
                { "NickNames", new List<string> { "N3" } },
                { "FavoriteGenre", Genre.Thriller },
                { "Genres", new List<Genre> { Genre.SciFi } },
                { "WorkAddress", new Address { AddressLine = "AL5", City = "C5" } },
                { 
                    "Addresses", 
                    new List<Address>
                    {
                        new Address { AddressLine = "AL6", City = "C6" },
                        new Address { AddressLine = "AL7", City = "C7" }
                    }
                },
                { 
                    "NextOfKin",
                    new NextOfKin
                    {
                        Name = "Nok 2",
                        HomeAddress = new Address
                        {
                            AddressLine = "AL7",
                            City = "C7",
                            DynamicProperties = new Dictionary<string, object> {
                                { "PostalCode", "PC7" }
                            }
                        }
                    }
                }
            };

            dataServiceContext.AddObject("Directors", director2);
            dataServiceContext.SaveChangesAsync().Wait();
        }
    }
}
