using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartSockets.DocumentDB_Objects
{
    public class DataEntry
    {
        public DateTime TimeStamp;
        public int EntryType;
        public int DeviceSignature;
        public Data Data;
         
    }

    public class Data
    {
        public int Voltage;
        public int Current;
        public int Power;
    }
    
}