using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuantityMeasurementApp
{
    // Implementation class for weight measurement that follows the IMeasurable interface
    public class WeightMeasurementImpl : IMeasurable
    {
        // Stores the type of weight unit (Kilogram, Gram, Pound)
        private WeightUnit unit;

        // Constructor used to initialize the weight unit
        public WeightMeasurementImpl(WeightUnit unitType)
        {
            unit = unitType;
        }

        // Returns the conversion factor relative to the base unit (Kilogram)
        public double GetConversionFactor()
        {
            switch (unit)
            {
                case WeightUnit.Kilogram:
                    return 1.0;          // Base unit

                case WeightUnit.Gram:
                    return 0.001;        // 1 gram = 0.001 kilogram

                case WeightUnit.Pound:
                    return 0.453592;     // 1 pound = 0.453592 kilogram

                default:
                    // Handles invalid unit values
                    throw new ArgumentException("Invalid Weight Unit");
            }
        }

        // Converts a given value to the base unit (Kilogram)
        public double ConvertToBaseUnit(double value)
        {
            return value * GetConversionFactor();
        }

        // Converts a value from the base unit (Kilogram) to the current unit
        public double ConvertFromBaseUnit(double baseValue)
        {
            return baseValue / GetConversionFactor();
        }

        // Returns the name of the current weight unit
        public string GetUnitName()
        {
            return unit.ToString();
        }
    }
}