using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using PropsClasses;
using ToolsCSharp;
using Lab6DBClasses;
using Lab6Classes;

using System.Xml;
using System.Xml.Serialization;
using System.IO;

using System.Data;
using System.Data.SqlClient;

using CustomerDB = Lab6DBClasses.CustomerSQLDB;
using DBCommand = System.Data.SqlClient.SqlCommand;


namespace PropsClasses
{

    [TestFixture]
    public class CustomerTest
    {
        /// <summary>
        /// The path of the database.
        /// </summary>
        private string dataSource = "Data Source=USL11402\\SQLEXPRESS;Initial Catalog=MMABooksUpdated;Integrated Security=True";
        CustomerDB db;

        CustomerProps c;
        CustomerProps c2;
        //IBaseProps c2;

        [SetUp]
        public void Setup()
        {
            db = new CustomerDB(dataSource);
            c = new CustomerProps();

            DBCommand command = new DBCommand();
            command.CommandText = "usp_testingResetData";
            command.CommandType = CommandType.StoredProcedure;
            db.RunNonQueryProcedure(command);

            c.customerID = 100;
            c.name = "Mickey Mouse";
            c.address = "101 Disney Land Lane";
            c.city = "California";
            c.state = "CA";
            c.zipCode = "99999";
        }

        [Test]
        public void TestGetState()
        {
            string xml = c.GetState();
            Assert.Greater(xml.Length, 0);
            Assert.True(xml.Contains(c.address));
            Console.WriteLine(xml);
        }

        [Test]
        public void TestSetState1()
        {
            CustomerProps newC = new CustomerProps();
            string xml = c.GetState();
            newC.SetState(xml);
            Assert.AreEqual(c.customerID, newC.customerID);
            Assert.AreEqual(c.address, newC.address);
        }

        [Test]
        public void TestClone()
        {
            CustomerProps newC = (CustomerProps)c.Clone();
            Assert.AreEqual(newC.GetState(), c.GetState());

        }
        [Test]
        public void TestDBRetrieve()
        {
            c2 = (CustomerProps)db.Retrieve(1);
            Assert.AreEqual(c2.city, "Birmingham");
        }
        [Test]
        public void TestDBCreate()
        {
            db.Create(c);
            CustomerProps c2 = (CustomerProps)db.Retrieve(c.customerID);
            Assert.AreEqual(c.address, c2.address);

        }
        [Test]
        public void TestDBUpdate()
        {
            c = (CustomerProps)db.Retrieve(1);
            c.address = "I like thanksgiving.";
            Assert.True(db.Update(c));
        }
        [Test]
        public void TestDBDelete()
        {
            c = (CustomerProps)db.Retrieve(2);
            Assert.True(db.Delete(c));
        }
        [Test]
        public void TestNewEventConstructor()
        {
            // not in Data Store - no id
            Customer c = new Customer(dataSource);
            Console.WriteLine(c.ToString());
            Assert.Greater(c.ToString().Length, 1);
        }
        [Test]
        public void TestRetrieveFromDataStoreContructor()
        {
            // retrieves from Data Store
            Customer c = new Customer(1, dataSource);
            Assert.AreEqual(c.CustomerID, 1);
            Assert.AreEqual(c.Name, "Molunguri, A");
            Console.WriteLine(c.ToString());
        }

        [Test]
        public void TestSaveToDataStore()
        {
            Customer c = new Customer(dataSource);
            c.Name = "Test Person";
            c.Address = "101 Main St.";
            c.City = "Cincinnati";
            c.State = "OH";
            c.ZipCode = "10101";
            c.Save();
            Assert.AreEqual(700, c.CustomerID);
        }

        [Test]
        public void TestUpdate()
        {
            Customer c = new Customer(3, dataSource);
            c.Name = "TEST";
            c.Address = "Test Customer Address";
            c.Save();

            c = new Customer(3, dataSource);
            Assert.AreEqual(c.Name, "TEST".Trim());
            Assert.AreEqual(c.Address, "Test Customer Address".Trim());
        }

        [Test]
        public void TestDelete()
        {
            Customer c = new Customer(2, dataSource);
            c.Delete();
            c.Save();
            Assert.Throws<Exception>(() => new Customer(2, dataSource));
        }
        [Test]
        public void TestStaticGetList()
        {
            List<Customer> customers = Customer.GetList(dataSource);
            Assert.AreEqual(696, customers.Count);
            Assert.AreEqual(1, customers[0].CustomerID);
            Assert.AreEqual("Molunguri, A", customers[0].Name);
        }
        [Test]
        public void TestGetList()
        {
            Customer c = new Customer(dataSource);
            List<Customer> customers = (List<Customer>)c.GetList();
            Assert.AreEqual(696, customers.Count);
            Assert.AreEqual(1, customers[0].CustomerID);
            Assert.AreEqual("1108 Johanna Bay Drive", customers[0].Address);
        }
        /*
         * [Test]
        
         * public void TestGetTable()
        {
            DataTable customers = Customer.GetTable(dataSource);
            Assert.AreEqual(customers.Rows.Count, 697);
        }
        */

        [Test]
        public void TestNoRequiredPropertiesNotSet()
        {
            // not in Data Store - userid, title and description must be provided
            Customer c = new Customer(dataSource);
            Assert.Throws<Exception>(() => c.Save());
        }

        [Test]
        public void TestSomeRequiredPropertiesNotSet()
        {
            // not in Data Store - all required data must be provided
            Customer c = new Customer(dataSource);
            Assert.Throws<Exception>(() => c.Save());
            c.Name= "TEST";
            Assert.Throws<Exception>(() => c.Save());
        }
        [Test]
        public void TestInvalidPropertyNameSet()
        {
            Customer c = new Customer(dataSource);
            Assert.Throws<ArgumentOutOfRangeException>(() => c.City = "012345678912345678910");
        }
        
        [Test]
        public void TestConcurrencyIssue()
        {
            Customer c1 = new Customer(1, dataSource);
            Customer c2 = new Customer(1, dataSource);

            c1.Name = "New firstName";
            c1.Save();

            c2.Name = "Edit secondName";
            Assert.Throws<Exception>(() => c2.Save());
        }
    }
}
