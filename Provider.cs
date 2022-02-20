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
            Console.WriteLine("ChocAn Terminal v1.0\n");
            Console.WriteLine("Hello Provider!");

            Console.WriteLine("Press any key to return to main menu...");
            Console.ReadKey();
        }
    }
}
