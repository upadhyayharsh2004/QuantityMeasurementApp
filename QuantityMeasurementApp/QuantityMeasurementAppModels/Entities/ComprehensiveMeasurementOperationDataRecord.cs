using System;

namespace QuantityMeasurementAppModels.Entities
{
    [Serializable]
    public class ComprehensiveMeasurementOperationDataRecord
    {
        // Operation descriptor (Add, Convert, Compare, etc.)
        public string descriptiveOperationIdentifierName { get; set; }

        // First input numeric value
        public double primaryInputNumericValue { get; set; }

        // First input unit
        public string primaryInputUnitDescriptor { get; set; }

        // Second input numeric value (if applicable)
        public double secondaryInputNumericValue { get; set; }

        // Second input unit (if applicable)
        public string secondaryInputUnitDescriptor { get; set; }

        // Final computed result value
        public double computedOutputResultValue { get; set; }

        // Category of measurement (Length, Volume, etc.)
        public string measurementCategoryDescriptor { get; set; }

        // Flag indicating error condition
        public bool isOperationMarkedAsError { get; set; }

        // Detailed error message if failure occurs
        public string detailedErrorDescriptionMessage { get; set; }

        // ========================== DEFAULT CONSTRUCTOR ==========================
        public ComprehensiveMeasurementOperationDataRecord()
        {
        }

        // ========================== TWO INPUT CONSTRUCTOR ==========================
        public ComprehensiveMeasurementOperationDataRecord(
            string operationName,
            double firstValue, string firstUnit,
            double secondValue, string secondUnit,
            double resultValue,
            string measurementCategory)
        {
            descriptiveOperationIdentifierName = operationName;
            primaryInputNumericValue = firstValue;
            primaryInputUnitDescriptor = firstUnit;
            secondaryInputNumericValue = secondValue;
            secondaryInputUnitDescriptor = secondUnit;
            computedOutputResultValue = resultValue;
            measurementCategoryDescriptor = measurementCategory;
            isOperationMarkedAsError = false;
        }

        // ========================== SINGLE INPUT CONSTRUCTOR ==========================
        public ComprehensiveMeasurementOperationDataRecord(
            string operationName,
            double inputValue, string inputUnit,
            double resultValue,
            string measurementCategory)
        {
            descriptiveOperationIdentifierName = operationName;
            primaryInputNumericValue = inputValue;
            primaryInputUnitDescriptor = inputUnit;
            computedOutputResultValue = resultValue;
            measurementCategoryDescriptor = measurementCategory;
            isOperationMarkedAsError = false;
        }

        // ========================== ERROR CONSTRUCTOR ==========================
        public ComprehensiveMeasurementOperationDataRecord(
            string operationName,
            string errorDescription)
        {
            descriptiveOperationIdentifierName = operationName;
            isOperationMarkedAsError = true;
            detailedErrorDescriptionMessage = errorDescription;
        }

        // ========================== STRING REPRESENTATION ==========================
        public override string ToString()
        {
            if (isOperationMarkedAsError)
            {
                return $"❌ Operation [{descriptiveOperationIdentifierName}] failed due to: {detailedErrorDescriptionMessage}";
            }

            return $"✅ Operation [{descriptiveOperationIdentifierName}] → Input: {primaryInputNumericValue} {primaryInputUnitDescriptor} | Result: {computedOutputResultValue}";
        }
    }
}