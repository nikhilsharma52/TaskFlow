using System;

namespace TaskFlow.Concepts
{
    class Variables
    {
        public static void Run()
        {
            byte number = 2;
            int count = 10;
            float totalPrice = 20.95f;
            char character = 'A';
            String firstName = "Nikhil";
            bool isWorking = true;


            Console.WriteLine(number);
            Console.WriteLine(count);
            Console.WriteLine(totalPrice);
            Console.WriteLine(character);
            Console.WriteLine(firstName);
            Console.WriteLine(isWorking);

            Console.WriteLine("{0} {1}", float.MinValue, float.MaxValue);
        }
    }
}
