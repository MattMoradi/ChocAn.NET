
using ChocAn;
using System;
using System.IO;
using Xunit;
using Xunit.Abstractions;
using System.Reflection;
using System.Linq;
using System.Text;

namespace Tests
{
    public class ProviderTests
    {
        
        [Fact]
        public void ValidateMemberTestSuspended()
        {
            Database database = new Database();

            database.members[0].name = "Henry";
            database.members[0].number = 444;
            database.members[0].status = Database.Validity.Validated;
            database.members[1].name = "Mark";
            database.members[1].number = 543;
            database.members[1].status = Database.Validity.Suspended;
            database.members[2].name = "Flip";
            database.members[2].number = 333;
            database.members[2].status = Database.Validity.Validated;

            Provider.memberNumInput = 543;
            Provider.ValidateMemberSearch(database);
            Assert.Equal("0", Provider.memberstatus);
        }
        [Fact]
        public void ValidateMemberTestValidated()
        {
            Database database = new Database();

            database.members[0].name = "Henry";
            database.members[0].number = 444;
            database.members[0].status = Database.Validity.Validated;
            database.members[1].name = "Mark";
            database.members[1].number = 543;
            database.members[1].status = Database.Validity.Suspended;
            database.members[2].name = "Flip";
            database.members[2].number = 333;
            database.members[2].status = Database.Validity.Validated;

            Provider.memberNumInput = 333;
            Provider.ValidateMemberSearch(database);
            Assert.Equal("", Provider.memberstatus);
        }
        
        [Fact]
        public void FindingProviderTestValid()
        {
            Database database = new Database();

            database.providers[0].name = "Freddy";
            database.providers[0].number = 421;

            database.providers[1].name = "John";
            database.providers[1].number = 888;

            Provider.providerNumbInput = 421;
            Provider.FindProvider(database);
            Assert.True(Provider.providerFound);
        }
        [Fact]
        public void FindingProviderTestFalse()
        {
            Database database = new Database();

            database.providers[0].name = "freddy";
            database.providers[0].number = 421;

            database.providers[1].name = "jhonny";
            database.providers[1].number = 991;

            Provider.providerNumbInput = 788;
            Provider.FindProvider(database);
            bool a = Provider.providerFound;
            bool b = false;
            Assert.NotEqual(a, b);
        }
        
        [Fact]
        public void TotalFeesTest()
        {
            Database database = new Database();
            database.providers[0].records = new Database.ProviderRecords[999];
            database.providers[0].records[0].memberName = "Frank";
            database.providers[0].records[0].fee = 127;
            database.providers[0].records[1].memberName = "Mike";
            database.providers[0].records[1].fee = 450;


            double expected = 577;
            double actual = Provider.TotalFeesCalc(database);

            Assert.Equal(expected, actual);
        }
        
    }
}
