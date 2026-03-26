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
        private readonly IExtremelyAdvancedQuantityMeasurementRepositoryHandlingAllDataPersistenceOperations extremelyImportantRepositoryInstanceUsedForPersistingAllMeasurementOperations;

        public QuantityMeasurementServiceImpl(IExtremelyAdvancedQuantityMeasurementRepositoryHandlingAllDataPersistenceOperations incomingRepositoryDependencyObjectForServiceInitialization)
        {
            this.extremelyImportantRepositoryInstanceUsedForPersistingAllMeasurementOperations = incomingRepositoryDependencyObjectForServiceInitialization;
        }

        // ========================== RESOLVE UNIT ==========================
        private IMeasurable ResolveMeasurementUnitFromInputStrings(string incomingMeasurementTypeParameter, string incomingUnitNameParameter)
        {
            switch (incomingMeasurementTypeParameter.ToLower())
            {
                case "length":
                    LinearMeasurementUnitCategoryIdentifier resolvedLengthUnitEnumValue = (LinearMeasurementUnitCategoryIdentifier)Enum.Parse(typeof(LinearMeasurementUnitCategoryIdentifier), incomingUnitNameParameter, true);
                    return new LengthMeasurementImpl(resolvedLengthUnitEnumValue);

                case "weight":
                    MassMeasurementSystemIdentifier resolvedWeightUnitEnumValue = (MassMeasurementSystemIdentifier)Enum.Parse(typeof(MassMeasurementSystemIdentifier), incomingUnitNameParameter, true);
                    return new WeightMeasurementImpl(resolvedWeightUnitEnumValue);

                case "volume":
                    LiquidCapacityMeasurementScaleIdentifier resolvedVolumeUnitEnumValue = (LiquidCapacityMeasurementScaleIdentifier)Enum.Parse(typeof(LiquidCapacityMeasurementScaleIdentifier), incomingUnitNameParameter, true);
                    return new VolumeMeasurementImpl(resolvedVolumeUnitEnumValue);

                case "temperature":
                    ThermalScaleRepresentationIdentifier resolvedTemperatureUnitEnumValue = (ThermalScaleRepresentationIdentifier)Enum.Parse(typeof(ThermalScaleRepresentationIdentifier), incomingUnitNameParameter, true);
                    return new TemperatureMeasurementImpl(resolvedTemperatureUnitEnumValue);

                default:
                    throw new ArgumentException("❌ Invalid or unsupported measurement type provided by user: " + incomingMeasurementTypeParameter);
            }
        }

        // ========================== ADD ==========================
        public UniversalMeasurementDataCarrierObject PerformAdditionOperationBetweenTwoQuantityObjectsAndReturnResult(
            UniversalMeasurementDataCarrierObject firstQuantityInputObjectForAddition,
            UniversalMeasurementDataCarrierObject secondQuantityInputObjectForAddition,
            string targetUnitForAdditionOperation)
        {
            try
            {
                IMeasurable firstResolvedUnitObject = ResolveMeasurementUnitFromInputStrings(firstQuantityInputObjectForAddition.categoricalMeasurementClassificationType, firstQuantityInputObjectForAddition.descriptiveUnitIdentifierName);
                IMeasurable secondResolvedUnitObject = ResolveMeasurementUnitFromInputStrings(secondQuantityInputObjectForAddition.categoricalMeasurementClassificationType, secondQuantityInputObjectForAddition.descriptiveUnitIdentifierName);
                IMeasurable targetResolvedUnitObject = ResolveMeasurementUnitFromInputStrings(firstQuantityInputObjectForAddition.categoricalMeasurementClassificationType, targetUnitForAdditionOperation);

                Quantity<IMeasurable> firstQuantityBusinessObject = new Quantity<IMeasurable>(firstQuantityInputObjectForAddition.numericalMagnitudeOfMeasurementValue, firstResolvedUnitObject);
                Quantity<IMeasurable> secondQuantityBusinessObject = new Quantity<IMeasurable>(secondQuantityInputObjectForAddition.numericalMagnitudeOfMeasurementValue, secondResolvedUnitObject);

                Quantity<IMeasurable> additionResultQuantityObject =
                    firstQuantityBusinessObject.Add(secondQuantityBusinessObject, targetResolvedUnitObject);

                extremelyImportantRepositoryInstanceUsedForPersistingAllMeasurementOperations.SaveQuantityMeasurementEntityIntoUnderlyingDataStorageSystem(
                    new ComprehensiveMeasurementOperationDataRecord(
                        "Addition Operation",
                        firstQuantityInputObjectForAddition.numericalMagnitudeOfMeasurementValue, firstQuantityInputObjectForAddition.descriptiveUnitIdentifierName,
                        secondQuantityInputObjectForAddition.numericalMagnitudeOfMeasurementValue, secondQuantityInputObjectForAddition.descriptiveUnitIdentifierName,
                        additionResultQuantityObject.GetValue(),
                        firstQuantityInputObjectForAddition.categoricalMeasurementClassificationType));

                return new UniversalMeasurementDataCarrierObject(
                    additionResultQuantityObject.GetValue(),
                    targetUnitForAdditionOperation,
                    firstQuantityInputObjectForAddition.categoricalMeasurementClassificationType);
            }
            catch (Exception exceptionOccurredDuringAdditionOperationExecution)
            {
                extremelyImportantRepositoryInstanceUsedForPersistingAllMeasurementOperations.SaveQuantityMeasurementEntityIntoUnderlyingDataStorageSystem(
                    new ComprehensiveMeasurementOperationDataRecord("Addition Operation Failed", exceptionOccurredDuringAdditionOperationExecution.Message));

                throw new QuantityMeasurementException(
                    "❌ Addition operation could not be completed due to error: " + exceptionOccurredDuringAdditionOperationExecution.Message,
                    exceptionOccurredDuringAdditionOperationExecution);
            }
        }

        // ========================== SUBTRACT ==========================
        public UniversalMeasurementDataCarrierObject PerformSubtractionOperationBetweenTwoQuantityObjectsAndReturnResult(
            UniversalMeasurementDataCarrierObject firstQuantityInputObjectForSubtraction,
            UniversalMeasurementDataCarrierObject secondQuantityInputObjectForSubtraction,
            string targetUnitForSubtractionOperation)
        {
            try
            {
                IMeasurable firstResolvedUnitObject = ResolveMeasurementUnitFromInputStrings(firstQuantityInputObjectForSubtraction.categoricalMeasurementClassificationType, firstQuantityInputObjectForSubtraction.descriptiveUnitIdentifierName);
                IMeasurable secondResolvedUnitObject = ResolveMeasurementUnitFromInputStrings(secondQuantityInputObjectForSubtraction.categoricalMeasurementClassificationType, secondQuantityInputObjectForSubtraction.descriptiveUnitIdentifierName);
                IMeasurable targetResolvedUnitObject = ResolveMeasurementUnitFromInputStrings(firstQuantityInputObjectForSubtraction.categoricalMeasurementClassificationType, targetUnitForSubtractionOperation);

                Quantity<IMeasurable> firstQuantityBusinessObject = new Quantity<IMeasurable>(firstQuantityInputObjectForSubtraction.numericalMagnitudeOfMeasurementValue, firstResolvedUnitObject);
                Quantity<IMeasurable> secondQuantityBusinessObject = new Quantity<IMeasurable>(secondQuantityInputObjectForSubtraction.numericalMagnitudeOfMeasurementValue, secondResolvedUnitObject);

                Quantity<IMeasurable> subtractionResultQuantityObject =
                    firstQuantityBusinessObject.Subtract(secondQuantityBusinessObject, targetResolvedUnitObject);

                extremelyImportantRepositoryInstanceUsedForPersistingAllMeasurementOperations.SaveQuantityMeasurementEntityIntoUnderlyingDataStorageSystem(
                    new ComprehensiveMeasurementOperationDataRecord(
                        "Subtraction Operation",
                        firstQuantityInputObjectForSubtraction.numericalMagnitudeOfMeasurementValue, firstQuantityInputObjectForSubtraction.descriptiveUnitIdentifierName,
                        secondQuantityInputObjectForSubtraction.numericalMagnitudeOfMeasurementValue, secondQuantityInputObjectForSubtraction.descriptiveUnitIdentifierName,
                        subtractionResultQuantityObject.GetValue(),
                        firstQuantityInputObjectForSubtraction.categoricalMeasurementClassificationType));

                return new UniversalMeasurementDataCarrierObject(
                    subtractionResultQuantityObject.GetValue(),
                    targetUnitForSubtractionOperation,
                    firstQuantityInputObjectForSubtraction.categoricalMeasurementClassificationType);
            }
            catch (Exception exceptionOccurredDuringSubtractionOperationExecution)
            {
                extremelyImportantRepositoryInstanceUsedForPersistingAllMeasurementOperations.SaveQuantityMeasurementEntityIntoUnderlyingDataStorageSystem(
                    new ComprehensiveMeasurementOperationDataRecord("Subtraction Operation Failed", exceptionOccurredDuringSubtractionOperationExecution.Message));

                throw new QuantityMeasurementException(
                    "❌ Subtraction operation could not be completed due to error: " + exceptionOccurredDuringSubtractionOperationExecution.Message,
                    exceptionOccurredDuringSubtractionOperationExecution);
            }
        }

        // ========================== DIVIDE ==========================
        public double PerformDivisionOperationBetweenTwoQuantityObjectsAndReturnNumericResult(
    UniversalMeasurementDataCarrierObject firstQuantityInputObjectForDivision,
    UniversalMeasurementDataCarrierObject secondQuantityInputObjectForDivision)
        {
            try
            {
                IMeasurable firstResolvedUnitObject =
                    ResolveMeasurementUnitFromInputStrings(
                        firstQuantityInputObjectForDivision.categoricalMeasurementClassificationType,
                        firstQuantityInputObjectForDivision.descriptiveUnitIdentifierName);

                IMeasurable secondResolvedUnitObject =
                    ResolveMeasurementUnitFromInputStrings(
                        secondQuantityInputObjectForDivision.categoricalMeasurementClassificationType,
                        secondQuantityInputObjectForDivision.descriptiveUnitIdentifierName);

                Quantity<IMeasurable> firstQuantityBusinessObject =
                    new Quantity<IMeasurable>(
                        firstQuantityInputObjectForDivision.numericalMagnitudeOfMeasurementValue,
                        firstResolvedUnitObject);

                Quantity<IMeasurable> secondQuantityBusinessObject =
                    new Quantity<IMeasurable>(
                        secondQuantityInputObjectForDivision.numericalMagnitudeOfMeasurementValue,
                        secondResolvedUnitObject);

                double divisionResultNumericValue =
                    firstQuantityBusinessObject.Divide(secondQuantityBusinessObject);

                extremelyImportantRepositoryInstanceUsedForPersistingAllMeasurementOperations
                    .SaveQuantityMeasurementEntityIntoUnderlyingDataStorageSystem(
                        new ComprehensiveMeasurementOperationDataRecord(
                            "Division Operation",
                            firstQuantityInputObjectForDivision.numericalMagnitudeOfMeasurementValue,
                            firstQuantityInputObjectForDivision.descriptiveUnitIdentifierName,
                            secondQuantityInputObjectForDivision.numericalMagnitudeOfMeasurementValue,
                            secondQuantityInputObjectForDivision.descriptiveUnitIdentifierName,
                            divisionResultNumericValue,
                            firstQuantityInputObjectForDivision.categoricalMeasurementClassificationType));

                return divisionResultNumericValue;
            }
            catch (Exception exceptionOccurredDuringDivisionOperationExecution)
            {
                extremelyImportantRepositoryInstanceUsedForPersistingAllMeasurementOperations
                    .SaveQuantityMeasurementEntityIntoUnderlyingDataStorageSystem(
                        new ComprehensiveMeasurementOperationDataRecord(
                            "Division Operation Failed",
                            exceptionOccurredDuringDivisionOperationExecution.Message));

                throw new QuantityMeasurementException(
                    "❌ Division operation failed: "
                    + exceptionOccurredDuringDivisionOperationExecution.Message,
                    exceptionOccurredDuringDivisionOperationExecution);
            }
        }

        // ========================== COMPARE ==========================
        public bool PerformComparisonOperationBetweenTwoQuantityObjectsAndReturnBooleanResult(
            UniversalMeasurementDataCarrierObject firstQuantityInputObjectForComparison,
            UniversalMeasurementDataCarrierObject secondQuantityInputObjectForComparison)
        {
            try
            {
                IMeasurable firstResolvedUnitObject = ResolveMeasurementUnitFromInputStrings(firstQuantityInputObjectForComparison.categoricalMeasurementClassificationType, firstQuantityInputObjectForComparison.descriptiveUnitIdentifierName);
                IMeasurable secondResolvedUnitObject = ResolveMeasurementUnitFromInputStrings(secondQuantityInputObjectForComparison.categoricalMeasurementClassificationType, secondQuantityInputObjectForComparison.descriptiveUnitIdentifierName);

                Quantity<IMeasurable> firstQuantityBusinessObject = new Quantity<IMeasurable>(firstQuantityInputObjectForComparison.numericalMagnitudeOfMeasurementValue, firstResolvedUnitObject);
                Quantity<IMeasurable> secondQuantityBusinessObject = new Quantity<IMeasurable>(secondQuantityInputObjectForComparison.numericalMagnitudeOfMeasurementValue, secondResolvedUnitObject);

                bool comparisonResultBooleanValue =
                    firstQuantityBusinessObject.Equals(secondQuantityBusinessObject);

                extremelyImportantRepositoryInstanceUsedForPersistingAllMeasurementOperations.SaveQuantityMeasurementEntityIntoUnderlyingDataStorageSystem(
                    new ComprehensiveMeasurementOperationDataRecord(
                        "Comparison Operation",
                        firstQuantityInputObjectForComparison.numericalMagnitudeOfMeasurementValue, firstQuantityInputObjectForComparison.descriptiveUnitIdentifierName,
                        secondQuantityInputObjectForComparison.numericalMagnitudeOfMeasurementValue, secondQuantityInputObjectForComparison.descriptiveUnitIdentifierName,
                        comparisonResultBooleanValue ? 1 : 0,
                        firstQuantityInputObjectForComparison.categoricalMeasurementClassificationType));

                return comparisonResultBooleanValue;
            }
            catch (Exception exceptionOccurredDuringComparisonOperationExecution)
            {
                extremelyImportantRepositoryInstanceUsedForPersistingAllMeasurementOperations.SaveQuantityMeasurementEntityIntoUnderlyingDataStorageSystem(
                    new ComprehensiveMeasurementOperationDataRecord("Comparison Operation Failed", exceptionOccurredDuringComparisonOperationExecution.Message));

                throw new QuantityMeasurementException(
                    "❌ Comparison operation could not be completed due to error: " + exceptionOccurredDuringComparisonOperationExecution.Message,
                    exceptionOccurredDuringComparisonOperationExecution);
            }
        }

        // ========================== CONVERT ==========================
        public UniversalMeasurementDataCarrierObject PerformConversionOperationBetweenUnitsAndReturnConvertedQuantity(
            UniversalMeasurementDataCarrierObject inputQuantityObjectForConversion,
            string targetUnitForConversionOperation)
        {
            try
            {
                IMeasurable sourceResolvedUnitObject =
                    ResolveMeasurementUnitFromInputStrings(inputQuantityObjectForConversion.categoricalMeasurementClassificationType, inputQuantityObjectForConversion.descriptiveUnitIdentifierName);

                IMeasurable targetResolvedUnitObject =
                    ResolveMeasurementUnitFromInputStrings(inputQuantityObjectForConversion.categoricalMeasurementClassificationType, targetUnitForConversionOperation);

                Quantity<IMeasurable> sourceQuantityBusinessObject =
                    new Quantity<IMeasurable>(inputQuantityObjectForConversion.numericalMagnitudeOfMeasurementValue, sourceResolvedUnitObject);

                Quantity<IMeasurable> convertedResultQuantityObject =
                    sourceQuantityBusinessObject.ConvertTo(targetResolvedUnitObject);

                extremelyImportantRepositoryInstanceUsedForPersistingAllMeasurementOperations.SaveQuantityMeasurementEntityIntoUnderlyingDataStorageSystem(
                    new ComprehensiveMeasurementOperationDataRecord(
                        "Conversion Operation",
                        inputQuantityObjectForConversion.numericalMagnitudeOfMeasurementValue,
                        inputQuantityObjectForConversion.descriptiveUnitIdentifierName,
                        convertedResultQuantityObject.GetValue(),
                        inputQuantityObjectForConversion.categoricalMeasurementClassificationType));

                return new UniversalMeasurementDataCarrierObject(
                    convertedResultQuantityObject.GetValue(),
                    targetUnitForConversionOperation,
                    inputQuantityObjectForConversion.categoricalMeasurementClassificationType);
            }
            catch (Exception exceptionOccurredDuringConversionOperationExecution)
            {
                extremelyImportantRepositoryInstanceUsedForPersistingAllMeasurementOperations.SaveQuantityMeasurementEntityIntoUnderlyingDataStorageSystem(
                    new ComprehensiveMeasurementOperationDataRecord("Conversion Operation Failed", exceptionOccurredDuringConversionOperationExecution.Message));

                throw new QuantityMeasurementException(
                    "❌ Conversion operation could not be completed due to error: " + exceptionOccurredDuringConversionOperationExecution.Message,
                    exceptionOccurredDuringConversionOperationExecution);
            }
        }
    }
}