using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Turnstyle.Client.Types
{
    public class Credentials
    {
        public string Login = "login";
        public string Secret = "secret";

        /// <summary>
        /// Parameterless constructor is needed for serialization
        /// </summary>
        private Credentials() { }

        public Credentials(string filePath)
        {
            this.ReadFromFile(filePath);
        }

        /// <summary>
        /// Look in APICredentials.xml for a sample xml to start from
        /// </summary>
        /// <param name="filePath"></param>
        private void ReadFromFile(string filePath)
        {
            Credentials credentials;

            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Credentials));
                credentials = (Credentials)serializer.Deserialize(fs);
            }

            this.Login = credentials.Login;
            this.Secret = credentials.Secret;
        }

        /// <summary>
        /// You can use this to create a first version of the serialized xml file
        /// </summary>
        /// <param name="filePath"></param>
        private void WriteToFile(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Credentials));
                serializer.Serialize(fs, this);
            }
        }
    }
}
