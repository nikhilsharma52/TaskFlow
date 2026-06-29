using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Concepts
{
    class TypeConversion
    {
        public static void Run()
        {
            // Implicit Conversion (automatically) - converting a smaller type to a larger type size
            int myInt = 9;
            double myDouble = myInt; // Automatic casting: int to double
            Console.WriteLine(myInt);      // Outputs 9
            Console.WriteLine(myDouble);   // Outputs 9

            /* Explicit Conversion (manually) - converting a larger type to a smaller size type
               There is a risk of data loss when converting from a larger type to a smaller type.
               That's why we need to tell the compiler that we want to do this conversion explicitly knowing that there might be data loss */

            double myDouble2 = 9.78;
            int myInt2 = (int)myDouble2; // Manual casting: double to int
            Console.WriteLine(myDouble2);   // Outputs 9.78
            Console.WriteLine(myInt2);      // Outputs 9

            // Using ToString() method
            int myInt3 = 10;
            string myString = myInt3.ToString(); // Convert int to string
            Console.WriteLine(myString);     // Outputs "10"

            // Using System.Convert class
            // this method is used when two variable are not compatible with each other for example converting string to int, int to string, var to int
            // Always handle exceptions when using Convert class because it can throw exceptions if the conversion is not possible

            string myString2 = "15";
            int myInt4 = Convert.ToInt32(myString2); // Convert string to int
            Console.WriteLine(myInt4);       // Outputs 15

            // Using Parse() method
            string myString3 = "20";
            int myInt5 = Int32.Parse(myString3); // Convert string to int
            Console.WriteLine(myInt5);       // Outputs 20
        }
    }
}
