using System;
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
        [OperationContract]
        bool SetNewRoom(Room room);

        [OperationContract]
        Room GetRoom(int ID);

        [OperationContract]
        List<Room> GetAllRooms();
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class Room
    {
        [DataMember]
        public int RoomID = -1;
        [DataMember]
        public int PropertyID;
    }
}
