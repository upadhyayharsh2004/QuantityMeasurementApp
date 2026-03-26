using System;
using QuantityMeasurementAppServices.Interfaces;
using QuantityMeasurementAppModels.DTOs;
using QuantityMeasurementAppModels.Entities;
using QuantityMeasurementAppBusiness.Exceptions;
using QuantityMeasurementAppRepositories.Interfaces;
using QuantityMeasurementAppBusiness.Interfaces;
using QuantityMeasurementAppBusiness;
using QuantityMeasurementAppBusiness.Implementations;
using QuantityMeasurementAppModels.Enums;

namespace QuantityMeasurementApp.Services
{
    public class QuantityMeasurementServiceImpl : IQuantityMeasurementService
    {
        // Repository used to persist operation results
        private readonly IQuantityMeasurementRepository extremelyImportantRepositoryInstanceUsedForPersistingAllMeasurementOperations;

        public QuantityMeasurementServiceImpl(IQuantityMeasurementRepository incomingRepositoryDependencyObject)
        {
            this.extremelyImportantRepositoryInstanceUsedForPersistingAllMeasurementOperations = incomingRepositoryDependencyObject;
        }

        // Helper method to resolve unit dynamically
        private IMeasurable ResolveUnit(string measurementTypeInputParameter, string unitNameInputParameter)
        {
            switch (measurementTypeInputParameter.ToLower())
            {
                case "length":
                    LengthUnit resolvedLengthUnitEnumValue = (LengthUnit)Enum.Parse(
                        typeof(LengthUnit), unitNameInputParameter, true);
                    return new LengthMeasurementImpl(resolvedLengthUnitEnumValue);

                case "weight":
                    WeightUnit resolvedWeightUnitEnumValue = (WeightUnit)Enum.Parse(
                        typeof(WeightUnit), unitNameInputParameter, true);
                    return new WeightMeasurementImpl(resolvedWeightUnitEnumValue);

                case "volume":
                    VolumeUnit resolvedVolumeUnitEnumValue = (VolumeUnit)Enum.Parse(
                        typeof(VolumeUnit), unitNameInputParameter, true);
                    return new VolumeMeasurementImpl(resolvedVolumeUnitEnumValue);

                case "temperature":
                    TemperatureUnit resolvedTemperatureUnitEnumValue = (TemperatureUnit)Enum.Parse(
                        typeof(TemperatureUnit), unitNameInputParameter, true);
                    return new TemperatureMeasurementImpl(resolvedTemperatureUnitEnumValue);

                default:
                    throw new ArgumentException(
                        "❌ Unsupported measurement type provided: " + measurementTypeInputParameter);
            }
        }

        // ========================== ADD ==========================
        public QuantityDTO Add(QuantityDTO firstQuantityInputObject, QuantityDTO secondQuantityInputObject,
            string targetUnitForAdditionOperation)
        {
            try
            {
                IMeasurable firstResolvedUnitObject = ResolveUnit(firstQuantityInputObject.MeasurementType, firstQuantityInputObject.UnitName);
                IMeasurable secondResolvedUnitObject = ResolveUnit(secondQuantityInputObject.MeasurementType, secondQuantityInputObject.UnitName);
                IMeasurable targetResolvedUnitObject = ResolveUnit(firstQuantityInputObject.MeasurementType, targetUnitForAdditionOperation);

                Quantity<IMeasurable> firstQuantityBusinessObject = new Quantity<IMeasurable>(firstQuantityInputObject.Value, firstResolvedUnitObject);
                Quantity<IMeasurable> secondQuantityBusinessObject = new Quantity<IMeasurable>(secondQuantityInputObject.Value, secondResolvedUnitObject);

                Quantity<IMeasurable> additionResultQuantityObject =
                    firstQuantityBusinessObject.Add(secondQuantityBusinessObject, targetResolvedUnitObject);

                extremelyImportantRepositoryInstanceUsedForPersistingAllMeasurementOperations.Save(
                    new QuantityMeasurementEntity(
                        "Add",
                        firstQuantityInputObject.Value, firstQuantityInputObject.UnitName,
                        secondQuantityInputObject.Value, secondQuantityInputObject.UnitName,
                        additionResultQuantityObject.GetValue(),
                        firstQuantityInputObject.MeasurementType));

                return new QuantityDTO(
                    additionResultQuantityObject.GetValue(),
                    targetUnitForAdditionOperation,
                    firstQuantityInputObject.MeasurementType);
            }
            catch (Exception exceptionOccurredDuringAdditionOperation)
            {
                extremelyImportantRepositoryInstanceUsedForPersistingAllMeasurementOperations.Save(
                    new QuantityMeasurementEntity("Add", exceptionOccurredDuringAdditionOperation.Message));

                throw new QuantityMeasurementException(
                    "❌ Addition operation failed due to error: " + exceptionOccurredDuringAdditionOperation.Message,
                    exceptionOccurredDuringAdditionOperation);
            }
        }

