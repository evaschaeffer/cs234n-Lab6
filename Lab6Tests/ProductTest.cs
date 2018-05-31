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

using ProductDB = Lab6DBClasses.ProductSQLDB;
using DBCommand = System.Data.SqlClient.SqlCommand;

namespace PropsClasses
{
    [TestFixture]
    public class ProductTest
    {
        private string dataSource = "Data Source=USL11402\\SQLEXPRESS;Initial Catalog=MMABooksUpdated;Integrated Security=True";
        ProductDB db;
        ProductProps p;
        ProductProps p2;
        Product product;

        [SetUp]
        public void Setup()
        {
            db = new ProductDB(dataSource);
            p = new ProductProps();

            DBCommand command = new DBCommand();
            command.CommandText = "usp_testingResetData";
            command.CommandType = CommandType.StoredProcedure;
            db.RunNonQueryProcedure(command);

            p.productID = 100;
            p.productCode = "xxx";
            p.description = "my test product";
            p.onHandQuantity = 2;
            p.unitPrice = 99;
        }

        [Test]
        public void LoadFromDatabase()
        {
            product = new Product(7, dataSource);
            Assert.AreEqual(7, product.ProductID);
            Assert.AreEqual("DB1R", product.ProductCode);

        }

        [Test]
        public void TestGetState()
        {
            string xml = p.GetState();
            Assert.Greater(xml.Length, 0);
            Assert.True(xml.Contains(p.description));
            Console.WriteLine(xml);
        }

        [Test]
        public void TestSetState1()
        {
            ProductProps newP = new ProductProps();
            string xml = p.GetState();
            newP.SetState(xml);
            Assert.AreEqual(p.productID, newP.productID);
            Assert.AreEqual(p.description, newP.description);
        }

        [Test]
        public void TestClone()
        {
            ProductProps newP = (ProductProps)p.Clone();
            Assert.AreEqual(newP.GetState(), p.GetState());
            
        }
        [Test]
        public void TestDBRetrieve()
        {
            p2 = (ProductProps)db.Retrieve(1);
            Assert.AreEqual(p2.productCode.Trim(), "A4CS");
        }

        [Test]
        public void TestDBCreate()
        {
            db.Create(p);
            ProductProps p2 = (ProductProps)db.Retrieve(p.productID);
            Assert.AreEqual(p.onHandQuantity, p2.onHandQuantity);

        }

        public void TestDBUpdate()
        {

        }
        
    }
}
