using System;
using System.Threading;
using Figgle;

namespace ChocAn
{
    class Program
    {
        static void Main(string[] args)
        {
            bool quit = false;

            Console.WriteLine("ChocAn.NET Terminal v1.0\n");

            Database data = new Database();
            data.persistence(data, true);

            while(!quit)
            {
                Console.Clear();
                Console.WriteLine("ChocAn.NET Terminal v1.0\n");
                Console.WriteLine(FiggleFonts.Standard.Render("ChocAn.NET"));
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
                    case "5":
                        data.save2disk(data);
                        quit = true; break;
                    default: 
                        Console.WriteLine("Invalid Input!");
                        Console.ReadKey(); break;
                }
            }
        }
    }
}