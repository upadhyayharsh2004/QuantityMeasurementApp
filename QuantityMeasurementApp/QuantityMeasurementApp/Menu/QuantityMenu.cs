using QuantityMeasurementApp.Controllers;
using QuantityMeasurementAppModels.DTOs;
using QuantityMeasurementApp.Interfaces;

namespace QuantityMeasurementApp.Menu
{
    public class QuantityMenu : IMenu
    {
        private readonly QuantityMeasurementController extremelyImportantControllerInstanceUsedForHandlingAllMenuOperations;

        public QuantityMenu(QuantityMeasurementController incomingControllerDependencyObject)
        {
            this.extremelyImportantControllerInstanceUsedForHandlingAllMenuOperations = incomingControllerDependencyObject;
        }

        public void Run()
        {
            while (true)
            {
                PrintHeader();
                int userSelectedMenuChoiceValue = GetChoice();

                if (userSelectedMenuChoiceValue == 0)
                {
                    Console.WriteLine("✅ Thank you for using the Quantity Measurement Application. Goodbye!");
                    break;
                }

                HandleSelection(userSelectedMenuChoiceValue);
            }
        }

        // ========================== DISPLAY ==========================

        private void PrintHeader()
        {
            Console.WriteLine("\n========= WELCOME TO QUANTITY MEASUREMENT TOOL =========");

            Console.WriteLine("\n[LENGTH OPERATIONS MENU]");
            Console.WriteLine(" 1) Compare Lengths   2) Convert Length   3) Add Length   4) Subtract Length   5) Divide Length");

            Console.WriteLine("\n[WEIGHT OPERATIONS MENU]");
            Console.WriteLine(" 6) Compare Weights   7) Convert Weight   8) Add Weight   9) Subtract Weight   10) Divide Weight");

            Console.WriteLine("\n[VOLUME OPERATIONS MENU]");
            Console.WriteLine("11) Compare Volumes  12) Convert Volume  13) Add Volume  14) Subtract Volume  15) Divide Volume");

            Console.WriteLine("\n[TEMPERATURE OPERATIONS MENU]");
            Console.WriteLine("16) Compare Temperatures  17) Convert Temperature  18) Add Temperatures");

            Console.WriteLine("\n0) Exit Application");
        }

        private int GetChoice()
        {
            Console.Write("\n👉 Enter your option: ");
            int.TryParse(Console.ReadLine(), out int parsedUserChoiceValue);
            return parsedUserChoiceValue;
        }

        // ========================== HANDLER ==========================

        private void HandleSelection(int userSelectedMenuChoiceValue)
        {
            switch (userSelectedMenuChoiceValue)
            {
                case 1: Console.WriteLine("\n👉 You selected: Compare Lengths"); Compare("Length"); break;
                case 2: Console.WriteLine("\n👉 You selected: Convert Length"); Convert("Length"); break;
                case 3: Console.WriteLine("\n👉 You selected: Add Length"); Add("Length"); break;
                case 4: Console.WriteLine("\n👉 You selected: Subtract Length"); Subtract("Length"); break;
                case 5: Console.WriteLine("\n👉 You selected: Divide Length"); Divide("Length"); break;

                case 6: Console.WriteLine("\n👉 You selected: Compare Weights"); Compare("Weight"); break;
                case 7: Console.WriteLine("\n👉 You selected: Convert Weight"); Convert("Weight"); break;
                case 8: Console.WriteLine("\n👉 You selected: Add Weight"); Add("Weight"); break;
                case 9: Console.WriteLine("\n👉 You selected: Subtract Weight"); Subtract("Weight"); break;
                case 10: Console.WriteLine("\n👉 You selected: Divide Weight"); Divide("Weight"); break;

                case 11: Console.WriteLine("\n👉 You selected: Compare Volumes"); Compare("Volume"); break;
                case 12: Console.WriteLine("\n👉 You selected: Convert Volume"); Convert("Volume"); break;
                case 13: Console.WriteLine("\n👉 You selected: Add Volume"); Add("Volume"); break;
                case 14: Console.WriteLine("\n👉 You selected: Subtract Volume"); Subtract("Volume"); break;
                case 15: Console.WriteLine("\n👉 You selected: Divide Volume"); Divide("Volume"); break;

                case 16: Console.WriteLine("\n👉 You selected: Compare Temperatures"); Compare("Temperature"); break;
                case 17: Console.WriteLine("\n👉 You selected: Convert Temperature"); Convert("Temperature"); break;
                case 18: Console.WriteLine("\n👉 You selected: Add Temperatures"); AddTemperature(); break;

                default:
                    Console.WriteLine("❌ Invalid selection! Please choose a valid option from the menu.");
                    break;
            }
        }

