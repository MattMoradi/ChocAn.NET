using ChocAn;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using Xunit;
using Xunit.Abstractions;
//27
namespace Tests
{
    [Collection("Sequential")]
    public class OperatorTests
    {
        private readonly ITestOutputHelper Output;
        private readonly Database database;
       
        public OperatorTests(ITestOutputHelper Output)
        {
            this.Output = Output;
            database = new Database();
           
        }
        [Fact]
        public void RemoveMemberTest()
        {
            string name = "Nancy Principato";
            int number = 364702985;
            string address = "1054 Pinewood Drive";
            string city = " Arlington Heights";
            string state = "Illionois";
            int zip = 60004;
            
            Operator.CreateMember(database, name, address, city, state, zip, number);
            Assert.True(database.members[0].name != null, "The member was not added to the database");
            Operator.RemoveMember(database, database.members[0].number);
            Assert.True(database.members[0].name == null,"The member was not removed from the database");
        }

        
        [Fact]
        public void RemoveProviderTest()
        {
            string name = "John Goodman";
            int number = 154782046;
            string address = "123 ne fake st";
            string city = "New City";
            string state = "Ohio";
            int zip = 12345;

            Operator.CreateProvider(database, name, address, city, state, zip, number);
            Assert.True(database.providers[0].name != null, "The provider was not added to the database");
            Operator.RemvoveProvider(database, database.providers[0].number);
            Assert.True(database.providers[0].name == null, "The provider was not removed from the database");
        }
        [Fact]
        public void CreateMemberTest()
        {
            string name = "Nancy Principato";
            int number = 364702985;
            string address = "1054 Pinewood Drive";
            string city = " Arlington Heights";
            string state = "Illionois";
            int zip = 60004;
            
            Operator.CreateMember(database, name, address, city, state, zip, number);
            
            Assert.True(database.members[0].name == name, "members name in database does not share same data'" + name + "' is not the same as '" + database.members[0].name + "'");
            Assert.True(database.members[0].number == number, "members number in database does not share same data");
            Assert.True(database.members[0].address == address, "members address in database does not share same data");
            Assert.True(database.members[0].city == city, "members city in database does not share same data");
            Assert.True(database.members[0].state == state, "members state in database does not share same data");
            Assert.True(database.members[0].zip == zip, "members zip in database does not share same data");
        }

        [Fact]
        public void CreateProviderTest()
        {
            string name = "John Goodman";
            int number = 154782046;
            string address = "123 ne fake st";
            string city = "New City";
            string state ="Ohio";
            int zip  = 12345;

            Operator.CreateProvider(database, name, address, city, state, zip, number);

            Assert.True(database.providers[0].name == name, "provider name in database does not share same data");
            Assert.True(database.providers[0].number == number, "provider number in database does not share same data");
            Assert.True(database.providers[0].address == address, "provider address in database does not share same data");
            Assert.True(database.providers[0].city == city, "provider city in database does not share same data");
            Assert.True(database.providers[0].state == state, "provider state in database does not share same data");
            Assert.True(database.providers[0].zip == zip, "provider zip in database does not share same data");
        }  
           
        [Fact]
        public void ValidInputTest()
        {
            string stringInt = "1";
            int intSelection;
            int numChoices = 3;
            Assert.True(Operator.ValidInput(stringInt, out intSelection, numChoices));
        }

        [Fact]
        public void GenerateIDTest()
        {
            for(int i =0; i < 999;i++)
                database.members[i].number = Operator.GenerateID(database, "Member");

            for(int i =0; i <999;i++)
             database.providers[i].number = Operator.GenerateID(database, "Provider");

            Assert.True(database.members.Count() == database.members.Distinct().Count(),"There was a duplicate number found in the member generate id");
            Assert.True(database.providers.Count() == database.providers.Distinct().Count(), "There was a duplicate number found in the providers generate id");
        }

