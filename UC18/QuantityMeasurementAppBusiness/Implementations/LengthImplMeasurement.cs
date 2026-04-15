using QuantityMeasurementAppBusiness.Interfaces;
using QuantityMeasurementAppModels.Enums;

namespace QuantityMeasurementAppBusiness.Implementations
{
    public class LengthImplMeasurement : IMeasurable
    {
        private ConversionUnit LengthUnit;
        public LengthImplMeasurement(ConversionUnit LengthUnitType)
        {
            LengthUnit = LengthUnitType;
        }
        public double FetchConversionFactor()
        {
            switch (LengthUnit)
            {
                case ConversionUnit.Feet:
                    return 1.0;

                case ConversionUnit.Inch:
                    return 1.0 / 12.0;

                case ConversionUnit.Yard:
                    return 3.0;

                case ConversionUnit.Centimeter:
                    return 0.0328084;

                default:
                    throw new ArgumentException("Invalid Typed Length Unit For Conversion");
            }
        }
        public string FetchUnitName()
        {
            return LengthUnit.ToString();
        }
        public bool SupportsArithmetic()
        {
            return true;
        }
        public void ValidateOperationSupport(string operation)
        {
        }

        public string GetMeasurementType()
        {
            return "Length";
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
            ConversionUnit convertedUnit = (ConversionUnit)Enum.Parse(typeof(ConversionUnit), convertedUnitName, true);
            return new LengthImplMeasurement(convertedUnit);
        }
    }
}