using System;

namespace ChocAn
{
    class Program
    {
        static void Main(string[] args)
        {
            bool quit = false;

            while(!quit)
            {
                Console.Clear();
                Console.WriteLine("ChocAn Terminal v1.0\n");
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
                    case "1": Operator.OperatorMain(); break;
                    case "2": Member.MemberMain(); break;
                    case "3": Provider.ProviderMain(); break;
                    case "4": Manager.ManagerMain(); break;
                    case "5": quit = true; break;
                    default: Console.WriteLine("Invalid Input!"); break;
                }
            }
        }
    }
}
