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

        public void PerformLengthComparisonBetweenTwoGivenLengthQuantityObjectsAndDisplayWhetherTheyAreEqualOrNot(QuantityDTO firstLengthQuantityObjectParameterForComparisonOperation, QuantityDTO secondLengthQuantityObjectParameterForComparisonOperation)
        {
            try
            {
                bool comparisonResultForLengthQuantities = extremelyImportantQuantityMeasurementServiceInstanceUsedForAllMeasurementOperations.Compare(firstLengthQuantityObjectParameterForComparisonOperation, secondLengthQuantityObjectParameterForComparisonOperation);

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

        public void PerformLengthConversionFromOneUnitToAnotherUnitAndDisplayTheConvertedResult(QuantityDTO inputLengthQuantityObjectToBeConvertedIntoTargetUnit, string targetUnitForLengthConversionOperation)
        {
            try
            {
                QuantityDTO convertedLengthQuantityResultAfterSuccessfulConversion = extremelyImportantQuantityMeasurementServiceInstanceUsedForAllMeasurementOperations.Convert(inputLengthQuantityObjectToBeConvertedIntoTargetUnit, targetUnitForLengthConversionOperation);
                Console.WriteLine($"✅ Conversion Result: {inputLengthQuantityObjectToBeConvertedIntoTargetUnit} is equal to {convertedLengthQuantityResultAfterSuccessfulConversion}");
            }
            catch (Exception exceptionOccurredDuringLengthConversionOperationExecution)
            {
                Console.WriteLine("❌ Error occurred during length conversion: " + exceptionOccurredDuringLengthConversionOperationExecution.Message);
            }
        }

        public void PerformAdditionOfTwoLengthQuantitiesAndDisplayResultInSpecifiedTargetUnit(QuantityDTO firstLengthQuantityObjectForAdditionOperation, QuantityDTO secondLengthQuantityObjectForAdditionOperation, string targetUnitForLengthAdditionOperationResult)
        {
            try
            {
                QuantityDTO additionResultForLengthQuantities = extremelyImportantQuantityMeasurementServiceInstanceUsedForAllMeasurementOperations.Add(firstLengthQuantityObjectForAdditionOperation, secondLengthQuantityObjectForAdditionOperation, targetUnitForLengthAdditionOperationResult);
                Console.WriteLine($"✅ Addition Result: {firstLengthQuantityObjectForAdditionOperation} + {secondLengthQuantityObjectForAdditionOperation} = {additionResultForLengthQuantities}");
            }
            catch (Exception exceptionOccurredDuringLengthAdditionOperationExecution)
            {
                Console.WriteLine("❌ Error occurred during length addition: " + exceptionOccurredDuringLengthAdditionOperationExecution.Message);
            }
        }

        public void PerformSubtractionOfTwoLengthQuantitiesAndDisplayResultInSpecifiedTargetUnit(QuantityDTO firstLengthQuantityObjectForSubtractionOperation, QuantityDTO secondLengthQuantityObjectForSubtractionOperation, string targetUnitForLengthSubtractionOperationResult)
        {
            try
            {
                QuantityDTO subtractionResultForLengthQuantities = extremelyImportantQuantityMeasurementServiceInstanceUsedForAllMeasurementOperations.Subtract(firstLengthQuantityObjectForSubtractionOperation, secondLengthQuantityObjectForSubtractionOperation, targetUnitForLengthSubtractionOperationResult);
                Console.WriteLine($"✅ Subtraction Result: {firstLengthQuantityObjectForSubtractionOperation} - {secondLengthQuantityObjectForSubtractionOperation} = {subtractionResultForLengthQuantities}");
            }
            catch (Exception exceptionOccurredDuringLengthSubtractionOperationExecution)
            {
                Console.WriteLine("❌ Error occurred during length subtraction: " + exceptionOccurredDuringLengthSubtractionOperationExecution.Message);
            }
        }

        public void PerformDivisionOfTwoLengthQuantitiesAndDisplayNumericalResult(QuantityDTO firstLengthQuantityObjectForDivisionOperation, QuantityDTO secondLengthQuantityObjectForDivisionOperation)
        {
            try
            {
                double divisionResultForLengthQuantities = extremelyImportantQuantityMeasurementServiceInstanceUsedForAllMeasurementOperations.Divide(firstLengthQuantityObjectForDivisionOperation, secondLengthQuantityObjectForDivisionOperation);
                Console.WriteLine($"✅ Division Result: {firstLengthQuantityObjectForDivisionOperation} / {secondLengthQuantityObjectForDivisionOperation} = {divisionResultForLengthQuantities}");
            }
            catch (Exception exceptionOccurredDuringLengthDivisionOperationExecution)
            {
                Console.WriteLine("❌ Error occurred during length division: " + exceptionOccurredDuringLengthDivisionOperationExecution.Message);
            }
        }

        // ========================== WEIGHT METHODS ==========================

        public void PerformWeightComparisonBetweenTwoGivenWeightQuantitiesAndDisplayResult(QuantityDTO firstWeightQuantityObjectParameter, QuantityDTO secondWeightQuantityObjectParameter)
        {
            try
            {
                bool comparisonResultForWeightQuantities = extremelyImportantQuantityMeasurementServiceInstanceUsedForAllMeasurementOperations.Compare(firstWeightQuantityObjectParameter, secondWeightQuantityObjectParameter);

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

        public void PerformWeightConversionFromOneUnitToAnotherUnitAndDisplayResult(QuantityDTO inputWeightQuantityObjectToBeConverted, string targetUnitForWeightConversionOperation)
        {
            try
            {
                QuantityDTO convertedWeightQuantityResult = extremelyImportantQuantityMeasurementServiceInstanceUsedForAllMeasurementOperations.Convert(inputWeightQuantityObjectToBeConverted, targetUnitForWeightConversionOperation);
                Console.WriteLine($"✅ Conversion Result: {inputWeightQuantityObjectToBeConverted} = {convertedWeightQuantityResult}");
            }
            catch (Exception exceptionOccurredDuringWeightConversionOperationExecution)
            {
                Console.WriteLine("❌ Error occurred during weight conversion: " + exceptionOccurredDuringWeightConversionOperationExecution.Message);
            }
        }

        public void PerformAdditionOfTwoWeightQuantitiesAndDisplayResult(QuantityDTO firstWeightQuantityObject, QuantityDTO secondWeightQuantityObject, string targetUnitForWeightAddition)
        {
            try
            {
                QuantityDTO result = extremelyImportantQuantityMeasurementServiceInstanceUsedForAllMeasurementOperations.Add(firstWeightQuantityObject, secondWeightQuantityObject, targetUnitForWeightAddition);
                Console.WriteLine($"✅ Addition Result: {firstWeightQuantityObject} + {secondWeightQuantityObject} = {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error occurred during weight addition: " + ex.Message);
            }
        }

        public void PerformSubtractionOfTwoWeightQuantitiesAndDisplayResult(QuantityDTO firstWeightQuantityObject, QuantityDTO secondWeightQuantityObject, string targetUnitForWeightSubtraction)
        {
            try
            {
                QuantityDTO result = extremelyImportantQuantityMeasurementServiceInstanceUsedForAllMeasurementOperations.Subtract(firstWeightQuantityObject, secondWeightQuantityObject, targetUnitForWeightSubtraction);
                Console.WriteLine($"✅ Subtraction Result: {firstWeightQuantityObject} - {secondWeightQuantityObject} = {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error occurred during weight subtraction: " + ex.Message);
            }
        }

        public void PerformDivisionOfTwoWeightQuantitiesAndDisplayResult(QuantityDTO firstWeightQuantityObject, QuantityDTO secondWeightQuantityObject)
        {
            try
            {
                double result = extremelyImportantQuantityMeasurementServiceInstanceUsedForAllMeasurementOperations.Divide(firstWeightQuantityObject, secondWeightQuantityObject);
                Console.WriteLine($"✅ Division Result: {firstWeightQuantityObject} / {secondWeightQuantityObject} = {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error occurred during weight division: " + ex.Message);
            }
        }

        // ========================== VOLUME METHODS ==========================

        public void PerformVolumeComparisonBetweenTwoGivenVolumeQuantitiesAndDisplayResult(QuantityDTO firstVolumeQuantityObject, QuantityDTO secondVolumeQuantityObject)
        {
            try
            {
                bool result = extremelyImportantQuantityMeasurementServiceInstanceUsedForAllMeasurementOperations.Compare(firstVolumeQuantityObject, secondVolumeQuantityObject);
                Console.WriteLine(result ? "✅ The given volume values are equal." : "❌ The given volume values are not equal.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error occurred during volume comparison: " + ex.Message);
            }
        }

        public void PerformVolumeConversionFromOneUnitToAnotherUnitAndDisplayResult(QuantityDTO inputVolumeQuantityObject, string targetUnitForVolumeConversion)
        {
            try
            {
                QuantityDTO result = extremelyImportantQuantityMeasurementServiceInstanceUsedForAllMeasurementOperations.Convert(inputVolumeQuantityObject, targetUnitForVolumeConversion);
                Console.WriteLine($"✅ Conversion Result: {inputVolumeQuantityObject} = {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error occurred during volume conversion: " + ex.Message);
            }
        }

        public void PerformVolumeAdditionBetweenTwoVolumeQuantitiesAndDisplayResult(QuantityDTO firstVolumeQuantityObject, QuantityDTO secondVolumeQuantityObject, string targetUnitForVolumeAddition)
        {
            try
            {
                QuantityDTO result = extremelyImportantQuantityMeasurementServiceInstanceUsedForAllMeasurementOperations.Add(firstVolumeQuantityObject, secondVolumeQuantityObject, targetUnitForVolumeAddition);
                Console.WriteLine($"✅ Addition Result: {firstVolumeQuantityObject} + {secondVolumeQuantityObject} = {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error occurred during volume addition: " + ex.Message);
            }
        }

        public void PerformVolumeSubtractionBetweenTwoVolumeQuantitiesAndDisplayResult(QuantityDTO firstVolumeQuantityObject, QuantityDTO secondVolumeQuantityObject, string targetUnitForVolumeSubtraction)
        {
            try
            {
                QuantityDTO result = extremelyImportantQuantityMeasurementServiceInstanceUsedForAllMeasurementOperations.Subtract(firstVolumeQuantityObject, secondVolumeQuantityObject, targetUnitForVolumeSubtraction);
                Console.WriteLine($"✅ Subtraction Result: {firstVolumeQuantityObject} - {secondVolumeQuantityObject} = {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error occurred during volume subtraction: " + ex.Message);
            }
        }

        public void PerformVolumeDivisionBetweenTwoVolumeQuantitiesAndDisplayResult(QuantityDTO firstVolumeQuantityObject, QuantityDTO secondVolumeQuantityObject)
        {
            try
            {
                double result = extremelyImportantQuantityMeasurementServiceInstanceUsedForAllMeasurementOperations.Divide(firstVolumeQuantityObject, secondVolumeQuantityObject);
                Console.WriteLine($"✅ Division Result: {firstVolumeQuantityObject} / {secondVolumeQuantityObject} = {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error occurred during volume division: " + ex.Message);
            }
        }

        // ========================== TEMPERATURE METHODS ==========================

        public void PerformTemperatureComparisonBetweenTwoTemperatureQuantitiesAndDisplayResult(QuantityDTO firstTemperatureQuantityObject, QuantityDTO secondTemperatureQuantityObject)
        {
            try
            {
                bool result = extremelyImportantQuantityMeasurementServiceInstanceUsedForAllMeasurementOperations.Compare(firstTemperatureQuantityObject, secondTemperatureQuantityObject);
                Console.WriteLine(result ? "✅ The given temperature values are equal." : "❌ The given temperature values are not equal.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error occurred during temperature comparison: " + ex.Message);
            }
        }

        public void PerformTemperatureConversionFromOneUnitToAnotherUnitAndDisplayResult(QuantityDTO inputTemperatureQuantityObject, string targetUnitForTemperatureConversion)
        {
            try
            {
                QuantityDTO result = extremelyImportantQuantityMeasurementServiceInstanceUsedForAllMeasurementOperations.Convert(inputTemperatureQuantityObject, targetUnitForTemperatureConversion);
                Console.WriteLine($"✅ Conversion Result: {inputTemperatureQuantityObject} = {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error occurred during temperature conversion: " + ex.Message);
            }
        }

        public void PerformTemperatureArithmeticOperationBetweenTwoTemperatureQuantitiesAndDisplayResult(QuantityDTO firstTemperatureQuantityObject, QuantityDTO secondTemperatureQuantityObject, string targetUnitForTemperatureArithmetic)
        {
            try
            {
                QuantityDTO result = extremelyImportantQuantityMeasurementServiceInstanceUsedForAllMeasurementOperations.Add(firstTemperatureQuantityObject, secondTemperatureQuantityObject, targetUnitForTemperatureArithmetic);
                Console.WriteLine($"✅ Arithmetic Result: {firstTemperatureQuantityObject} + {secondTemperatureQuantityObject} = {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error occurred during temperature calculation: " + ex.Message);
            }
        }
    }
}