        [Fact]
        public void AccountTypeIsInDatabaseTest()
        {

            Assert.True(!Operator.AccountTypeIsInDatabase(database, "Provider"), "There is no provider in the database");
            Assert.True(!Operator.AccountTypeIsInDatabase(database, "Member"), "There is no member in the database");
           
            database.members[0].name = "Carson";
            database.providers[0].name = "Steve";

            Assert.True(Operator.AccountTypeIsInDatabase(database, "Provider"), "There is no provider in the database");
            Assert.True(Operator.AccountTypeIsInDatabase(database, "Member"), "There is no member in the database");

        }

        [Fact]
        public void IsValidZipTest()
        {
            string zipString = "99999";
            int zipInt;
            
            Assert.True(Operator.IsValidZip(zipString,out zipInt));
        }

        [Fact]
        public void IsValidDateTest()
        {
            string date = "12-13-2005";
            DateTime result;
            Assert.True(Operator.IsValidDate(date, out result));
        }

        [Fact]
        public void IsValidFeeTest()
        {
            string fee = "99999.99";
            double result;
            Assert.True(Operator.IsValidFee(fee, out result),"The fee enterered is not valid");
        }

        [Fact]
        public void IsValidServiceCodeTest()
        {
            string service = "884422";
            int code;
            Assert.True(Operator.IsValidServiceCode(service, out code), "Service is not a valid code");
        }
        
        [Fact]
        public void CreateMemberRecordTest()
        {
            string name = "Carson Hansen";
            int service = 884422;
            string date = "3/11/2022";;
            Operator.CreateMember(database, "John Goodman", "123 ne fake st", "New City", "OR", 97854, 154782046);
            Operator.CreateMemberRecord(database, 0, "Carson Hansen", "3/11/2022",service);
            Assert.True(database.members[0].records[0].date == date,"The record date is not the same");
            Assert.True(database.members[0].records[0].providerName == name, "The record date is not the same");
            Assert.True(database.members[0].records[0].service == "Hamster", "The service is not the same");
        }

        [Fact]
        public void CreateProviderRecordTest()
        {
            string name = "John Goodman";
            int number = 154782046;
            string address = "123 ne fake st";
            string city = "New City";
            string state = "Ohio";
            int zip = 12345;

            Operator.CreateProvider(database, name, address, city, state, zip, number);
            int serviceCode = 884422;
            string date = "10/20/2020";
            string memName = "Carson Hansen";
            double fee = 999.99;
            string comment = "This is a comment";
            int memNumber = 785403695;
            Operator.CreateProviderRecord(database, 0, "10/20/2020", 884422, 999.99, "This is a comment", "Carson Hansen", 785403695);
            Assert.True(database.providers[0].records[0].date == date,"The record date is not the same");
            Assert.True(database.providers[0].records[0].serviceCode == serviceCode, "The service code is not the same");
            Assert.True(database.providers[0].records[0].fee.Equals(fee),"The fees are not the same");
            Assert.True(database.providers[0].records[0].comment == comment,"The comments are not the same");
            Assert.True(database.providers[0].records[0].memberName == memName,"The names are not the same");
            Assert.True(database.providers[0].records[0].memberNumber == memNumber,"The account numbers are not the same");
        }

        [Fact]
        public void HasARecordTest()
        {
            Operator.CreateProvider(database, "Ann Guajardo", "126 Valley Drive", "Norristown", "PA", 19403, 536475036);
            Operator.CreateMember(database, "John Goodman", "123 ne fake st", "New City", "OR", 97854, 154782046);
            Operator.CreateMemberRecord(database, 0, "Carson Hansen", "3/11/2022", 884422);
            Operator.CreateProviderRecord(database, 0, "10/20/2020", 884422, 999.99, "This is a comment", "Carson Hansen", 785403695);
            Assert.True(Operator.HasARecord(database, "Member",0),"Member does not have a valid record");
            Assert.True(Operator.HasARecord(database, "Provider", 0), "Provider does not have a valid record");
        }
    }
}
