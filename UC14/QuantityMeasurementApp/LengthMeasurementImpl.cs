using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuantityMeasurementApp
{
    // Implementation of IMeasurable for length measurements
    // Handles conversion logic and arithmetic support for length units
    public class LengthMeasurementImpl : IMeasurable
    {
        // Stores the selected length unit (Feet, Inch, Yard, Centimeter)
        private LengthUnit unit;

        // Constructor used to initialize the length unit
        public LengthMeasurementImpl(LengthUnit unitType)
        {
            unit = unitType;
        }

        // Returns the conversion factor relative to the base unit (Feet)
        public double GetConversionFactor()
        {
            switch (unit)
            {
                case LengthUnit.Feet:
                    return 1.0; // Base unit

                case LengthUnit.Inch:
                    return 1.0 / 12.0; // 1 inch = 1/12 feet

                case LengthUnit.Yard:
                    return 3.0; // 1 yard = 3 feet

                case LengthUnit.Centimeter:
                    return 0.0328084; // 1 centimeter ≈ 0.0328084 feet

                default:
                    // Handles invalid unit values
                    throw new ArgumentException("Invalid Length Unit");
            }
        }

        // Converts a value from the current unit to the base unit (Feet)
        public double ConvertToBaseUnit(double value)
        {
            return value * GetConversionFactor();
        }

        // Converts a value from the base unit (Feet) back to the current unit
        public double ConvertFromBaseUnit(double baseValue)
        {
            return baseValue / GetConversionFactor();
        }

        // Returns the name of the current length unit
        public string GetUnitName()
        {
            return unit.ToString();
        }

        // Indicates that arithmetic operations are supported for length measurements
        public bool SupportsArithmetic()
        {
            return true;
        }

        // Validates arithmetic operations for length measurements
        // No restrictions: all arithmetic operations are allowed
        public void ValidateOperationSupport(string operation)
        {
            // All arithmetic operations supported
        }
    }
}