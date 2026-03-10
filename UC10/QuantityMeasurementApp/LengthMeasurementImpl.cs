using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuantityMeasurementApp
{
    // Implementation of IMeasurable interface for Length measurements
    public class LengthMeasurementImpl : IMeasurable
    {
        // Stores the length unit type
        private LengthUnit unit;

        // Constructor to initialize the length unit
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
                    return 0.0328084; // 1 cm = 0.0328084 feet

                default:
                    throw new ArgumentException("Invalid Length Unit");
            }
        }

        // Converts a value from the current unit to the base unit
        public double ConvertToBaseUnit(double value)
        {
            return value * GetConversionFactor();
        }

        // Converts a value from the base unit to the current unit
        public double ConvertFromBaseUnit(double baseValue)
        {
            return baseValue / GetConversionFactor();
        }

        // Returns the name of the unit
        public string GetUnitName()
        {
            return unit.ToString();
        }
    }
}