using QuantityMeasurementAppModels.DTOs;
using QuantityMeasurementAppServices.Interfaces;

namespace QuantityMeasurementApp.Controllers
{
    public class QuantityMeasurementController
    {
        private IQuantityMeasurementService extremelyImportantQuantityMeasurementServiceInstanceUsedForAllMeasurementOperations;

        public QuantityMeasurementController(IQuantityMeasurementService incomingQuantityMeasurementServiceDependencyObject)
        {
            if (incomingQuantityMeasurementServiceDependencyObject == null)
            {
                throw new ArgumentException("Service cannot be null");
            }

            this.extremelyImportantQuantityMeasurementServiceInstanceUsedForAllMeasurementOperations = incomingQuantityMeasurementServiceDependencyObject;
        }

        // ========================== LENGTH METHODS ==========================

        public void PerformLengthComparisonBetweenTwoGivenLengthQuantityObjectsAndDisplayWhetherTheyAreEqualOrNot(UniversalMeasurementDataCarrierObject firstLengthQuantityObjectParameterForComparisonOperation, UniversalMeasurementDataCarrierObject secondLengthQuantityObjectParameterForComparisonOperation)
        {
            try
            {
                bool comparisonResultForLengthQuantities = extremelyImportantQuantityMeasurementServiceInstanceUsedForAllMeasurementOperations.PerformComparisonOperationBetweenTwoQuantityObjectsAndReturnBooleanResult(firstLengthQuantityObjectParameterForComparisonOperation, secondLengthQuantityObjectParameterForComparisonOperation);

                if (comparisonResultForLengthQuantities)
                    Console.WriteLine("✅ The given length values are equal.");
                else
                    Console.WriteLine("❌ The given length values are not equal.");
            }
            catch (Exception exceptionOccurredDuringLengthComparisonOperationExecution)
            {
                Console.WriteLine("❌ Error occurred during length comparison: " + exceptionOccurredDuringLengthComparisonOperationExecution.Message);
            }
        }

        public void PerformLengthConversionFromOneUnitToAnotherUnitAndDisplayTheConvertedResult(UniversalMeasurementDataCarrierObject inputLengthQuantityObjectToBeConvertedIntoTargetUnit, string targetUnitForLengthConversionOperation)
        {
            try
            {
                UniversalMeasurementDataCarrierObject convertedLengthQuantityResultAfterSuccessfulConversion = extremelyImportantQuantityMeasurementServiceInstanceUsedForAllMeasurementOperations.PerformConversionOperationBetweenUnitsAndReturnConvertedQuantity(inputLengthQuantityObjectToBeConvertedIntoTargetUnit, targetUnitForLengthConversionOperation);
                Console.WriteLine($"✅ Conversion Result: {inputLengthQuantityObjectToBeConvertedIntoTargetUnit} is equal to {convertedLengthQuantityResultAfterSuccessfulConversion}");
            }
            catch (Exception exceptionOccurredDuringLengthConversionOperationExecution)
            {
                Console.WriteLine("❌ Error occurred during length conversion: " + exceptionOccurredDuringLengthConversionOperationExecution.Message);
            }
        }

        public void PerformAdditionOfTwoLengthQuantitiesAndDisplayResultInSpecifiedTargetUnit(UniversalMeasurementDataCarrierObject firstLengthQuantityObjectForAdditionOperation, UniversalMeasurementDataCarrierObject secondLengthQuantityObjectForAdditionOperation, string targetUnitForLengthAdditionOperationResult)
        {
            try
            {
                UniversalMeasurementDataCarrierObject additionResultForLengthQuantities = extremelyImportantQuantityMeasurementServiceInstanceUsedForAllMeasurementOperations.PerformAdditionOperationBetweenTwoQuantityObjectsAndReturnResult(firstLengthQuantityObjectForAdditionOperation, secondLengthQuantityObjectForAdditionOperation, targetUnitForLengthAdditionOperationResult);
                Console.WriteLine($"✅ Addition Result: {firstLengthQuantityObjectForAdditionOperation} + {secondLengthQuantityObjectForAdditionOperation} = {additionResultForLengthQuantities}");
            }
            catch (Exception exceptionOccurredDuringLengthAdditionOperationExecution)
            {
                Console.WriteLine("❌ Error occurred during length addition: " + exceptionOccurredDuringLengthAdditionOperationExecution.Message);
            }
        }

