using System;
using System.Collections.Generic;
using System.Text;

namespace ChocAn
{
    class Member
    {
        public static void MemberMain(Database.Members[] members)
        {
            Console.Clear();
            Console.WriteLine("ChocAn.NET Terminal v1.0\n");
            Console.WriteLine("Hello Member!");

            /* // some example member database calls
            members[0].name = "John Johnson";
            members[0].number = 421;
            members[0].address = "johnny street";
            members[0].address = "Johnsonville";
            members[0].state = "JJ";
            members[0].status = (Database.Validity)2;

            // instantiation necessary for writing to records
            members[0].records = new Database.MemberRecords[15];
            members[0].records[0].date = "02-24-2022";
            members[0].records[1].date = "02-25-2022";
            */

            Console.WriteLine("Press any key to return to main menu...");
            Console.ReadKey();
        }
    }
}
