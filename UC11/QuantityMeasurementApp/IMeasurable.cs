using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuantityMeasurementApp
{
    // Interface defining the contract for all measurable units
    // Any measurement implementation (Length, Weight, Volume, etc.)
    // must implement these methods to support conversion and unit identification
    public interface IMeasurable
    {
        // Returns the conversion factor relative to the base unit
        double GetConversionFactor();

        // Converts a given value to the base unit of the measurement category
        double ConvertToBaseUnit(double value);

        // Converts a value from the base unit back to the specific unit
        double ConvertFromBaseUnit(double baseValue);

        // Returns the name of the measurement unit
        string GetUnitName();
    }
}