        public void PerformSubtractionOfTwoLengthQuantitiesAndDisplayResultInSpecifiedTargetUnit(UniversalMeasurementDataCarrierObject firstLengthQuantityObjectForSubtractionOperation, UniversalMeasurementDataCarrierObject secondLengthQuantityObjectForSubtractionOperation, string targetUnitForLengthSubtractionOperationResult)
        {
            try
            {
                UniversalMeasurementDataCarrierObject subtractionResultForLengthQuantities = extremelyImportantQuantityMeasurementServiceInstanceUsedForAllMeasurementOperations.PerformSubtractionOperationBetweenTwoQuantityObjectsAndReturnResult(firstLengthQuantityObjectForSubtractionOperation, secondLengthQuantityObjectForSubtractionOperation, targetUnitForLengthSubtractionOperationResult);
                Console.WriteLine($"✅ Subtraction Result: {firstLengthQuantityObjectForSubtractionOperation} - {secondLengthQuantityObjectForSubtractionOperation} = {subtractionResultForLengthQuantities}");
            }
            catch (Exception exceptionOccurredDuringLengthSubtractionOperationExecution)
            {
                Console.WriteLine("❌ Error occurred during length subtraction: " + exceptionOccurredDuringLengthSubtractionOperationExecution.Message);
            }
        }

        public void PerformDivisionOfTwoLengthQuantitiesAndDisplayNumericalResult(UniversalMeasurementDataCarrierObject firstLengthQuantityObjectForDivisionOperation, UniversalMeasurementDataCarrierObject secondLengthQuantityObjectForDivisionOperation)
        {
            try
            {
                double divisionResultForLengthQuantities = extremelyImportantQuantityMeasurementServiceInstanceUsedForAllMeasurementOperations.PerformDivisionOperationBetweenTwoQuantityObjectsAndReturnNumericResult(firstLengthQuantityObjectForDivisionOperation, secondLengthQuantityObjectForDivisionOperation);
                Console.WriteLine($"✅ Division Result: {firstLengthQuantityObjectForDivisionOperation} / {secondLengthQuantityObjectForDivisionOperation} = {divisionResultForLengthQuantities}");
            }
            catch (Exception exceptionOccurredDuringLengthDivisionOperationExecution)
            {
                Console.WriteLine("❌ Error occurred during length division: " + exceptionOccurredDuringLengthDivisionOperationExecution.Message);
            }
        }

        // ========================== WEIGHT METHODS ==========================

        public void PerformWeightComparisonBetweenTwoGivenWeightQuantitiesAndDisplayResult(UniversalMeasurementDataCarrierObject firstWeightQuantityObjectParameter, UniversalMeasurementDataCarrierObject secondWeightQuantityObjectParameter)
        {
            try
            {
                bool comparisonResultForWeightQuantities = extremelyImportantQuantityMeasurementServiceInstanceUsedForAllMeasurementOperations.PerformComparisonOperationBetweenTwoQuantityObjectsAndReturnBooleanResult(firstWeightQuantityObjectParameter, secondWeightQuantityObjectParameter);

                if (comparisonResultForWeightQuantities)
                    Console.WriteLine("✅ The given weight values are equal.");
                else
                    Console.WriteLine("❌ The given weight values are not equal.");
            }
            catch (Exception exceptionOccurredDuringWeightComparisonOperationExecution)
            {
                Console.WriteLine("❌ Error occurred during weight comparison: " + exceptionOccurredDuringWeightComparisonOperationExecution.Message);
            }
        }

        public void PerformWeightConversionFromOneUnitToAnotherUnitAndDisplayResult(UniversalMeasurementDataCarrierObject inputWeightQuantityObjectToBeConverted, string targetUnitForWeightConversionOperation)
        {
            try
            {
                UniversalMeasurementDataCarrierObject convertedWeightQuantityResult = extremelyImportantQuantityMeasurementServiceInstanceUsedForAllMeasurementOperations.PerformConversionOperationBetweenUnitsAndReturnConvertedQuantity(inputWeightQuantityObjectToBeConverted, targetUnitForWeightConversionOperation);
                Console.WriteLine($"✅ Conversion Result: {inputWeightQuantityObjectToBeConverted} = {convertedWeightQuantityResult}");
            }
            catch (Exception exceptionOccurredDuringWeightConversionOperationExecution)
            {
                Console.WriteLine("❌ Error occurred during weight conversion: " + exceptionOccurredDuringWeightConversionOperationExecution.Message);
            }
        }

