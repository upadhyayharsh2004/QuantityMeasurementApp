using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuantityMeasurementApp
{
    //Implementation of IMeasurable for handling temperature measurements
    public class TemperatureMeasurementImpl : IMeasurable
    {
        //Stores the specific temperature unit
        private TemperatureUnit unit;

        //Constructor to initialize the temperature unit type
        public TemperatureMeasurementImpl(TemperatureUnit unitType)
        {
            unit = unitType;
        }

        //Temperature conversion does not rely on a constant conversion factor
        public double GetConversionFactor()
        {
            return 1.0;
        }

        //Converts a temperature value from the current unit to the base unit (Celsius)
        public double ConvertToBaseUnit(double value)
        {
            switch (unit)
            {
                case TemperatureUnit.Celsius:
                    //Already in base unit
                    return value;

                case TemperatureUnit.Fahrenheit:
                    //Convert Fahrenheit to Celsius
                    return (value - 32) * 5 / 9;

                case TemperatureUnit.Kelvin:
                    //Convert Kelvin to Celsius
                    return value - 273.15;

                default:
                    //Handles invalid or unsupported temperature units
                    throw new ArgumentException("Invalid Temperature Unit");
            }
        }

        //Converts a temperature value from the base unit (Celsius) to the target unit
        public double ConvertFromBaseUnit(double baseValue)
        {
            switch (unit)
            {
                case TemperatureUnit.Celsius:
                    //Already in base unit
                    return baseValue;

                case TemperatureUnit.Fahrenheit:
                    //Convert Celsius to Fahrenheit
                    return (baseValue * 9 / 5) + 32;

                case TemperatureUnit.Kelvin:
                    //Convert Celsius to Kelvin
                    return baseValue + 273.15;

                default:
                    //Handles invalid or unsupported temperature units
                    throw new ArgumentException("Invalid Temperature Unit");
            }
        }

        //Returns the name of the temperature unit
        public string GetUnitName()
        {
            return unit.ToString();
        }

        //Indicates that arithmetic operations are not supported for temperature
        public bool SupportsArithmetic()
        {
            return false;
        }

        //Throws an exception if any arithmetic operation is attempted
        public void ValidateOperationSupport(string operation)
        {
            throw new NotSupportedException("Temperature does not support " + operation + " operation");
        }
    }
}