using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuantityMeasurementApp
{
    // Interface defining common behavior for measurable units
    // Any class implementing this interface must provide
    // conversion logic and unit information
    public interface IMeasurable
    {
        // Returns the conversion factor relative to the base unit
        double GetConversionFactor();

        // Converts a given value to the base unit
        double ConvertToBaseUnit(double value);

        // Converts a value from the base unit to the specific unit
        double ConvertFromBaseUnit(double baseValue);

        // Returns the name of the unit
        string GetUnitName();
    }
}