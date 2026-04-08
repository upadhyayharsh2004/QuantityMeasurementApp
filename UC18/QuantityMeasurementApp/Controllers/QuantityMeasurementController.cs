using QuantityMeasurementAppModels.DTOs;
using QuantityMeasurementAppServices.Interfaces;

namespace QuantityMeasurementApp.Controllers
{
    public class QuantityMeasurementController
    {
        private IQuantityServiceImplsConvert quantityService;
        public void ExecuteLengthValuesDivision(QuantityDTOs firstValue, QuantityDTOs secondValue)
        {
            try
            {
                double resultValue = quantityService.Divison(firstValue, secondValue);
                Console.WriteLine(resultValue);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error Message Caught: " + exception.Message);
            }
        }
        public void ExecuteWeightValuesComparison(QuantityDTOs firstValue, QuantityDTOs secondValue)
        {
            try
            {
                bool resultValue = quantityService.Comparison(firstValue, secondValue);
                if (resultValue)
                {
                    Console.WriteLine("Both Values Weights Are Equal");
                }
                else
                {
                    Console.WriteLine("Both Values Weights Are Not Equal");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error Message Caught: " + exception.Message);
            }
        }
        public void ExecuteLengthValuesComparison(QuantityDTOs firstValue, QuantityDTOs secondValue)
        {
            try
            {
                bool resultValue = quantityService.Comparison(firstValue, secondValue);
                if (resultValue)
                {
                    Console.WriteLine("Both Value Lengths Are Equal");
                }
                else
                {
                    Console.WriteLine("Both Value Lengths Are Not Equal");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error Message Caught: " + exception.Message);
            }
        }
        public void ExecuteWeightValuesConversion(QuantityDTOs quantityValue, string targetUnitValue)
        {
            try
            {
                QuantityDTOs resultValue = quantityService.Conversion(quantityValue, targetUnitValue);
                Console.WriteLine(resultValue);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error Message Caught: " + exception.Message);
            }
        }
        public void ExecuteWeightValuesAddition(QuantityDTOs firstValue, QuantityDTOs secondValue, string targetUnitValue)
        {
            try
            {
                QuantityDTOs resultValue = quantityService.Combine(firstValue, secondValue, targetUnitValue);
                Console.WriteLine(resultValue);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error Message Caught: " + exception.Message);
            }
        }
        public void ExecuteTemperatureValuesArithmetic(QuantityDTOs firstValue, QuantityDTOs secondValue, string targetUnitValue)
        {
            try
            {
                QuantityDTOs resultValue = quantityService.Combine(firstValue, secondValue, targetUnitValue);
                Console.WriteLine(resultValue);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error Message Caught: " + exception.Message);
            }
        }
        public void ExecuteWeightValuesDivision(QuantityDTOs firstValue, QuantityDTOs secondValue)
        {
            try
            {
                double resultValue = quantityService.Divison(firstValue, secondValue);
                Console.WriteLine(resultValue);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error Message Caught: " + exception.Message);
            }
        }
        public void ExecuteVolumeValuesComparison(QuantityDTOs firstValue, QuantityDTOs secondValue)
        {
            try
            {
                bool resultValue = quantityService.Comparison(firstValue, secondValue);
                if (resultValue)
                {
                    Console.WriteLine("Volumes Values Are Equal");
                }
                else
                {
                    Console.WriteLine("Volumes Values Are Not Equal");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error Message Caught: " + exception.Message);
            }
        }
        public void ExecuteVolumeValuesConversion(QuantityDTOs quantityValue, string targetUnitValue)
        {
            try
            {
                QuantityDTOs resultValue = quantityService.Conversion(quantityValue, targetUnitValue);
                Console.WriteLine(resultValue);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error Message Caught: " + exception.Message);
            }
        }
        public QuantityMeasurementController(IQuantityServiceImplsConvert QuantityService)
        {
            if (QuantityService == null)
            {
                throw new ArgumentException("Quantity Value Service cannot be null");
            }
            this.quantityService = QuantityService;
        }
        public void ExecuteLengthValuesSubtraction(QuantityDTOs firstValue, QuantityDTOs secondValue, string targetUnitValue)
        {
            try
            {
                QuantityDTOs resultValue = quantityService.Difference(firstValue, secondValue, targetUnitValue);
                Console.WriteLine(resultValue);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error Message Caught: " + exception.Message);
            }
        }
        public void ExecuteLengthValuesConversion(QuantityDTOs quantityValue, string targetUnitValue)
        {
            try
            {
                QuantityDTOs resultValue = quantityService.Conversion(quantityValue, targetUnitValue);
                Console.WriteLine(resultValue);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error Message Caught: " + exception.Message);
            }
        }
        public void ExecuteVolumeValuesDivision(QuantityDTOs firstValue, QuantityDTOs secondValue)
        {
            try
            {
                double resultValue = quantityService.Divison(firstValue, secondValue);
                Console.WriteLine(resultValue);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error Message Caught: " + exception.Message);
            }
        }
        public void ExecuteTemperatureValuesComparison(QuantityDTOs firstValue, QuantityDTOs secondValue)
        {
            try
            {
                bool resultValue = quantityService.Comparison(firstValue, secondValue);
                if (resultValue)
                {
                    Console.WriteLine("Temperatures Both Values Are Equal");
                }
                else
                {
                    Console.WriteLine("Temperatures Both Values Are Not Equal");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error Message Caught: " + exception.Message);
            }
        }
        public void ExecuteTemperatureValuesConversion(QuantityDTOs quantityValue, string targetUnitValue)
        {
            try
            {
                QuantityDTOs resultValue = quantityService.Conversion(quantityValue, targetUnitValue);
                Console.WriteLine(resultValue);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error Message Caught: " + exception.Message);
            }
        }
        public void ExecuteLengthValuesAddition(QuantityDTOs firstValue, QuantityDTOs secondValue, string targetUnitValue)
        {
            try
            {
                QuantityDTOs resultValue = quantityService.Combine(firstValue, secondValue, targetUnitValue);
                Console.WriteLine(resultValue);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error Message Caught: " + exception.Message);
            }
        }
        public void ExecuteWeightvaluesSubtraction(QuantityDTOs firstValue, QuantityDTOs secondValue, string targetUnitValue)
        {
            try
            {
                QuantityDTOs resultValue = quantityService.Difference(firstValue, secondValue, targetUnitValue);
                Console.WriteLine(resultValue);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error Message Caught: " + exception.Message);
            }
        }
        public void ExecuteVolumeValuesAddition(QuantityDTOs firstValue, QuantityDTOs secondValue, string targetUnitValue)
        {
            try
            {
                QuantityDTOs resultValue = quantityService.Combine(firstValue, secondValue, targetUnitValue);
                Console.WriteLine(resultValue);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error Message Caught: " + exception.Message);
            }
        }
        public void ExecuteVolumeValuesSubtraction(QuantityDTOs firstValue, QuantityDTOs secondValue, string targetUnitValue)
        {
            try
            {
                QuantityDTOs resultValue = quantityService.Difference(firstValue, secondValue, targetUnitValue);
                Console.WriteLine(resultValue);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error Message Caught: " + exception.Message);
            }
        }
    }
}