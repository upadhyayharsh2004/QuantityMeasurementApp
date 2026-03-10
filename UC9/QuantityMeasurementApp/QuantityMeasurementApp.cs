using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantityMeasurementApp
{
    // Main class to run the Quantity Measurement Application
    public class QuantityMeasurementApp
    {
        // Entry point of the program
        public static void Main(string[] args)
        {
            // Creating length objects with different units
            Length l1 = new Length(1.0, LengthUnit.FEET);
            Length l2 = new Length(12.0, LengthUnit.INCH);

            // Checking equality between two lengths
            Console.WriteLine("Length Equality:");
            Console.WriteLine(l1.Equals(l2));

            // Adding two length values
            Length sumLength = l1.Add(l2);
            Console.WriteLine("Length Addition: " + sumLength);

            // Creating weight objects with different units
            Weight w1 = new Weight(1.0, WeightUnit.KILOGRAM);
            Weight w2 = new Weight(1000.0, WeightUnit.GRAM);

            // Checking equality between two weights
            Console.WriteLine("Weight Equality:");
            Console.WriteLine(w1.Equals(w2));

            // Adding two weight values
            Weight sumWeight = w1.Add(w2);
            Console.WriteLine("Weight Addition: " + sumWeight);
        }
    }
}