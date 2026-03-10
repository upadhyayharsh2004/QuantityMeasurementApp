using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuantityMeasurementApp
{
    // Implementation of IMeasurable for temperature measurements
    // Handles temperature conversions and restricts arithmetic operations
    public class TemperatureMeasurementImpl : IMeasurable
    {
        // Stores the selected temperature unit (Celsius, Fahrenheit, Kelvin)
        private TemperatureUnit unit;

        // Constructor used to initialize the temperature unit
        public TemperatureMeasurementImpl(TemperatureUnit unitType)
        {
            unit = unitType;
        }

        // Temperature does not use a fixed conversion factor like other measurements
        // This method returns a default value to satisfy the interface contract
        public double GetConversionFactor()
        {
            return 1.0;
        }

        // Converts a temperature value to the base unit (Celsius)
        public double ConvertToBaseUnit(double value)
        {
            switch (unit)
            {
                case TemperatureUnit.Celsius:
                    return value; // Already in base unit

                case TemperatureUnit.Fahrenheit:
                    return (value - 32) * 5 / 9; // Convert Fahrenheit to Celsius

                case TemperatureUnit.Kelvin:
                    return value - 273.15; // Convert Kelvin to Celsius

                default:
                    // Handles invalid temperature unit values
                    throw new ArgumentException("Invalid Temperature Unit");
            }
        }

        // Converts a temperature value from the base unit (Celsius) to the target unit
        public double ConvertFromBaseUnit(double baseValue)
        {
            switch (unit)
            {
                case TemperatureUnit.Celsius:
                    return baseValue;

                case TemperatureUnit.Fahrenheit:
                    return (baseValue * 9 / 5) + 32; // Convert Celsius to Fahrenheit

                case TemperatureUnit.Kelvin:
                    return baseValue + 273.15; // Convert Celsius to Kelvin

                default:
                    // Handles invalid temperature unit values
                    throw new ArgumentException("Invalid Temperature Unit");
            }
        }

        // Returns the name of the current temperature unit
        public string GetUnitName()
        {
            return unit.ToString();
        }

        // Indicates that arithmetic operations are not supported for temperature
        public bool SupportsArithmetic()
        {
            return false;
        }

        // Throws an exception if an arithmetic operation is attempted on temperature
        public void ValidateOperationSupport(string operation)
        {
            throw new NotSupportedException("Temperature does not support " + operation + " operation");
        }
    }
}