using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Homework2
{
    class EncoderDecoder
    {
        public string Encode(OrderClass orderObject)
        {
            string str ="";
            str += orderObject.getsenderID()+"/";
            str += orderObject.getreceiverID() + "/";
            str += orderObject.getCreditCardNumber() +"/";
            str += orderObject.getNoOfRooms();
            return str;
        }

        public OrderClass Decode(string str)
        {
            char[] delimiter = { '/' };
            string[] splitted = str.Split(delimiter);
            OrderClass newOrder = new OrderClass();
            newOrder.setsenderID(splitted[0]);
            newOrder.setreceiverID(splitted[1]);
            newOrder.setCreditCardNumber((Int32.Parse(splitted[2])));
            newOrder.setNoOfRooms((Int32.Parse(splitted[3])));
            return newOrder;
        }
    }
}
