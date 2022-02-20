using System;
using System.Collections.Generic;
using System.Text;

namespace ChocAn
{
    public class Database
    {
        public Members[] members = new Members[999];
        public Providers[] providers = new Providers[999];

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