using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuantityMeasurementAppBusiness.Interfaces
{
    public interface IMeasurable
    {
        double FetchConversionFactor();
        double NormalizeToBaseUnit(double value);
 
        double NormalizeFromBaseUnit(double baseValue);

        string FetchUnitName();

        bool SupportsArithmetic();
        void ValidateOperationSupport(string operation);
        string GetMeasurementType();    
        IMeasurable GetUnitByName(string unitName);  
    }
}