using System;

namespace QuantityMeasurementAppModels.Entities
{
    /*
     * ============================================================================================
     * The [Serializable] attribute indicates that objects of this class can be converted into a
     * stream of bytes. This is particularly useful when:
     * 
     * - Saving object data into files (e.g., JSON, binary files)
     * - Sending objects over a network
     * - Storing objects in memory caches or sessions
     * 
     * In this application, serialization is important because measurement data may be stored
     * and retrieved later, especially using JSON serialization/deserialization mechanisms.
     * ============================================================================================
     */
    [Serializable]
    public class QuantityMeasurementEntity
    {
        /*
         * ========================================================================================
         * PROPERTIES SECTION
         * 
         * These properties define the structure of a quantity measurement operation.
         * This class acts as an "Entity", meaning it represents actual data stored or processed
         * in the system (for example, records of calculations or conversions).
         * 
         * Each property is defined with { get; set; }, which means:
         * - 'get' allows reading the value
         * - 'set' allows modifying the value
         * 
         * This makes the class flexible and compatible with serializers like JsonSerializer.
         * ========================================================================================
         */

        // Stores the type of operation performed (e.g., "Add", "Subtract", "Convert", "Compare")
        public string Operation       { get; set; }

        // Represents the first input value in the operation (e.g., 10 in "10 meters")
        public double FirstValue      { get; set; }

        // Represents the unit of the first value (e.g., "meters", "kg", etc.)
        public string FirstUnit       { get; set; }

        // Represents the second input value (used in operations involving two operands)
        public double SecondValue     { get; set; }

        // Represents the unit of the second value
        public string SecondUnit      { get; set; }

        // Stores the result obtained after performing the operation
        public double ResultValue     { get; set; }

        // Indicates the type/category of measurement (e.g., "Length", "Mass", "Volume")
        public string MeasurementType { get; set; }

        // A flag to indicate whether an error occurred during the operation
        public bool   IsError         { get; set; }

        // Stores the error message in case the operation fails
        public string ErrorMessage    { get; set; }

        /*
         * ========================================================================================
         * DEFAULT CONSTRUCTOR
         * 
         * This parameterless constructor is required by the JsonSerializer for deserialization.
         * 
         * Why is this important?
         * - When JSON data is read from a file, the serializer needs to create an empty object
         *   first and then populate its properties.
         * - Without this constructor, deserialization may fail.
         * 
         * Note:
         * - This constructor does not initialize values explicitly.
         * - Properties will be assigned automatically during deserialization.
         * ========================================================================================
         */
        public QuantityMeasurementEntity()
        {
        }

        /*
         * ========================================================================================
         * CONSTRUCTOR FOR TWO-OPERAND OPERATIONS
         * 
         * This constructor is used when the operation involves two input values.
         * Examples of such operations include:
         * - Addition (e.g., 5m + 10m)
         * - Subtraction
         * - Comparison
         * - Division
         * 
         * Parameters Explanation:
         * - operation       : Name/type of operation being performed
         * - firstValue      : First input numeric value
         * - firstUnit       : Unit of the first value
         * - secondValue     : Second input numeric value
         * - secondUnit      : Unit of the second value
         * - resultValue     : Result after performing the operation
         * - measurementType : Category of measurement (Length, Volume, etc.)
         * 
         * Additional Behavior:
         * - Sets IsError to false since this represents a successful operation
         * ========================================================================================
         */
        public QuantityMeasurementEntity(string operation,
            double firstValue,  string firstUnit,
            double secondValue, string secondUnit,
            double resultValue,
            string measurementType)
        {
            Operation       = operation;
            FirstValue      = firstValue;
            FirstUnit       = firstUnit;
            SecondValue     = secondValue;
            SecondUnit      = secondUnit;
            ResultValue     = resultValue;
            MeasurementType = measurementType;
            IsError         = false;
        }

        /*
         * ========================================================================================
         * CONSTRUCTOR FOR SINGLE-OPERAND OPERATIONS
         * 
         * This constructor is specifically designed for operations that involve only one input.
         * The most common example is:
         * - Unit Conversion (e.g., converting 10 meters to centimeters)
         * 
         * Parameters Explanation:
         * - operation       : The operation name (typically "Convert")
         * - inputValue      : The value to be converted
         * - inputUnit       : The unit of the input value
         * - resultValue     : The converted result value
         * - measurementType : Category of measurement
         * 
         * Key Point:
         * - SecondValue and SecondUnit are not used here because the operation requires only
         *   one operand.
         * - IsError is set to false to indicate a successful operation.
         * ========================================================================================
         */
        public QuantityMeasurementEntity(string operation,
            double inputValue, string inputUnit,
            double resultValue,
            string measurementType)
        {
            Operation       = operation;
            FirstValue      = inputValue;
            FirstUnit       = inputUnit;
            ResultValue     = resultValue;
            MeasurementType = measurementType;
            IsError         = false;
        }

        /*
         * ========================================================================================
         * CONSTRUCTOR FOR ERROR HANDLING
         * 
         * This constructor is used when an operation fails due to some reason.
         * Examples:
         * - Invalid unit conversion
         * - Division by zero
         * - Unsupported measurement type
         * 
         * Parameters:
         * - operation     : The operation that caused the error
         * - errorMessage  : A descriptive message explaining the error
         * 
         * Behavior:
         * - Sets IsError to true
         * - Stores the error message for debugging or display purposes
         * ========================================================================================
         */
        public QuantityMeasurementEntity(string operation, string errorMessage)
        {
            Operation    = operation;
            IsError      = true;
            ErrorMessage = errorMessage;
        }

        /*
         * ========================================================================================
         * METHOD: ToString()
         * 
         * This method overrides the default ToString() method from the base Object class.
         * It provides a human-readable representation of the object.
         * 
         * Behavior:
         * - If an error has occurred (IsError == true), it returns a formatted error message.
         * - Otherwise, it returns a string showing the operation, input value, unit, and result.
         * 
         * Example Outputs:
         * - "[Add] 10 meters -> 20"
         * - "[Convert] 5 kg -> 5000"
         * - "[Divide] ERROR: Cannot divide by zero"
         * 
         * Importance:
         * - Helps in debugging and logging
         * - Improves readability when printing objects
         * ========================================================================================
         */
        public override string ToString()
        {
            if (IsError)
            {
                return "[" + Operation + "] ERROR: " + ErrorMessage;
            }
            return "[" + Operation + "] "
                + FirstValue + " " + FirstUnit
                + " -> " + ResultValue;
        }
    }
}