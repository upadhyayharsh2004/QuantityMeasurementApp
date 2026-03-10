using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantityMeasurementApp
{
    // Enum representing different length units
    public enum LengthUnit
    {
        FEET,
        INCH,
        YARD,
        CENTIMETER
    }

    // Static class containing helper methods for length unit conversions
    public static class LengthUnitExtensions
    {
        // Returns the conversion factor relative to the base unit (Feet)
        public static double GetConversionFactor(LengthUnit unit)
        {
            switch (unit)
            {
                case LengthUnit.FEET:
                    return 1.0; // Base unit

                case LengthUnit.INCH:
                    return 1.0 / 12.0; // 1 inch = 1/12 feet

                case LengthUnit.YARD:
                    return 3.0; // 1 yard = 3 feet

                case LengthUnit.CENTIMETER:
                    return 1.0 / 30.48; // 1 cm = 1/30.48 feet

                default:
                    // Throws exception if an invalid unit is passed
                    throw new ArgumentException("Invalid unit");
            }
        }

        // Converts a value from the specified unit to the base unit (Feet)
        public static double ConvertToBaseUnit(LengthUnit unit, double value)
        {
            return value * GetConversionFactor(unit);
        }

        // Converts a value from the base unit (Feet) to the specified unit
        public static double ConvertFromBaseUnit(LengthUnit unit, double baseValue)
        {
            return baseValue / GetConversionFactor(unit);
        }
    }
}