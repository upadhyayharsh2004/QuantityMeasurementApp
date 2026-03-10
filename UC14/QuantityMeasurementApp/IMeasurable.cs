using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuantityMeasurementApp
{
    // Interface defining the behavior required for all measurable units
    // Any measurement type (Length, Weight, Volume, Temperature, etc.) must implement this interface
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

        // -------- UC-14: Temperature Measurement with Selective Arithmetic Support --------

        // Indicates whether arithmetic operations are supported for this measurement type
        bool SupportsArithmetic();

        // Validates if a specific arithmetic operation is allowed for this measurement type
        void ValidateOperationSupport(string operation);
    }
}