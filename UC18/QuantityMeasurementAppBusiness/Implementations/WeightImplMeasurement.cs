using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuantityMeasurementAppBusiness.Interfaces;
using QuantityMeasurementAppModels.Enums;

namespace QuantityMeasurementAppBusiness.Implementations
{
    public class WeightImplMeasurement : IMeasurable
    {
        private MassUnit massedUnit;
        public bool SupportsArithmetic()
        {
            return true;
        }
        public void ValidateOperationSupport(string operation)
        {
        }
        public string GetMeasurementType()
        {
            return "Weight";
        }
        public WeightImplMeasurement(MassUnit massedUnitType)
        {
            massedUnit = massedUnitType;
        }
        public double FetchConversionFactor()
        {
            switch (massedUnit)
            {
                case MassUnit.Kilogram:
                    return 1.0;

                case MassUnit.Gram:
                    return 0.001;

                case MassUnit.Pound:
                    return 0.453592;

                default:
                    throw new ArgumentException("Invalid Typed Volume Unit For Operations");
            }
        }
        public string FetchUnitName()
        {
            return massedUnit.ToString();
        }
        public double NormalizeToBaseUnit(double value)
        {
            return value * FetchConversionFactor();
        }
        public double NormalizeFromBaseUnit(double baseValue)
        {
            return baseValue / FetchConversionFactor();
        }
        public IMeasurable GetUnitByName(string convertedUnitName)
        {
            MassUnit convertedParsedUnit = (MassUnit)Enum.Parse(typeof(MassUnit), convertedUnitName, true);
            return new WeightImplMeasurement(convertedParsedUnit);
        }
    }
}