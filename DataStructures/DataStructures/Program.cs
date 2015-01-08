using System;

namespace DataStructures
{
    class Program
    {
        static void Main(string[] args)
        {

            var buffer = new CircularBuffer<double>(capacity: 3);
            //setting up the event.
            buffer.ItemDiscarded += buffer_ItemDiscarded;
            ProcessInput(buffer);

            //Converter<double, DateTime> conver = d =>
            //    new DateTime(2010, 1, 1).AddDays(d);

            //inference is used here.
            //var asDtes = buffer.Map(conver);
            //foreach (var item in asDtes)
            //{
            //    Console.WriteLine(item);
            //}
            Console.WriteLine("--");

            Action<double> print = d => Console.WriteLine(d);
            //here we dont need to add the generic type
            //as this is infered from the compliler.
            buffer.Dump(print);
            //foreach (var item in asInts)
            //{
            //    Console.WriteLine(item);
            //}
            SumTheValues(buffer);
            Console.Read();
        }

        static void buffer_ItemDiscarded(object sender, ItemDiscardedEventArgs<double> e)
        {
            Console.WriteLine("Buffer is full! Discarding {0} for the new item {1}", 
                e.ItemDiscarded, e.NewItem);
        }


        private static void SumTheValues(IBuffer<double> buffer)
        {
            var sum = 0.0;
            Console.WriteLine("Buffer: ");
            while (!buffer.IsEmpty)
            {
                sum += buffer.Read();
            }
            Console.WriteLine(sum);
        }

        private static void ProcessInput(CircularBuffer<double> buffer)
        {
            while (true)
            {
                var value = 0.0;
                var input = Console.ReadLine();

                if (double.TryParse(input, out value))
                {
                    buffer.Write(value);
                    continue;
                }
                break;
            }
        }
    }
}
