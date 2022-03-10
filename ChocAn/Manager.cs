using System;
using System.Collections.Generic;
using System.Text;

namespace ChocAn
{ //Namespace Bracket
    class Manager
    { //Class bracket
        public static void ManagerMain(Database.Providers[] providers)
        {

            Console.Clear();
            Console.WriteLine("ChocAn.NET Terminal v1.0\n");
            Console.WriteLine("Hello Manager!");
            ManagerMenu(providers);          //Where most of the magic happens
            Console.WriteLine("Press any key to return to the console menu...");
            Console.ReadKey();
        }

        private static void ManagerMenu(Database.Providers[] providers) 
        {   //Manager menu bracket
            string tempString = "";
            int tempInt = 0;
            bool menuActive = true;
            
            //Looping menu -----
            while (menuActive)
            {
                Console.Clear();
                //Main menu prompt -----
                Console.WriteLine("Select one of the options\n");
                Console.WriteLine("1. Fill the first 5 slots with predictable garbage for testing.\n");
                Console.WriteLine("2. List provider info and fees due.\n"); 
                Console.WriteLine("3. Display Data summary.\n");
                Console.WriteLine("4. Exit back to terminal menu.\n");
                tempString = Console.ReadLine();
                //Main menu prompt ^^^^^
                //check if the user provided valid input -----
                while (!ValidInput(tempString, out tempInt, 4))
                {
                    Console.WriteLine("Please enter a valid option such as 1, 2, 3, or 4\n");
                    Console.Out.Flush();
                    tempString = Console.ReadLine();
                } //check if the user provided valid input ^^^^^
                Console.Clear();
                //Main menu switch -----
                switch (tempInt) 
                {
                    case 1:
                        Console.WriteLine("Generating testing garbage data... \n");
                        FunctionA(providers);                //Generates garbage data for testing
                        Console.WriteLine("\n\nPress any key to return to the console menu...");
                        Console.ReadKey();
                        Console.WriteLine("Returning to manager menu...");
                        break;
                    case 2:
                        Console.WriteLine("Gathering data... \n");
                        FunctionB(providers);                //Display provider names, number of fees, consultations, and total fees.
                        Console.WriteLine("\n\nPress any key to return to the console menu...");
                        Console.ReadKey();
                        Console.WriteLine("Returning to manager menu...");
                        break;
                    case 3:
                        Console.WriteLine("Gathering data summary... ");
                        FunctionC(providers);               //Display the data summary
                        Console.WriteLine("\n\nPress any key to return to the console menu...");
                        Console.ReadKey();
                        Console.WriteLine("Returning to manager menu...");
                        break;
                    case 4:
                        Console.WriteLine("Exit confirmed... \n");
                        menuActive = false;         //The only way to properly exit the menu
                        break;
                    default:
                        {
                            Console.WriteLine("ERROR: input recognition failed...\nReturning to menu\n");
                            break;
                        } 
                } //Main menu switch bracket
                
            } //Looping menu bracket
            
        }   //Manager menu bracket
        private static bool ValidInput(string stringSelection, out int intSelection, int  numChoices)
        {
            //try and parse user input into an int
            bool validInput = int.TryParse(stringSelection, out intSelection);
            
            //check if user provided an int and the selection is a valid option
            if(!validInput || intSelection < 1 || intSelection > numChoices)
                return false;
            return true;
        }
        private static bool ValidInputD(string stringSelection, out double floatSelection, double numChoices)
        {
            //try and parse user input into an int
            bool validInput = double.TryParse(stringSelection, out floatSelection);

            //check if user provided an int and the selection is a valid option
            if (!validInput || floatSelection > numChoices)
                return false;
            return true;
        }

