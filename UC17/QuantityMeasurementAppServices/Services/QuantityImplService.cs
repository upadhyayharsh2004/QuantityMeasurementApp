using System;
using QuantityMeasurementAppServices.Interfaces;
using QuantityMeasurementAppModels.DTOs;
using QuantityMeasurementAppModels.Entities;
using QuantityMeasurementAppModels.Enums;
using QuantityMeasurementAppBusiness.Exceptions;
using QuantityMeasurementAppRepositories.Interfaces;
using QuantityMeasurementAppBusiness.Interfaces;
using QuantityMeasurementAppBusiness;
using QuantityMeasurementAppBusiness.Implementations;

namespace QuantityMeasurementApp.Services
{
    public class QuantityImplService : IQuantityServiceImplsConvert
    {
        private readonly IQuantityLogRepository serviceRepository;
        public QuantityImplService(IQuantityLogRepository logRepository)
        {
            this.serviceRepository = logRepository;
        }
        private IMeasurable ResolveConvertedUnit(string resolveMeasurementType, string resolveUnitName)
        {
            if (resolveMeasurementType.ToLower() == "length")
            {
                ConversionUnit convertedUnit =
                    (ConversionUnit)Enum.Parse(typeof(ConversionUnit), resolveUnitName, true);

                return new LengthImplMeasurement(convertedUnit);
            }
            else if (resolveMeasurementType.ToLower() == "weight")
            {
                MassUnit weightedUnit =
                    (MassUnit)Enum.Parse(typeof(MassUnit), resolveUnitName, true);

                return new WeightImplMeasurement(weightedUnit);
            }
            else if (resolveMeasurementType.ToLower() == "volume")
            {
                VolumeUnitType measuredUnit =
                    (VolumeUnitType)Enum.Parse(typeof(VolumeUnitType), resolveUnitName, true);

                return new VolumeImplMeasurement(measuredUnit);
            }
            else if (resolveMeasurementType.ToLower() == "temperature")
            {
                ThermalTemperatureUnit thermalUnit =
                    (ThermalTemperatureUnit)Enum.Parse(typeof(ThermalTemperatureUnit), resolveUnitName, true);

                return new TemperatureImplMeasurement(thermalUnit);
            }
            else
            {
                throw new ArgumentException("Not Knowing About The Measurement Type: " + resolveMeasurementType);
            }
        }
        public QuantityDTOs Combine(QuantityDTOs quantityFirst, QuantityDTOs quantitySecond, string quantityTargetUnit)
        {
            try
            {
                IMeasurable measurementUnit1 = ResolveConvertedUnit(quantityFirst.MeasurementTypeDTOs, quantityFirst.UnitNameDTOs);

                IMeasurable measurementUnit2 = ResolveConvertedUnit(quantitySecond.MeasurementTypeDTOs, quantitySecond.UnitNameDTOs);

                IMeasurable measurementTarget = ResolveConvertedUnit(quantityFirst.MeasurementTypeDTOs, quantityTargetUnit);

                Quantity<IMeasurable> quantity1 = new Quantity<IMeasurable>(quantityFirst.ValueDTOs, measurementUnit1);

                Quantity<IMeasurable> quantity2 = new Quantity<IMeasurable>(quantitySecond.ValueDTOs, measurementUnit2);

                Quantity<IMeasurable> measurementResult = quantity1.Add(quantity2, measurementTarget);

                serviceRepository.SaveRecords(new QuantityEntity(DoubleSingleOperationType.Add.ToString(), quantityFirst.ValueDTOs, quantityFirst.UnitNameDTOs, quantitySecond.ValueDTOs, quantitySecond.UnitNameDTOs, measurementResult.GetValue(), quantityFirst.MeasurementTypeDTOs));

                return new QuantityDTOs(measurementResult.GetValue(), quantityTargetUnit, quantityFirst.MeasurementTypeDTOs);
            }
            catch (Exception exception)
            {
                serviceRepository.SaveRecords(new QuantityEntity(DoubleSingleOperationType.Add.ToString(), exception.Message));

                throw new QuantityException("Quantity Measurement App Operation Of Add failed: " + exception.Message, exception);
            }
        }
        public QuantityDTOs Difference(QuantityDTOs quantityFirst, QuantityDTOs quantitySecond, string quantityTargetUnit)
        {
            try
            {
                IMeasurable measurementUnit1 = ResolveConvertedUnit(quantityFirst.MeasurementTypeDTOs, quantityFirst.UnitNameDTOs);

                IMeasurable measurementUnit2 = ResolveConvertedUnit(quantitySecond.MeasurementTypeDTOs, quantitySecond.UnitNameDTOs);

                IMeasurable quantityTarget = ResolveConvertedUnit(quantityFirst.MeasurementTypeDTOs, quantityTargetUnit);

                Quantity<IMeasurable> quantity1 = new Quantity<IMeasurable>(quantityFirst.ValueDTOs, measurementUnit1);

                Quantity<IMeasurable> quantity2 = new Quantity<IMeasurable>(quantitySecond.ValueDTOs, measurementUnit2);

                Quantity<IMeasurable> measurementResult = quantity1.Subtract(quantity2, quantityTarget);

                serviceRepository.SaveRecords(new QuantityEntity(DoubleSingleOperationType.Subtract.ToString(), quantityFirst.ValueDTOs, quantityFirst.UnitNameDTOs, quantitySecond.ValueDTOs, quantitySecond.UnitNameDTOs, measurementResult.GetValue(), quantityFirst.MeasurementTypeDTOs));

                return new QuantityDTOs(measurementResult.GetValue(), quantityTargetUnit, quantityFirst.MeasurementTypeDTOs);
            }
            catch (Exception exception)
            {
                serviceRepository.SaveRecords(new QuantityEntity(DoubleSingleOperationType.Subtract.ToString(), exception.Message));

                throw new QuantityException("Quantity Measurement App Operation Of Subtraction failed: " + exception.Message, exception);
            }
        }
        public double Divison(QuantityDTOs quantityFirst, QuantityDTOs quantitySecond)
        {
            try
            {
                IMeasurable measurementUnit1 = ResolveConvertedUnit(quantityFirst.MeasurementTypeDTOs, quantityFirst.UnitNameDTOs);

                IMeasurable measurementUnit2 = ResolveConvertedUnit(quantitySecond.MeasurementTypeDTOs, quantitySecond.UnitNameDTOs);

                Quantity<IMeasurable> quantity1 = new Quantity<IMeasurable>(quantityFirst.ValueDTOs, measurementUnit1);

                Quantity<IMeasurable> quantity2 = new Quantity<IMeasurable>(quantitySecond.ValueDTOs, measurementUnit2);

                double measurementResult = quantity1.Divide(quantity2);

                serviceRepository.SaveRecords(new QuantityEntity(DoubleSingleOperationType.Divide.ToString(), quantityFirst.ValueDTOs, quantityFirst.UnitNameDTOs, quantitySecond.ValueDTOs, quantitySecond.UnitNameDTOs, measurementResult, quantityFirst.MeasurementTypeDTOs));

                return measurementResult;
            }
            catch (Exception exception)
            {
                serviceRepository.SaveRecords(new QuantityEntity(DoubleSingleOperationType.Divide.ToString(), exception.Message));

                throw new QuantityException("Quantity Measurement App Operation Of Divison failed: " + exception.Message, exception);
            }
        }
        public bool Comparison(QuantityDTOs quantityFirst, QuantityDTOs quantitySecond)
        {
            try
            {
                IMeasurable measurementUnit1 = ResolveConvertedUnit(quantityFirst.MeasurementTypeDTOs, quantityFirst.UnitNameDTOs);

                IMeasurable measurementUnit2 = ResolveConvertedUnit(quantitySecond.MeasurementTypeDTOs, quantitySecond.UnitNameDTOs);

                Quantity<IMeasurable> quantity1 = new Quantity<IMeasurable>(quantityFirst.ValueDTOs, measurementUnit1);

                Quantity<IMeasurable> quantity2 = new Quantity<IMeasurable>(quantitySecond.ValueDTOs, measurementUnit2);

                bool measurementResult = quantity1.Equals(quantity2);

                serviceRepository.SaveRecords(new QuantityEntity(DoubleSingleOperationType.Compare.ToString(), quantityFirst.ValueDTOs, quantityFirst.UnitNameDTOs, quantitySecond.ValueDTOs, quantitySecond.UnitNameDTOs, measurementResult ? 1 : 0, quantityFirst.MeasurementTypeDTOs));

                return measurementResult;
            }
            catch (Exception exception)
            {
                serviceRepository.SaveRecords(new QuantityEntity(DoubleSingleOperationType.Compare.ToString(), exception.Message));

                throw new QuantityException("Quantity Measurement App Operation Of Comparison failed: " + exception.Message, exception);
            }
        }
        public QuantityDTOs Conversion(QuantityDTOs quantityFirst, string quantityTargetUnit)
        {
            try
            {
                IMeasurable measurementUnit2 = ResolveConvertedUnit(quantityFirst.MeasurementTypeDTOs, quantityFirst.UnitNameDTOs);

                IMeasurable measurementTarget = ResolveConvertedUnit(quantityFirst.MeasurementTypeDTOs, quantityTargetUnit);

                Quantity<IMeasurable> quantity1 = new Quantity<IMeasurable>(quantityFirst.ValueDTOs, measurementUnit2);

                Quantity<IMeasurable> measurementResult = quantity1.ChangeTo(measurementTarget);

                serviceRepository.SaveRecords(new QuantityEntity(DoubleSingleOperationType.Convert.ToString(), quantityFirst.ValueDTOs, quantityFirst.UnitNameDTOs, measurementResult.GetValue(), quantityFirst.MeasurementTypeDTOs));

                return new QuantityDTOs(measurementResult.GetValue(), quantityTargetUnit, quantityFirst.MeasurementTypeDTOs);
            }
            catch (Exception exception)
            {
                serviceRepository.SaveRecords(new QuantityEntity(DoubleSingleOperationType.Convert.ToString(), exception.Message));

                throw new QuantityException("Quantity Measurement App Operation Of Conversion failed: " + exception.Message, exception);
            }
        }
    }
}