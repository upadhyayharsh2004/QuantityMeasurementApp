using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuantityMeasurementAppBusiness.Interfaces;
using QuantityMeasurementAppModels.Enums;

namespace QuantityMeasurementAppBusiness.Implementations
{
    public class TemperatureImplMeasurement : IMeasurable
    {
        private ThermalTemperatureUnit TemperatureUnit;

        // Constructor
        public TemperatureImplMeasurement(ThermalTemperatureUnit ConvertedUnitType)
        {
            TemperatureUnit = ConvertedUnitType;
        }
        public double FetchConversionFactor()
        {
            return 1.0;
        }
        public string FetchUnitName()
        {
            return TemperatureUnit.ToString();
        }
        public bool SupportsArithmetic()
        {
            return false;
        }
        public void ValidateOperationSupport(string operation)
        {
            throw new NotSupportedException("Temperature not supported this unit " + operation + " operation");
        }
        public string GetMeasurementType()
        {
            return "Temperature";
        }
        public double NormalizeToBaseUnit(double value)
        {
            switch (TemperatureUnit)
            {
                case ThermalTemperatureUnit.Celsius:
                    return value;

                case ThermalTemperatureUnit.Fahrenheit:
                    return (value - 32) * 5 / 9;

                case ThermalTemperatureUnit.Kelvin:
                    return value - 273.15;

                default:
                    throw new ArgumentException("Invalid Typed For Conversion of Temperature Unit");
            }
        }
        public double NormalizeFromBaseUnit(double baseValue)
        {
            switch (TemperatureUnit)
            {
                case ThermalTemperatureUnit.Celsius:
                    return baseValue;

                case ThermalTemperatureUnit.Fahrenheit:
                    return (baseValue * 9 / 5) + 32;

                case ThermalTemperatureUnit.Kelvin:
                    return baseValue + 273.15;

                default:
                    throw new ArgumentException("Invalid Typed For Conversion of Temperature Unit");
            }
        }
        public IMeasurable GetUnitByName(string convertedUnitName)
        {
            ThermalTemperatureUnit convertedUnit = (ThermalTemperatureUnit)Enum.Parse(typeof(ThermalTemperatureUnit), convertedUnitName, true);
            return new TemperatureImplMeasurement(convertedUnit);
        }
    }
}