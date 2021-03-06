﻿using System;
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
        [Test]
        public void TestDBUpdate()
        {
            p = (ProductProps)db.Retrieve(1);
            p.description = "I like thanksgiving.";
            Assert.True(db.Update(p));
            
        }
        [Test]
        public void TestDDBelete()
        {
            p = (ProductProps)db.Retrieve(2);
            Assert.True(db.Delete(p));
        }
        [Test]
        public void TestNewEventConstructor()
        {
            // not in Data Store - no id
            Product p = new Product(dataSource);
            Console.WriteLine(p.ToString());
            Assert.Greater(p.ToString().Length, 1);
        }
        [Test]
        public void TestRetrieveFromDataStoreContructor()
        {
            // retrieves from Data Store
            Product p = new Product(1, dataSource);
            Assert.AreEqual(p.ProductID, 1);
            Assert.AreEqual(p.Description, "Murach's ASP.NET 4 Web Programming with C# 2010");
            Console.WriteLine(p.ToString());
        }

        [Test]
        public void TestSaveToDataStore()
        {
            Product p = new Product(dataSource);
            p.ProductCode = "TWO";
            p.Description = "I like to have two drinks in my hand.";
            p.Save();
            Assert.AreEqual(17, p.ProductID);
        }

        [Test]
        public void TestUpdate()
        {
            Product p = new Product(3, dataSource);
            p.ProductCode = "TEST";
            p.Description = "Test Product";
            p.Save();

            p = new Product(3, dataSource);
            Assert.AreEqual(p.ProductCode, "TEST".Trim());
            Assert.AreEqual(p.Description, "Test Product".Trim());
        }

        [Test]
        public void TestDelete()
        {
            Product p = new Product(2, dataSource);
            p.Delete();
            p.Save();
            Assert.Throws<Exception>(() => new Product(2, dataSource));
        }
        [Test]
        public void TestStaticGetList()
        {
            List<Product> products = Product.GetList(dataSource);
            Assert.AreEqual(16, products.Count);
            Assert.AreEqual(1, products[0].ProductID);
            Assert.AreEqual("A4CS", products[0].ProductCode);
        }
        [Test]
        public void TestGetList()
        {
            Product p = new Product(dataSource);
            List<Product> products = (List<Product>)p.GetList();
            Assert.AreEqual(16, products.Count);
            Assert.AreEqual("A4CS", products[0].ProductCode);
            Assert.AreEqual("Murach's ASP.NET 4 Web Programming with C# 2010", products[0].Description);
        }
        [Test]
        public void TestGetTable()
        {
            DataTable products = Product.GetTable(dataSource);
            Assert.AreEqual(products.Rows.Count, 16);
        }

        [Test]
        public void TestNoRequiredPropertiesNotSet()
        {
            // not in Data Store - userid, title and description must be provided
            Product p = new Product(dataSource);
            Assert.Throws<Exception>(() => p.Save());
        }

        [Test]
        public void TestSomeRequiredPropertiesNotSet()
        {
            // not in Data Store - userid, title and description must be provided
            Product p = new Product(dataSource);
            Assert.Throws<Exception>(() => p.Save());
            p.ProductCode = "TEST";
            Assert.Throws<Exception>(() => p.Save());
        }
        [Test]
        public void TestInvalidPropertyProductCodeSet()
        {
            Product p = new Product(dataSource);
            Assert.Throws<ArgumentOutOfRangeException>(() => p.ProductCode = "012345678912");
        }
        [Test]
        public void TestConcurrencyIssue()
        {
            Product p1 = new Product(1, dataSource);
            Product p2 = new Product(1, dataSource);

            p1.Description = "Updated this first";
            p1.Save();

            p2.Description = "Updated this second";
            Assert.Throws<Exception>(() => p2.Save());
        }
    }
}
