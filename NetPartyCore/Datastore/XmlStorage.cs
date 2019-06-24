using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using NetPartyCore.Datastore.Model;

namespace NetPartyCore.Datastore
{
    class XmlStorage : IStorage
    {
        
        

        private XDocument document;
        
        public XmlStorage()
        {
            if (!File.Exists("partycli.xml"))
            {
                XmlWriter xw = XmlWriter.Create("partycli.xml");
                using (xw)
                {
                    var newXDocument = new XDocument(
                        new XElement("Datastore",
                            new XElement("Config",
                                new XElement("Username", ""),
                                new XElement("Password", "")
                            ),
                            new XElement("Servers")
                        )
                    );

                    newXDocument.Save(xw);
                }
                xw.Close();
            }

            XmlReader xr = XmlReader.Create("partycli.xml");
            using (xr)
            {
                document = XDocument.Load(xr);
            }
            xr.Close();
        }

        public Client GetConfiguration()
        {
            throw new NotImplementedException();
        }

        public DataContext GetContext()
        {
            throw new NotImplementedException();
        }

        public List<Server> GetServers()
        {
            throw new NotImplementedException();
        }

        public void SetConfiguration(Client client)
        {
            document.Root.Element("Config").Element("Username").SetValue(client.Username);
            document.Root.Element("Config").Element("Password").SetValue(client.Password);
            document.Save("partycli.xml");
        }

        public void SetSevers(List<Server> servers)
        {
            throw new NotImplementedException();
        }

    }
}
