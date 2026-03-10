using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantityMeasurementApp
{
    // Enum representing different weight units
    public enum WeightUnit
    {
        KILOGRAM,
        GRAM,
        POUND
    }

    // Static class containing helper methods for weight conversions
    public static class WeightUnitExtensions
    {
        // Returns the conversion factor of the unit relative to the base unit (Kilogram)
        public static double GetConversionFactor(WeightUnit unit)
        {
            switch (unit)
            {
                case WeightUnit.KILOGRAM:
                    return 1.0; // Base unit

                case WeightUnit.GRAM:
                    return 0.001; // 1 gram = 0.001 kilogram

                case WeightUnit.POUND:
                    return 0.453592; // 1 pound = 0.453592 kilogram

                default:
                    // Throws exception if an invalid unit is provided
                    throw new ArgumentException("Invalid unit");
            }
        }

        // Converts a value from the given unit to the base unit (Kilogram)
        public static double ConvertToBaseUnit(WeightUnit unit, double value)
        {
            return value * GetConversionFactor(unit);
        }

        // Converts a value from the base unit (Kilogram) to the specified unit
        public static double ConvertFromBaseUnit(WeightUnit unit, double baseValue)
        {
            return baseValue / GetConversionFactor(unit);
        }
    }
}