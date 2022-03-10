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
    }

}