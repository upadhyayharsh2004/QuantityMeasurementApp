using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuantityMeasurementApp
{
    // Implementation of IMeasurable for volume measurements
    // Handles conversion logic and arithmetic support for volume units
    public class VolumeMeasurementImpl : IMeasurable
    {
        // Stores the selected volume unit (Litre, Millilitre, Gallon)
        private VolumeUnit unit;

        // Constructor used to initialize the volume unit
        public VolumeMeasurementImpl(VolumeUnit unitType)
        {
            unit = unitType;
        }

        // Returns the conversion factor relative to the base unit (Litre)
        public double GetConversionFactor()
        {
            switch (unit)
            {
                case VolumeUnit.Litre:
                    return 1.0; // Base unit

                case VolumeUnit.Millilitre:
                    return 0.001; // 1 millilitre = 0.001 litre

                case VolumeUnit.Gallon:
                    return 3.78541; // 1 gallon ≈ 3.78541 litres

                default:
                    // Handles invalid unit values
                    throw new ArgumentException("Invalid Volume Unit");
            }
        }

        // Converts a value from the current unit to the base unit (Litre)
        public double ConvertToBaseUnit(double value)
        {
            return value * GetConversionFactor();
        }

        // Converts a value from the base unit (Litre) back to the current unit
        public double ConvertFromBaseUnit(double baseValue)
        {
            return baseValue / GetConversionFactor();
        }

        // Returns the name of the current volume unit
        public string GetUnitName()
        {
            return unit.ToString();
        }

        // Indicates that arithmetic operations are supported for volume measurements
        public bool SupportsArithmetic()
        {
            return true;
        }

        // Validates arithmetic operation support
        // For volume measurements, all arithmetic operations are allowed
        public void ValidateOperationSupport(string operation)
        {
            // All arithmetic operations supported
        }
    }
}