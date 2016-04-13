using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartSockets.DocumentDB_Objects
{
    public class Device
    {
        public int DeviceID;
        public DateTime LastModified;
        public DataEntry[] EntryCollection;
    }
}