        public void PerformAdditionOfTwoWeightQuantitiesAndDisplayResult(UniversalMeasurementDataCarrierObject firstWeightQuantityObject, UniversalMeasurementDataCarrierObject secondWeightQuantityObject, string targetUnitForWeightAddition)
        {
            try
            {
                UniversalMeasurementDataCarrierObject result = extremelyImportantQuantityMeasurementServiceInstanceUsedForAllMeasurementOperations.PerformAdditionOperationBetweenTwoQuantityObjectsAndReturnResult(firstWeightQuantityObject, secondWeightQuantityObject, targetUnitForWeightAddition);
                Console.WriteLine($"✅ Addition Result: {firstWeightQuantityObject} + {secondWeightQuantityObject} = {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error occurred during weight addition: " + ex.Message);
            }
        }

        public void PerformSubtractionOfTwoWeightQuantitiesAndDisplayResult(UniversalMeasurementDataCarrierObject firstWeightQuantityObject, UniversalMeasurementDataCarrierObject secondWeightQuantityObject, string targetUnitForWeightSubtraction)
        {
            try
            {
                UniversalMeasurementDataCarrierObject result = extremelyImportantQuantityMeasurementServiceInstanceUsedForAllMeasurementOperations.PerformSubtractionOperationBetweenTwoQuantityObjectsAndReturnResult(firstWeightQuantityObject, secondWeightQuantityObject, targetUnitForWeightSubtraction);
                Console.WriteLine($"✅ Subtraction Result: {firstWeightQuantityObject} - {secondWeightQuantityObject} = {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error occurred during weight subtraction: " + ex.Message);
            }
        }

        public void PerformDivisionOfTwoWeightQuantitiesAndDisplayResult(UniversalMeasurementDataCarrierObject firstWeightQuantityObject, UniversalMeasurementDataCarrierObject secondWeightQuantityObject)
        {
            try
            {
                double result = extremelyImportantQuantityMeasurementServiceInstanceUsedForAllMeasurementOperations.PerformDivisionOperationBetweenTwoQuantityObjectsAndReturnNumericResult(firstWeightQuantityObject, secondWeightQuantityObject);
                Console.WriteLine($"✅ Division Result: {firstWeightQuantityObject} / {secondWeightQuantityObject} = {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error occurred during weight division: " + ex.Message);
            }
        }

        // ========================== VOLUME METHODS ==========================

        public void PerformVolumeComparisonBetweenTwoGivenVolumeQuantitiesAndDisplayResult(UniversalMeasurementDataCarrierObject firstVolumeQuantityObject, UniversalMeasurementDataCarrierObject secondVolumeQuantityObject)
        {
            try
            {
                bool result = extremelyImportantQuantityMeasurementServiceInstanceUsedForAllMeasurementOperations.PerformComparisonOperationBetweenTwoQuantityObjectsAndReturnBooleanResult(firstVolumeQuantityObject, secondVolumeQuantityObject);
                Console.WriteLine(result ? "✅ The given volume values are equal." : "❌ The given volume values are not equal.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error occurred during volume comparison: " + ex.Message);
            }
        }

        public void PerformVolumeConversionFromOneUnitToAnotherUnitAndDisplayResult(UniversalMeasurementDataCarrierObject inputVolumeQuantityObject, string targetUnitForVolumeConversion)
        {
            try
            {
                UniversalMeasurementDataCarrierObject result = extremelyImportantQuantityMeasurementServiceInstanceUsedForAllMeasurementOperations.PerformConversionOperationBetweenUnitsAndReturnConvertedQuantity(inputVolumeQuantityObject, targetUnitForVolumeConversion);
                Console.WriteLine($"✅ Conversion Result: {inputVolumeQuantityObject} = {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error occurred during volume conversion: " + ex.Message);
            }
        }

        public void PerformVolumeAdditionBetweenTwoVolumeQuantitiesAndDisplayResult(UniversalMeasurementDataCarrierObject firstVolumeQuantityObject, UniversalMeasurementDataCarrierObject secondVolumeQuantityObject, string targetUnitForVolumeAddition)
        {
            try
            {
                UniversalMeasurementDataCarrierObject result = extremelyImportantQuantityMeasurementServiceInstanceUsedForAllMeasurementOperations.PerformAdditionOperationBetweenTwoQuantityObjectsAndReturnResult(firstVolumeQuantityObject, secondVolumeQuantityObject, targetUnitForVolumeAddition);
                Console.WriteLine($"✅ Addition Result: {firstVolumeQuantityObject} + {secondVolumeQuantityObject} = {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error occurred during volume addition: " + ex.Message);
            }
        }

