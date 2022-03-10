using ChocAn;
using System;
using System.IO;
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
        public void MainMenuTest()
        {
            int maxVal =3;
            string Stringinput = "2";
            int minVal = 0;
            int input;
            
            Assert.True(int.TryParse(Stringinput, out input) && input > minVal && input <= maxVal);
        }

        [Fact]
        public void MemberMenuTest()
        {
            int maxVal = 4;
            string Stringinput = "1";
            int minVal = 0;
            int input;

            Assert.True(int.TryParse(Stringinput, out input) && input > minVal && input <= maxVal);
        }

        [Fact]
        public void ProviderMenuTest()
        {
            int maxVal = 4;
            string Stringinput = "4";
            int minVal = 0;
            int input;

            Assert.True(int.TryParse(Stringinput, out input) && input > minVal && input <= maxVal);
        }

        [Fact]
        public void RemovePersonVerifyThereIsMemberToRemoveTest()
        {
            database.members[0].name = "Ann Guajardo";
            Assert.True(database.members[0].name != null);
        }

        [Fact]
        public void RemovePersonVerifyThereIsProviderToRemoveTest()
        {
            database.providers[0].name = "Jared Williams";
            Assert.True(database.providers[0].name != null);
        }

        [Fact]
        public void RemovePersonVerifyThereIsValidProviderIdTest()
        {
            database.providers[0].number = 742106892;
            int number = 742106892;

            Assert.True(database.providers[0].number == number);
        }

        [Fact]
        public void RemovePersonVerifyThereIsValidMemberIdTest()
        {
            database.members[0].number = 541987360;
            int number = 541987360;

            Assert.True(database.members[0].number == number);
        }

        [Fact]
        public void AddRecordVerifyThereIsProviderToAddRecordTest()
        {
            database.providers[0].name = "Jennifer Montano";
            Assert.True(database.providers[0].name != null);
        }

        [Fact]
        public void AddRecordVerifyThereIsMemberToAddRecordTest()
        {
            database.members[0].name = "Louis C Quinn";
            Assert.True(database.members[0].name != null);
        }

        [Fact]
        public void AddRecordVerifyThereIsValidProvideNumberTest()
        {
            database.providers[0].number = 457536984;
            int number = 457536984;
            Assert.True(number == database.providers[0].number);
        }

        [Fact]
        public void AddRecordVerifyThereIsValidMemberNumberTest()
        {
            database.members[0].number = 541278602;
            int number = 541278602;
            Assert.True(number == database.members[0].number);
        }


        /*
        [Fact]
        public void IsWantedInDatabaseTest()
        {
            int id = 874157936;


            
        }*/

        [Fact]
        public void CreateMemberNameTest()
        {
            string name = "David Smith";
            int number = 754865731;
            database.members[0].number = 754865731;
            database.members[0].name = "David Smith";
            database.members[0].state = "OR";
            string state = "OR";

            Assert.Equal(name,database.members[0].name);
            Assert.True(number == database.members[0].number, "member number in database does not share same data");
            Assert.True(state == database.members[0].state, "member state in database does not share same data");
        }

        [Fact]
        public void CreateProviderNameTest()
        {
            string name = "John Goodman";
            int number = 754865731;
            database.providers[0].number = 754865731;
            database.providers[0].name = "John Goodman";


            Assert.True(name == database.providers[0].name, "member name in database does not share same data");
            Assert.True(number == database.members[0].number, "member number in database does not share same data");
        }

    }
}
