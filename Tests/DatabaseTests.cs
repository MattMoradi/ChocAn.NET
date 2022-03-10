using ChocAn;
using System;
using System.IO;
using Xunit;
using Xunit.Abstractions;


namespace Tests
{
    public class Memberstest
    {
        private readonly Database.Members[] members = new Database.Members[999];

        [Fact]
        public void Validtest1()
        {
            members[0].status = (Database.Validity)0;
            int expected = 1;
            int actual = Member.is_valid(members, 0);
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void Validtest2()
        {
            members[3].status = (Database.Validity)2;
            int expected = 2;
            int actual = Member.is_valid(members, 3);
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void Validtest3()
        {
            members[0].status = (Database.Validity)1;
            int expected = 3;
            int actual = Member.is_valid(members, 0);
            Assert.Equal(expected, actual);
        }
    }
}