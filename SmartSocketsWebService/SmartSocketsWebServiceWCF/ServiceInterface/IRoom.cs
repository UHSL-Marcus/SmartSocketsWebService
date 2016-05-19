﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SmartSocketsWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISmartSocketsWebService" in both code and config file together.
    public partial interface ISmartSocketsWebService
    {
        [OperationContract][WebInvoke]
        bool SetNewRoom(Room room, out string ID);

        [OperationContract][WebInvoke]
        bool UpdateRoom(Room room);

        [OperationContract][WebInvoke]
        bool RemoveRoom(int ID);

        [OperationContract][WebInvoke]
        bool GetRoom(int ID, out Room result);

        [OperationContract][WebInvoke]
        bool GetAllRooms(out List<Room> result);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class Room
    {
        [DataMember]
        public int? RoomID;
        [DataMember]
        public int? PropertyID;
        [DataMember]
        public string RoomName;
    }
}
