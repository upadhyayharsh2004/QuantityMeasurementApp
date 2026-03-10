using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuantityMeasurementApp
{
    // Main class for the console-based Quantity Measurement application
    public class QuantityMeasurementApp
    {
        // Entry point of the program
        public static void Main(string[] args)
        {
            // Display menu options for the user
            Console.WriteLine("===== Quantity Measurement App =====");
            Console.WriteLine("1. Check Length Equality");
            Console.WriteLine("2. Convert Length");
            Console.WriteLine("3. Compare Two Lengths");
            Console.WriteLine("4. Add Two Lengths");
            Console.WriteLine("5. Check Weight Equality");
            Console.WriteLine("6. Convert Weight");
            Console.WriteLine("7. Compare Two Weights");
            Console.WriteLine("8. Add Two Weights");
            Console.Write("Enter Your Choice: ");

            // Read user's menu selection
            int choice = int.Parse(Console.ReadLine());

            // Perform action based on user's choice
            switch (choice)
            {
                // Case 1: Check equality between two length values
                case 1:
                    Console.Write("Enter First Value: ");
                    double firstValue = double.Parse(Console.ReadLine());

                    Console.Write("Enter First Unit (Feet/Inch/Centimeter/Yard): ");
                    LengthUnit firstUnit = (LengthUnit)Enum.Parse(typeof(LengthUnit), Console.ReadLine(), true);

                    Console.Write("Enter Second Value: ");
                    double secondValue = double.Parse(Console.ReadLine());

                    Console.Write("Enter Second Unit (Feet/Inch/Centimeter/Yard): ");
                    LengthUnit secondUnit = (LengthUnit)Enum.Parse(typeof(LengthUnit), Console.ReadLine(), true);

                    // Create Quantity objects for both values
                    Quantity<IMeasurable> first = new Quantity<IMeasurable>(firstValue, new LengthMeasurementImpl(firstUnit));
                    Quantity<IMeasurable> second = new Quantity<IMeasurable>(secondValue, new LengthMeasurementImpl(secondUnit));

                    DemonstrateLengthEquality(first, second);
                    break;

                // Case 2: Convert length from one unit to another
                case 2:
                    Console.WriteLine("Enter Value: ");
                    double value = double.Parse(Console.ReadLine());

                    Console.WriteLine("Enter From Unit (Feet/Inch/Centimeter/Yard): ");
                    LengthUnit fromUnit = (LengthUnit)Enum.Parse(typeof(LengthUnit), Console.ReadLine(), true);

                    Console.WriteLine("Enter To Unit (Feet/Inch/Centimeter/Yard): ");
                    LengthUnit toUnit = (LengthUnit)Enum.Parse(typeof(LengthUnit), Console.ReadLine(), true);

                    DemonstrateLengthConversion(value, fromUnit, toUnit);
                    break;

                // Case 3: Compare two length quantities
                case 3:
                    Console.Write("Enter First Value: ");
                    double value1 = double.Parse(Console.ReadLine());

                    Console.Write("Enter First Unit (Feet/Inch/Centimeter/Yard): ");
                    LengthUnit unit1 = (LengthUnit)Enum.Parse(typeof(LengthUnit), Console.ReadLine(), true);

                    Console.Write("Enter Second Value: ");
                    double value2 = double.Parse(Console.ReadLine());

                    Console.Write("Enter Second Unit (Feet/Inch/Centimeter/Yard): ");
                    LengthUnit unit2 = (LengthUnit)Enum.Parse(typeof(LengthUnit), Console.ReadLine(), true);

                    DemonstrateLengthComparison(value1, unit1, value2, unit2);
                    break;

                // Case 4: Add two lengths and display result in target unit
                case 4:
                    Console.Write("Enter First Value: ");
                    double addValue1 = double.Parse(Console.ReadLine());

                    Console.Write("Enter First Unit (Feet/Inch/Centimeter/Yard): ");
                    LengthUnit addUnit1 = (LengthUnit)Enum.Parse(typeof(LengthUnit), Console.ReadLine(), true);

                    Console.Write("Enter Second Value: ");
                    double addValue2 = double.Parse(Console.ReadLine());

                    Console.Write("Enter Second Unit (Feet/Inch/Centimeter/Yard): ");
                    LengthUnit addUnit2 = (LengthUnit)Enum.Parse(typeof(LengthUnit), Console.ReadLine(), true);

                    Console.Write("Enter Target Unit (Feet/Inch/Centimeter/Yard): ");
                    LengthUnit targetUnit = (LengthUnit)Enum.Parse(typeof(LengthUnit), Console.ReadLine(), true);

                    // Create quantity objects
                    Quantity<IMeasurable> length1 = new Quantity<IMeasurable>(addValue1, new LengthMeasurementImpl(addUnit1));
                    Quantity<IMeasurable> length2 = new Quantity<IMeasurable>(addValue2, new LengthMeasurementImpl(addUnit2));

                    DemonstrateLengthAddition(length1, length2, targetUnit);
                    break;

                // Case 5: Check equality between two weight values
                case 5:
                    Console.Write("Enter First Value: ");
                    double w1 = double.Parse(Console.ReadLine());

                    Console.Write("Enter First Unit (Kilogram/Gram/Pound): ");
                    WeightUnit wu1 = (WeightUnit)Enum.Parse(typeof(WeightUnit), Console.ReadLine(), true);

                    Console.Write("Enter Second Value: ");
                    double w2 = double.Parse(Console.ReadLine());

                    Console.Write("Enter Second Unit (Kilogram/Gram/Pound): ");
                    WeightUnit wu2 = (WeightUnit)Enum.Parse(typeof(WeightUnit), Console.ReadLine(), true);

                    Quantity<IMeasurable> weight1 = new Quantity<IMeasurable>(w1, new WeightMeasurementImpl(wu1));
                    Quantity<IMeasurable> weight2 = new Quantity<IMeasurable>(w2, new WeightMeasurementImpl(wu2));

                    DemonstrateWeightEquality(weight1, weight2);
                    break;

                // Case 6: Convert weight units
                case 6:
                    Console.Write("Enter Value: ");
                    double valueW = double.Parse(Console.ReadLine());

                    Console.Write("From Unit (Kilogram/Gram/Pound): ");
                    WeightUnit fromW = (WeightUnit)Enum.Parse(typeof(WeightUnit), Console.ReadLine(), true);

                    Console.Write("To Unit (Kilogram/Gram/Pound): ");
                    WeightUnit toW = (WeightUnit)Enum.Parse(typeof(WeightUnit), Console.ReadLine(), true);

                    DemonstrateWeightConversion(valueW, fromW, toW);
                    break;

                // Case 7: Compare two weight quantities
                case 7:
                    Console.Write("Enter First Value: ");
                    double compareWeight1 = double.Parse(Console.ReadLine());

                    Console.Write("Enter First Unit (Kilogram/Gram/Pound): ");
                    WeightUnit compareWeightUnit1 = (WeightUnit)Enum.Parse(typeof(WeightUnit), Console.ReadLine(), true);

                    Console.Write("Enter Second Value: ");
                    double compareWeight2 = double.Parse(Console.ReadLine());

                    Console.Write("Enter Second Unit (Kilogram/Gram/Pound): ");
                    WeightUnit compareWeightUnit2 = (WeightUnit)Enum.Parse(typeof(WeightUnit), Console.ReadLine(), true);

                    DemonstrateWeightComparison(compareWeight1, compareWeightUnit1, compareWeight2, compareWeightUnit2);
                    break;

                // Case 8: Add two weights and display result
                case 8:
                    Console.Write("Enter First Value: ");
                    double aw1 = double.Parse(Console.ReadLine());

                    Console.Write("Enter First Unit (Kilogram/Gram/Pound): ");
                    WeightUnit au1 = (WeightUnit)Enum.Parse(typeof(WeightUnit), Console.ReadLine(), true);

                    Console.Write("Enter Second Value: ");
                    double aw2 = double.Parse(Console.ReadLine());

                    Console.Write("Enter Second Unit (Kilogram/Gram/Pound): ");
                    WeightUnit au2 = (WeightUnit)Enum.Parse(typeof(WeightUnit), Console.ReadLine(), true);

                    Console.Write("Enter Target Unit (Kilogram/Gram/Pound): ");
                    WeightUnit targetW = (WeightUnit)Enum.Parse(typeof(WeightUnit), Console.ReadLine(), true);

                    Quantity<IMeasurable> qw1 = new Quantity<IMeasurable>(aw1, new WeightMeasurementImpl(au1));
                    Quantity<IMeasurable> qw2 = new Quantity<IMeasurable>(aw2, new WeightMeasurementImpl(au2));

                    DemonstrateWeightAddition(qw1, qw2, targetW);
                    break;

                // Default case for invalid menu selection
                default:
                    Console.WriteLine("Invalid Choice");
                    break;
            }
        }

        // Checks if two length quantities are equal
        public static void DemonstrateLengthEquality(Quantity<IMeasurable> first, Quantity<IMeasurable> second)
        {
            Console.WriteLine(first.Equals(second)
                 ? "Both Values Are Equal"
                 : "Both Values Are Different");
        }

        // Converts a length value from one unit to another
        public static void DemonstrateLengthConversion(double value, LengthUnit fromUnit, LengthUnit toUnit)
        {
            Quantity<IMeasurable> length = new Quantity<IMeasurable>(value, new LengthMeasurementImpl(fromUnit));
            Quantity<IMeasurable> converted = length.ConvertTo(new LengthMeasurementImpl(toUnit));

            Console.WriteLine($"{length} = {converted}");
        }

        // Overloaded method for length conversion using an existing Quantity object
        public static void DemonstrateLengthConversion(Quantity<IMeasurable> length, LengthUnit toUnit)
        {
            Quantity<IMeasurable> converted = length.ConvertTo(new LengthMeasurementImpl(toUnit));
            Console.WriteLine($"{length} = {converted}");
        }

        // Compares two length quantities
        public static void DemonstrateLengthComparison(double value1, LengthUnit unit1,
                                                       double value2, LengthUnit unit2)
        {
            Quantity<IMeasurable> first = new Quantity<IMeasurable>(value1, new LengthMeasurementImpl(unit1));
            Quantity<IMeasurable> second = new Quantity<IMeasurable>(value2, new LengthMeasurementImpl(unit2));

            DemonstrateLengthEquality(first, second);
        }

        // Adds two length quantities and prints result
        public static void DemonstrateLengthAddition(Quantity<IMeasurable> first,
                                                     Quantity<IMeasurable> second,
                                                     LengthUnit targetUnit)
        {
            Quantity<IMeasurable> sum =
                first.Add(second, new LengthMeasurementImpl(targetUnit));

            Console.WriteLine($"{first} + {second} = {sum}");
        }

        // Checks equality between two weight quantities
        public static void DemonstrateWeightEquality(Quantity<IMeasurable> first,
                                                     Quantity<IMeasurable> second)
        {
            Console.WriteLine(first.Equals(second)
                ? "Weights Are Equal"
                : "Weights Are Not Equal");
        }

        // Adds two weight quantities and prints result
        public static void DemonstrateWeightAddition(Quantity<IMeasurable> first,
                                                     Quantity<IMeasurable> second,
                                                     WeightUnit target)
        {
            Quantity<IMeasurable> sum = first.Add(second, new WeightMeasurementImpl(target));
            Console.WriteLine($"{first} + {second} = {sum}");
        }

        // Compares two weight quantities
        public static void DemonstrateWeightComparison(double value1, WeightUnit unit1,
                                                       double value2, WeightUnit unit2)
        {
            Quantity<IMeasurable> first = new Quantity<IMeasurable>(value1, new WeightMeasurementImpl(unit1));
            Quantity<IMeasurable> second = new Quantity<IMeasurable>(value2, new WeightMeasurementImpl(unit2));

            DemonstrateWeightEquality(first, second);
        }

        // Converts weight from one unit to another
        public static void DemonstrateWeightConversion(Quantity<IMeasurable> weight, WeightUnit toUnit)
        {
            Quantity<IMeasurable> converted = weight.ConvertTo(new WeightMeasurementImpl(toUnit));
            Console.WriteLine($"{weight} = {converted}");
        }

        // Overloaded method for weight conversion
        public static void DemonstrateWeightConversion(double valueW, WeightUnit fromW, WeightUnit toW)
        {
            Quantity<IMeasurable> weight = new Quantity<IMeasurable>(valueW, new WeightMeasurementImpl(fromW));
            Quantity<IMeasurable> converted = weight.ConvertTo(new WeightMeasurementImpl(toW));

            Console.WriteLine($"{weight} = {converted}");
        }
    }
}