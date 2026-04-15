using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuantityMeasurementAppBusiness.Interfaces;
using QuantityMeasurementAppModels.Enums;

namespace QuantityMeasurementAppBusiness.Implementations
{
    public class VolumeImplMeasurement : IMeasurable
    {
        private VolumeUnitType volumeUnitType;
        public VolumeImplMeasurement(VolumeUnitType convertedUnitType)
        {
            volumeUnitType = convertedUnitType;
        }
        public bool SupportsArithmetic()
        {
            return true;
        }
        public double FetchConversionFactor()
        {
            switch (volumeUnitType)
            {
                case VolumeUnitType.Litre:
                    return 1.0;

                case VolumeUnitType.Millilitre:
                    return 0.001;

                case VolumeUnitType.Gallon:
                    return 3.78541;

                default:
                    throw new ArgumentException("Invalid Typed Volume Unit For Operations");
            }
        }
        public string FetchUnitName()
        {
            return volumeUnitType.ToString();
        }
        public string GetMeasurementType()
        {
            return "Volume";
        }
        public void ValidateOperationSupport(string operation)
        {
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
            VolumeUnitType convertedParsedUnit = (VolumeUnitType)Enum.Parse(typeof(VolumeUnitType), convertedUnitName, true);
            return new VolumeImplMeasurement(convertedParsedUnit);
        }
    }
}