        // ========================== OPERATIONS ==========================

        private void Compare(string type)
        {
            var firstQuantityInputObject = Input(type, "First");
            var secondQuantityInputObject = Input(type, "Second");

            if (type == "Length")
                extremelyImportantControllerInstanceUsedForHandlingAllMenuOperations.PerformLengthComparisonBetweenTwoGivenLengthQuantityObjectsAndDisplayWhetherTheyAreEqualOrNot(firstQuantityInputObject, secondQuantityInputObject);

            else if (type == "Weight")
                extremelyImportantControllerInstanceUsedForHandlingAllMenuOperations.PerformWeightComparisonBetweenTwoGivenWeightQuantitiesAndDisplayResult(firstQuantityInputObject, secondQuantityInputObject);

            else if (type == "Volume")
                extremelyImportantControllerInstanceUsedForHandlingAllMenuOperations.PerformVolumeComparisonBetweenTwoGivenVolumeQuantitiesAndDisplayResult(firstQuantityInputObject, secondQuantityInputObject);

            else
                extremelyImportantControllerInstanceUsedForHandlingAllMenuOperations.PerformTemperatureComparisonBetweenTwoTemperatureQuantitiesAndDisplayResult(firstQuantityInputObject, secondQuantityInputObject);
        }

        private void Convert(string type)
        {
            var quantityInputObjectForConversion = Input(type, "");

            Console.Write("👉 Enter target unit: ");
            string targetUnitForConversion = Console.ReadLine();

            if (type == "Length")
                extremelyImportantControllerInstanceUsedForHandlingAllMenuOperations.PerformLengthConversionFromOneUnitToAnotherUnitAndDisplayTheConvertedResult(quantityInputObjectForConversion, targetUnitForConversion);

            else if (type == "Weight")
                extremelyImportantControllerInstanceUsedForHandlingAllMenuOperations.PerformWeightConversionFromOneUnitToAnotherUnitAndDisplayResult(quantityInputObjectForConversion, targetUnitForConversion);

            else if (type == "Volume")
                extremelyImportantControllerInstanceUsedForHandlingAllMenuOperations.PerformVolumeConversionFromOneUnitToAnotherUnitAndDisplayResult(quantityInputObjectForConversion, targetUnitForConversion);

            else
                extremelyImportantControllerInstanceUsedForHandlingAllMenuOperations.PerformTemperatureConversionFromOneUnitToAnotherUnitAndDisplayResult(quantityInputObjectForConversion, targetUnitForConversion);
        }

