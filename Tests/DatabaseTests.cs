using ChocAn;
using System;
using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace Tests
{
    public class DatabaseTests
    {
        private readonly ITestOutputHelper Output;
        private readonly char slash;
        private readonly Database database;

        public DatabaseTests(ITestOutputHelper Output)
        {
            this.Output = Output;
            slash = Path.DirectorySeparatorChar;
            database = new Database();
        }

        [Fact]
        public void ProviderDirectoryTest()
        {

            Assert.True(File.Exists("ProviderDirectory.txt"));
            Output.WriteLine(Environment.CurrentDirectory + slash + "ProviderDirectory.txt");
        }

        [Fact]
        public void EFTTest()
        {
            database.writeEFT(database);
            Assert.True(File.Exists("EFT.txt"));
        }

        [Fact]
        public void DBSaveTest()
        {
            database.save2disk(database);
            Assert.True(Directory.Exists("Members"));
            Assert.True(Directory.Exists("Providers"));
        }

        [Fact]
        public void PersistenceTest()
        {
            if (Directory.Exists("Members") || Directory.Exists("Providers"))
            {
                Directory.Delete("Members", true);
                Directory.Delete("Providers", true);
            }

            database.members[0].name = "John Johnson";
            database.members[0].number = 12345678;
            database.members[0].address = "Johnny Street";
            database.members[0].city = "jville";
            database.members[0].state = "JJ";
            database.members[0].zip = 12345;
            database.members[0].status = (Database.Validity)2;

            database.save2disk(database);

            Database test = new Database();

            test.persistence(test, false);

            Assert.Equal("John Johnson", test.members[0].name);
            Assert.Equal(12345678, test.members[0].number);
            Assert.Equal("Johnny Street", test.members[0].address);
            Assert.Equal("jville", test.members[0].city);
            Assert.Equal("JJ", test.members[0].state);
            Assert.Equal(12345, test.members[0].zip);
            Assert.Equal((Database.Validity)2, test.members[0].status);
        }
    }
}