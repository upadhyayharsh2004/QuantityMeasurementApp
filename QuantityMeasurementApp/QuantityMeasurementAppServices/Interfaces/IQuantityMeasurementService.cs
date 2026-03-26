using System;
using QuantityMeasurementAppModels.DTOs;

namespace QuantityMeasurementAppServices.Interfaces
{
    public interface IQuantityMeasurementService
    {
        UniversalMeasurementDataCarrierObject PerformAdditionOperationBetweenTwoQuantityObjectsAndReturnResult(
            UniversalMeasurementDataCarrierObject firstQuantityInputObjectForAddition,
            UniversalMeasurementDataCarrierObject secondQuantityInputObjectForAddition,
            string targetUnitForAdditionOperation);

        UniversalMeasurementDataCarrierObject PerformSubtractionOperationBetweenTwoQuantityObjectsAndReturnResult(
            UniversalMeasurementDataCarrierObject firstQuantityInputObjectForSubtraction,
            UniversalMeasurementDataCarrierObject secondQuantityInputObjectForSubtraction,
            string targetUnitForSubtractionOperation);

        double PerformDivisionOperationBetweenTwoQuantityObjectsAndReturnNumericResult(
            UniversalMeasurementDataCarrierObject firstQuantityInputObjectForDivision,
            UniversalMeasurementDataCarrierObject secondQuantityInputObjectForDivision);

        bool PerformComparisonOperationBetweenTwoQuantityObjectsAndReturnBooleanResult(
            UniversalMeasurementDataCarrierObject firstQuantityInputObjectForComparison,
            UniversalMeasurementDataCarrierObject secondQuantityInputObjectForComparison);

        UniversalMeasurementDataCarrierObject PerformConversionOperationBetweenUnitsAndReturnConvertedQuantity(
            UniversalMeasurementDataCarrierObject inputQuantityObjectForConversion,
            string targetUnitForConversionOperation);
    }
}