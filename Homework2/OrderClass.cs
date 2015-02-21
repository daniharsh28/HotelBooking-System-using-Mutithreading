using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Homework2
{
    class OrderClass
    {
        private string senderID;
        private string receiverID;
        private int creditCardNumber;
        private int noOfRooms;

        //public OrderClass(string senderID, string receiverID, int creditCardNumber, int noOfRooms)
        //{
        //    this.senderID = senderID;
         //   this.receiverID = receiverID;
         //   this.creditCardNumber = creditCardNumber;
        //    this.noOfRooms = noOfRooms;
        //}

        public void setsenderID(string senderID)
        {
            this.senderID = senderID;
        }

        public string getsenderID()
        {
            return this.senderID;
        }

        public void setreceiverID(string receiverID)
        {
            //Console.WriteLine(Thread.CurrentThread.Name + "is setting receiver Id to " + receiverID);
           this.receiverID = receiverID;
        }

        public string getreceiverID()
        {
            //Console.WriteLine(Thread.CurrentThread.Name + "is getting Receiver Id to " + this.receiverID);
            return this.receiverID;
        }

        public void setCreditCardNumber(int creditCardNumber)
        {
           this.creditCardNumber = creditCardNumber;
        }

        public int getCreditCardNumber()
        {
            return this.creditCardNumber;
        }

        public void setNoOfRooms(int noOfRooms)
        {
            this.noOfRooms = noOfRooms;
        }

        public int getNoOfRooms()
        {
            return this.noOfRooms;
        }
    }
}
