namespace QuantityMeasurementApp
{
    public class QuantityMeasurementApp
    {
        public static void Main(string[] args)
        {
            // Start the demo for comparing two length measurements
            DemonstrateLengthEquality();
        }

        public static void DemonstrateLengthEquality()
        {
            // Read the first measurement value from the user
            Console.WriteLine("Enter First Value: ");
            double firstValue = double.Parse(Console.ReadLine());

            // Read the first measurement unit and convert input text to enum
            Console.WriteLine("Enter First Unit (Feet/Inch): ");
            LengthUnit firstUnit = (LengthUnit)Enum.Parse(typeof(LengthUnit), Console.ReadLine(), true);

            // Read the second measurement value from the user
            Console.WriteLine("Enter Second Value: ");
            double secondValue = double.Parse(Console.ReadLine());

            // Read the second measurement unit and convert input text to enum
            Console.WriteLine("Enter Second Unit (Feet/Inch): ");
            LengthUnit secondUnit = (LengthUnit)Enum.Parse(typeof(LengthUnit), Console.ReadLine(), true);

            // Create objects representing the two length quantities
            QuantityLength first = new QuantityLength(firstValue, firstUnit);
            QuantityLength second = new QuantityLength(secondValue, secondUnit);

            // Compare the two objects and display the result
            Console.WriteLine(first.Equals(second)
                ? "Both Values Are Equal"
                : "Both Values Are Different");
        }
    }
}