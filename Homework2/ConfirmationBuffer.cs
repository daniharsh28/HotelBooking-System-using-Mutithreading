using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Homework2
{
    class ConfirmationBuffer{
    
        OrderClass orderObject = null;
        private Boolean writeable = true;
        
        //HotelSupplier put confirmation value in buffer
        public void putConfirmationBuffer(OrderClass orderObject){
            lock (this)
            {
                while (!writeable)
                {
                    try
                    {
                        Monitor.Wait(this);
                    }
                    catch { Console.WriteLine("Error"); }

                }
                this.orderObject = orderObject;
                writeable = false;
                Monitor.PulseAll(this);
            }
        }
        //TravelAgency get confirmation value from the buffer
        public OrderClass getConfirmationBuffer()
        {
           lock(this)
           {
              while (writeable)
                {
               try
                {
                    Monitor.Wait(this);
               }
               catch { Console.WriteLine("Error"); }
            }
               writeable = true;
               Monitor.Pulse(this);
               return this.orderObject;
                
           }
        }
    }
}
