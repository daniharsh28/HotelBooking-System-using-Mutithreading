using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Homework2
{
    class Program
    {
        public static HotelSupplier[] hotelsupplier = new HotelSupplier[3];
        public static TravelAgency[] agency = new TravelAgency[5];
        public static Thread[] hotelsupplierthreads = new Thread[3];
        public static Thread[] travelagencythreads = new Thread[5];
        public static MultiCellBuffer buffer = new MultiCellBuffer();
        public static ConfirmationBuffer confirmationbuffer = new ConfirmationBuffer();
        static void Main(string[] args)
        {
            TravelAgency temp = new TravelAgency();
            HotelSupplier.priceCut += new priceCutEvent(temp.hotelOnSale);

            Console.WriteLine(" WELCOME TO PROGRAM ");
            Console.WriteLine(" TEAM 598 Pseudo! \n");
            Thread.Sleep(1000);

            for (int i = 0; i < 3; i++)
            {
                hotelsupplier[i] = new HotelSupplier("HotelSupplier" + (i + 1));
                hotelsupplierthreads[i] = new Thread(new ThreadStart(hotelsupplier[i].hotelSupplierFunction));
                Console.WriteLine("HotelSupplier " + (i + 1) + " Created");
                hotelsupplierthreads[i].Name = "HotelSupplier" + (i + 1);
                hotelsupplierthreads[i].Start(); //Start HotelSupplier Thread

            }

            for (int i = 0; i < 5; i++)
            {
                agency[i] = new TravelAgency("TravelAgency" + (i + 1));
                travelagencythreads[i] = new Thread(new ThreadStart(agency[i].travelAgencyFunc));
                travelagencythreads[i].Name = "TravelAgency" + (i + 1);
                Console.WriteLine("TravelAgency " + (i + 1) + " Created");
                travelagencythreads[i].Start(); //Start TravelAgency Thread

            }

            for (int i = 0; i < 3; i++)
            {
                hotelsupplierthreads[i].Join();
            }

            for (int i = 0; i < 5; i++)
            {
                travelagencythreads[i].Join();
            }



            Console.WriteLine(" Team 598 pseudo!");
            Console.WriteLine(" Team Members: \n");
            Console.WriteLine(" Harsh Dani: 33.33%");
            Console.WriteLine(" Maunil Mehta: 33.33%");
            Console.WriteLine(" Rushi Shah: 33.33%");

            Console.WriteLine("\n Done!");
            Console.ReadKey();
        }
    }
}