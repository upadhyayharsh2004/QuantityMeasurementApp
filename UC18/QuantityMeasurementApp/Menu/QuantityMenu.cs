using QuantityMeasurementApp.Controllers;
using QuantityMeasurementAppModels.DTOs;
using QuantityMeasurementApp.Interfaces;

namespace QuantityMeasurementApp.Menu
{
    public class QuantityMenu : IMenu
    {
        private QuantityMeasurementController quantityController;
        public QuantityMenu(QuantityMeasurementController MeasurementController)
        {
            this.quantityController = MeasurementController;
        }

        public void Execute()
        {
            while (true)
            {
                Console.WriteLine("1.  Compare two lengths (check if they are equal or not)");
                Console.WriteLine("2.  Convert a length from one unit to another");
                Console.WriteLine("3.  Add two lengths and get the result");
                Console.WriteLine("4.  Subtract one length from another");
                Console.WriteLine("5.  Divide one length by another");

                Console.WriteLine("6.  Compare two weights (check if they are equal or not)");
                Console.WriteLine("7.  Convert a weight from one unit to another");
                Console.WriteLine("8.  Add two weights and get the result");
                Console.WriteLine("9.  Subtract one weight from another");
                Console.WriteLine("10. Divide one weight by another");

                Console.WriteLine("11. Compare two volumes (check if they are equal or not)");
                Console.WriteLine("12. Convert a volume from one unit to another");
                Console.WriteLine("13. Add two volumes and get the result");
                Console.WriteLine("14. Subtract one volume from another");
                Console.WriteLine("15. Divide one volume by another");

                Console.WriteLine("16. Compare two temperatures (check if they are equal or not)");
                Console.WriteLine("17. Convert a temperature from one unit to another");
                Console.WriteLine("18. Perform temperature calculations");
                Console.WriteLine("0.  Exit");
                Console.Write("Enter Your Choice: ");
                int choice = int.Parse(Console.ReadLine());

                if (choice == 0)
                {
                    Console.WriteLine("Exit Successful From Quantity Measurement App");
                    return;
                }
                else if (choice == 1)
                {
                    QuantityDTOs firstValued = FetchValuesQuantity("First", "Length", "Feet/Inch/Centimeter/Yard");
                    QuantityDTOs secondValued = FetchValuesQuantity("Second", "Length", "Feet/Inch/Centimeter/Yard");
                    quantityController.ExecuteLengthValuesComparison(firstValued, secondValued);
                }
                else if (choice == 2)
                {
                    QuantityDTOs quantity = FetchValuesQuantity("", "Length", "Feet/Inch/Centimeter/Yard");
                    Console.WriteLine("Enter The Targeted Unit For Choice"+" "+choice+" "+"Operation");
                    string targetUnitValued = Console.ReadLine();
                    quantityController.ExecuteLengthValuesConversion(quantity, targetUnitValued);
                }
                else if (choice == 3)
                {
                    QuantityDTOs firstValued = FetchValuesQuantity("First", "Length", "Feet/Inch/Centimeter/Yard");
                    QuantityDTOs secondValued = FetchValuesQuantity("Second", "Length", "Feet/Inch/Centimeter/Yard");
                    Console.WriteLine("Enter The Targeted Unit For Choice"+" "+choice+" "+"Operation");
                    string targetUnitValued = Console.ReadLine();
                    quantityController.ExecuteLengthValuesAddition(firstValued, secondValued, targetUnitValued);
                }
                else if (choice == 4)
                {
                    QuantityDTOs firstValued = FetchValuesQuantity("First", "Length", "Feet/Inch/Centimeter/Yard");
                    QuantityDTOs secondValued = FetchValuesQuantity("Second", "Length", "Feet/Inch/Centimeter/Yard");
                    Console.WriteLine("Enter The Targeted Unit For Choice"+" "+choice+" "+"Operation");
                    string targetUnitValued = Console.ReadLine();
                    quantityController.ExecuteLengthValuesSubtraction(firstValued, secondValued, targetUnitValued);
                }
                else if (choice == 5)
                {
                    QuantityDTOs firstValued = FetchValuesQuantity("First", "Length", "Feet/Inch/Centimeter/Yard");
                    QuantityDTOs secondValued = FetchValuesQuantity("Second", "Length", "Feet/Inch/Centimeter/Yard");
                    quantityController.ExecuteLengthValuesDivision(firstValued, secondValued);
                }
                else if (choice == 6)
                {
                    QuantityDTOs firstValued = FetchValuesQuantity("First", "Weight", "Kilogram/Gram/Pound");
                    QuantityDTOs secondValued = FetchValuesQuantity("Second", "Weight", "Kilogram/Gram/Pound");
                    quantityController.ExecuteWeightValuesComparison(firstValued, secondValued);
                }
                else if (choice == 7)
                {
                    QuantityDTOs quantityValued = FetchValuesQuantity("", "Weight", "Kilogram/Gram/Pound");
                    Console.WriteLine("Enter The Targeted Unit For Choice"+" "+choice+" "+"Operation");
                    string targetUnitValued = Console.ReadLine();
                    quantityController.ExecuteWeightValuesConversion(quantityValued, targetUnitValued);
                }
                else if (choice == 8)
                {
                    QuantityDTOs firstValued = FetchValuesQuantity("First", "Weight", "Kilogram/Gram/Pound");
                    QuantityDTOs secondValued = FetchValuesQuantity("Second", "Weight", "Kilogram/Gram/Pound");
                    Console.WriteLine("Enter The Targeted Unit For Choice"+" "+choice+" "+"Operation");
                    string targetUnitValued = Console.ReadLine();
                    quantityController.ExecuteWeightValuesAddition(firstValued, secondValued, targetUnitValued);
                }
                else if (choice == 9)
                {
                    QuantityDTOs firstValued = FetchValuesQuantity("First", "Weight", "Kilogram/Gram/Pound");
                    QuantityDTOs secondValued = FetchValuesQuantity("Second", "Weight", "Kilogram/Gram/Pound");
                    Console.WriteLine("Enter The Targeted Unit For Choice"+" "+choice+" "+"Operation");
                    string targetUnitValued = Console.ReadLine();
                    quantityController.ExecuteWeightvaluesSubtraction(firstValued, secondValued, targetUnitValued);
                }
                else if (choice == 10)
                {
                    QuantityDTOs firstValued = FetchValuesQuantity("First", "Weight", "Kilogram/Gram/Pound");
                    QuantityDTOs secondValued = FetchValuesQuantity("Second", "Weight", "Kilogram/Gram/Pound");
                    quantityController.ExecuteWeightValuesDivision(firstValued, secondValued);
                }
                else if (choice == 11)
                {
                    QuantityDTOs firstValued = FetchValuesQuantity("First", "Volume", "Litre/Millilitre/Gallon");
                    QuantityDTOs secondValued = FetchValuesQuantity("Second", "Volume", "Litre/Millilitre/Gallon");
                    quantityController.ExecuteVolumeValuesComparison(firstValued, secondValued);
                }
                else if (choice == 12)
                {
                    QuantityDTOs quantityValued = FetchValuesQuantity("", "Volume", "Litre/Millilitre/Gallon");
                    Console.WriteLine("Enter The Targeted Unit For Choice"+" "+choice+" "+"Operation");
                    string targetUnitValued = Console.ReadLine();
                    quantityController.ExecuteVolumeValuesConversion(quantityValued, targetUnitValued);
                }
                else if (choice == 13)
                {
                    QuantityDTOs firstValued = FetchValuesQuantity("First", "Volume", "Litre/Millilitre/Gallon");
                    QuantityDTOs secondValued = FetchValuesQuantity("Second", "Volume", "Litre/Millilitre/Gallon");
                    Console.WriteLine("Enter The Targeted Unit For Choice"+" "+choice+" "+"Operation");
                    string targetUnitValued = Console.ReadLine();
                    quantityController.ExecuteVolumeValuesAddition(firstValued, secondValued, targetUnitValued);
                }
                else if (choice == 14)
                {
                    QuantityDTOs firstValued = FetchValuesQuantity("First", "Volume", "Litre/Millilitre/Gallon");
                    QuantityDTOs secondValued = FetchValuesQuantity("Second", "Volume", "Litre/Millilitre/Gallon");
                    Console.WriteLine("Enter The Targeted Unit For Choice"+" "+choice+" "+"Operation");
                    string targetUnitValued = Console.ReadLine();
                    quantityController.ExecuteVolumeValuesSubtraction(firstValued, secondValued, targetUnitValued);
                }
                else if (choice == 15)
                {
                    QuantityDTOs firstValued = FetchValuesQuantity("First", "Volume", "Litre/Millilitre/Gallon");
                    QuantityDTOs secondValued = FetchValuesQuantity("Second", "Volume", "Litre/Millilitre/Gallon");
                    Console.WriteLine("Enter The Targeted Unit For Choice"+" "+choice+" "+"Operation");
                    quantityController.ExecuteVolumeValuesDivision(firstValued, secondValued);
                }
                else if (choice == 16)
                {
                    QuantityDTOs firstValued = FetchValuesQuantity("First", "Temperature", "Celsius/Fahrenheit/Kelvin");
                    QuantityDTOs secondValued = FetchValuesQuantity("Second", "Temperature", "Celsius/Fahrenheit/Kelvin");
                    quantityController.ExecuteTemperatureValuesComparison(firstValued, secondValued);
                }
                else if (choice == 17)
                {
                    QuantityDTOs quantity = FetchValuesQuantity("", "Temperature", "Celsius/Fahrenheit/Kelvin");
                    Console.WriteLine("Enter The Targeted Unit For Choice"+" "+choice+" "+"Operation");
                    string targetUnitValue = Console.ReadLine();
                    quantityController.ExecuteTemperatureValuesConversion(quantity, targetUnitValue);
                }
                else if (choice == 18)
                {
                    quantityController.ExecuteTemperatureValuesArithmetic(new QuantityDTOs(100, "Celsius", "Temperature"), new QuantityDTOs(50, "Celsius", "Temperature"), "Celsius");
                }
                else
                {
                    Console.WriteLine("Invalid Choice For Operation in Quantity Measurement App");
                }
            }
        }
        private QuantityDTOs FetchValuesQuantity(string quantityLabel, string quantityMeasurementType, string qunatityUnitHint)
        {
            string prefixQuantity = "";
            if (quantityLabel != "")
            {
                prefixQuantity = quantityLabel + " ";
            }
            Console.WriteLine("Enter The Unit Quantity Value");
            double valueQuantity = double.Parse(Console.ReadLine());

            Console.WriteLine("Enter The Unit Quantity Targeted Value For The Operation");

            string unitValued = Console.ReadLine();

            return new QuantityDTOs(valueQuantity, unitValued, quantityMeasurementType);
        }
    }
}