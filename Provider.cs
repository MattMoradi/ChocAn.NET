using System;
using System.Collections.Generic;
using System.Text;

namespace ChocAn
{
    static class Provider
    {
        public static void ProviderMain(Database database)
        {
            Console.Clear();
            Console.WriteLine("ChocAn.NET Terminal v1.0\n");
            Console.WriteLine("Hello Provider!");

            /* // some example database calls
            database.providers[0].name = "freddy";
            database.providers[0].number = 421;
            database.providers[0].totalFee = 420.99;

            database.providers[1].name = "jhonny";
            database.providers[1].number = 4421;
            database.providers[1].totalFee = 599.99;

            // instantiation necessary for writing to records
            database.providers[0].records = new Database.ProviderRecords[999];
            database.providers[0].records[0].date = "02-20-2022";

            database.writeEFT(database);

            //database.save2disk(database);
            */

            Console.WriteLine("Press any key to return to main menu...");
            Console.ReadKey();
        }
    }
}
