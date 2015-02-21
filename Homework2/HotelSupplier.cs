using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Homework2.BankServiceReference;

namespace Homework2
{
    public delegate void priceCutEvent(Int32 price, string h_id);
    public delegate void orderProcessed(Int32 b);
    class HotelSupplier
    {
       
        //properties specific to hotel
        private int hotelPrice = 15;
        //id for hotel(receiver_id)
        private string hotelId;
        private OrderClass orderObj= null;
        //reference to multicell buffer 
        EncoderDecoder e = new EncoderDecoder();
        static AutoResetEvent auto = new AutoResetEvent(false);
        
        
        public HotelSupplier(string h_id)
        {
            hotelId = h_id;
        }
        public static event priceCutEvent priceCut;
        

        public int gethotelPrice() 
        { 
            return hotelPrice; 
        }

        public void hotelSupplierFunction()
        {
            
                if (!(Thread.CurrentThread.Name.Equals("HotelSupplier1")))
                {
                    Thread.Sleep(2000);
                }
                
                
                if (!(Thread.CurrentThread.Name.Equals("HotelSupplier1")))
                {
                    auto.WaitOne(); //Wait HotelSupplier Thread
                    
                }
                for (int i = 0; i < 5; i++)
                {

                Console.WriteLine("Continuing Thread is: " + Thread.CurrentThread.Name);
                this.pricingModel();


                //Buffer
                while (true)
                {
                    Console.WriteLine("\nThread " + Thread.CurrentThread.Name + " is waiting for to read the order");
                    string str = Program.buffer.getOneCell();
                    if (str != null)
                    {
                        //Console.WriteLine("Order String is " + str);
                        orderObj = e.Decode(str);
                        //Console.WriteLine("Got Order Object!");
                        break;
                    }
                }
                Console.WriteLine(Thread.CurrentThread.Name + " is processing order " + orderObj.getsenderID());
                Thread orderprocess = new Thread(() => orderProcessing(orderObj));
                orderprocess.Start();
                orderprocess.Join();
                
                //Thread.Sleep(5000);
            }
                auto.Set(); //Release only one HotelSupplier Thread
        }

        public void orderProcessing(OrderClass orderObj)
        {
                bool valid = false;
                
                int creditCardNo = orderObj.getCreditCardNumber();
                Service1Client bank = new Service1Client();
                try
                {
                    valid = bank.validateCreditCard(creditCardNo);

                }
                catch (Exception e)
                {
                    Console.WriteLine("Service Error!");
                }

                if (valid)
                {
                    int hotelAmount = orderObj.getNoOfRooms() * hotelPrice ;
                    int tax = (int) 0.1 * hotelAmount;
                    int LocationCharge = (int) 0.01 * hotelAmount;

                    int Amount = hotelAmount + tax + LocationCharge;

                    Console.WriteLine("Calculated Amount is " + Amount + " placed by "+ orderObj.getsenderID()+ " received by " +orderObj.getreceiverID());
                   
                    
                    
                        Program.confirmationbuffer.putConfirmationBuffer(orderObj); // Put Confirmation value in confirmation buffer
                       // Console.WriteLine("Order has been put into confirmationbuffer");
                        
                    }
                    
                   
                }
                          


        public void pricingModel()
        {
            while (true)
            {
                int temp;
                Random r = new Random();
                temp = r.Next(13, 18);
                // Console.WriteLine("Generated Price is:" + temp);
                if (temp < hotelPrice)
                {

                    priceCut(temp, Thread.CurrentThread.Name);
                    break;
                }
                else
                {
                    hotelPrice = temp;
                    
                }
            }
        }

        


    }
}
