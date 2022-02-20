using System;
using System.Collections.Generic;
using System.Text;

namespace ChocAn
{
    class Manager
    {
        public static void ManagerMain(Database.Providers[] providers)
        {
            Console.Clear();
            Console.WriteLine("ChocAn Terminal v1.0\n");
            Console.WriteLine("Hello Manager!");
            Console.WriteLine("Press any key to return to main menu...");
            Console.ReadKey();
        }
    }
}