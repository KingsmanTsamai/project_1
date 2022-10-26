using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ST2_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1{

        [OperationContract]
        bool incrementDonation(decimal funds, int ids);//4
        [OperationContract]
        bool addDonation(Donation newClient); //3
        [OperationContract]
        Donation  checkIfdonated(string email, string ID);         //2
        [OperationContract]
        List<Cause> getCauses();                         //5
        [OperationContract]
        Decimal getTotalDonation();                       // 1
        [OperationContract]
        bool registerVolunteer(string name,string surname,string email); //7
        [OperationContract]
        bool checkIfVolonteerExist(string email);   //8
        [OperationContract]
        List<Cause> getSingleItem();                   //6

       
    }
}
