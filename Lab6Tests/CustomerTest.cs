using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using PropsClasses;
using ToolsCSharp;
using Lab6DBClasses;

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
        CustomerSQLDB db;

        CustomerProps c;

        [SetUp]
        public void Setup()
        {
            c = new CustomerProps();
            c.customerID = 100;
            c.name = "Mickey Mouse";
            c.address = "101 Disney Land Lane";
            c.city = "California";
            c.state = "CA";
            c.zipCode = 99999;
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
    }
}
