using ChocAn;
using System;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Tests
{
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

            Assert.True(database.members[0].name == name, "members name in database does not share same data");
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
    }
}
