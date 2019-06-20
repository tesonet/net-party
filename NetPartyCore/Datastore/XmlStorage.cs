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

        public XmlStorage()
        {
            if (!File.Exists(@"c:\temp\test.txt"))
            {
                this.CreateBaiscXml();
            }
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
            throw new NotImplementedException();
        }

        public void SetSevers(List<Server> servers)
        {
            throw new NotImplementedException();
        }

        private XDocument CreateBaiscXml()
        {
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings xws = new XmlWriterSettings();
            xws.Indent = true;

            using (XmlWriter xw = XmlWriter.Create(sb, xws))
            {
                XDocument doc = new XDocument(
                    new XElement("Datastore",
                        new XElement("Config",
                            new XElement("Username", ""),
                            new XElement("Password", "")
                        ),
                        new XElement("Servers")
                    )
                 );
                doc.Save(xw);
                return doc;
            }
        }

    }
}
