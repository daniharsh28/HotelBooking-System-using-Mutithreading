using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Homework2
{
    //TravelAgency Function is starting point of TravelAgency Thread!
    class TravelAgency
    {
        private string senderID = ""; 
        private static string receiverID = "" ;
        private static int creditCardNumber = 5000;
        private int price;
        Random rnd = new Random();
        EncoderDecoder e = new EncoderDecoder();
       
        public static AutoResetEvent mre = new AutoResetEvent(false);
        
        public TravelAgency()
        {

        }

        public TravelAgency(string senderID)
        {
            this.senderID = senderID;

        }
        public void hotelOnSale(Int32 price, String id)  //This event callback method! 
        {
          
            receiverID = id;
            this.price = price;
            Console.WriteLine("Current Price is: " + price);
            Thread.Sleep(500);
            mre.Set();//Release one TravelAgency Thread
            
        }

        public void travelAgencyFunc()
        {

            for (int i = 0; i < 3; i++)
            {
  
                mre.WaitOne();//All TravelAgency  go in Wait State
                OrderClass oc = new OrderClass();
                oc.setsenderID(senderID);
                oc.setreceiverID(receiverID);
                oc.setCreditCardNumber(creditCard());
                oc.setNoOfRooms(rnd.Next(5,50));
                try
                {
                    string encodedString = e.Encode(oc);    
                    Program.buffer.setOneCell(encodedString);
                }

                catch(Exception e) {
                    Console.WriteLine("Error!");
                }
               
                // }
                while (true)
                {
           
                    OrderClass order = Program.confirmationbuffer.getConfirmationBuffer();  //Get confirmation value from buffer      
                    Console.WriteLine("The order is confirmed by " + order.getreceiverID() + " placed by " + order.getsenderID() );
                    Console.WriteLine("\n");
                    Thread.Sleep(500);
                    break;
                }

                
            }

        }
                    
        public int creditCard(){
            lock (this)
            {
                creditCardNumber++;
                return creditCardNumber;
            }
        }
    }
}
