using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ToolsCSharp;
using Lab6DBClasses;
using Lab6Classes;
using PropsClasses;

using ProductDB = Lab6DBClasses.ProductSQLDB;

using System.Data;

namespace Lab6Classes
{
    public class Product : BaseBusiness
    {
        #region properties
        /// <summary>
        /// Read-only ID property.
        /// </summary>
        public int ProductID
        {
            get
            {
                return ((ProductProps)mProps).productID;
            }
        }

        /// <summary>
        /// Read/Write property. 
        /// </summary>
        public string ProductCode
        {
            get
            {
                return ((ProductProps)mProps).productCode;
            }

            set
            {
                if (!(value == ((ProductProps)mProps).productCode))
                {
                    if (value.Trim().Length >= 1 && value.Trim().Length <= 10)
                    {
                        mRules.RuleBroken("ProductCode", false);
                        ((ProductProps)mProps).productCode = value;
                        mIsDirty = true;
                    }

                    else
                    {
                        throw new ArgumentOutOfRangeException("ProductCode must be between 1 and 10 characters.");
                    }
                }
            }
        }

        /// <summary>
        /// Read/Write property. 
        /// </summary>
        /// <exception cref="ArgumentException">
        /// 
        /// </exception>
        public string Description
        {
            get
            {
                return ((ProductProps)mProps).description;
            }

            set
            {
                if (!(value == ((ProductProps)mProps).description))
                {
                    if (value.Trim().Length >= 1 && value.Trim().Length <= 50)
                    {
                        mRules.RuleBroken("Description", false);
                        ((ProductProps)mProps).description = value;
                        mIsDirty = true;
                    }

                    else
                    {
                        throw new ArgumentException("Description must be between 1 and 50 characters");
                    }
                }
            }
        }

        /// <summary>
        /// Read/Write property. 
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown if the value is not a positive number.
        /// </exception>
        public decimal UnitPrice
        {
            get
            {
                return ((ProductProps)mProps).unitPrice;
            }

            set
            {
                if (value >= 0)
                {
                    ((ProductProps)mProps).unitPrice = value;
                    mIsDirty = true;
                }

                else
                {
                    throw new ArgumentException("UnitPrice must be a positive number.");
                }
            }
        }

        /// <summary>
        /// Read/Write property. 
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown if the value is not a positive number.
        /// </exception>
        public int OnHandQuantity
        {
            get
            {
                return ((ProductProps)mProps).onHandQuantity;
            }

            set
            {
                if (!(value == ((ProductProps)mProps).onHandQuantity))
                {
                    if (value >= 0)
                    {
                        ((ProductProps)mProps).onHandQuantity = value;
                        mIsDirty = true;
                    }

                    else
                    {
                        throw new ArgumentException("OnHandQuantity must be a positive number.");
                    }
                }
            }
        }
        #endregion

        #region Setup
        /// <summary>
        /// 
        /// </summary>		
        protected override void SetDefaultProperties()
        {
        }

        /// <summary>
        /// Sets required fields for a record.
        /// </summary>
        protected override void SetRequiredRules()
        {
            mRules.RuleBroken("ProductCode", true);
            mRules.RuleBroken("Description", true);
        }

        /// <summary>
        /// Instantiates mProps and mOldProps as new Props objects.
        /// Instantiates mbdReadable and mdbWriteable as new DB objects.
        /// </summary>
        protected override void SetUp()
        {
            mProps = new ProductProps();
            mOldProps = new ProductProps();

            if (this.mConnectionString == "")
            {
                mdbReadable = new ProductDB();
                mdbWriteable = new ProductDB();
            }

            else
            {
                mdbReadable = new ProductDB(this.mConnectionString);
                mdbWriteable = new ProductDB(this.mConnectionString);
            }
        }
        #endregion

        #region constructors
        /// <summary>
        /// Default constructor - does nothing.
        /// </summary>
        public Product() : base()
        {
        }

        /// <summary>
        /// One arg constructor.
        /// Calls methods SetUp(), SetRequiredRules(), 
        /// SetDefaultProperties() and BaseBusiness one arg constructor.
        /// </summary>
        /// <param name="cnString">DB connection string.
        /// This value is passed to the one arg BaseBusiness constructor, 
        /// which assigns the it to the protected member mConnectionString.</param>
        public Product(string cnString)
            : base(cnString)
        {
        }

        /// <summary>
        /// Two arg constructor.
        /// Calls methods SetUp() and Load().
        /// </summary>
        /// <param name="key">ID number of a record in the database.
        /// Sent as an arg to Load() to set values of record to properties of an 
        /// object.</param>
        /// <param name="cnString">DB connection string.
        /// This value is passed to the one arg BaseBusiness constructor, 
        /// which assigns the it to the protected member mConnectionString.</param>
        public Product(int key, string cnString)
            : base(key, cnString)
        {
        }

        public Product(int key)
            : base(key)
        {
        }

        // *** I added these 2 so that I could create a 
        // business object from a properties object
        // I added the new constructors to the base class
        public Product(ProductProps props)
            : base(props)
        {
        }

        public Product(ProductProps props, string cnString)
            : base(props, cnString)
        {
        }
        #endregion


        #region others
        /// <summary>
        /// Retrieves a list of Events.
        /// </summary>public override object GetList()
        public override object GetList()
        {
            List<Product> products = new List<Product>();
            List<ProductProps> props = new List<ProductProps>();


            props = (List<ProductProps>)mdbReadable.RetrieveAll(props.GetType());
            foreach (ProductProps prop in props)
            {
                Product p = new Product(prop, this.mConnectionString);
                products.Add(p);
            }

            return products;
        }

        public static List<Product> GetList(string cnString)
        {
            ProductDB db = new ProductDB(cnString);
            List<Product> products = new List<Product>();
            List<ProductProps> props = new List<ProductProps>();

            // *** methods in the textdb and sqldb classes don't match
            // Ideally, I should go back and fix the IReadDB interface!
            props = (List<ProductProps>)db.RetrieveAll(props.GetType());
            foreach (ProductProps prop in props)
            {
                // *** creates the business object from the props objet
                Product p = new Product(prop, cnString);
                products.Add(p);
            }

            return products;
        }

        // *** this is new
        public static DataTable GetTable(string cnString)
        {
            ProductDB db = new ProductDB(cnString);
            return db.RetrieveTable();
        }

        public static DataTable GetTable()
        {
            ProductDB db = new ProductDB();
            return db.RetrieveTable();
        }

        
        #endregion
    }
}
