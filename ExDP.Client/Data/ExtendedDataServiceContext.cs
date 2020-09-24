using ClientNS.Models;
using Microsoft.OData.Client;
using Microsoft.OData.Edm;
using System;
using System.Net;

namespace ClientNS.Data
{
    public partial class ExtendedDataServiceContext : DataServiceContext
    {
        public ExtendedDataServiceContext(Uri serviceUri)
            : base(serviceUri)
        {
            Format.LoadServiceModel = LoadServiceModel;
            Format.UseJson();
        }

        private IEdmModel LoadServiceModel()
        {
            // Get the service metadata's Uri
            var metadataUri = GetMetadataUri();
            // Create a HTTP request to the metadata's Uri 
            // in order to get a representation for the data model
            var request = WebRequest.CreateHttp(metadataUri);
            
            using (var response = request.GetResponse())
            {
                // Translate the response into an in-memory stream
                using (var stream = response.GetResponseStream())
                {
                    // Convert the stream into an XML representation
                    using (var reader = System.Xml.XmlReader.Create(stream))
                    {
                        // Parse the XML representation of the data model
                        // into an EDM that can be utilized by OData client libraries
                        return Microsoft.OData.Edm.Csdl.CsdlReader.Parse(reader);
                    }
                }
            }
        }
    }
}
