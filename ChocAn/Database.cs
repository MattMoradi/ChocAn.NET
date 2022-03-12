using System;
using System.IO;
using System.Collections.Generic;

namespace ChocAn
{
    public class Database
    {
        public Members[] members = new Members[999];
        public Providers[] providers = new Providers[999];
        bool persistent = false;

        public Database()
        {
            try
            {
                // Save Provider Directory File
                StreamWriter pd = new StreamWriter("ProviderDirectory.txt");
                pd.WriteLine(ProviderDirectory.Aerobics + " - " + (int)ProviderDirectory.Aerobics + ": $127");
                pd.WriteLine(ProviderDirectory.Dietitian + " - " + (int)ProviderDirectory.Dietitian + ": $700");
                pd.WriteLine(ProviderDirectory.Vanilla + " - " + (int)ProviderDirectory.Vanilla + ": $450");
                pd.WriteLine(ProviderDirectory.Hamster + " - " + (int)ProviderDirectory.Hamster + $": $824");
                pd.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        public void persistence(Database data)
        {
            bool validLoad = false;
           try
           {
                if(Directory.Exists("Members") || Directory.Exists("Providers"))
                {
                    while (validLoad == false)
                    {
                        Console.Write("Local Database Detected! Attempt to load? (Y / N): ");
                        var load = Console.ReadLine();

                        if (load == "Y" || load == "y")
                        {
                            validLoad = true;
                            persistent = true;
                            string input, line;
                            int i = 0;

                            // Populate Member Database
                            foreach (var file in Directory.EnumerateFiles("Members", "*.txt"))
                            {  
                                using (StreamReader Reader = new StreamReader(file))
                                {
                                    data.members[i].name = Reader.ReadLine();
                                    data.members[i].number = int.Parse(Reader.ReadLine());
                                    data.members[i].address = Reader.ReadLine();
                                    data.members[i].city = Reader.ReadLine();
                                    data.members[i].state = Reader.ReadLine();
                                    data.members[i].zip = short.Parse(Reader.ReadLine());
                                    data.members[i].status = (Validity)int.Parse(Reader.ReadLine());
                                    //Reader.ReadLine();

                                    data.members[i].records = new MemberRecords[999];

                                    for (int j = 0; (line = Reader.ReadLine()) != null; j++)
                                    {
                                        data.members[i].records[j].date = Reader.ReadLine();
                                        data.members[i].records[j].providerName = Reader.ReadLine();
                                        data.members[i].records[j].service = Reader.ReadLine();
                                    }
                                    Reader.Close();
                                }
                                i++;
                            }

                            i = 0;

                            foreach (var file in Directory.EnumerateFiles("Providers", "*.txt"))
                            {
                                using (StreamReader Reader = new StreamReader(file))
                                {
                                    data.providers[i].name = Reader.ReadLine();
                                    data.providers[i].number = int.Parse(Reader.ReadLine());
                                    data.providers[i].address = Reader.ReadLine();
                                    data.providers[i].city = Reader.ReadLine();
                                    data.providers[i].state = Reader.ReadLine();
                                    data.providers[i].zip = short.Parse(Reader.ReadLine());
                                    data.providers[i].consultations = short.Parse(Reader.ReadLine());
                                    data.providers[i].totalFee = double.Parse(Reader.ReadLine());

                                    data.providers[i].records = new ProviderRecords[999];
                                    for (int j = 0; (line = Reader.ReadLine()) != null; j++)
                                    {
                                        data.providers[i].records[j].date = Reader.ReadLine();
                                        input = Reader.ReadLine();
                                        if(input != null)
                                        data.providers[i].records[j].timestamp = DateTime.Parse(input);
                                        data.providers[i].records[j].memberName = Reader.ReadLine();
                                        input = Reader.ReadLine();
                                        if(input != null)
                                        data.providers[i].records[j].memberNumber = int.Parse(input);
                                        input = Reader.ReadLine();
                                        if (input != null)
                                        data.providers[i].records[j].serviceCode = int.Parse(input);
                                        input = Reader.ReadLine();
                                        if (input != null)
                                        data.providers[i].records[j].fee = double.Parse(input);
                                        data.providers[i].records[j].comment = Reader.ReadLine();
                                    }
                                }
                            }
                            i++;
                            displayTest(data);
                        }
                        else if (load == "N" || load == "n")
                        {
                            validLoad = true;
                            Console.WriteLine("Database Load Cancelled. Database will be overwritten.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid Input!\n");
                            validLoad = false;
                        }
                    }
                }
           }
            catch(Exception e)
            {
                Console.WriteLine("Failed to load database from disk!\n");
                Console.WriteLine("Exception: " + e + "\n");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        public void save2disk(Database data)
        {
            StreamWriter Writer;

            try
            {
                if (persistent == true)
                {
                    Directory.Delete("Members", persistent);
                    Directory.Delete("Providers", persistent);
                }

                Directory.CreateDirectory("Members");

                for (int i = 0; data.members[i].name != null; i++)
                {
                    //Console.WriteLine("Member" + Path.DirectorySeparatorChar + $"{data.members[i].name}.txt");
                    Writer = new StreamWriter("Members" + Path.DirectorySeparatorChar + $"{data.members[i].name}.txt");
                    Writer.WriteLine(data.members[i].name);
                    Writer.WriteLine(data.members[i].number);
                    Writer.WriteLine(data.members[i].address);
                    Writer.WriteLine(data.members[i].city);
                    Writer.WriteLine(data.members[i].state);
                    Writer.WriteLine(data.members[i].zip);
                    Writer.WriteLine((int)data.members[i].status);
                    Writer.WriteLine();

                    for (int j = 0; data.members[i].records != null && data.members[i].records[j].date != null; j++)
                    {
                        Writer.WriteLine(data.members[i].records[j].date);
                        Writer.WriteLine(data.members[i].records[j].providerName);
                        Writer.WriteLine(data.members[i].records[j].service);
                        Writer.WriteLine();
                    }
                    Writer.Close();
                }

                Directory.CreateDirectory("Providers");

                for (int i = 0; data.providers[i].name != null; i++)
                {
                    Writer = new StreamWriter("Providers" + Path.DirectorySeparatorChar + $"{data.providers[i].name}.txt");
                    Writer.WriteLine(data.providers[i].name);
                    Writer.WriteLine(data.providers[i].number);
                    Writer.WriteLine(data.providers[i].address);
                    Writer.WriteLine(data.providers[i].city);
                    Writer.WriteLine(data.providers[i].state);
                    Writer.WriteLine(data.providers[i].zip);
                    Writer.WriteLine(data.providers[i].consultations);
                    Writer.WriteLine(data.providers[i].totalFee);
                    Writer.WriteLine();

                    for (int j = 0; data.providers[i].records != null && data.providers[i].records[j].date != null; j++)
                    {
                        Writer.WriteLine(data.providers[i].records[j].date);
                        Writer.WriteLine(data.providers[i].records[j].timestamp);
                        Writer.WriteLine(data.providers[i].records[j].memberName);
                        Writer.WriteLine(data.providers[i].records[j].memberNumber);
                        Writer.WriteLine(data.providers[i].records[j].serviceCode);
                        Writer.WriteLine(data.providers[i].records[j].fee);
                        Writer.WriteLine(data.providers[i].records[j].comment);
                        Writer.WriteLine();
                    }
                    Writer.Close();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Failed to save database to disk!\n");
                Console.WriteLine("Exception: " + e.Message + "\n");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        public void writeEFT(Database data)
        {
            try
            {
                StreamWriter EFT = new StreamWriter("EFT.txt");
                for (int i = 0; data.providers[i].name != null; i++)
                {
                    if (data.providers[i].totalFee > 0)
                    {
                        EFT.WriteLine(data.providers[i].name);
                        EFT.WriteLine(data.providers[i].number);
                        EFT.WriteLine(data.providers[i].totalFee);
                        EFT.WriteLine();
                    }
                }
                EFT.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine("Failed to write EFT!");
                Console.WriteLine("Exception: " + e.Message);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        public void displayTest(Database data)
        {
            Console.WriteLine("\n --- Member Database Display Test --- \n");
            for (int i = 0; data.members[i].name != null; i++)
            {
                Console.WriteLine("Name: " + data.members[i].name);
                Console.WriteLine("Number: " + data.members[i].number);
                Console.WriteLine("Address: " + data.members[i].address);
                Console.WriteLine("City: " + data.members[i].city);
                Console.WriteLine("State: " + data.members[i].state);
                Console.WriteLine("Zip: " + data.members[i].zip);
                Console.WriteLine("Status: " + data.members[i].status);

                for (int j = 0; data.members[i].records != null && data.members[i].records[j].date != null; j++)
                {
                    Console.WriteLine("\n-- Record --");
                    Console.WriteLine("Date: " + data.members[i].records[j].date);
                    Console.WriteLine("Provider: " + data.members[i].records[j].providerName);
                    Console.WriteLine("Service: " + data.members[i].records[j].service);
                }
                Console.WriteLine();
            }

            Console.WriteLine("\n --- Provider Database Display Test ---");
            for(int i = 0; data.providers[i].name != null; i++)
            {
                Console.WriteLine("Name: " + data.providers[i].name);
                Console.WriteLine("Number: " + data.providers[i].number);
                Console.WriteLine("Address: " + data.providers[i].address);
                Console.WriteLine("City: " + data.providers[i].city);
                Console.WriteLine("State: " + data.providers[i].state);
                Console.WriteLine("Zip: " + data.providers[i].zip);
                Console.WriteLine("Consultations: " + data.providers[i].consultations);
                Console.WriteLine("Total Fee: " + data.providers[i].totalFee);

                for (int j = 0; data.providers[i].records != null && data.providers[i].records[j].date != null; j++)
                {
                    Console.WriteLine("\n-- Record --");
                    Console.WriteLine("Date: " + data.providers[i].records[j].date);
                    Console.WriteLine("Timestamp: " + data.providers[i].records[j].timestamp);
                    Console.WriteLine("Member Name: " + data.providers[i].records[j].memberName);
                    Console.WriteLine("Member Number: " + data.providers[i].records[j].memberNumber);
                    Console.WriteLine("Service Code: " + data.providers[i].records[j].serviceCode);
                    Console.WriteLine("Fee: " + data.providers[i].records[j].fee);
                    Console.WriteLine("Comment: " + data.providers[i].records[j].comment);
                }
            }
            Console.ReadLine();
        }

        public struct Members
        {
            public string name;
            public int number;
            public string address;
            public string city;
            public string state;
            public int zip;
            public Validity status;
            public MemberRecords[] records;
        }

        public enum Validity { Validated, Invalid, Suspended };

        public struct Providers
        {
            public string name;
            public int number;
            public string address;
            public string city;
            public string state;
            public int zip;
            public short consultations;
            public double totalFee;
            public ProviderRecords[] records;
        }

        public struct MemberRecords
        {
            public string date;
            public string providerName;
            public string service;
        }

        public struct ProviderRecords
        {
            public string date;
            public DateTime timestamp;
            public string memberName;
            public int memberNumber;
            public int serviceCode;
            public double fee;
            public string comment;
        }

        public enum ProviderDirectory
        {
            Dietitian = 598470,
            Aerobics = 883948,
            Vanilla = 997254,
            Hamster = 884422
        }
    }
}