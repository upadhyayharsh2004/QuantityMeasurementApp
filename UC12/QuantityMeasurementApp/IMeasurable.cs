using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuantityMeasurementApp
{
    // Interface that defines the behavior required for all measurable units
    // Any measurement type (Length, Weight, Volume, etc.) must implement this interface
    public interface IMeasurable
    {
        // Returns the conversion factor relative to the base unit
        double GetConversionFactor();

        // Converts a value from the current unit to the base unit
        double ConvertToBaseUnit(double value);

        // Converts a value from the base unit back to the current unit
        double ConvertFromBaseUnit(double baseValue);

        // Returns the name of the measurement unit
        string GetUnitName();
    }
}