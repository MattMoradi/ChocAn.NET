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
            PopulateAccounts(database);
            MainMenu(database);
        }

        //Displays the main menu to the user
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
                Console.WriteLine("4. Display Database\n");//will be removed when testing is done
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
                        DisplayDatabase(database);
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

        //Displays the member options to the user
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
                        AddPerson(database,"Member");
                        break;
                    case 2:
                        //Console.WriteLine("Remove member not implemented\n");
                        RemovePerson(database, "Member");
                        break;
                    case 3:
                        Console.WriteLine("Edit member not implemented\n");
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

        //Displays that provider options to the user
        private static void ProviderMenu(Database database)
        {
            string stringSelection;
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
                        AddPerson(database, "Provider");
                        break;
                    case 2:
                        //  Console.WriteLine("Remove provider not implemented\n");
                        RemovePerson(database, "Provider");
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

        //work in prog
        private static void AddRecord(Database database, string type)
        {
            int idRecordToAdd = 0;
            string personString;

            Console.WriteLine("Enter the nine digit id of the " + type + " you want to add a record to\n");
            personString = Console.ReadLine();

            //ensure user provides valid data
            while (!int.TryParse(personString, out idRecordToAdd) && !isInDatabase(database,type, idRecordToAdd))
            {
                Console.WriteLine("Please enter a valid id number\n");
                personString = Console.ReadLine();
            }

            if (type == "Member")
                AddMemberRecord(database, idRecordToAdd);
            else
                RemvoveProvider(database, idRecordToAdd);
        }

        private static void AddMemberRecord(Database database, int index)
        {
            Console.WriteLine("Enter the data of the service (MM-DD-YYYY)");
            database.members[index].records[database.members[index].records.Length - 1].date = Console.ReadLine();
            Console.WriteLine("Enter the name of the provider who provided the serviced for " + database.members[index].name);
            database.members[index].records[database.members[index].records.Length - 1].providerName = Console.ReadLine();
            Console.WriteLine("Enter the number of the service provided");
            database.members[index].records[database.members[index].records.Length - 1].service = Console.ReadLine();
        }


        //Helper function to check if user provided valid input. The first argument
        //is a string representation of the user input. The second argument is the int representation of
        //the user input. The third argument is the number of options the user can select from the menu.
        //Will return true if the string was able to be parsed as an int. False if it was not or if the user
        //selected an invalid menu option.
        private static bool ValidInput(string stringSelection, out int intSelection, int numChoices)
        {
            //try and parse user input into an int
            bool validInput = int.TryParse(stringSelection, out intSelection);

            //check if user provided an int and the selection is a valid option
            if (!validInput || intSelection < 1 || intSelection > numChoices)
                return false;
            return true;
        }

        //Remove member from database.This function takes the database
        //and the id of the member that is being removed
        private static void RemoveMember(Database database, int id)
        {
            int i = 0;
            bool found = false;
           
            //while there is a valid member at index i
            while (database.members[i].name != null && !found)
            {
                //compare the current member at index i to the id to be removed
                if (database.members[i].number == id)
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
                        //replace data at the elemet that is being removed with last element data
                        database.members[i].name = database.members[mNumMembers - 1].name;
                        database.members[i].number = database.members[mNumMembers - 1].number;
                        database.members[i].city = database.members[mNumMembers - 1].city;
                        database.members[i].address = database.members[mNumMembers - 1].address;
                        database.members[i].state = database.members[mNumMembers - 1].state;
                        database.members[i].status = database.members[mNumMembers - 1].status;
                        database.members[i].records = database.members[mNumMembers - 1].records;
                        database.members[i].zip = database.members[mNumMembers - 1].zip;

                        //set last element to null
                        database.members[mNumMembers - 1].name = null;
                        database.members[mNumMembers - 1].number = 0;
                        database.members[mNumMembers - 1].city = null;
                        database.members[mNumMembers - 1].address = null;
                        database.members[mNumMembers - 1].state = null;
                        database.members[mNumMembers - 1].status = 0;
                        database.members[mNumMembers - 1].records = null;
                        database.members[mNumMembers - 1].zip = 0;
                    }
                    found = true;
                    --mNumMembers;
                    database.save2disk(database);
                }
                i++;
            }
            if(!found)
                Console.WriteLine("Member was not found\n");
            else
                Console.WriteLine("Member was successfully removed\n");
        }

        //Remove provider from datrabase. This function takes the database
        //and the id of the provider that is being removed
        private static void RemvoveProvider(Database database, int id)
        {
            bool found = false;
            int i = 0;

            //while there is a valid provider at index i
            while (database.providers[i].name != null && !found)
            {
                //compare the current provider at index i to the id to be removed
                if (database.providers[i].number == id)
                {
                    //check if member to remove is at the end of the array
                    if (i + 1 == mNumProviders)
                    {
                        database.providers[i].name = null;
                        database.providers[i].number = 0;
                        database.providers[i].city = null;
                        database.providers[i].address = null;
                        database.providers[i].state = null;
                        database.providers[i].totalFee = 0;
                        database.providers[i].consultations = 0;
                        database.providers[i].records = null;
                        database.providers[i].zip = 0;
                    }
                    else
                    {
                        //replace data at the element that is being removed with last element data
                        database.providers[i].name = database.providers[mNumProviders - 1].name;
                        database.providers[i].number = database.providers[mNumProviders - 1].number;
                        database.providers[i].city = database.providers[mNumProviders - 1].city;
                        database.providers[i].address = database.providers[mNumProviders - 1].address;
                        database.providers[i].state = database.providers[mNumProviders - 1].state;
                        database.providers[i].totalFee = database.providers[mNumProviders - 1].totalFee;
                        database.providers[i].consultations = database.providers[mNumProviders - 1].consultations;
                        database.providers[i].records = database.providers[mNumProviders - 1].records;
                        database.providers[i].zip = database.providers[mNumProviders - 1].zip;

                        //set last element to null in provider array
                        database.providers[mNumProviders - 1].name = null;
                        database.providers[mNumProviders - 1].number = 0;
                        database.providers[mNumProviders - 1].city = null;
                        database.providers[mNumProviders - 1].address = null;
                        database.providers[mNumProviders - 1].state = null;
                        database.providers[mNumProviders - 1].totalFee = 0;
                        database.providers[mNumProviders - 1].consultations = 0;
                        database.providers[mNumProviders - 1].records = null;
                        database.providers[mNumProviders - 1].zip = 0;
                    }
                    found = true;
                    --mNumProviders;
                    database.save2disk(database);
                }
                i++;
            }
            if (!found)
                Console.WriteLine("Provider was not found\n");
            else
                Console.WriteLine("Provider was successfully removed\n");
        }

        //Get user input to remove either a member or provider from the database.
        //This function takes the database and a string that represents the type of
        //account that should be removed
        private static void RemovePerson(Database database, string type)
        {
            int idToRemove = 0;
            string personString;

            Console.WriteLine("Enter the nine digit id of the " + type + " you want to remove\n");
            personString = Console.ReadLine();

            //ensure user provides valid data
            while (!int.TryParse(personString, out idToRemove))
            {
                Console.WriteLine("Please enter a valid id number\n");
                personString = Console.ReadLine();
            }

            if (type == "Member")
                RemoveMember(database, idToRemove);
            else
                RemvoveProvider(database, idToRemove);
        }

        //Get user input to add either a member or provider to the database.
        //This function takes the database and a string that represents the type of
        //account that should be added
        private static void AddPerson(Database database, string type)
        {
            int accountId;
            int zip;
            string memberZipString;
            string accountType;
            string name;
            string address;
            string city;
            string state;

            //check what kind of account is being created
            if (type == "Member")
                accountType = "Member";
            else
                accountType = "Provider";

            //gather user input
            Console.WriteLine("Enter the " + accountType + "'s first and last name (25 character limit)\n");
            name = Console.ReadLine();
            Console.WriteLine("Enter the " + accountType + "'s address (25 character limit)\n");
            address = Console.ReadLine();
            Console.WriteLine("Enter the " + accountType + "'s city (14 character limit)\n");
            city = Console.ReadLine();
            Console.WriteLine("Enter the " + accountType + "'s two letter state abbreviation\n");
            state = Console.ReadLine();
            Console.WriteLine("Enter the " + accountType + "'s zip code\n");
            memberZipString = Console.ReadLine();

            //try and parse zip code into an int
            while (!int.TryParse(memberZipString, out zip))
            {
                Console.WriteLine("Enter a valid zip code\n");
                memberZipString = Console.ReadLine();
            }

            //generate account id
            accountId = GenerateID(database, accountType);

            //truncate all data to character limit
            if (name.Length > 25)
                name = name.Substring(0, 25);
            if (address.Length > 25)
                address = address.Substring(0, 25);
            if (city.Length > 14)
                city = city.Substring(0, 14);
            if (state.Length > 2)
                state = state.Substring(0, 2);

            //copy data into proper account type
            if (type == "Member")
                CreateMember(database, name, address, city, state, zip, accountId);
            else
                CreateProvider(database, name, address, city, state, zip, accountId);

        }

        //Creates a member account by taking the database and the member fields
        private static void CreateMember(Database database, string name, string address, string city, string state, int zip,int id)
        {
            database.members[mNumMembers].name = name;
            database.members[mNumMembers].address = address;
            database.members[mNumMembers].city = city; 
            database.members[mNumMembers].state = state;
            database.members[mNumMembers].zip = zip;
            database.members[mNumMembers].status = (Database.Validity)0;
            database.members[mNumMembers].number = id;
            database.members[mNumMembers].records = new Database.MemberRecords[50];
            mNumMembers++;
            database.save2disk(database);
            Console.WriteLine("Member was successfully created\n");
        }

        //Creates a provider account by taking the database and the provider fields
        private static void CreateProvider(Database database, string name, string address, string city, string state, int zip, int id)
        {
            database.providers[mNumProviders].name = name;
            database.providers[mNumProviders].address = address;
            database.providers[mNumProviders].city = city;
            database.providers[mNumProviders].state = state;
            database.providers[mNumProviders].zip = zip;
            database.providers[mNumProviders].consultations = 0;
            database.providers[mNumProviders].totalFee = 0;
            database.providers[mNumProviders].records = new Database.ProviderRecords[50];
            database.providers[mNumProviders].number = id;
            mNumProviders++;
            database.save2disk(database);
            Console.WriteLine("Provider was successfully created\n");
        }

        //Generate a random  9 digit id number.
        //Regenerate id if match has NOT been tested
        private static int GenerateID(Database database, string type)
        {
            Random rand = new Random();
            int randNum = rand.Next(100000000, 999999999);
            int i = 0;

            if (type == "Member")
            {
                while (database.members[i].name != null)
                {
                    //if random number matches number in database
                    //reset counter and generates new random number
                    if (database.members[i].number == randNum)
                    {
                        i = 0;
                        randNum = rand.Next(100000000, 999999999);
                    }
                    else
                        i++;
                }
            }
            else if (type == "Provider")
            {

                while (database.providers[i].name != null)
                {
                    //if random number matches number in database
                    //reset counter and generates new random number
                    if (database.providers[i].number == randNum)
                    {
                        i = 0;
                        randNum = rand.Next(100000000, 999999999);
                     
                    }
                    else
                        i++;
                }
            }
            else
                return -1;
            Console.WriteLine("moving onto next account\n");
            return randNum;
        }

        //Display the content of the database for testing
        private static void DisplayDatabase(Database database)
        {
            int members = 0;
            int providers = 0;
            int i = 0;

            while (database.members[i].address != null)
            {
                Console.WriteLine("Name: " + database.members[i].name);
                Console.WriteLine("Address: " + database.members[i].address);
                Console.WriteLine("City: " + database.members[i].city);
                Console.WriteLine("State: " + database.members[i].state);
                Console.WriteLine("Zip: " + database.members[i].zip);
                Console.WriteLine("Number: " + database.members[i].number);
                Console.WriteLine("Validity: " + database.members[i].status);
                Console.WriteLine("\n");
                i++;
                ++members;
            }

            i = 0;
            Console.WriteLine("\n-_-Providers-_-\n");

            while (database.providers[i].address != null)
            {
                Console.WriteLine("Name: " + database.providers[i].name);
                Console.WriteLine("Address: " + database.providers[i].address);
                Console.WriteLine("City: " + database.providers[i].city);
                Console.WriteLine("State: " + database.providers[i].state);
                Console.WriteLine("Zip: " + database.providers[i].zip);
                Console.WriteLine("Number: " + database.providers[i].number);
                Console.WriteLine("\n");
                i++;
                ++providers;
            }
            int total = providers + members;
            Console.WriteLine("Members displayed: " + members + "\n");
            Console.WriteLine("Providers displayed: " + providers + "\n");
            Console.WriteLine("Total displayed: " + total + "\n");
        }

        //Test function to populate member and provider accounts. This function will be removed when testing is completed
        private static void PopulateAccounts(Database database)
        {
            //providers
           // CreateProvider(database, "Steve Harvey", "123 fake st", "Tigard", "OR", 97223, GenerateID(database, "Provider"));
           // CreateProvider(database, "Jennifer Montano", "2060 Hide A Way Road", "Santa Clara", "CA", 95050, GenerateID(database, "Provider"));
          //  CreateProvider(database, "David Smith", "2174 Jehovah Drive", "Rocky Mount", "VA", 24151, GenerateID(database, "Provider"));
          //  CreateProvider(database, "Jared Williams", "1151 Khale Street", "Murrells Inlet", "SC", 29576, GenerateID(database, "Provider"));
          //  CreateProvider(database, "Ann Guajardo", "126 Valley Drive", "Norristown", "PA", 19403, GenerateID(database, "Provider"));
            //members
            CreateMember(database, "Lisa J Macklin", "4429 Jerry Dove Drive", "Myrtle Beach", "SC", 29577, GenerateID(database, "Member"));
            CreateMember(database, "Mary B Kirkland", "1703 Point Street", "Chicago", "IL", 60620, GenerateID(database, "Member"));
            CreateMember(database, "Louis C Quinn", "2603 Short Street", "Austin", "TX", 78660, GenerateID(database, "Member"));
            CreateMember(database, "Lorraine J Mayer", "2849 Heritage Road", "Madera", "CA", 93638, GenerateID(database, "Member"));
            CreateMember(database, "David R Kean", "1146 Hickman Street", "Chicago", "IL", 60654, GenerateID(database, "Member"));

        }

        private static bool isInDatabase(Database database, string type, int id)
        {
            bool found = false;
            if(type == "Member")
            {
               for(int i=0; i < mNumMembers; i++)
                {
                    if (database.members[i].number == id)
                        found = true;
                }
            }
            else
            {
                for (int i = 0; i < mNumProviders; i++)
                {
                    if (database.providers[i].number == id)
                        found = true;
                }
            }
            return found;
        }
    }
}