        // ========================== SUBTRACT ==========================
        public QuantityDTO Subtract(QuantityDTO firstQuantityInputObject, QuantityDTO secondQuantityInputObject,
            string targetUnitForSubtractionOperation)
        {
            try
            {
                IMeasurable firstResolvedUnitObject = ResolveUnit(firstQuantityInputObject.MeasurementType, firstQuantityInputObject.UnitName);
                IMeasurable secondResolvedUnitObject = ResolveUnit(secondQuantityInputObject.MeasurementType, secondQuantityInputObject.UnitName);
                IMeasurable targetResolvedUnitObject = ResolveUnit(firstQuantityInputObject.MeasurementType, targetUnitForSubtractionOperation);

                Quantity<IMeasurable> firstQuantityBusinessObject = new Quantity<IMeasurable>(firstQuantityInputObject.Value, firstResolvedUnitObject);
                Quantity<IMeasurable> secondQuantityBusinessObject = new Quantity<IMeasurable>(secondQuantityInputObject.Value, secondResolvedUnitObject);

                Quantity<IMeasurable> subtractionResultQuantityObject =
                    firstQuantityBusinessObject.Subtract(secondQuantityBusinessObject, targetResolvedUnitObject);

                extremelyImportantRepositoryInstanceUsedForPersistingAllMeasurementOperations.Save(
                    new QuantityMeasurementEntity(
                        "Subtract",
                        firstQuantityInputObject.Value, firstQuantityInputObject.UnitName,
                        secondQuantityInputObject.Value, secondQuantityInputObject.UnitName,
                        subtractionResultQuantityObject.GetValue(),
                        firstQuantityInputObject.MeasurementType));

                return new QuantityDTO(
                    subtractionResultQuantityObject.GetValue(),
                    targetUnitForSubtractionOperation,
                    firstQuantityInputObject.MeasurementType);
            }
            catch (Exception exceptionOccurredDuringSubtractionOperation)
            {
                extremelyImportantRepositoryInstanceUsedForPersistingAllMeasurementOperations.Save(
                    new QuantityMeasurementEntity("Subtract", exceptionOccurredDuringSubtractionOperation.Message));

                throw new QuantityMeasurementException(
                    "❌ Subtraction operation failed due to error: " + exceptionOccurredDuringSubtractionOperation.Message,
                    exceptionOccurredDuringSubtractionOperation);
            }
        }

        // ========================== DIVIDE ==========================
        public double Divide(QuantityDTO firstQuantityInputObject, QuantityDTO secondQuantityInputObject)
        {
            try
            {
                IMeasurable firstResolvedUnitObject = ResolveUnit(firstQuantityInputObject.MeasurementType, firstQuantityInputObject.UnitName);
                IMeasurable secondResolvedUnitObject = ResolveUnit(secondQuantityInputObject.MeasurementType, secondQuantityInputObject.UnitName);

                Quantity<IMeasurable> firstQuantityBusinessObject = new Quantity<IMeasurable>(firstQuantityInputObject.Value, firstResolvedUnitObject);
                Quantity<IMeasurable> secondQuantityBusinessObject = new Quantity<IMeasurable>(secondQuantityInputObject.Value, secondResolvedUnitObject);

                double divisionResultNumericValue =
                    firstQuantityBusinessObject.Divide(secondQuantityBusinessObject);

                extremelyImportantRepositoryInstanceUsedForPersistingAllMeasurementOperations.Save(
                    new QuantityMeasurementEntity(
                        "Divide",
                        firstQuantityInputObject.Value, firstQuantityInputObject.UnitName,
                        secondQuantityInputObject.Value, secondQuantityInputObject.UnitName,
                        divisionResultNumericValue,
                        firstQuantityInputObject.MeasurementType));

                return divisionResultNumericValue;
            }
            catch (Exception exceptionOccurredDuringDivisionOperation)
            {
                extremelyImportantRepositoryInstanceUsedForPersistingAllMeasurementOperations.Save(
                    new QuantityMeasurementEntity("Divide", exceptionOccurredDuringDivisionOperation.Message));

                throw new QuantityMeasurementException(
                    "❌ Division operation failed due to error: " + exceptionOccurredDuringDivisionOperation.Message,
                    exceptionOccurredDuringDivisionOperation);
            }
        }

