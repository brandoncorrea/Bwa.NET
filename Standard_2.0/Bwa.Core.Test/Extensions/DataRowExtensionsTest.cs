using System;
using System.Data;
using Bwa.Core.Test.Models;
using Bwa.Core.Extensions;

namespace Bwa.Core.Test.Extensions
{
	public class DataRowExtensionsTest
	{
        DataTable Table;

        [SetUp]
        public void SetUp()
        {
            Table = new DataTable();
            Table.Columns.Add("FirstInitial", typeof(char));
            Table.Columns.Add("Age", typeof(int));
            Table.Columns.Add("Foo", typeof(double));
            Table.Columns.Add("IsPublished", typeof(string));
            Table.Columns.Add("DateOfBirth", typeof(DateTime));
        }

        [Test]
        public void DeserializesEmptyDataRow()
        {
            var result = Table.NewRow().Deserialize<Author>();
            Assert.AreEqual('\0', result.FirstInitial);
            Assert.AreEqual(null, result.LastName);
            Assert.AreEqual(DateTime.MinValue, result.DateOfBirth);
            Assert.AreEqual(0, result.Age);
            Assert.IsFalse(result.IsPublished);
            Assert.AreEqual(0, result.NetWorth);
        }

        [Test]
        public void DeserializesPopulatedDataRow()
        {
            var row = Table.NewRow();
            row.SetField("FirstInitial", 'B');
            row.SetField("Age", 45);
            row.SetField("Foo", 3.14);
            row.SetField("IsPublished", "True");
            row.SetField("DateOfBirth", new DateTime(2022, 1, 2));
            var result = row.Deserialize<Author>();
            Assert.AreEqual('B', result.FirstInitial);
            Assert.AreEqual(null, result.LastName);
            Assert.AreEqual(new DateTime(2022, 1, 2), result.DateOfBirth);
            Assert.AreEqual(45, result.Age);
            Assert.IsTrue(result.IsPublished);
            Assert.AreEqual(0, result.NetWorth);

        }
    }
}
