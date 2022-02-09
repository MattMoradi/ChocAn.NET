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
            Console.WriteLine("Press any key to return to main menu...");
            Console.ReadKey();
        }

        private static void MainMenu()
        {
            string stringSelection;
            bool validInput;
            int intSelection;

            Console.WriteLine("Select one of the options\n");
            Console.WriteLine("1. Modify a member\n");
            Console.WriteLine("2. Modify an operator\n");
            Console.WriteLine("3. Exit\n");
            stringSelection = Console.ReadLine();
            
            //try to parse user input into an int
            validInput = int.TryParse(stringSelection, out intSelection);

            //validate user provided proper input
            while(!validInput || intSelection < 1 || intSelection > 3)
            {
                Console.WriteLine("Please enter a valid option\n");
                stringSelection = Console.ReadLine();
                validInput = int.TryParse(stringSelection, out intSelection);
            }

            if(intSelection == 1)
                MemberMenu();
            else if(intSelection == 2)
                OperatorMenu();
            else
                return;
        }

        private static void MemberMenu()
        {
            Console.WriteLine("Select one of the options\n");
            Console.WriteLine("1. Add new member\n");
            Console.WriteLine("2. Remove existing member\n");
            Console.WriteLine("3. Edit existing member\n");
            Console.WriteLine("4. Exit\n");
        }

        private static void OperatorMenu()
        {
            Console.WriteLine("Select one of the options\n");
            Console.WriteLine("1. Add new operator\n");
            Console.WriteLine("2. Remove existing operator\n");
            Console.WriteLine("3. Edit existing operator\n");
        }




        //Remove
        private static bool RemoveMember()
        {

            return true;
        }
        
        //Add a member to the database. Returns
        //true if the member was successfully added,
        //false if the member was not added
        //TODO: Validate ints to make sure user does not put in
        //characters other than numbers into field
        private static bool AddMember()
        {
            string memberName;
            int memberId;
            string memberAddress;
            string memberCity;
            string memberState;
            int memberZip;

            Console.WriteLine("Enter the first and last name of the member (25 character limit)\n");
            memberName = Console.ReadLine();

            Console.WriteLine("Enter the member number\n");
            memberId= Convert.ToInt32(Console.ReadLine());
            //going to want to check database after this line to make sure no two IDs are the same

            Console.WriteLine("Enter the member's address (25 character limit)\n");
            memberAddress= Console.ReadLine();
            Console.WriteLine("Enter the member's city (14 character limit)\n");
            memberCity= Console.ReadLine();
            Console.WriteLine("Enter the two letter state abbreviation\n");
            memberState= Console.ReadLine();
            Console.WriteLine("Enter the member's zip code\n");
            memberZip= Convert.ToInt32(Console.ReadLine());

        //place all gatehred information into member class and add to database
        //return true if member was added, false member was not added

            return true;
        }
    }
}
