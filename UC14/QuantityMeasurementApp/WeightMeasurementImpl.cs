using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuantityMeasurementApp
{
    //Implementation of IMeasurable for handling weight measurements
    public class WeightMeasurementImpl : IMeasurable
    {
        //Stores the selected weight unit
        private WeightUnit unit;

        //Constructor to initialize the weight unit type
        public WeightMeasurementImpl(WeightUnit unitType)
        {
            unit = unitType;
        }

        //Returns the conversion factor relative to the base unit (Kilogram)
        public double GetConversionFactor()
        {
            switch (unit)
            {
                case WeightUnit.Kilogram:
                    //Base unit for weight
                    return 1.0;

                case WeightUnit.Gram:
                    //1 gram = 0.001 kilogram
                    return 0.001;

                case WeightUnit.Pound:
                    //1 pound = 0.453592 kilograms
                    return 0.453592;

                default:
                    //Handles invalid or unsupported weight units
                    throw new ArgumentException("Invalid Weight Unit");
            }
        }

        //Converts a value from the current unit to the base unit (Kilogram)
        public double ConvertToBaseUnit(double value)
        {
            return value * GetConversionFactor();
        }

        //Converts a value from the base unit (Kilogram) to the current unit
        public double ConvertFromBaseUnit(double baseValue)
        {
            return baseValue / GetConversionFactor();
        }

        //Returns the name of the weight unit
        public string GetUnitName()
        {
            return unit.ToString();
        }

        //Indicates that arithmetic operations are supported for weight measurements
        public bool SupportsArithmetic()
        {
            return true;
        }

        //Validates arithmetic operations (no restrictions for weight measurements)
        public void ValidateOperationSupport(string operation)
        {
            //All arithmetic operations are allowed for weight measurements
        }
    }
}