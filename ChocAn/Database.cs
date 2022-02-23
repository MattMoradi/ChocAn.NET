using System;
using System.IO;
using System.Collections.Generic;

namespace ChocAn
{
    public class Database
    {
        public Members[] members = new Members[999];
        public Providers[] providers = new Providers[999];

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

        public void save2disk(Database data)
        {
            StreamWriter Writer;

            try
            {
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
                    Writer.WriteLine(data.members[i].status);

                    for (int j = 0; data.members[i].records != null && data.members[i].records[j].date != null; j++)
                    {
                        Writer.WriteLine(data.members[i].records[j].date);
                        Writer.WriteLine(data.members[i].records[j].providerName);
                        Writer.WriteLine(data.members[i].records[j].service);
                        Writer.WriteLine();
                    }
                    Writer.WriteLine();
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
                    Writer.WriteLine();
                    Writer.Close();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Failed to save database to disk!");
                Console.WriteLine("Exception: " + e.Message);
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
            }
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
            public string memberNumber;
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