using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using QuantityMeasurementAppModels.DTOs;

namespace QuantityMeasurementAppServices.Interfaces
{
    public interface IQuantityServiceImplsConvert
    {
        QuantityDTOs Combine(QuantityDTOs firstValue, QuantityDTOs secondValue, string targetUnitValued);
        QuantityDTOs Difference(QuantityDTOs firstValue, QuantityDTOs secondValue, string targetUnitValued);

        double Divison(QuantityDTOs firstValue, QuantityDTOs secondValue);

        bool Comparison(QuantityDTOs firstValue, QuantityDTOs secondValue);

        QuantityDTOs Conversion(QuantityDTOs quantityValued, string targetUnitValued);
    }
}