using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ST2_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        DataClasses1DataContext db=new DataClasses1DataContext();
        public bool addDonation(Donation newClient)
        {
            // Donation donate = donated(email, causesId);

            var newDonation = new Donation() { 
               DonerName=newClient.DonerName,
               DonerEmail=newClient.DonerEmail,
               DonationAmout=newClient.DonationAmout,
            
            };
                db.Donations.InsertOnSubmit(newClient);
                try
                {
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    ex.GetBaseException();
                    return false;
                }

            
            
        }

        public bool checkDonerIfExist(string email)
        {
            var check = (from p in db.Donations
                         where p.DonerEmail.Equals(email)
                         select p).FirstOrDefault();
            if (check != null)
            {
                return true;
            }
            else {
                return false;
            }
        }

        public bool checkIfVolonteerExist(string email)
        {
            var user = (from k in db.Clients
                           where k.Email.Equals(email)
                           select k).FirstOrDefault();
            if (user != null)
            {
                return true;
            }
            else {
                return false;
            }
        }

        public Donation checkIfdonated(string email, string id)
        {
            var donation=(from k in db.Donations
                              where k.DonerEmail.Equals(email) && k.SelectedCauseId.Equals(id)
                              select k).FirstOrDefault();
            if (donation == null)
            {
                return null;

            }
            else {

                return donation;
            }
        }

        public List<Cause> getCauses()
        {
            dynamic l_causes=new List<Cause>();
            dynamic cause = (from p in db.Causes
                             where p.Id !=5 &&  !p.Id.Equals(1)
                             select p).DefaultIfEmpty();
            foreach (Cause k in cause) { 
              l_causes.Add(k);
            }
            return l_causes;
        }


        public decimal getTotalDonation()
        {
            dynamic donations = (from p in db.Donations
                                 select p).DefaultIfEmpty();
            decimal total = 0;
            foreach (Donation p in donations) {
                total += p.DonationAmout;
              }
            return total;
        }

        public bool registerVolunteer(string name, string surname, string email)
        {
            var newVolonteer = new Client()
            {
                  Name = name,
                  Surname = surname,
                  Email = email,

            };
            db.Clients.InsertOnSubmit(newVolonteer);
            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                ex.GetBaseException();
                return false;
            }
        }

        public Cause getSingleItem(int ID)
        {
            var prod = (from p in db.Causes
                        where p.Id.Equals(ID)
                        select p).FirstOrDefault();
            if (prod != null)
            {
                var newItem = new Cause()
                {
                     CauseName=prod.CauseName,
                     CauseDescription=prod.CauseDescription,
                     CauseImg=prod.CauseImg,
                     CauseRequiredMoney=prod.CauseRequiredMoney,

                };
                return newItem;
            }
            else
            {

                return null;
            }
        }

        public bool incrementDonation(decimal funds, int ids)
        {
            var chelete = (from p in db.Donations
                           where p.Id.Equals(ids)
                           select p).FirstOrDefault();
            //So 
            if (chelete != null) {
                        funds += chelete.DonationAmout;
            }
            //it has to add the amount in the database\
            try{ db.SubmitChanges(); return true;}
            catch (Exception ex){ ex.GetBaseException(); return false;}


        }
    }
}
