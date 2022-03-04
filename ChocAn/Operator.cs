using System;
using System.Collections.Generic;
using System.Text;

namespace ChocAn
{
    static class Operator
    {
        private static int mNumMembers = 0;
        private static int mNumProviders = 0;
        public static void OperatorMain(Database database)
        {
            Console.Clear();
            Console.WriteLine("ChocAn.NET Terminal v1.0\n");
            Console.WriteLine("Hello Operator!");

            MainMenu(database);
        }

        private static void MainMenu(Database database)
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
                Console.WriteLine("4. Display Database\n");
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
                        MemberMenu(database);
                        break;
                    case 2:
                        ProviderMenu(database);
                        break;
                    case 3:
                        isDone = true;
                        break;
                    case 4:
                        DisplayDataBase(database);
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

        private static void MemberMenu(Database database)
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
                while (!ValidInput(stringSelection, out intSelection, 4))
                {
                    Console.WriteLine("Please enter a valid option\n");
                    stringSelection = Console.ReadLine();
                }

                Console.Clear();

                switch (intSelection)
                {
                    case 1:
                        AddMember(database);
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

        private static void ProviderMenu(Database database)
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
        private static bool ValidInput(string stringSelection, out int intSelection, int numChoices)
        {
            //try and parse user input into an int
            bool validInput = int.TryParse(stringSelection, out intSelection);

            //check if user provided an int and the selection is a valid option
            if (!validInput || intSelection < 1 || intSelection > numChoices)
                return false;
            return true;
        }

        //Remove
        private static void RemoveMember(Database database)
        {
            int memberIdRemove = 0;
            string memberString;
            int i = 0;
            bool found = false;
            Console.WriteLine("Enter the six digit id of the member you want to remove\n");
            memberString = Console.ReadLine();

            //ensure user provides valid data
            while (((!int.TryParse(memberString, out memberIdRemove)) && memberIdRemove < 100000) || memberString != "exit")
            {
                Console.WriteLine("Enter a valid member id or type 'exit' to return to the last menu\n");
                memberString = Console.ReadLine();
            }


            //while there is a valid member at index i
            while (database.members[i].name != null && !found)
            {
                //compare the current member at index i to the id to be removed
                if (database.members[i].number == memberIdRemove)
                {
                    //check if member to remove is at the end of the array
                    if (i + 1 == mNumMembers)
                    {
                        database.members[i].name = null;
                        database.members[i].number = 0;
                        database.members[i].city = null;
                        database.members[i].address = null;
                        database.members[i].state = null;
                        database.members[i].status = 0;
                        database.members[i].records = null;
                    }
                    //move last member of array into the place of the member being removed
                    else
                    {
                        //update data
                        database.members[i].name = database.members[mNumMembers - 1].name;
                        database.members[i].number = database.members[mNumMembers - 1].number;
                        database.members[i].city = database.members[mNumMembers - 1].city;
                        database.members[i].address = database.members[mNumMembers - 1].address;
                        database.members[i].state = database.members[mNumMembers - 1].state;
                        database.members[i].status = database.members[mNumMembers - 1].status;
                        database.members[i].records = database.members[mNumMembers - 1].records;

                        //set last element to null
                        database.members[i].name = null;
                        database.members[i].number = 0;
                        database.members[i].city = null;
                        database.members[i].address = null;
                        database.members[i].state = null;
                        database.members[i].status = 0;
                        database.members[i].records = null;
                    }
                    found = true;
                    --mNumMembers;
                }
                i++;
            }


        }


        private static void AddMember(Database database)
        {
            int memberId;
            string memberZipString;

            Console.WriteLine("Enter the first and last name of the member (25 character limit)\n");
            database.members[mNumMembers].name = Console.ReadLine();
            Console.WriteLine("Enter the member's address (25 character limit)\n");
            database.members[mNumMembers].address = Console.ReadLine();
            Console.WriteLine("Enter the member's city (14 character limit)\n");
            database.members[mNumMembers].city = Console.ReadLine();
            Console.WriteLine("Enter the two letter state abbreviation\n");
            database.members[mNumMembers].state = Console.ReadLine();
            Console.WriteLine("Enter the member's zip code\n");
            memberZipString = Console.ReadLine();

            //try and parse zip code into an int
            while (!int.TryParse(memberZipString, out database.members[mNumMembers].zip))

            {
                Console.WriteLine("Enter a valid zip code\n");
                memberZipString = Console.ReadLine();
            }

            //generate member id
            memberId = GenerateID(database, "Member");
            //set member status to valid
            database.members[mNumMembers].status = (Database.Validity)0;

            //truncate all data to character limit
            if (database.members[mNumMembers].name.Length > 25)
                database.members[mNumMembers].name = database.members[mNumMembers].name.Substring(0, 25);
            if (database.members[mNumMembers].address.Length > 25)
                database.members[mNumMembers].address = database.members[mNumMembers].address.Substring(0, 25);
            if (database.members[mNumMembers].city.Length > 25)
                database.members[mNumMembers].city = database.members[mNumMembers].city.Substring(0, 25);
            if (database.members[mNumMembers].state.Length > 2)
                database.members[mNumMembers].state = database.members[mNumMembers].state.Substring(0, 2);
            database.members[mNumMembers].number = memberId;
            mNumMembers++;
        }

        //Generate a random ID number.
        private static int GenerateID(Database database, string type)
        {
            Random rand = new Random();
            int randNum = rand.Next(100000000, 999999999);
            int i = 0;

            if (type == "Member")
            {
                while (database.members[i].number != 0)
                {
                    //if random number matches number in database
                    //reset counter and generate new random number
                    if (database.members[i].number == randNum)
                    {
                        i = 0;
                        randNum = rand.Next(1000, 9999);
                    }
                    else
                        i++;
                }
            }
            else if (type == "Provider")
            {
                while (database.providers[i].number != 0)
                {
                    //if random number matches number in database
                    //reset counter and generate new random number
                    if (database.providers[i].number == randNum)
                    {
                        i = 0;
                        randNum = rand.Next(1000, 9999);
                    }
                    else
                        i++;
                }


            }
            else
                return -1;

            return randNum;
        }

        private static void DisplayDataBase(Database database)
        {
            int total = 0;
            int members = 0;
            int providers = 0;
            int i = 0;
            while (database.members[i].address != null)
            {
                Console.WriteLine("Name: " + database.members[i].name);
                Console.WriteLine("Address: " + database.members[i].address);
                Console.WriteLine("Zip: " + database.members[i].zip);
                Console.WriteLine("State: " + database.members[i].state);
                Console.WriteLine("Number: " + database.members[i].number);
                Console.WriteLine("City: " + database.members[i].city);
                Console.WriteLine("Validity: " + database.members[i].status);
                i++;
            }
            i = 0;
            Console.WriteLine("\n\nProviders\n\n");
            while (database.providers[i].address != null)
            {
                Console.WriteLine("Name: " + database.providers[i].name);
                Console.WriteLine("Address: " + database.providers[i].address);
                Console.WriteLine("Zip: " + database.providers[i].zip);
                Console.WriteLine("State: " + database.providers[i].state);
                Console.WriteLine("Number: " + database.providers[i].number);
                Console.WriteLine("City: " + database.providers[i].city);
                i++;
            }



            Console.WriteLine("Members displayed: " + members + "\n");
            Console.WriteLine("Providers displayed: " + providers + "\n");
            Console.WriteLine("Total displayed: " + total + "\n");
        }
    }
}