        private void Add(string type)
        {
            var firstQuantityInputObject = Input(type, "First");
            var secondQuantityInputObject = Input(type, "Second");

            Console.Write("👉 Enter result unit: ");
            string targetUnitForAddition = Console.ReadLine();

            if (type == "Length")
                extremelyImportantControllerInstanceUsedForHandlingAllMenuOperations.PerformAdditionOfTwoLengthQuantitiesAndDisplayResultInSpecifiedTargetUnit(firstQuantityInputObject, secondQuantityInputObject, targetUnitForAddition);

            else if (type == "Weight")
                extremelyImportantControllerInstanceUsedForHandlingAllMenuOperations.PerformAdditionOfTwoWeightQuantitiesAndDisplayResult(firstQuantityInputObject, secondQuantityInputObject, targetUnitForAddition);

            else if (type == "Volume")
                extremelyImportantControllerInstanceUsedForHandlingAllMenuOperations.PerformVolumeAdditionBetweenTwoVolumeQuantitiesAndDisplayResult(firstQuantityInputObject, secondQuantityInputObject, targetUnitForAddition);
        }

        private void Subtract(string type)
        {
            var firstQuantityInputObject = Input(type, "First");
            var secondQuantityInputObject = Input(type, "Second");

            Console.Write("👉 Enter result unit: ");
            string targetUnitForSubtraction = Console.ReadLine();

            if (type == "Length")
                extremelyImportantControllerInstanceUsedForHandlingAllMenuOperations.PerformSubtractionOfTwoLengthQuantitiesAndDisplayResultInSpecifiedTargetUnit(firstQuantityInputObject, secondQuantityInputObject, targetUnitForSubtraction);

            else if (type == "Weight")
                extremelyImportantControllerInstanceUsedForHandlingAllMenuOperations.PerformSubtractionOfTwoWeightQuantitiesAndDisplayResult(firstQuantityInputObject, secondQuantityInputObject, targetUnitForSubtraction);

            else if (type == "Volume")
                extremelyImportantControllerInstanceUsedForHandlingAllMenuOperations.PerformVolumeSubtractionBetweenTwoVolumeQuantitiesAndDisplayResult(firstQuantityInputObject, secondQuantityInputObject, targetUnitForSubtraction);
        }

        private void Divide(string type)
        {
            var firstQuantityInputObject = Input(type, "First");
            var secondQuantityInputObject = Input(type, "Second");

            if (type == "Length")
                extremelyImportantControllerInstanceUsedForHandlingAllMenuOperations.PerformDivisionOfTwoLengthQuantitiesAndDisplayNumericalResult(firstQuantityInputObject, secondQuantityInputObject);

            else if (type == "Weight")
                extremelyImportantControllerInstanceUsedForHandlingAllMenuOperations.PerformDivisionOfTwoWeightQuantitiesAndDisplayResult(firstQuantityInputObject, secondQuantityInputObject);

            else if (type == "Volume")
                extremelyImportantControllerInstanceUsedForHandlingAllMenuOperations.PerformVolumeDivisionBetweenTwoVolumeQuantitiesAndDisplayResult(firstQuantityInputObject, secondQuantityInputObject);
        }

        private void AddTemperature()
        {
            var firstTemperatureQuantityObject = Input("Temperature", "First");
            var secondTemperatureQuantityObject = Input("Temperature", "Second");

            Console.Write("👉 Enter result unit: ");
            string targetUnitForTemperatureAddition = Console.ReadLine();

            extremelyImportantControllerInstanceUsedForHandlingAllMenuOperations.PerformTemperatureArithmeticOperationBetweenTwoTemperatureQuantitiesAndDisplayResult(firstTemperatureQuantityObject, secondTemperatureQuantityObject, targetUnitForTemperatureAddition);
        }

        // ========================== INPUT ==========================

        private QuantityDTO Input(string type, string label)
        {
            string prefix = string.IsNullOrEmpty(label) ? "" : label + " ";

            Console.Write($"👉 Enter {prefix}Value: ");
            double parsedNumericValue = double.Parse(Console.ReadLine());

            Console.Write($"👉 Enter {prefix}Unit: ");
            string unitInputFromUser = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(unitInputFromUser))
            {
                Console.WriteLine("❌ Unit cannot be empty! Please enter a valid unit.");
                return Input(type, label);
            }

            return new QuantityDTO(parsedNumericValue, unitInputFromUser, type);
        }
    }
}