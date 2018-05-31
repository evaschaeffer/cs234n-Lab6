using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Xml.Serialization;
using System.Configuration;

namespace ToolsCSharp
{
    public abstract class BaseTextDB
    {
        private string folder = "..\\..\\..\\Files\\";
        private string fileName = "base.xml";

        public BaseTextDB()
        {
            AppSettingsReader configReader = new AppSettingsReader();
            folder = (configReader.GetValue("dbObjectFileLocation", folder.GetType())).ToString();
            fileName = this.GetType().Name + ".xml";
        }

        public BaseTextDB(string fileLocation)
        {
            if (fileLocation != "")
            {
                folder = fileLocation;
            }
            else
            {
                AppSettingsReader configReader = new AppSettingsReader();
                folder = (configReader.GetValue("dbObjectFileLocation", folder.GetType())).ToString();
            }
            fileName = this.GetType().Name + ".xml";
        }

        public object RetrieveAll(Type type)
        {
            /*
            if (folderName != "")
                folder = folderName;

            else
            {
                AppSettingsReader configReader = new AppSettingsReader();
                folder = (configReader.GetValue("dbObjectFileLocation", folder.GetType())).ToString();
                fileName = this.GetType().Name + ".xml";
            }
            */

            Stream reader = new FileStream(folder + fileName, FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(type);
            object obj = serializer.Deserialize(reader);
            reader.Close();
            return obj;
        }

        /*
        public object ReadAll(string fileLocation, string fileName, Type type)
        {
            if (fileLocation != "")
                folder = fileLocation;
            else
            {
                AppSettingsReader configReader = new AppSettingsReader();
                folder = (configReader.GetValue("dbObjectFileLocation", folder.GetType())).ToString();
            }
            Stream reader = new FileStream(folder + fileName + ".xml", FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(type);
            object obj = serializer.Deserialize(reader);
            reader.Close();
            return obj;
        }
        */ 

        public void WriteAll(Object obj)
        {
            Stream writer = new FileStream(folder + fileName, FileMode.Create);
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            serializer.Serialize(writer, obj);
            writer.Close();
        }

        /*
        public void WriteAll(string fileLocation, String fileName, Object obj)
        {
            if (fileLocation != "")
                folder = fileLocation;
            else
            {
                AppSettingsReader configReader = new AppSettingsReader();
                folder = (configReader.GetValue("dbObjectFileLocation", folder.GetType())).ToString();
            }
            Stream writer = new FileStream(folder + fileName + ".xml", FileMode.Create);
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            serializer.Serialize(writer, obj);
            writer.Close();
        }
        */

    }
}
