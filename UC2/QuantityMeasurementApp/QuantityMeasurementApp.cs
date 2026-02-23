namespace QuantityMeasurementApp
{
    public class QuantityMeasurementApp
    {
        public static void Main(string[] args)
        {
            // Call method to check equality for Feet values
            DemonstrateFeetEquality();

            // Call method to check equality for Inches values
            DemonstrateInchesEquality();
        }

        public static void DemonstrateFeetEquality()
        {
            // Read first measurement in feet from user
            Console.WriteLine("Enter First Value (In Feet): ");
            double firstValue = double.Parse(Console.ReadLine());

            // Read second measurement in feet from user
            Console.WriteLine("Enter Second Value (In Feet): ");
            double secondValue = double.Parse(Console.ReadLine());

            // Create Feet objects using entered values
            Feet first = new Feet(firstValue);
            Feet second = new Feet(secondValue);

            // Compare both Feet objects and display result
            Console.WriteLine(first.Equals(second) ?
            "Both Values Are Equal" : "Both Values Are Different");
        }

        public static void DemonstrateInchesEquality()
        {
            // Read first measurement in inches from user
            Console.WriteLine("Enter First Value (In Inches): ");
            double firstValue = double.Parse(Console.ReadLine());

            // Read second measurement in inches from user
            Console.WriteLine("Enter Second Value (In Inches): ");
            double secondValue = double.Parse(Console.ReadLine());

            // Create Inches objects using entered values
            Inches first = new Inches(firstValue);
            Inches second = new Inches(secondValue);

            // Compare both Inches objects and display result
            Console.WriteLine(first.Equals(second) ?
            "Both Values Are Equal" : "Both Values Are Different");
        }
    }
}