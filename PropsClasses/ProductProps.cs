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
    public class ProductProps : IBaseProps
    {
        #region instance variables
        /// <summary>
        /// 
        /// </summary>
        public int productID = Int32.MinValue;

        /// <summary>
        /// 
        /// </summary>
        public string productCode = "";

        /// <summary>
        /// 
        /// </summary>
        public string description = "";

        /// <summary>
        /// 
        /// </summary>
        public decimal unitPrice = 0m;

        /// <summary>
        /// 
        /// </summary>
        public int onHandQuantity = 0;

        /// <summary>
        /// ConcurrencyID. See main docs, don't manipulate directly
        /// </summary>
        public int ConcurrencyID = 0;
        #endregion

        #region constructor
        /// <summary>
        /// Constructor. This object should only be instantiated by Customer, not used directly.
        /// </summary>
        public ProductProps()
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
        /// <param name="dr"></param>
        public void SetState(DBDataReader dr)
        {
            this.productID = (Int32)dr["ProductID"];
            this.productCode = ((string)dr["ProductCode"]).Trim();
            this.description = (string)dr["Description"];
            this.unitPrice = (decimal)dr["UnitPrice"];
            this.onHandQuantity = (Int32)dr["OnHandQuantity"];
            this.ConcurrencyID = (Int32)dr["ConcurrencyID"];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        public void SetState(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(this.GetType());
            StringReader reader = new StringReader(xml);
            ProductProps p = (ProductProps)serializer.Deserialize(reader);
            this.productID = p.productID;
            this.productCode = p.productCode;
            this.description = p.description;
            this.unitPrice = p.unitPrice;
            this.onHandQuantity = p.onHandQuantity;
            this.ConcurrencyID = p.ConcurrencyID;
        }
        #endregion

        #region ICloneable Members
        /// <summary>
        /// Clones this object.
        /// </summary>
        /// <returns>A clone of this object.</returns>
        public object Clone()
        {
            ProductProps p = new ProductProps();
            p.productID = this.productID;
            p.productCode = this.productCode;
            p.description = this.description;
            p.unitPrice = this.unitPrice;
            p.onHandQuantity = this.onHandQuantity;
            p.ConcurrencyID = this.ConcurrencyID;
            return p;
        }
        #endregion
    }
}