        private static void FunctionA(Database.Providers[] providers)                 //junk data filler for testing
        {
            bool runA = true;
            Console.WriteLine("Single line grid display activated...");
            short i = 0;
            while (i < 5)
            {
                Console.WriteLine("filling Provider #" + i + " with junk data...");
                providers[i].name = "freddy" + i;
                providers[i].number = 100 + i;
                providers[i].consultations = i;
                ++providers[i].consultations;
                providers[i].totalFee = 1000.10 + i;
                Console.WriteLine("slot " + i + " filled... ");
                ++i;
            }
            while(runA)
            {
                Console.WriteLine("Press 1 to manually add more garbage or press 2 to exit garbage generation.");
                string tempSA = "";
                int ii = 0;
                tempSA = Console.ReadLine();
                //check if the user provided valid input -----
                while (!ValidInput(tempSA, out ii, 2))
                {
                    Console.WriteLine("Please enter 1 to add garbage or 2 to stop adding garbage.\n");
                    Console.Out.Flush();
                    tempSA = Console.ReadLine();
                } //check if the user provided valid input ^^^^^
                if (ii == 1) 
                {
                    while (providers[i].name != null && i < 999) //Make sure we are indexed to the first empty data slot
                    {
                        ++i;
                    }
                    Console.WriteLine("Next empty provider slot is slot #" + i +"\nEnter the name of the next provider...");
                    providers[i].name = Console.ReadLine();

                    Console.WriteLine("Now enter the provider number (up to 999999)...");
                    tempSA = Console.ReadLine();
                    //check if the user provided valid input -----
                    while (!ValidInput(tempSA, out ii, 999999))
                    {
                        Console.WriteLine("Please enter a valid option such as any number between 1 and 999999.\n");
                        Console.Out.Flush();
                        tempSA = Console.ReadLine();
                    } //check if the user provided valid input ^^^^^
                    providers[i].number = ii;

                    Console.WriteLine("Now enter the number of consultations (up to 999)...");
                    tempSA = Console.ReadLine();
                    //check if the user provided valid input -----
                    while (!ValidInput(tempSA, out ii, 999))
                    {
                        Console.WriteLine("Please enter a valid option such as any number between 1 and 999.\n");
                        Console.Out.Flush();
                        tempSA = Console.ReadLine();
                    } //check if the user provided valid input ^^^^^
                    providers[i].consultations = Convert.ToInt16(ii);

                    double iid = 0;
                    Console.WriteLine("Finally enter the total fees (up to 999.99)...");
                    tempSA = Console.ReadLine();
                    //check if the user provided valid input -----
                    while (!ValidInputD(tempSA, out iid, 999.99))
                    {
                        Console.WriteLine("Please enter a valid option such as any number between 1 and 999.99.\n");
                        Console.Out.Flush();
                        tempSA = Console.ReadLine();
                    } //check if the user provided valid input ^^^^^
                    providers[i].totalFee = iid;
                }
                if (ii == 2)
                {
                    runA = false;
                    Console.WriteLine("Done entering junk data.");
                }
            }
        }

        private static void FunctionB(Database.Providers[] providers)                 //Display names, provider numbers, consultations, and total fees
        {
            Console.WriteLine("Provider list display activated... \nProvider, Provider Number, Number of consultations (C), Total Fee");
            int j = 0;
            while (providers[j].name != null)
            {
                Console.WriteLine("...");
                Console.Write(providers[j].name + ",");
                Console.Write("#" + providers[j].number + ",");
                Console.Write("C" + providers[j].consultations + ",");
                Console.WriteLine("$" + providers[j].totalFee);
                ++j;
            }
            Console.WriteLine("End of data.");
        }
        private static void FunctionC(Database.Providers[] providers)               //Display a summary of the data organized in a legible format
        {
            int k = 0;
            short kc = 0;
            decimal kk = 0;
            while (providers[k].name != null)
            {
                kk += Convert.ToDecimal(providers[k].totalFee);
                kc += Convert.ToInt16((decimal)providers[k].consultations);
                ++k;
            }
            //Console.WriteLine("Yo this is function C running....\nIt currently doesn't do anything besides this statement.");
            Math.Round(kk, 2);
            Console.Out.Flush();
            Console.WriteLine("There are " + k + " total providers.");
            Console.WriteLine("There are " + kc + " total consultations.");
            Console.WriteLine("$" + kk + " Is the total sum of provider fees.");
        }
    } //Class Bracket
} //Namespace Bracket

