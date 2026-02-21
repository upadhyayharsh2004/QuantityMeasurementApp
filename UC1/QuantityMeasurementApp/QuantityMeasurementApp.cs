namespace QuantityMeasurementApp
{
    public class QuantityMeasurementApp
    {
        public static void Main(string[] args)
        {
            double firstValue;

            double secondValue;

            // Validate first input

            Console.Write("Enter First Value In Feet: ");

            while (!double.TryParse(Console.ReadLine(), out firstValue))
            {
                Console.WriteLine("Invalid input! Please enter a numeric value.");

                Console.Write("Enter First Value In Feet: ");
            }

            // Validate second input

            Console.Write("Enter Second Value In Feet: ");

            while (!double.TryParse(Console.ReadLine(), out secondValue))
            {
                Console.WriteLine("Invalid input! Please enter a numeric value.");

                Console.Write("Enter Second Value In Feet: ");
            }

            // Create Feet objects

            Feet firstFeet = new Feet(firstValue);

            Feet secondFeet = new Feet(secondValue);

            // Compare values

            bool result = firstFeet.Equals(secondFeet);

            // Print result

            Console.WriteLine("Input: " + firstValue + " ft and " + secondValue + " ft");

            if (result)
            {
                Console.WriteLine("Output: Equal (true)");
            }
            else
            {
                Console.WriteLine("Output: Not Equal (false)");
            }
        }
    }
}