using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ChocAn
{
    public class Provider
    {
        public static bool MemberNumValid = false;
        public static int MemberNumber;
        public static bool providerFound = false;
        public static int providerIndex = 0;
        public static string memberstatus = "";
        public static int memberNumInput;
        public static int providerNumbInput;
        public static void ProviderMain(Database database)
        {
            bool quit = false;
            while (!quit)
            {
                Console.Clear();
                Console.WriteLine("ChocAn.NET Terminal v1.0\n");
                Console.WriteLine("Hello Provider!");
                Console.WriteLine("Select an option:\n");
                Console.WriteLine("1) Validate a member");
                Console.WriteLine("2) Bill ChocAn");
                Console.WriteLine("3) Request Provider Directory");
                Console.WriteLine("4) Display Provider records");
                Console.WriteLine("5) Total Fees");
                Console.WriteLine("6) Return to main menu \n");
                Console.Write("Input Selection: ");
                
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ValidateMember(database);
                        break;
                    case "2":
                        Bill(database);
                        break;
                    case "3":
                        DisplayDirectory();
                        break;
                    case "4":
                        DisplayProviderRecords(database);
                        break;
                    case "5":
                        TotalFees(database);
                        break;
                    case "6":
                        quit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid Input!");
                        break;
                }
            }
        }
        public static void ValidateMember(Database database)
        {
            MemberNumValid = false;
            bool quit = false;
            while (!quit)
            {
                Console.Write("Enter the member number:");
                memberNumInput = Convert.ToInt32(Console.ReadLine());

                if(memberNumInput != 0) ValidateMemberSearch(database);

                if (MemberNumValid == true)
                {
                    Console.WriteLine("Validated");
                    Console.WriteLine("You may proceed to bill.");
                    Console.Write("Press any key to continue:");
                    Console.ReadKey();
                    quit = true;
                }
                else
                {
                    if (memberstatus == "0") Console.WriteLine("The member is suspended.");
                    else if (memberstatus == "1") Console.WriteLine("The member validity status is invalid.");
                    else
                    {
                        Console.WriteLine("The number you entered is invalid.");
                        

                    }
                    Console.Write("Press any key to try another member number or ");
                    Console.Write("press 0 to go back to the provider menu:");
                    var input = Console.ReadLine();
                    if (input == "0") quit = true;
                }
            }
        }
        public static void ValidateMemberSearch(Database database)
        {
            for (int i = 0; i < database.members.Length; i++)
            {
                if (memberNumInput == database.members[i].number)
                {
                    if (database.members[i].status == Database.Validity.Suspended) memberstatus = "0";
                    else if (database.members[i].status == Database.Validity.Invalid) memberstatus = "1";
                    else MemberNumValid = true;
                    MemberNumber = memberNumInput;
                }
            }
        }
        public static void Bill(Database database)
        {
            Console.Clear();
            if (MemberNumValid == false) ValidateMember(database);
            Console.Clear();

            if (MemberNumValid && !providerFound)
            {
                Console.Write("Enter your provider number:");
                providerNumbInput = Convert.ToInt32(Console.ReadLine());

                if(providerNumbInput != 0) FindProvider(database);
                if (!providerFound)
                {
                    Console.WriteLine("Wrong provider number.");
                    Console.Write("Press any key to go back to the main provider menu:");
                    Console.ReadKey();
                }
            }

            if (MemberNumValid && providerFound)
            {

                if (database.providers[providerIndex].records == null)  database.providers[providerIndex].records = new Database.ProviderRecords[999];

                int i = 0;
                int recordIndex = 0;
                while (database.providers[providerIndex].records[i].memberName != null)
                {
                    i++;
                    if (database.providers[providerIndex].records[i].memberName == null) recordIndex = i;
                }
                database.providers[providerIndex].records[recordIndex].timestamp = DateTime.Now;
                Console.Write("Enter the date that the service was provided in the MM-DD-YYYY format:");
                var serviceDate = Console.ReadLine();
                database.providers[providerIndex].records[recordIndex].date = serviceDate;

                Console.Write("Enter the member's name:");
                database.providers[providerIndex].records[recordIndex].memberName = Console.ReadLine();
                database.providers[providerIndex].records[recordIndex].memberNumber = MemberNumber;

                bool quit = false;
                Console.WriteLine("If you know the service code and want to enter it now, enter 1.");
                Console.WriteLine("Otherwise, enter 2 to request Provider Directory:");
                Console.Write("Input Selection: ");
                var input1 = Console.ReadLine();
                int serviceCodeInput = 0;
                bool serviceCodeValid = false;
                string serviceCodeName = "";
                if (input1 == "1")
                {
                    Console.Write("Enter the service code:");
                    serviceCodeInput = Convert.ToInt32(Console.ReadLine());
                    foreach (var data in Enum.GetNames(typeof(Database.ProviderDirectory)))
                    {
                        if (serviceCodeInput == (int)Enum.Parse(typeof(Database.ProviderDirectory), data))
                        {
                            serviceCodeValid = true;
                            serviceCodeName = data.ToString();
                        }
                    }
                }
                if (input1 == "2") DisplayDirectory();
                var VerifyInput = "";

                if (!serviceCodeValid)
                {
                    while (!quit)
                    {
                        Console.WriteLine("\nSelect an option:\n");
                        Console.WriteLine("2) To request Provider Directory.");
                        Console.WriteLine("3) To enter the service code.");
                        Console.WriteLine("4) To go back to the main provider menu.");
                        Console.Write("Input Selection: ");
                        var input = Console.ReadLine();
                        
                        if (input == "2") DisplayDirectory();
                        if (input == "3" || input1 == "2" && input != "4")
                        {
                            Console.Write("\nEnter the service code:");
                            serviceCodeInput = Convert.ToInt32(Console.ReadLine());
                            foreach (var data in Enum.GetNames(typeof(Database.ProviderDirectory)))
                            {
                                if (serviceCodeInput == (int)Enum.Parse(typeof(Database.ProviderDirectory), data))
                                {
                                    serviceCodeValid = true;
                                    serviceCodeName = data.ToString();
                                }
                            }
                        }
                        if (input == "4" || serviceCodeValid) quit = true;
                    }
                }
                if (serviceCodeValid)
                {
                    Console.Write("The name of the service corresponding to the code you entered is: ");
                    Console.WriteLine(serviceCodeName);
                    Console.Write("If this is the correct service, enter 0 to verify, otherwise enter 1:");
                    VerifyInput = Console.ReadLine();
                    if (VerifyInput == "1") serviceCodeValid = false;
                }
                if (serviceCodeValid && VerifyInput == "0")
                {
                    Console.WriteLine("Would you like to add a comment about the service?");
                    Console.Write("Enter 1 for yes or 0 for no:");
                    var commentchoice = Console.ReadLine();
                    if (commentchoice == "1")
                    {
                        Console.Write("Enter the comment:");
                        var comment = Console.ReadLine();
                        if (comment.Length > 100) comment = comment.Substring(0, 100);
                        database.providers[providerIndex].records[recordIndex].comment = comment;
                    }
                    database.providers[providerIndex].records[recordIndex].serviceCode = serviceCodeInput;
                    string line = File.ReadLines("ProviderDirectory.txt").FirstOrDefault(x => x.StartsWith(serviceCodeName));
                    string[] textSplit = line.Split("$");
                    Console.WriteLine("The fee to be paid for this service is ${0} \n\n", textSplit[1]);
                    database.providers[providerIndex].records[recordIndex].fee = Convert.ToDouble(textSplit[1]);
                    database.providers[providerIndex].consultations += 1;
                    database.writeEFT(database);


                    //For verification purposes
                    Console.WriteLine("For verification, you will now be asked to enter the following:");
                    Console.WriteLine("the date, member name and number, service code, and fee to be paid.");
                    Console.WriteLine("Whatever isn't the same as what was entered before will be replaced by what you enter here.");
                    Console.Write("Date:");
                    var VerifyDate = Console.ReadLine();
                    if (VerifyDate != serviceDate) database.providers[providerIndex].records[recordIndex].date = VerifyDate;
                    else Console.WriteLine("Date verified");

                    Console.Write("Member name:");
                    var VerifyMemberName = Console.ReadLine();
                    if (VerifyMemberName != database.providers[providerIndex].records[recordIndex].memberName) database.providers[providerIndex].records[recordIndex].memberName = VerifyMemberName;
                    else Console.WriteLine("Member name verified");

                    Console.Write("Member number:");
                    int VerifyMemberNumber = Convert.ToInt32(Console.ReadLine());
                    if (VerifyMemberNumber != MemberNumber) database.providers[providerIndex].records[recordIndex].memberNumber = VerifyMemberNumber;
                    else Console.WriteLine("Member number verified");

                    Console.Write("Service code:");
                    int VerifyServiceCode = Convert.ToInt32(Console.ReadLine());
                    if (VerifyServiceCode != serviceCodeInput) database.providers[providerIndex].records[recordIndex].serviceCode = VerifyServiceCode;
                    else Console.WriteLine("Service code verified");

                    Console.Write("The fee to be paid:");
                    double Verifyfee = Convert.ToDouble(Console.ReadLine());
                    if (Verifyfee != database.providers[providerIndex].records[recordIndex].fee)
                    {
                        if (Verifyfee > 999.99)
                        {
                            while (Verifyfee > 999.99)
                            {
                                Console.Write("The fee must be above 999.99:");
                                Verifyfee = Convert.ToDouble(Console.ReadLine());
                            }
                        }
                        if (Verifyfee <= 999.99) database.providers[providerIndex].records[recordIndex].fee = Verifyfee;
                    }
                    else Console.WriteLine("Fee verified");
                    Console.Write("Press any key to go back to the main provider menu:");
                    Console.ReadKey();
                }
            }
        }
        public static void FindProvider(Database database)
        {
            for (int i = 0; i < database.providers.Length; i++)
            {
                if (providerNumbInput == database.providers[i].number)
                {
                    providerIndex = i;
                    providerFound = true;
                }
            }
        }
        public static void DisplayDirectory()
        {
            Console.Clear();
            Console.Write("\n");
            try
            {
                using (var sr = new StreamReader("ProviderDirectory.txt"))
                {
                    Console.WriteLine(sr.ReadToEnd());
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            Console.Write("Press any key to continue:");
            Console.ReadKey();
        }
        public static void DisplayProviderRecords(Database database)
        {
            Console.Clear();
            if (!providerFound)
            {
                Console.Write("Enter your provider number:");
                int providerNumbInput = Convert.ToInt32(Console.ReadLine());

                for (int i = 0; i < database.providers.Length; i++)
                {
                    if (providerNumbInput == database.providers[i].number)
                    {
                        providerIndex = i;
                        providerFound = true;
                    }
                }
                if (!providerFound)
                {
                    Console.WriteLine("Wrong provider number.");
                    Console.Write("Press any key to go back to the main provider menu:");
                    Console.ReadKey();
                }
            }
            if(providerFound)
            {
                Console.WriteLine("Provider information:");
                Console.WriteLine("Name: {0}", database.providers[providerIndex].name);
                Console.WriteLine("Number: {0}", database.providers[providerIndex].number);
                Console.WriteLine("Street address: {0}", database.providers[providerIndex].address);
                Console.WriteLine("City: {0}", database.providers[providerIndex].city);
                Console.WriteLine("State: {0}", database.providers[providerIndex].state);
                Console.WriteLine("Zip code: {0}\n", database.providers[providerIndex].zip);
                for (int i = 0; database.providers[providerIndex].records[i].memberName != null; i++)
                {
                    Console.WriteLine("\nDate of service: {0}", database.providers[providerIndex].records[i].date);
                    Console.WriteLine("Date and time data were received by the computer: {0}", database.providers[providerIndex].records[i].timestamp);
                    Console.WriteLine("Member name: {0}", database.providers[providerIndex].records[i].memberName);
                    Console.WriteLine("Member number: {0}", database.providers[providerIndex].records[i].memberNumber);
                    Console.WriteLine("Service code: {0}", database.providers[providerIndex].records[i].serviceCode);
                    Console.WriteLine("Fee to be paid: {0}", database.providers[providerIndex].records[i].fee);
                }
                Console.WriteLine("Number of consultations: {0}", database.providers[providerIndex].consultations);
                TotalFees(database);
                database.writeEFT(database);
                Console.ReadKey();
                Console.Write("Press any key to go back to the main provider menu:");
            }
        }
        public static double TotalFees(Database database)
        {
            if (!providerFound)
            {
                Console.Write("Enter your provider number:");
                providerNumbInput = Convert.ToInt32(Console.ReadLine());

                FindProvider(database);
                if (!providerFound)
                {
                    Console.WriteLine("Wrong provider number.");
                    Console.Write("Press any key to go back to the main provider menu:");
                    Console.ReadKey();
                }
            }
            double totalfees = 0;
            if(providerFound)
            {
                if (database.providers[0].records != null)
                {

                    TotalFeesCalc(database);
                    Console.Write("Press any key to go back to the main provider menu:");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("There are no fees to total.");
                    Console.Write("Press any key to continue");
                    Console.ReadKey();
                }
            }
            return totalfees;
        }
        public static double TotalFeesCalc(Database database)
        {
            //calculates the total fee for the provider
            double totalfees = 0;

            for (int i = 0; database.providers[providerIndex].records[i].memberName != null; i++)
            {
                totalfees += database.providers[providerIndex].records[i].fee;
            }
            database.providers[providerIndex].totalFee = totalfees;
            Console.WriteLine("Total fee: {0}\n\n", database.providers[providerIndex].totalFee);

            return totalfees;
        }
    }
}
