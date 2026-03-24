namespace QuantityMeasurementAppModels.Enums
{
    /*
     * ============================================================================================
     * ENUM: ArithmeticOperation
     * 
     * This enum (short for "enumeration") is used to define a set of named constant values
     * that represent different types of arithmetic operations supported by the system.
     * 
     * Why use an enum instead of strings?
     * --------------------------------------------------------------------------------------------
     * - Provides type safety (prevents invalid values from being used)
     * - Improves code readability and maintainability
     * - Avoids spelling mistakes that can occur with string-based operations
     * - Makes the code more structured and self-explanatory
     * 
     * In this application, ArithmeticOperation is used to standardize the operations
     * that can be performed on quantities such as addition, subtraction, and division.
     * 
     * Each enum value internally maps to an integer starting from 0 by default:
     * - ADD       = 0
     * - SUBTRACT  = 1
     * - DIVIDE    = 2
     * 
     * These values can be used in logic such as switch statements, condition checks,
     * or method parameters to control program behavior in a clean and efficient way.
     * ============================================================================================
     */
    public enum ArithmeticOperation
    {
        /*
         * ----------------------------------------------------------------------------------------
         * Represents the addition operation.
         * 
         * Example:
         * - Adding two quantities like 5 meters + 10 meters
         * - The result would typically be the sum of both values in a compatible unit
         * 
         * This enum value can be used to trigger addition logic in the application.
         * ----------------------------------------------------------------------------------------
         */
        ADD,

        /*
         * ----------------------------------------------------------------------------------------
         * Represents the subtraction operation.
         * 
         * Example:
         * - Subtracting one quantity from another like 10 meters - 3 meters
         * - The result would be the difference between the two values
         * 
         * This enum value ensures subtraction logic is applied consistently.
         * ----------------------------------------------------------------------------------------
         */
        SUBTRACT,

        /*
         * ----------------------------------------------------------------------------------------
         * Represents the division operation.
         * 
         * Example:
         * - Dividing one value by another like 10 / 2
         * - Special care must be taken to handle cases such as division by zero
         * 
         * This enum value is used to identify when division logic should be executed.
         * ----------------------------------------------------------------------------------------
         */
        DIVIDE
    }
}