        // ========================== COMPARE ==========================
        public bool Compare(QuantityDTO firstQuantityInputObject, QuantityDTO secondQuantityInputObject)
        {
            try
            {
                IMeasurable firstResolvedUnitObject = ResolveUnit(firstQuantityInputObject.MeasurementType, firstQuantityInputObject.UnitName);
                IMeasurable secondResolvedUnitObject = ResolveUnit(secondQuantityInputObject.MeasurementType, secondQuantityInputObject.UnitName);

                Quantity<IMeasurable> firstQuantityBusinessObject = new Quantity<IMeasurable>(firstQuantityInputObject.Value, firstResolvedUnitObject);
                Quantity<IMeasurable> secondQuantityBusinessObject = new Quantity<IMeasurable>(secondQuantityInputObject.Value, secondResolvedUnitObject);

                bool comparisonResultBooleanValue =
                    firstQuantityBusinessObject.Equals(secondQuantityBusinessObject);

                extremelyImportantRepositoryInstanceUsedForPersistingAllMeasurementOperations.Save(
                    new QuantityMeasurementEntity(
                        "Compare",
                        firstQuantityInputObject.Value, firstQuantityInputObject.UnitName,
                        secondQuantityInputObject.Value, secondQuantityInputObject.UnitName,
                        comparisonResultBooleanValue ? 1 : 0,
                        firstQuantityInputObject.MeasurementType));

                return comparisonResultBooleanValue;
            }
            catch (Exception exceptionOccurredDuringComparisonOperation)
            {
                extremelyImportantRepositoryInstanceUsedForPersistingAllMeasurementOperations.Save(
                    new QuantityMeasurementEntity("Compare", exceptionOccurredDuringComparisonOperation.Message));

                throw new QuantityMeasurementException(
                    "❌ Comparison operation failed due to error: " + exceptionOccurredDuringComparisonOperation.Message,
                    exceptionOccurredDuringComparisonOperation);
            }
        }

        // ========================== CONVERT ==========================
        public QuantityDTO Convert(QuantityDTO inputQuantityObjectForConversion, string targetUnitForConversionOperation)
        {
            try
            {
                IMeasurable sourceResolvedUnitObject =
                    ResolveUnit(inputQuantityObjectForConversion.MeasurementType, inputQuantityObjectForConversion.UnitName);

                IMeasurable targetResolvedUnitObject =
                    ResolveUnit(inputQuantityObjectForConversion.MeasurementType, targetUnitForConversionOperation);

                Quantity<IMeasurable> sourceQuantityBusinessObject =
                    new Quantity<IMeasurable>(inputQuantityObjectForConversion.Value, sourceResolvedUnitObject);

                Quantity<IMeasurable> convertedResultQuantityObject =
                    sourceQuantityBusinessObject.ConvertTo(targetResolvedUnitObject);

                extremelyImportantRepositoryInstanceUsedForPersistingAllMeasurementOperations.Save(
                    new QuantityMeasurementEntity(
                        "Convert",
                        inputQuantityObjectForConversion.Value,
                        inputQuantityObjectForConversion.UnitName,
                        convertedResultQuantityObject.GetValue(),
                        inputQuantityObjectForConversion.MeasurementType));

                return new QuantityDTO(
                    convertedResultQuantityObject.GetValue(),
                    targetUnitForConversionOperation,
                    inputQuantityObjectForConversion.MeasurementType);
            }
            catch (Exception exceptionOccurredDuringConversionOperation)
            {
                extremelyImportantRepositoryInstanceUsedForPersistingAllMeasurementOperations.Save(
                    new QuantityMeasurementEntity("Convert", exceptionOccurredDuringConversionOperation.Message));

                throw new QuantityMeasurementException(
                    "❌ Conversion operation failed due to error: " + exceptionOccurredDuringConversionOperation.Message,
                    exceptionOccurredDuringConversionOperation);
            }
        }
    }
}