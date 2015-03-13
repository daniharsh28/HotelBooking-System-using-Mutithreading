# Event Driven E-Commerce
Hotel-Booking application, where Hotel-Supplier threads will randomly generate prices, if the price is less than the price generated before, it will fire an event which will notify Travel Agency thread and Travel-Agency thread will encrypt the order and put it in the multicell buffer.

Hotel-Supplier will calculate the price based on the order and send the confirmation to Travel-Agency thread. 

We started 5 travel agency threads and 3 hotel supplier threads and used various synchronization constructs such as Monitors and Thread scheduling constructs such as  AutoResetEvent and ManualResetEvent and delegates in C#.
