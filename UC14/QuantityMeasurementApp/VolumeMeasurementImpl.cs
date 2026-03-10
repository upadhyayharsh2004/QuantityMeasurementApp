using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuantityMeasurementApp
{
    //Implementation of IMeasurable for handling volume measurements
    public class VolumeMeasurementImpl : IMeasurable
    {
        //Stores the selected volume unit
        private VolumeUnit unit;

        //Constructor to initialize the volume unit type
        public VolumeMeasurementImpl(VolumeUnit unitType)
        {
            unit = unitType;
        }

        //Returns the conversion factor relative to the base unit (Litre)
        public double GetConversionFactor()
        {
            switch (unit)
            {
                case VolumeUnit.Litre:
                    //Base unit for volume
                    return 1.0;

                case VolumeUnit.Millilitre:
                    //1 millilitre = 0.001 litre
                    return 0.001;

                case VolumeUnit.Gallon:
                    //1 gallon = 3.78541 litres
                    return 3.78541;

                default:
                    //Handles invalid or unsupported volume units
                    throw new ArgumentException("Invalid Volume Unit");
            }
        }

        //Converts a value from the current unit to the base unit (Litre)
        public double ConvertToBaseUnit(double value)
        {
            return value * GetConversionFactor();
        }

        //Converts a value from the base unit (Litre) to the current unit
        public double ConvertFromBaseUnit(double baseValue)
        {
            return baseValue / GetConversionFactor();
        }

        //Returns the name of the volume unit
        public string GetUnitName()
        {
            return unit.ToString();
        }

        //Indicates that arithmetic operations are supported for volume measurements
        public bool SupportsArithmetic()
        {
            return true;
        }

        //Validates arithmetic operations (no restrictions for volume measurements)
        public void ValidateOperationSupport(string operation)
        {
            //All arithmetic operations are allowed for volume measurements
        }
    }
}