        public void PerformVolumeSubtractionBetweenTwoVolumeQuantitiesAndDisplayResult(UniversalMeasurementDataCarrierObject firstVolumeQuantityObject, UniversalMeasurementDataCarrierObject secondVolumeQuantityObject, string targetUnitForVolumeSubtraction)
        {
            try
            {
                UniversalMeasurementDataCarrierObject result = extremelyImportantQuantityMeasurementServiceInstanceUsedForAllMeasurementOperations.PerformSubtractionOperationBetweenTwoQuantityObjectsAndReturnResult(firstVolumeQuantityObject, secondVolumeQuantityObject, targetUnitForVolumeSubtraction);
                Console.WriteLine($"✅ Subtraction Result: {firstVolumeQuantityObject} - {secondVolumeQuantityObject} = {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error occurred during volume subtraction: " + ex.Message);
            }
        }

        public void PerformVolumeDivisionBetweenTwoVolumeQuantitiesAndDisplayResult(UniversalMeasurementDataCarrierObject firstVolumeQuantityObject, UniversalMeasurementDataCarrierObject secondVolumeQuantityObject)
        {
            try
            {
                double result = extremelyImportantQuantityMeasurementServiceInstanceUsedForAllMeasurementOperations.PerformDivisionOperationBetweenTwoQuantityObjectsAndReturnNumericResult(firstVolumeQuantityObject, secondVolumeQuantityObject);
                Console.WriteLine($"✅ Division Result: {firstVolumeQuantityObject} / {secondVolumeQuantityObject} = {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error occurred during volume division: " + ex.Message);
            }
        }

        // ========================== TEMPERATURE METHODS ==========================

        public void PerformTemperatureComparisonBetweenTwoTemperatureQuantitiesAndDisplayResult(UniversalMeasurementDataCarrierObject firstTemperatureQuantityObject, UniversalMeasurementDataCarrierObject secondTemperatureQuantityObject)
        {
            try
            {
                bool result = extremelyImportantQuantityMeasurementServiceInstanceUsedForAllMeasurementOperations.PerformComparisonOperationBetweenTwoQuantityObjectsAndReturnBooleanResult(firstTemperatureQuantityObject, secondTemperatureQuantityObject);
                Console.WriteLine(result ? "✅ The given temperature values are equal." : "❌ The given temperature values are not equal.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error occurred during temperature comparison: " + ex.Message);
            }
        }

        public void PerformTemperatureConversionFromOneUnitToAnotherUnitAndDisplayResult(UniversalMeasurementDataCarrierObject inputTemperatureQuantityObject, string targetUnitForTemperatureConversion)
        {
            try
            {
                UniversalMeasurementDataCarrierObject result = extremelyImportantQuantityMeasurementServiceInstanceUsedForAllMeasurementOperations.PerformConversionOperationBetweenUnitsAndReturnConvertedQuantity(inputTemperatureQuantityObject, targetUnitForTemperatureConversion);
                Console.WriteLine($"✅ Conversion Result: {inputTemperatureQuantityObject} = {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error occurred during temperature conversion: " + ex.Message);
            }
        }

        public void PerformTemperatureArithmeticOperationBetweenTwoTemperatureQuantitiesAndDisplayResult(UniversalMeasurementDataCarrierObject firstTemperatureQuantityObject, UniversalMeasurementDataCarrierObject secondTemperatureQuantityObject, string targetUnitForTemperatureArithmetic)
        {
            try
            {
                UniversalMeasurementDataCarrierObject result = extremelyImportantQuantityMeasurementServiceInstanceUsedForAllMeasurementOperations.PerformAdditionOperationBetweenTwoQuantityObjectsAndReturnResult(firstTemperatureQuantityObject, secondTemperatureQuantityObject, targetUnitForTemperatureArithmetic);
                Console.WriteLine($"✅ Arithmetic Result: {firstTemperatureQuantityObject} + {secondTemperatureQuantityObject} = {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error occurred during temperature calculation: " + ex.Message);
            }
        }
    }
}