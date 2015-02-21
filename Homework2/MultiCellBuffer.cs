using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Homework2
{
    class MultiCellBuffer{
     const int SIZE = 3;
        string[] buffer = new string[SIZE];
        int head = 0, tail = 0, n = 0;
        public static Semaphore writerSempahore = new Semaphore(3, 3);
        public static Semaphore readerSemaphore = new Semaphore(3, 3);
        EncoderDecoder e = new EncoderDecoder();
       //TravelAgency Set value of order in the buffer
        public void setOneCell(string c)
        {
            writerSempahore.WaitOne();
            lock (this)
            {

                while (n == SIZE)
                {
                    Monitor.Wait(this);
                }


               // Console.WriteLine(Thread.CurrentThread.Name+"is enterd in SetOneCell Method");
                buffer[tail] = c;
                tail = (tail + 1) % SIZE;
                n++;
               // Console.WriteLine("writing thread " + Thread.CurrentThread.Name + " " + c + " " + n);
                writerSempahore.Release();
                Monitor.Pulse(this);
            }
        }
        //HotelSupplier get order from the buffer
        public string getOneCell()
        {

            readerSemaphore.WaitOne();
           // Console.WriteLine("Currently Reading thread is"+Thread.CurrentThread.Name);
            lock (this)
            {
                while (n == 0)
                {
                    try { Monitor.Wait(this); }
                    catch { Console.WriteLine("Error!"); }
                }

                //Console.WriteLine("reading thread entered");
                string c = buffer[head];
                //Console.WriteLine("Received String is " + c);
                //Console.WriteLine("Receiver Id is " + e.Decode(c).getreceiverID());
                if (e.Decode(c).getreceiverID().Equals(Thread.CurrentThread.Name))
                {

                    head = (head + 1) % SIZE;
                    n--;

                   
                    readerSemaphore.Release();
                    Monitor.Pulse(this);
                    return c;
                }
                else {
                    readerSemaphore.Release();
                    Monitor.Pulse(this);
                    return null; 
                }
            }
        }
    }
}
