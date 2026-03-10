using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuantityMeasurementApp
{
    //Implementation of IMeasurable for handling length measurements
    public class LengthMeasurementImpl : IMeasurable
    {
        //Stores the specific length unit
        private LengthUnit unit;

        //Constructor to initialize the length unit type
        public LengthMeasurementImpl(LengthUnit unitType)
        {
            unit = unitType;
        }

        //Returns the conversion factor relative to the base unit (Feet)
        public double GetConversionFactor()
        {
            switch (unit)
            {
                case LengthUnit.Feet:
                    //Base unit for length
                    return 1.0;

                case LengthUnit.Inch:
                    //1 inch = 1/12 feet
                    return 1.0 / 12.0;

                case LengthUnit.Yard:
                    //1 yard = 3 feet
                    return 3.0;

                case LengthUnit.Centimeter:
                    //1 centimeter = 0.0328084 feet
                    return 0.0328084;

                default:
                    //Handles invalid or unsupported length units
                    throw new ArgumentException("Invalid Length Unit");
            }
        }

        //Converts a value from the current unit to the base unit (Feet)
        public double ConvertToBaseUnit(double value)
        {
            return value * GetConversionFactor();
        }

        //Converts a value from the base unit (Feet) to the current unit
        public double ConvertFromBaseUnit(double baseValue)
        {
            return baseValue / GetConversionFactor();
        }

        //Returns the name of the length unit
        public string GetUnitName()
        {
            return unit.ToString();
        }

        //Indicates that arithmetic operations are supported for length measurements
        public bool SupportsArithmetic()
        {
            return true;
        }

        //Validates arithmetic operations (no restrictions for length measurements)
        public void ValidateOperationSupport(string operation)
        {
            //All arithmetic operations are allowed for length measurements
        }
    }
}