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
            ManagerMenu();          //Where most of the magic happens
            Console.WriteLine("Press any key to return to main menu...");
            Console.ReadKey();
        }

        private static void ManagerMenu() 
        {   //Manager menu bracket
            string tempString = "";
            int tempInt = 0;
            bool menuActive = true;
            Console.Clear();
            //Looping menu -----
            while (menuActive)
            {
                //Main menu prompt -----
                Console.WriteLine("Select one of the options\n");
                Console.WriteLine("1. Do stuff haha\n");
                Console.WriteLine("2. Do other stuff\n");
                Console.WriteLine("3. Exit\n");
                tempString = Console.ReadLine();
                //Main menu prompt ^^^^^
                //check if user provided valid input -----
                while (!ValidInput(tempString, out tempInt, 3))
                {
                    Console.WriteLine("Please enter a valid option such as 1, 2, or 3\n");
                    Console.Out.Flush();
                    tempString = Console.ReadLine();
                } //check if user provided valid input ^^^^^
                //Main menu switch -----
                switch (tempInt) 
                {
                    case 1:
                        Console.WriteLine("This is where funtion A would run\n");
                        //FunctionA();                //Operation A
                        break;
                    case 2:
                        Console.WriteLine("This is where funtion B would run\n");
                        //FunctionB();                //Operation B 
                        break;
                    case 3:
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
    } //Class Bracket
} //Namespace Bracket