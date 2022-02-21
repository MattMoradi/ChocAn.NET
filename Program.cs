using System;

namespace ChocAn
{
    class Program
    {
        static void Main(string[] args)
        {
            bool quit = false;

            Database data = new Database();

            while(!quit)
            {
                Console.Clear();
                Console.WriteLine("ChocAn.NET Terminal v1.0\n");
                Console.WriteLine("--- Select Your Role: ---\n");
                Console.WriteLine("1) Operator");
                Console.WriteLine("2) Member");
                Console.WriteLine("3) Provider");
                Console.WriteLine("4) Manager");
                Console.WriteLine("5) Quit \n");
                Console.Write("Input Selection: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1": Operator.OperatorMain(data); break;
                    case "2": Member.MemberMain(data.members); break;
                    case "3": Provider.ProviderMain(data); break;
                    case "4": Manager.ManagerMain(data.providers); break;
                    case "5": quit = true; break;
                    default: Console.WriteLine("Invalid Input!"); break;
                }
            }
        }
    }
}