using QuantityMeasurementApp.Controllers;
using QuantityMeasurementAppModels.DTOs;
using QuantityMeasurementApp.Interfaces;

namespace QuantityMeasurementApp.Menu
{
    public class QuantityMenu : IMenu
    {
        private readonly QuantityMeasurementController controller;

        public QuantityMenu(QuantityMeasurementController controller)
        {
            this.controller = controller;
        }

        public void Run()
        {
            while (true)
            {
                PrintHeader();
                int choice = GetChoice();

                if (choice == 0)
                {
                    Console.WriteLine("Thank you for using the app!");
                    break;
                }

                HandleSelection(choice);
            }
        }

        // ---------------- DISPLAY ----------------
        private void PrintHeader()
        {
            Console.WriteLine("\n========= QUANTITY TOOL =========");

            Console.WriteLine("\n[LENGTH]");
            Console.WriteLine(" 1) Compare   2) Convert   3) Add   4) Subtract   5) Divide");

            Console.WriteLine("\n[WEIGHT]");
            Console.WriteLine(" 6) Compare   7) Convert   8) Add   9) Subtract   10) Divide");

            Console.WriteLine("\n[VOLUME]");
            Console.WriteLine("11) Compare  12) Convert  13) Add  14) Subtract  15) Divide");

            Console.WriteLine("\n[TEMPERATURE]");
            Console.WriteLine("16) Compare  17) Convert  18) Add");

            Console.WriteLine("\n0) Exit");
        }

        private int GetChoice()
        {
            Console.Write("\nEnter option: ");
            int.TryParse(Console.ReadLine(), out int choice);
            return choice;
        }

        // ---------------- HANDLER ----------------
        private void HandleSelection(int choice)
        {
            switch (choice)
            {
                case 1: Compare("Length"); break;
                case 2: Convert("Length"); break;
                case 3: Add("Length"); break;
                case 4: Subtract("Length"); break;
                case 5: Divide("Length"); break;

                case 6: Compare("Weight"); break;
                case 7: Convert("Weight"); break;
                case 8: Add("Weight"); break;
                case 9: Subtract("Weight"); break;
                case 10: Divide("Weight"); break;

                case 11: Compare("Volume"); break;
                case 12: Convert("Volume"); break;
                case 13: Add("Volume"); break;
                case 14: Subtract("Volume"); break;
                case 15: Divide("Volume"); break;

                case 16: Compare("Temperature"); break;
                case 17: Convert("Temperature"); break;
                case 18: AddTemperature(); break;

                default:
                    Console.WriteLine("Invalid selection!");
                    break;
            }
        }

        // ---------------- OPERATIONS ----------------

        private void Compare(string type)
        {
            var a = Input(type, "First");
            var b = Input(type, "Second");

            if (type == "Length") controller.PerformLengthComparison(a, b);
            else if (type == "Weight") controller.PerformWeightComparison(a, b);
            else if (type == "Volume") controller.PerformVolumeComparison(a, b);
            else controller.PerformTemperatureComparison(a, b);
        }

        private void Convert(string type)
        {
            var q = Input(type, "");

            Console.Write("Convert to: ");
            string target = Console.ReadLine();

            if (type == "Length") controller.PerformLengthConversion(q, target);
            else if (type == "Weight") controller.PerformWeightConversion(q, target);
            else if (type == "Volume") controller.PerformVolumeConversion(q, target);
            else controller.PerformTemperatureConversion(q, target);
        }

        private void Add(string type)
        {
            var a = Input(type, "First");
            var b = Input(type, "Second");

            Console.Write("Result Unit: ");
            string target = Console.ReadLine();

            if (type == "Length") controller.PerformLengthAddition(a, b, target);
            else if (type == "Weight") controller.PerformWeightAddition(a, b, target);
            else if (type == "Volume") controller.PerformVolumeAddition(a, b, target);
        }

        private void Subtract(string type)
        {
            var a = Input(type, "First");
            var b = Input(type, "Second");

            Console.Write("Result Unit: ");
            string target = Console.ReadLine();

            if (type == "Length") controller.PerformLengthSubtraction(a, b, target);
            else if (type == "Weight") controller.PerformWeightSubtraction(a, b, target);
            else if (type == "Volume") controller.PerformVolumeSubtraction(a, b, target);
        }

        private void Divide(string type)
        {
            var a = Input(type, "First");
            var b = Input(type, "Second");

            if (type == "Length") controller.PerformLengthDivision(a, b);
            else if (type == "Weight") controller.PerformWeightDivision(a, b);
            else if (type == "Volume") controller.PerformVolumeDivision(a, b);
        }

        private void AddTemperature()
        {
            var a = Input("Temperature", "First");
            var b = Input("Temperature", "Second");

            Console.Write("Result Unit: ");
            string target = Console.ReadLine();

            controller.PerformTemperatureArithmetic(a, b, target);
        }

        // ---------------- INPUT ----------------

        private QuantityDTO Input(string type, string label)
        {
            string prefix = string.IsNullOrEmpty(label) ? "" : label + " ";

            Console.Write($"{prefix}Value: ");
            double value = double.Parse(Console.ReadLine());

            Console.Write($"{prefix}Unit: ");
            string unit = Console.ReadLine();

            // Basic validation
            if (string.IsNullOrWhiteSpace(unit))
            {
                Console.WriteLine("Unit cannot be empty!");
                return Input(type, label);
            }

            return new QuantityDTO(value, unit, type);
        }
    }
}