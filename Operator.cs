using System;
using System.Collections.Generic;
using System.Text;

namespace ChocAn
{
    static class Operator
    {

        public static void OperatorMain()
        {
            Console.Clear();
            Console.WriteLine("ChocAn Terminal v1.0\n");
            Console.WriteLine("Hello Operator!");
            MainMenu();
        }

        private static void MainMenu()
        {
            string stringSelection = "";
            int intSelection = 0;
            bool isDone = false;
            Console.Clear();
            do
            {
                Console.WriteLine("Select one of the options\n");
                Console.WriteLine("1. Modify a member\n");
                Console.WriteLine("2. Modify a provider\n");
                Console.WriteLine("3. Exit\n");
                stringSelection = Console.ReadLine();

                //check if user provided valid input
                while (!ValidInput(stringSelection, out intSelection, 3))
                {
                    Console.WriteLine("Please enter a valid option\n");
                    stringSelection = Console.ReadLine();
                }

                Console.Clear();

                switch (intSelection)
                {
                    case 1:
                        MemberMenu();
                        break;
                    case 2:
                        ProviderMenu();
                        break;
                    case 3:
                        isDone = true;
                        break;
                    default:
                        {
                            Console.WriteLine("ERROR: should not have gotten here...\nReturning to last menu\n");
                            isDone = true;
                            break;
                        } 
                }
            } while (!isDone);
        }

        private static void MemberMenu()
        {
            string stringSelection = "";
            int intSelection = 0;
            bool isDone = false;
            Console.Clear();
            do
            {
                Console.WriteLine("Select one of the options\n");
                Console.WriteLine("1. Add new member\n");
                Console.WriteLine("2. Remove existing member\n");
                Console.WriteLine("3. Edit existing member\n");
                Console.WriteLine("4. Exit\n");
                stringSelection = Console.ReadLine();

                //check if user provided valid input
                while (!ValidInput(stringSelection, out intSelection,4))
                {
                    Console.WriteLine("Please enter a valid option\n");
                    stringSelection = Console.ReadLine();
                }

                Console.Clear();

                switch (intSelection)
                {
                    case 1:
                        Console.WriteLine("Add new member not implemented\n");
                        break;
                    case 2:
                        Console.WriteLine("Remove member not implemented\n");
                        break;
                    case 3:
                        Console.WriteLine("Edit member not implemented\n");
                        break;
                    case 4:
                        isDone = true;
                        break;
                    default:
                        Console.WriteLine("ERROR: should not have gotten here...\nReturning to last menu\n");
                        break;
                }
            } while (!isDone);
        }

        private static void ProviderMenu()
        {
            string stringSelection = "";
            int intSelection = 0;
            bool isDone = false;
            Console.Clear();
            do
            {
                Console.WriteLine("Select one of the options\n");
                Console.WriteLine("1. Add new provider\n");
                Console.WriteLine("2. Remove existing provider\n");
                Console.WriteLine("3. Edit existing provider\n");
                Console.WriteLine("4. Exit\n");
                stringSelection = Console.ReadLine();

                //check if user provided valid input
                while (!ValidInput(stringSelection, out intSelection, 4))
                {
                    Console.WriteLine("Please enter a valid option\n");
                    stringSelection = Console.ReadLine();
                }

                Console.Clear();

                switch (intSelection)
                { 
                    case 1:
                        Console.WriteLine("Add new provider not implemented\n");
                        break;
                    case 2:
                        Console.WriteLine("Remove provider not implemented\n");
                        break;
                    case 3:
                        Console.WriteLine("Edit provider not implemented\n");
                        break;
                    case 4:
                        isDone = true;
                        break;
                    default:
                    {
                        Console.WriteLine("ERROR: should not have gotten here...\nReturning to last menu\n");
                        isDone = true;
                        break;
                    }   
                }  
            } while (!isDone);
        }

        //Helper function to check if user provided valid input
        private static bool ValidInput(string stringSelection, out int intSelection, int  numChoices)
        {
            //try and parse user input into an int
            bool validInput = int.TryParse(stringSelection, out intSelection);
            
            //check if user provided an int and the selection is a valid option
            if(!validInput || intSelection < 1 || intSelection > numChoices)
                return false;
            return true;
        }

        //Remove
        private static bool RemoveMember()
        {
            string memberNumber = "";
            Console.WriteLine("Enter the number of the member you want to remove\n");
            memberNumber = Console.ReadLine();
            return true;
        }

        //Add a member to the database. Returns
        //true if the member was successfully added,
        //false if the member was not added
        //TODO: Validate ints to make sure user does not put in
        //characters other than numbers into field
        private static bool AddMember()
        {
            string memberName = "";
            int memberId = -1;
            string memberAddress = "";
            string memberCity = "";
            string memberState = "";
            int memberZip = -1;

            Console.WriteLine("Enter the first and last name of the member (25 character limit)\n");
            memberName = Console.ReadLine();

            Console.WriteLine("Enter the member number\n");
            memberId = Convert.ToInt32(Console.ReadLine());
            //going to want to check database after this line to make sure no two IDs are the same

            Console.WriteLine("Enter the member's address (25 character limit)\n");
            memberAddress = Console.ReadLine();
            Console.WriteLine("Enter the member's city (14 character limit)\n");
            memberCity = Console.ReadLine();
            Console.WriteLine("Enter the two letter state abbreviation\n");
            memberState = Console.ReadLine();
            Console.WriteLine("Enter the member's zip code\n");
            memberZip = Convert.ToInt32(Console.ReadLine());

            //place all gathered information into member class and add to database
            //return true if member was added, false if member was not added

            return true;
        }
    }
}
