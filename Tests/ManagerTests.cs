using ChocAn;
using System;
using System.IO;
using Xunit;
using Xunit.Abstractions;



namespace Tests
{
    public class ManagerTests
    {
        [Fact]
        public void ManagerTest()
        {
            Database data = new Database();

            short expectedProv = 0;
            int expectedCon = 0;
            decimal expectedSum = 0;
            short j = 1;

            while (expectedProv < 10)
            {
                data.providers[expectedProv].name = "test1";
                data.providers[expectedProv].number = 111111 + expectedProv;
                j += expectedProv;
                data.providers[expectedProv].consultations = j;
                data.providers[expectedProv].totalFee = 10.1 + expectedProv;
                expectedSum += 10.1m + Convert.ToDecimal(expectedProv);
                expectedCon += j;
                ++expectedProv;
            }
            int actualProv = 0;
            short actualCon = 0;
            decimal actualSum = 0;
            Manager.SummaryGenerator(data.providers, ref actualProv, ref actualCon, ref actualSum);
            Assert.True(expectedSum == actualSum, "Sums did not match");
            Assert.True(expectedCon == actualCon, "Consultations did not match");
            Assert.True(expectedProv == actualProv, "Providers did not match");
        }
    }
}


/*
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChocAn;
using System;
using System.Collections.Generic;
using System.Text;


namespace ChocAn.Tests
{
    [TestClass()]
    public class ManagerTest
    {
        [TestMethod()]
        public void SummaryTestFeesSum()
        {
            Database data = new Database();
            short expectedProv = 0;
            short j = 1;
            int expectedCon = 0;
            decimal expectedSum = 0;
            while (expectedProv < 10)
            {
                data.providers[expectedProv].name = "test1";
                data.providers[expectedProv].number = 111111 + expectedProv;
                j += expectedProv;
                data.providers[expectedProv].consultations = j;
                data.providers[expectedProv].totalFee = 10.1 + expectedProv;
                expectedSum += 10.1m + Convert.ToDecimal(expectedProv);
                expectedCon += j;
                ++expectedProv;
            }
            int actualProv = 0;
            short actualCon = 0;
            decimal actualSum = 0;
            Manager.SummaryGenerator(data.providers, ref actualProv, ref actualCon, ref actualSum);
            Assert.AreEqual(expectedSum, actualSum);
            Assert.AreEqual(expectedCon, actualCon);
            Assert.AreEqual(expectedProv, actualProv);
        }
    }
}
*/