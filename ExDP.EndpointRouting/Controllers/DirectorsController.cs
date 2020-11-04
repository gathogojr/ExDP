using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using ServiceNS.Models;

namespace ServiceNS.Controllers
{
    public class DirectorsController : ODataController
    {
        private static readonly List<Director> _directors = new List<Director>
        {
            new Director
            {
                Id = 1,
                Name = "Director 1",
                DynamicProperties = new Dictionary<string, object>
                {
                    { "Title", "Dr." }, // Primitive - string
                    { "YearOfBirth", 1970 }, // Primitive - integer
                    { "Salary", 700000m }, // Primitive - decimal
                    { "BigInt", 6078747774547L }, // Primitive - long integer
                    { "Pi", 3.14159265359d }, // Primitive - double
                    // Primitive collection
                    { "NickNames", new List<string> { "N1", "N2" } },
                    { "FavoriteGenre", Genre.SciFi }, // Enum
                    // Enum collection
                    { "Genres", new List<Genre>{ Genre.Epic, Genre.Thriller } },
                    { "WorkAddress", new Address // Complex
                        {
                            AddressLine = "AL1",
                            City = "C1",
                            DynamicProperties = new Dictionary<string, object>
                            {
                                { "PostalCode", "DPC01" }
                            }
                        }
                    },
                    { "Addresses", new List<Address> // Complex collection
                        {
                            new Address { AddressLine = "AL2", City = "C2" },
                            new Address { AddressLine = "AL3", City = "C3" }
                        }
                    },
                    { "NextOfKin", new NextOfKin // Complex with nested complex
                        {
                            Name = "Nok 1",
                            HomeAddress = new Address { AddressLine = "AL4", City = "C4" }
                        }
                    }
                }
            }
        };

        [EnableQuery]
        public IQueryable<Director> Get()
        {
            return _directors.AsQueryable();
        }

        [EnableQuery]
        public SingleResult<Director> Get([FromODataUri] int key)
        {
            var director = _directors.SingleOrDefault(d => d.Id.Equals(key));

            return SingleResult.Create(new[] { director }.AsQueryable());
        }

        public async Task<IActionResult> Post([FromBody] Director director)
        {
            var directorItem = _directors.SingleOrDefault(d => d.Id.Equals(director.Id));

            await Task.Run(() => { /* Persist entity async */ });

            return Created(new Uri(
                $"{Request.Scheme}://{Request.Host}{Request.Path}/{director.Id}",
                UriKind.Absolute), director);
        }
    }
}
