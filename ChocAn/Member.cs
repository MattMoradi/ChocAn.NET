using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;


namespace ChocAn
{
    public class Member
    {


        public static void MemberMain(Database.Members[] members)
        {
            Boolean quit = false;
            int valid = 0;
            string input;
            int inumber;
            Console.Clear();
            Console.WriteLine("ChocAn.NET Terminal v1.0\n");
            Console.WriteLine("Hello Member!\n");



            while (!quit)
            {
                Boolean match = false;

                do
                {
                    Console.Write("Please enter a valid Member Number (9-digits found in the back of the card): ");
                    input = Console.ReadLine();
                } while (!int.TryParse(input, out inumber));

                for (int i = 0; i < members.Length; ++i)
                {
                    if (members[i].number == 0)
                        break;
                    if (members[i].number == inumber) //when the user input 11 digits or more it true
                    {
                        valid = is_valid(members, i);
                        switch (valid)
                        {
                            case 1:
                                MemberInfo(members, i);
                                MemberRecords(members, i);
                                match = true;
                                break;
                            case 2:
                                Console.WriteLine("\nThis account has been suspended! \nTo activate your account you will need to pay the fees!");
                                match = true;
                                break;
                            case 3:
                                Console.WriteLine("The Member number you have entered is invalid! ");
                                match = true;
                                break;

                        }
                    }
                    
                }
                if (!match)
                {
                    if (!is_9digit(inumber))
                        Console.WriteLine("The Member number has to be 9-digits! ");
                    else
                        Console.WriteLine("The Member number you have entered does not exist! ");
                }
                Boolean choice = true;
                while (choice) {
                    Console.Write("\n1) Enter a new member number.\n2) Quit.\nEnter your option: ");
                    var option = Console.ReadLine();
                    switch (option)
                    {
                        case "1":
                            quit = false; choice = false; break;
                        case "2":
                            quit = true; choice = false; break;
                        default:
                            Console.WriteLine("\nInvalid option!"); break;
                    }

                }
            }

        }
        public static int is_valid(Database.Members[] members, int member_i)
        {
            if (members[member_i].status == (Database.Validity)0)
                return 1;

            if (members[member_i].status == (Database.Validity)2)
                return 2;

            if (members[member_i].status == (Database.Validity)1)
                return 3;

            else 
                return 0;
            }

        public static void MemberInfo(Database.Members[] members, int i)
        {
            Console.WriteLine("\nWelcome, "+members[i].name);
            Console.WriteLine("\nMember number: "+ members[i].number);
            Console.WriteLine("\nAddress: " + members[i].address);
            Console.WriteLine("\nCity: " + members[i].city);
            Console.WriteLine("\nState: " + members[i].state);
            Console.WriteLine("\nZip Code: " + members[i].zip);

        }
        public static void MemberRecords(Database.Members[] members, int member_i)
        {
            if (members[member_i].records != null)
            {
                MemberDate(members, member_i);

            }
        }
        public static void MemberDate(Database.Members[] members, int member_i)
        {
            int size = 0;
            //Get number of records in a member file
            for(int i = 0; i < 51; i++)
            {
                if (members[member_i].records[i].date == null)
                    break;
                size++;
            }
            //Create a copy of the dates array
            DateTime[] dates = new DateTime[size];
            //Set the dates array
            for (int i = 0; i < size; i++) {
                if (members[member_i].records[i].date == null)
                    break;
                dates[i] = DateTime.ParseExact(members[member_i].records[i].date,"MM-dd-yyyy", CultureInfo.InvariantCulture);
            }
            //Sort the dates
            Array.Sort(dates);
            //Display the records sorted by date
            display_records(members, member_i, dates);
        }

        //check if the user entered 9 digits
        public static Boolean is_9digit(int inumber)
        {
            int check = 99999999 - inumber;
            int check1 = 999999999 - inumber;
            if (check < 0 && check1 >= 0)
                return true;
            else 
                return false;
        }

        //Display the sorted by date records
        private static void display_records(Database.Members[] members, int member_i,DateTime [] dates)
        {
            DateTime d1;
            int count = 0;
            for (int d = 0; d < dates.Length; d++)
            {
                count++;
                for(int i = 0; i < members.Length; i++)
                {
                    //Convert the records date to Datetime to sort by dates
                    if (DateTime.TryParse(members[member_i].records[i].date, out d1) && (d1 == dates[d]))
                    {
                        Console.WriteLine("\n\n\tRecord {" + count + "}");
                        Console.WriteLine("\nDate: " + members[member_i].records[i].date);
                        Console.WriteLine("\nProvider namer: " + members[member_i].records[i].providerName);
                        Console.WriteLine("\nService: " + members[member_i].records[i].service);
                        break;
                    }
                }
            }
        }

    }
}
