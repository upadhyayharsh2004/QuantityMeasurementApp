using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuantityMeasurementApp
{
    //Interface defining common behavior for all measurable units
    public interface IMeasurable
    {
        //Returns the conversion factor relative to the base unit
        double GetConversionFactor();

        //Converts a value from the current unit to the base unit
        double ConvertToBaseUnit(double value);

        //Converts a value from the base unit to the current unit
        double ConvertFromBaseUnit(double baseValue);

        //Returns the name of the unit as a string
        string GetUnitName();

        //UC-14: Temperature Measurement with Selective Arithmetic Support and IMeasurable Refactoring

        //Indicates whether arithmetic operations are supported for the measurement type
        bool SupportsArithmetic();

        //Validates whether a specific arithmetic operation is allowed
        void ValidateOperationSupport(string operation);
    }
}