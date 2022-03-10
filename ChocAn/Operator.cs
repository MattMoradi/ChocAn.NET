using System;
using System.Globalization;
using System.Linq;
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

        //Displays the main menu to the user. This function
        //takes the database as an argument
        private static void MainMenu(Database database)
        {
            string stringSelection;
            int intSelection;
            bool isDone = false;
            Console.Clear();

            do
            {
                Console.WriteLine("Select one of the options\n");
                Console.WriteLine("1. Modify a member\n");
                Console.WriteLine("2. Modify a provider\n");
                Console.WriteLine("3. Exit\n");
                stringSelection = Console.ReadLine();

                while (!ValidInput(stringSelection, out intSelection, 3))
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
                    default:
                        {
                            Console.WriteLine("ERROR: should not have gotten here...\nReturning to last menu\n");
                            isDone = true;
                            break;
                        }
                }
            } while (!isDone);
        }

        //Displays the member menu to the user. This function
        //takes the database as an argument
        private static void MemberMenu(Database database)
        {
            string stringSelection;
            int intSelection;
            bool isDone = false;
            Console.Clear();

            do
            {
                Console.WriteLine("Select one of the options\n");
                Console.WriteLine("1. Add new member\n");
                Console.WriteLine("2. Remove existing member\n");
                Console.WriteLine("3. Edit existing member Record\n");
                Console.WriteLine("4. Exit\n");
                stringSelection = Console.ReadLine();

                while (!ValidInput(stringSelection, out intSelection, 4))
                {
                    Console.WriteLine("Please enter a valid option\n");
                    stringSelection = Console.ReadLine();
                }

                Console.Clear();

                switch (intSelection)
                {
                    case 1:
                        AddPerson(database, "Member");
                        break;
                    case 2:
                        RemovePerson(database, "Member");
                        break;
                    case 3:
                        RecordModificationMenu(database, "Member");
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

        //Displays that provider menu to the user.This function
        //takes the database as an argument
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
                        RemovePerson(database, "Provider");
                        break;
                    case 3:
                        RecordModificationMenu(database, "Provider");
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

        //Get user input to add either a member or provider to the database.
        //This function takes the database and a string that represents the type of
        //account that should be added as arguments
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

            while (!int.TryParse(memberZipString, out zip) || (int.TryParse(memberZipString, out zip) && zip < 10000)
                || (int.TryParse(memberZipString, out zip) && zip > 99999))
            {
                Console.WriteLine("Enter a valid zip code\n");
                memberZipString = Console.ReadLine();
            }

            accountId = GenerateID(database, accountType);

            if (name.Length > 25)
                name = name.Substring(0, 25);
            if (address.Length > 25)
                address = address.Substring(0, 25);
            if (city.Length > 14)
                city = city.Substring(0, 14);
            if (state.Length > 2)
                state = state.Substring(0, 2);

            if (type == "Member")
                CreateMember(database, name, address, city, state, zip, accountId);
            else
                CreateProvider(database, name, address, city, state, zip, accountId);
        }

        //Get user input to remove either a member or provider from the database.
        //This function takes the database and a string that represents the type of
        //account that should be removed as arguments
        private static void RemovePerson(Database database, string type)
        {
            int idToRemove;
            string personString;

            if ((type == "Provider" && database.providers[0].name == null) || (type == "Member" && database.members[0].name == null))
            {
                Console.WriteLine("There are no " + type + "s to remove a record from. Returning to last menu\n");
                return;
            }

            Console.WriteLine("Enter the nine digit id of the " + type + " you want to remove\n");
            personString = Console.ReadLine();

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

        //Gets the account type and number of a member or provider to add a record to.
        //This function takes the database and the account type as a string as arguments.
        private static void AddRecord(Database database, string type)
        {
            string personString;
            int accountIndex;

            if ((type == "Provider" && database.providers[0].name == null) || (type == "Member" && database.members[0].name == null))
            {
                Console.WriteLine("There are no " + type + "s to add a record to. Returning to last menu\n");
                return;
            }

            Console.WriteLine("Enter the nine digit id of the " + type + " you want to add a record to\n");
            personString = Console.ReadLine();

            while (!IsWantedInDatabase(database, type, personString, out accountIndex))
            {
                Console.WriteLine("Please enter a valid id number\n");
                personString = Console.ReadLine();
            }

            if (type == "Member")
                AddMemberRecord(database, accountIndex);
            else
                AddProviderRecord(database, accountIndex);
        }

        //Lets user remove a record from either a member or provider. This function
        //takes the database and the account type as a string as arguments.
        private static void RemoveRecord(Database database, string type)
        {
            string personString;
            int accountIndex;

            if ((type == "Provider" && database.providers[0].name == null) || (type == "Member" && database.members[0].name == null))
            {
                Console.WriteLine("There are no " + type + "s to remove a record from. Returning to last menu\n");
                return;
            }

            Console.WriteLine("Enter the nine digit id of the " + type + " you want to add a record to\n");
            personString = Console.ReadLine();

            while (!IsWantedInDatabase(database, type, personString, out accountIndex))
            {
                Console.WriteLine("Please enter a valid id number\n");
                personString = Console.ReadLine();
            }

            if (type == "Member")
                RemoveMemberRecord(database, accountIndex);
            else
                RemoveProviderRecord(database, accountIndex);
        }

        //Lets user remove a record from a provider.This function takes the database
        //and the index of where the provider is located in the database as arguments
        private static void RemoveProviderRecord(Database database, int index)
        {
            string selectionString;
            int selectionInt;
            int count = 0;

            //find location of first null element in record array
            int lastEl = database.providers[index].records.Count(x => x.memberName != null);

            if (database.providers[index].records[0].memberName == null)
            {
                Console.WriteLine("This provider has no records to remove. Returning to last menu\n");
                return;
            }

            for (int i = 0; i < lastEl; i++)
            {
                count += 1;
                Console.WriteLine("Record # " + count);
                Console.WriteLine("Member Name: " + database.providers[index].records[i].memberName);
                Console.WriteLine("Member Number: " + database.providers[index].records[i].memberNumber);
                Console.WriteLine("Service Code: " + database.providers[index].records[i].serviceCode);
                Console.WriteLine("Date: " + database.providers[index].records[i].date);
                Console.WriteLine("-_-_-_-_-_-_-_-_-_-_\n");
            }

            Console.WriteLine("Enter the number of the record you want to remove\n");
            selectionString = Console.ReadLine();

            while (!int.TryParse(selectionString, out selectionInt) || (int.TryParse(selectionString, out selectionInt) && selectionInt > lastEl)
                || (int.TryParse(selectionString, out selectionInt) && selectionInt < 1))
            {
                Console.WriteLine("Enter a valid record number to remove");
                selectionString = Console.ReadLine();
            }

            selectionInt--;

            //check if record being removed is the last element in the array
            if (selectionInt == lastEl - 1)
            {
                database.providers[index].records[selectionInt].date = null;
                database.providers[index].records[selectionInt].memberName = null;
                database.providers[index].records[selectionInt].memberNumber = 0;
                database.providers[index].records[selectionInt].serviceCode = 0;
                database.providers[index].records[selectionInt].fee = 0;
                database.providers[index].consultations--;
                database.providers[index].records[selectionInt].comment = null;
            }
            else
            {//switch element that is being removed with last element's data
                database.providers[index].records[selectionInt].date = database.providers[index].records[lastEl - 1].date;
                database.providers[index].records[selectionInt].memberName = database.providers[index].records[lastEl - 1].memberName;
                database.providers[index].records[selectionInt].memberNumber = database.providers[index].records[lastEl - 1].memberNumber;
                database.providers[index].records[selectionInt].serviceCode = database.providers[index].records[lastEl - 1].serviceCode;
                database.providers[index].records[selectionInt].fee = database.providers[index].records[lastEl - 1].fee;
                database.providers[index].consultations--;
                database.providers[index].records[selectionInt].comment = database.providers[index].records[lastEl - 1].comment;

                //set last element in record array to null
                database.providers[index].records[lastEl - 1].date = null;
                database.providers[index].records[lastEl - 1].memberName = null;
                database.providers[index].records[lastEl - 1].memberNumber = 0;
                database.providers[index].records[lastEl - 1].serviceCode = 0;
                database.providers[index].records[lastEl - 1].fee = 0;
                database.providers[index].records[lastEl - 1].comment = null;
            }
            database.save2disk(database);
            Console.WriteLine("The record was succesfully removed\n");
        }

        //lets user remove a record from a member. This function takes the database
        //and the index of where the member is located in the database as arguments
        private static void RemoveMemberRecord(Database database, int index)
        {
            string selectionString;
            int selectionInt;
            int count = 0;

            //find location of first null element in record array
            int lastEl = database.members[index].records.Count(x => x.providerName != null);

            if (database.members[index].records[0].providerName == null)
            {
                Console.WriteLine("This member has no records to remove. Returning to last menu\n");
                return;
            }


            for (int i = 0; i < lastEl; i++)
            {
                count += 1;
                Console.WriteLine("Record # " + count);
                Console.WriteLine("Provider Name: " + database.members[index].records[i].providerName);
                Console.WriteLine("Service: " + database.members[index].records[i].service);
                Console.WriteLine("Date: " + database.members[index].records[i].date);
                Console.WriteLine("-_-_-_-_-_-_-_-_-_-_\n");
            }

            Console.WriteLine("\nEnter the number of the record you want to remove\n");
            selectionString = Console.ReadLine();

            while (!int.TryParse(selectionString, out selectionInt) || (int.TryParse(selectionString, out selectionInt) && selectionInt > lastEl)
                || (int.TryParse(selectionString, out selectionInt) && selectionInt < 1))
            {
                Console.WriteLine("Enter a valid record number to remove");
                selectionString = Console.ReadLine();
            }

            selectionInt--;

            //check if record being removed is the last element in the array
            if (selectionInt == lastEl - 1)
            {
                database.members[index].records[selectionInt].date = null;
                database.members[index].records[selectionInt].providerName = null;
                database.members[index].records[selectionInt].service = null;
            }
            else
            {//switch element that is being removed with last element's data
                database.members[index].records[selectionInt].date = database.members[index].records[lastEl - 1].date;
                database.members[index].records[selectionInt].providerName = database.members[index].records[lastEl - 1].providerName;
                database.members[index].records[selectionInt].service = database.members[index].records[lastEl - 1].service;

                //set last element in record array to null
                database.members[index].records[lastEl - 1].date = null;
                database.members[index].records[lastEl - 1].providerName = null;
                database.members[index].records[lastEl - 1].service = null;
            }
            database.save2disk(database);
            Console.WriteLine("The record was succesfully removed\n");
        }

        //displays the record modification menu to the user. This function takes the database
        //and a string representation of the account type that will be modified as arguments
        private static void RecordModificationMenu(Database database, string type)
        {
            string stringSelection;
            int intSelection;
            bool isDone = false;
            Console.Clear();

            do
            {
                Console.WriteLine("Select one of the options\n");
                Console.WriteLine("1. Add new record\n");
                Console.WriteLine("2. Remove existing record\n");
                Console.WriteLine("3. Modify existing record\n");
                Console.WriteLine("4. Exit\n");
                stringSelection = Console.ReadLine();

                while (!ValidInput(stringSelection, out intSelection, 4))
                {
                    Console.WriteLine("Please enter a valid option\n");
                    stringSelection = Console.ReadLine();
                }

                Console.Clear();

                switch (intSelection)
                {
                    case 1:
                        AddRecord(database, type);
                        break;
                    case 2:
                        RemoveRecord(database, type);
                        break;
                    case 3:
                        ModifyRecord(database, type);
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

        //Displays the modificy record menu to the user.This function takes the database
        //and a string representation of the account type that will be modified as arguments
        private static void ModifyRecord(Database database, string type)
        {
            string personString;
            int accountIndex;

            if ((type == "Provider" && database.providers[0].name == null) || (type == "Member" && database.members[0].name == null))
            {
                Console.WriteLine("There are no " + type + "s to modify. Returning to last menu\n");
                return;
            }

            Console.WriteLine("Enter the nine digit id of the " + type + " you want modify a record from\n");
            personString = Console.ReadLine();

            while (!IsWantedInDatabase(database, type, personString, out accountIndex))
            {
                Console.WriteLine("Please enter a valid id number\n");
                personString = Console.ReadLine();
            }

            if (type == "Member")
                ModifyMemberRecord(database, accountIndex);
            else
                ModifyProviderRecord(database, accountIndex);
        }

        //Lets user modify a provider record. This function takes the database
        //and the index of where the member or provider is stored in the database as arguments
        private static void ModifyProviderRecord(Database database, int index)
        {
            string selectionString;
            int recordIndex;
            string memberIdString;
            int memberIndex;
            int count = 0;
            string stringChoice;
            int intChoice;
            string serviceCodeString;
            int serviceCodeInt;
            bool isValidInt;
            bool isValidService;
            string date;
            string feeString;
            double feeDouble;
            DateTime result;
            string comment;

            //find location of first null element in record array
            int lastEl = database.providers[index].records.Count(x => x.memberName != null);

            if (database.providers[index].records[0].memberName == null)
            {
                Console.WriteLine("This provider has no records to modify. Returning to last menu\n");
                return;
            }

            for (int i = 0; i < lastEl; i++)
            {
                count += 1;
                Console.WriteLine("Record # " + count);
                Console.WriteLine("member name " + database.providers[index].records[i].memberName);
                Console.WriteLine("member number  " + database.providers[index].records[i].memberNumber);
                Console.WriteLine("Service code " + database.providers[index].records[i].serviceCode);
                Console.WriteLine("Computer time stamp " + database.providers[index].records[i].timestamp);
                Console.WriteLine("Date " + database.providers[index].records[i].date);
                Console.WriteLine("Fee " + database.providers[index].records[i].fee);
                Console.WriteLine("Comment " + database.providers[index].records[i].comment);
                Console.WriteLine("-_-_-_-_-_-_-_-_-_-_\n");
            }

            Console.WriteLine("\nEnter the number of the record you want to remove\n");
            selectionString = Console.ReadLine();

            while (!int.TryParse(selectionString, out recordIndex) || (int.TryParse(selectionString, out recordIndex) && recordIndex > lastEl)
                || (int.TryParse(selectionString, out recordIndex) && recordIndex < 1))
            {
                Console.WriteLine("Enter a valid record number to modify");
                selectionString = Console.ReadLine();
            }

            Console.WriteLine("Which field do you want to modify?\n");
            Console.WriteLine("1.Member\n");
            Console.WriteLine("2.Service code\n");
            Console.WriteLine("3.Date of the service\n");
            Console.WriteLine("4.Fee\n");
            Console.WriteLine("5.Comment\n");
            stringChoice = Console.ReadLine();

            while (!ValidInput(stringChoice, out intChoice, 5))
            {
                Console.WriteLine("Please enter a valid option\n");
                stringChoice = Console.ReadLine();
            }

            switch (intChoice)
            {
                case 1:
                    {
                        Console.WriteLine("Enter the nine digit id of the member who recieved the service \n");
                        memberIdString = Console.ReadLine();

                        while (!IsWantedInDatabase(database, "Member", memberIdString, out memberIndex))
                        {
                            Console.WriteLine("Please enter a valid id number\n");
                            memberIdString = Console.ReadLine();
                        }
                        database.providers[index].records[recordIndex - 1].memberName = database.members[memberIndex].name;
                        database.providers[index].records[recordIndex - 1].memberNumber = database.members[memberIndex].number;
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("Enter the service code for the service");
                        serviceCodeString = Console.ReadLine();
                        isValidInt = int.TryParse(serviceCodeString, out serviceCodeInt);
                        isValidService = Enum.IsDefined(typeof(Database.ProviderDirectory), serviceCodeInt);

                        //ensure user provides valid data
                        while ((!isValidInt && !isValidService) || (isValidInt && !isValidService))
                        {
                            Console.WriteLine("Please enter a valid service code\n");
                            serviceCodeString = Console.ReadLine();
                            isValidInt = int.TryParse(serviceCodeString, out serviceCodeInt);
                            isValidService = Enum.IsDefined(typeof(Database.ProviderDirectory), serviceCodeInt);
                        }

                        //string service = Enum.GetName(typeof(Database.ProviderDirectory), serviceCodeInt);
                        database.providers[index].records[recordIndex - 1].serviceCode = serviceCodeInt;
                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("Enter the date of the service (mm-dd-yyyy)");
                        date = Console.ReadLine();
                      
                        while (!DateTime.TryParse(date, out result))
                        {
                            Console.WriteLine("Enter a valid date in mm-dd-YYYY format\n");
                            date = Console.ReadLine();
                        }

                        date = result.ToString();
                        database.providers[index].records[recordIndex - 1].date = date;
                        break;
                    }
                case 4:
                    {
                        Console.WriteLine("Enter the cost of the service ($99,999.99 limit)\n");
                        feeString = Console.ReadLine();

                        while (!double.TryParse(feeString, out feeDouble) || (double.TryParse(feeString, out feeDouble) && feeDouble > 99999.99))
                        {
                            Console.WriteLine("Enter a valid cost for the service\n");
                            feeString = Console.ReadLine();
                        }
                        database.providers[index].records[recordIndex - 1].fee = feeDouble;
                        break;
                    }
                case 5:
                    {
                        Console.WriteLine("Enter a comment about the service (100 character limit)\n");
                        comment = Console.ReadLine();
                        database.providers[index].records[recordIndex - 1].comment = comment;
                        break;
                    }
                default:
                    {
                        Console.WriteLine("ERROR: should not have gotten here...\nReturning to last menu\n");
                        break;
                    }
            }
            database.save2disk(database);
            Console.WriteLine("Record was updated");
        }

        //Lets user modify a member record. This function takes the database
        //and the index of where the member or provider is stored in the database as arguments
        private static void ModifyMemberRecord(Database database, int index)
        {
            string selectionString;
            int recordIndex;
            string providerNumberString;
            int providerIndex;
            int count = 0;
            string stringChoice;
            int intChoice;
            string serviceCodeString;
            int serviceCodeInt;
            bool isValidInt;
            bool isValidService;
            string date;
            DateTime result;

            //find location of first null element in record array
            int lastEl = database.members[index].records.Count(x => x.providerName != null);

            if (database.members[index].records[0].providerName == null)
            {
                Console.WriteLine("This member has no records to modify. Returning to last menu\n");
                return;
            }

            for (int i = 0; i < lastEl; i++)
            {
                count += 1;
                Console.WriteLine("Record # " + count);
                Console.WriteLine("Provider Name: " + database.members[index].records[i].providerName);
                Console.WriteLine("Service: " + database.members[index].records[i].service);
                Console.WriteLine("Date: " + database.members[index].records[i].date);
                Console.WriteLine("-_-_-_-_-_-_-_-_-_-_\n");
            }

            Console.WriteLine("Enter the number of the record you want to remove\n");
            selectionString = Console.ReadLine();

            while (!int.TryParse(selectionString, out recordIndex) || (int.TryParse(selectionString, out recordIndex) && recordIndex > lastEl)
                || (int.TryParse(selectionString, out recordIndex) && recordIndex < 1))
            {
                Console.WriteLine("Enter a valid record number to modify");
                selectionString = Console.ReadLine();
            }

            Console.WriteLine("Which field do you want to modify?\n");
            Console.WriteLine("1.Provider's name\n");
            Console.WriteLine("2.Service provided\n");
            Console.WriteLine("3.Date of the service\n");
            stringChoice = Console.ReadLine();

            while (!ValidInput(stringChoice, out intChoice, 3))
            {
                Console.WriteLine("Please enter a valid option\n");
                stringChoice = Console.ReadLine();
            }

            switch (intChoice)
            {
                case 1:
                    {
                        Console.WriteLine("Enter the number of the provider who provided the serviced for " + database.members[index].name);
                        providerNumberString = Console.ReadLine();
                        while (!IsWantedInDatabase(database, "Provider", providerNumberString, out providerIndex))
                        {
                            Console.WriteLine("Enter a valid provider number\n");
                            providerNumberString = Console.ReadLine();
                        }
                        database.members[index].records[recordIndex - 1].providerName = database.providers[providerIndex].name;
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("Enter the service code for the service");
                        serviceCodeString = Console.ReadLine();
                        isValidInt = int.TryParse(serviceCodeString, out serviceCodeInt);
                        isValidService = Enum.IsDefined(typeof(Database.ProviderDirectory), serviceCodeInt);

                        //ensure user provides valid data
                        while ((!isValidInt && !isValidService) || (isValidInt && !isValidService))
                        {
                            Console.WriteLine("Please enter a valid service code\n");
                            serviceCodeString = Console.ReadLine();
                            isValidInt = int.TryParse(serviceCodeString, out serviceCodeInt);
                            isValidService = Enum.IsDefined(typeof(Database.ProviderDirectory), serviceCodeInt);
                        }

                        string service = Enum.GetName(typeof(Database.ProviderDirectory), serviceCodeInt);
                        database.members[index].records[recordIndex - 1].service = service;
                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("Enter the date of the service (mm-dd-yyyy)");
                        date = Console.ReadLine();
                        
                        while (!DateTime.TryParse(date, out result))
                        {
                            Console.WriteLine("Enter a valid date in mm-dd-YYYY format\n");
                            date = Console.ReadLine();
                        }

                        date = result.ToString();
                        database.members[index].records[recordIndex - 1].date = date;
                        break;
                    }
                default:
                    {
                        Console.WriteLine("ERROR: should not have gotten here...\nReturning to last menu\n");
                        break;
                    }
            }
            database.save2disk(database);
            Console.Write("Record was updated");
        }

        //Lets user a record to a provider. This function takes the database
        //and the index where the provider is located in the database as arguments
        private static void AddProviderRecord(Database database, int index)
        {
            string date;
            string feeString;
            double feeDouble;
            string memberIdString;
            int memberIndex;
            string serviceCodeString;
            int serviceCodeInt;
            bool isValidService;
            string comment;
            bool isValidInt;
            DateTime result;

            if (index >= database.members.Length || index < 0)
            {
                Console.WriteLine("ERROR: attempted to access element out of bounds...\n Returning to last menu");
                return;
            }

            if (database.members[0].name == null)
            {
                Console.WriteLine("There are no members in the database to add a record about. Returning to last menu\n");
                return;
            }

            Console.WriteLine("Enter the nine digit id of the member who recieved the service \n");
            memberIdString = Console.ReadLine();

            while (!IsWantedInDatabase(database, "Member", memberIdString, out memberIndex))
            {
                Console.WriteLine("Please enter a valid id number\n");
                memberIdString = Console.ReadLine();
            }
           
            Console.WriteLine("Enter the date of the service (mm-dd-yyyy)");
            date = Console.ReadLine();

            while (!DateTime.TryParse(date, out result))
            {
               Console.WriteLine("Enter a valid date in dd-mm-yyyy format\n");
               date = Console.ReadLine();
            }

           date = result.ToString();


            Console.WriteLine("Enter the service code of the service");
            serviceCodeString = Console.ReadLine();

            isValidInt = int.TryParse(serviceCodeString, out serviceCodeInt);
            isValidService = Enum.IsDefined(typeof(Database.ProviderDirectory), serviceCodeInt);

            //ensure user provides valid data
            while ((!isValidInt && !isValidService) || (isValidInt && !isValidService))
            {
                Console.WriteLine("Please enter a valid service code\n");
                serviceCodeString = Console.ReadLine();
                isValidInt = int.TryParse(serviceCodeString, out serviceCodeInt);
                isValidService = Enum.IsDefined(typeof(Database.ProviderDirectory), serviceCodeInt);
            }

            Console.WriteLine("Do you want to add a comment about this service? (y/n)\n");
            comment = Console.ReadLine();

            if (comment.Length > 1)
                comment = comment.Substring(0, 1);

            if (comment == "y" || comment == "Y")
            {
                Console.WriteLine("Enter a comment about the service (100 character limit)\n");
                comment = Console.ReadLine();
            }
            else
                comment = "";

            Console.WriteLine("Enter the cost of the service ($99,999.99 limit)\n");
            feeString = Console.ReadLine();

            while (!double.TryParse(feeString, out feeDouble) || (double.TryParse(feeString, out feeDouble) && feeDouble > 99999.99))
            {
                Console.WriteLine("Enter a valid cost for the service\n");
                feeString = Console.ReadLine();
            }

            if (comment.Length > 100)
                comment = comment.Substring(0, 100);

            //get next empty element in record array
            int i = database.providers[index].records.Count(x => x.memberName != null);

            database.providers[index].records[i].timestamp = DateTime.Now;
            database.providers[index].records[i].date = date;
            database.providers[index].records[i].memberName = database.members[memberIndex].name;
            database.providers[index].records[i].memberNumber = database.members[memberIndex].number;
            database.providers[index].records[i].serviceCode = serviceCodeInt;
            database.providers[index].records[i].fee = feeDouble;
            database.providers[index].consultations++;
            database.providers[index].records[i].comment = comment;

            database.save2disk(database);
            Console.WriteLine("Provider record was succesfully added\n");
        }


        //Lets user add a record to an existing member. This function takes the
        //database and the index where the member is located in the database as arguments
        private static void AddMemberRecord(Database database, int index)
        {
            string date;
            string providerNumberString;
            string service;
            int providerIndex;
            string serviceCodeString;
            int serviceCodeInt;
            bool isValidService;
            bool isValidInt;
            DateTime result;

            if (database.providers[0].name == null)
            {
                Console.WriteLine("There are no providers in the database to preform the service. Returning to last menu\n");
                return;
            }

            Console.WriteLine("Enter the date of the service (mm-dd-yyyy)");
            date = Console.ReadLine();

            while (!DateTime.TryParse(date, out result))
            {
                Console.WriteLine("Enter a valid date in mm-dd-YYYY format\n");
                date = Console.ReadLine();
            }

            date = result.ToString();

            Console.WriteLine("Enter the number of the provider who provided the serviced for " + database.members[index].name);
            providerNumberString = Console.ReadLine();

            while (!IsWantedInDatabase(database, "Provider", providerNumberString, out providerIndex))
            {
                Console.WriteLine("Enter a valid provider number\n");
                providerNumberString = Console.ReadLine();
            }

            Console.WriteLine("Enter the service code of the service");
            serviceCodeString = Console.ReadLine();

            isValidInt = int.TryParse(serviceCodeString, out serviceCodeInt);
            isValidService = Enum.IsDefined(typeof(Database.ProviderDirectory), serviceCodeInt);

            while ((!isValidInt && !isValidService) || (isValidInt && !isValidService))
            {
                Console.WriteLine("Please enter a valid service code\n");
                serviceCodeString = Console.ReadLine();
                isValidInt = int.TryParse(serviceCodeString, out serviceCodeInt);
                isValidService = Enum.IsDefined(typeof(Database.ProviderDirectory), serviceCodeInt);
            }

            //convert enum value into a string
            service = Enum.GetName(typeof(Database.ProviderDirectory), serviceCodeInt);

            //get next empty element in record array
            int i = database.members[index].records.Count(x => x.providerName != null);

            //set record data to member
            database.members[index].records[i].date = date;
            database.members[index].records[i].providerName = database.providers[providerIndex].name;
            database.members[index].records[i].service = service;
            database.save2disk(database);
            Console.WriteLine("Member record was succesfully added\n");
        }

        //Remove member from database.This function takes the database
        //and the id of the member that is being removed as arguments
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
            if (!found)
                Console.WriteLine("Member was not found\n");
            else
                Console.WriteLine("Member was successfully removed\n");
        }

        //Remove provider from database. This function takes the database
        //and the id of the provider that is being removed as arguments
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

        //Creates a member account by taking the database and the member fields as arguments
        private static void CreateMember(Database database, string name, string address, string city, string state, int zip, int id)
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
            Console.WriteLine(database.members[mNumMembers - 1].name + "'s member id is " + database.members[mNumMembers - 1].number + "\n");
        }

        //Creates a provider account by taking the database and the provider fields as arguments
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
            Console.WriteLine(database.providers[mNumProviders - 1].name + "'s provider id is " + database.providers[mNumProviders - 1].number+ "\n");
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

        ///Generates a random 9 digit id number. This function takes
        //the database and the account type as arguments
        private static int GenerateID(Database database, string type)
        {
            Random rand = new Random();
            int randNum = rand.Next(100000000, 999999999);
            int i = 0;

            if (type == "Member")
            {
                //while there is a valid member
                while (database.members[i].name != null)
                {
                    //if random number matches number in database
                    //reset counter and generate new random number
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
                //while there is a valid provider
                while (database.providers[i].name != null)
                {
                    //if random number matches number in database
                    //reset counter and generate new random number
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
            return randNum;
        }

        //Function to check if a member or provider is in the database and is the one trying to be found.
        //If the member or provider is found, their name, account number, and address will be displayed.
        //The user will be prompted to either enter 'y' or 'n' to determine if the account being displayed
        //is the one they are looking for. This function will return true if it is the account they were looking for
        //false if it is not the account they were looking for. This function takes the database, a string representation
        //of the account type trying to be found, a string representation of an account id, and an index where the account
        //is located in the database. This funciton will also set the 'index' argument
        //to the element the account is being stored, or -1 if it was not found or not wanted
        private static bool IsWantedInDatabase(Database database, string type, string stringId, out int index)
        {
            bool foundAndWanted = false;
            index = -1;
            string choice;
            int id;

            if (!int.TryParse(stringId, out id))
                return foundAndWanted;

            if (type == "Member")
            {
                for (int i = 0; i < mNumMembers; i++)
                {
                    //if id is found, display member name, number, and address
                    if (database.members[i].number == id)
                    {
                        Console.WriteLine("Name: " + database.members[i].name);
                        Console.WriteLine("Member Number: " + database.members[i].number);
                        Console.WriteLine("Address: " + database.members[i].address);
                        Console.WriteLine("Is this the member you want to add a record to? (y/n)\n");
                        choice = Console.ReadLine();

                        //check if user input needs to be truncated
                        if (choice.Length > 1)
                            choice = choice.Substring(0, 1);

                        //check if member displayed is the member they were looking for
                        if (choice == "y" || choice == "Y")
                        {
                            foundAndWanted = true;
                            index = i;
                            return foundAndWanted;
                        }
                        else
                        {
                            index = -1;
                            return foundAndWanted;
                        }
                    }
                }
                return foundAndWanted;
            }
            else
            {
                for (int i = 0; i < mNumProviders; i++)
                {
                    //if id is found, display provider name, number, and address
                    if (database.providers[i].number == id)
                    {
                        Console.WriteLine("Name: " + database.providers[i].name);
                        Console.WriteLine("Member Number: " + database.providers[i].number);
                        Console.WriteLine("Address: " + database.providers[i].address);
                        Console.WriteLine("Is this the provider you want to add a record to? (y/n)\n");
                        choice = Console.ReadLine();

                        //check if user input needs to be truncated
                        if (choice.Length > 1)
                            choice = choice.Substring(0, 1);

                        //check if member displayed is the member they were looking for
                        if (choice == "y" || choice == "Y")
                        {
                            foundAndWanted = true;
                            index = i;
                            return foundAndWanted;
                        }
                        else
                        {
                            index = -1;
                            return foundAndWanted;
                        }
                    }
                }
                return foundAndWanted;
            }
        }
    }
}