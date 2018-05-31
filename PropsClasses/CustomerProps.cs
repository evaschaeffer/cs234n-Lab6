using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using ToolsCSharp;

using DBDataReader = System.Data.SqlClient.SqlDataReader;
using System.Data.SqlClient;

namespace PropsClasses
{
    [Serializable()]
    public class CustomerProps : IBaseProps
    {
        #region instance variables
        /// <summary>
        /// 
        /// </summary>
        public int customerID = Int32.MinValue;

        /// <summary>
        /// 
        /// </summary>
        public string name = "";

        /// <summary>
        /// 
        /// </summary>
        public string address = "";

        /// <summary>
        /// 
        /// </summary>
        public string city = "";

        /// <summary>
        /// 
        /// </summary>
        public string state = "";

        /// <summary>
        /// 
        /// </summary>
        public int zipCode = Int32.MinValue;

        /// <summary>
        /// ConcurrencyID. See main docs, don't manipulate directly
        /// </summary>
        public int ConcurrencyID = 0;
        #endregion

        #region constructor
        /// <summary>
        /// Constructor. This object should only be instantiated by Customer, not used directly.
        /// </summary>
        public CustomerProps()
        {
        }
        #endregion

        #region BaseProps Members
        /// <summary>
        /// Serializes this props object to XML, and writes the key-value pairs to a string.
        /// </summary>
        /// <returns>String containing key-value pairs</returns>	
        public string GetState()
        {
            XmlSerializer serializer = new XmlSerializer(this.GetType());
            StringWriter writer = new StringWriter();
            serializer.Serialize(writer, this);
            return writer.GetStringBuilder().ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        public void SetState(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(this.GetType());
            StringReader reader = new StringReader(xml);
            CustomerProps c = (CustomerProps)serializer.Deserialize(reader);
            this.customerID = c.customerID;
            this.name = c.name;
            this.address = c.address;
            this.city = c.city;
            this.zipCode = c.zipCode;
            this.ConcurrencyID = c.ConcurrencyID;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        public void SetState(DBDataReader dr)
        {
            this.customerID = (Int32)dr["CustomerID"];
            this.name = (string)dr["Name"];
            this.address = (string)dr["Address"];
            this.city = (string)dr["City"];
            this.zipCode = (Int32)dr["ZipCode"];
            this.ConcurrencyID = (Int32)dr["ConcurrencyID"];
        }
        #endregion

        #region ICloneable Members
        /// <summary>
        /// Clones this object.
        /// </summary>
        /// <returns>A clone of this object.</returns>
        public object Clone()
        {
            CustomerProps c = new CustomerProps();
            c.customerID = this.customerID;
            c.name = this.name;
            c.address = this.address;
            c.city = this.city;
            c.state = this.state;
            c.zipCode = this.zipCode;
            c.ConcurrencyID = this.ConcurrencyID;
            return c;
        }
        #endregion
    }
}
