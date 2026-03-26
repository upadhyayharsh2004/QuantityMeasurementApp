using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuantityMeasurementAppBusiness.Interfaces;
using QuantityMeasurementAppModels.Enums;

namespace QuantityMeasurementAppBusiness
{
    /// <summary>
    /// ============================================================================================================
    /// CLASS: Quantity<U>
    /// ============================================================================================================
    /// 
    /// PURPOSE:
    /// This is a GENERIC class that represents a measurable quantity along with its unit.
    /// 
    /// It acts as the CORE ENGINE of the Quantity Measurement Application and is responsible for:
    /// - Storing a value along with its unit
    /// - Performing unit conversions
    /// - Supporting arithmetic operations (Add, Subtract, Divide)
    /// - Ensuring validation and correctness of operations
    /// - Comparing quantities with precision tolerance
    /// 
    /// ------------------------------------------------------------------------------------------------------------
    /// GENERIC DESIGN:
    /// 
    /// The class uses GENERICS:
    ///     Quantity<U> where U : IMeasurable
    /// 
    /// This means:
    /// - The class works with ANY measurement type (Length, Weight, Volume, Temperature, etc.)
    /// - The only requirement is that the type must implement IMeasurable
    /// 
    /// BENEFITS:
    /// - Code reusability
    /// - Type safety
    /// - Scalability (new measurement types can be added easily)
    /// 
    /// ------------------------------------------------------------------------------------------------------------
    /// CORE CONCEPT:
    /// 
    /// All operations follow a BASE UNIT STRATEGY:
    /// 
    /// 1. Convert values into BASE UNIT using ConvertToBaseUnit()
    /// 2. Perform calculations in base unit
    /// 3. Convert result back using ConvertFromBaseUnit()
    /// 
    /// This ensures:
    /// - Accuracy
    /// - Consistency
    /// - Avoidance of direct unit-to-unit complexity
    /// 
    /// ------------------------------------------------------------------------------------------------------------
    /// FLOATING POINT PRECISION:
    /// 
    /// Since floating-point calculations can introduce small precision errors,
    /// an epsilon value is used to compare values safely.
    /// 
    /// epsilon = 0.0001 → tolerance threshold
    /// 
    /// ============================================================================================================
    /// </summary>
    public class Quantity<U> where U : IMeasurable
    {
        /// <summary>
        /// Stores the numeric value of the quantity.
        /// 
        /// This value is immutable (readonly) to ensure:
        /// - Data integrity
        /// - Thread safety
        /// - Predictable behavior
        /// </summary>
        private readonly double Value;

        /// <summary>
        /// Stores the unit associated with the value.
        /// 
        /// The unit defines:
        /// - How conversion is performed
        /// - What measurement type this quantity belongs to
        /// </summary>
        private readonly U Unit;

        /// <summary>
        /// Defines a tolerance value used for comparing floating-point numbers.
        /// 
        /// Since double values may have minor precision errors,
        /// we compare values within this acceptable range.
        /// </summary>
        private const double epsilon = 0.0001;

        /// <summary>
        /// CONSTRUCTOR:
        /// Initializes a Quantity object with a value and unit.
        /// 
        /// VALIDATIONS PERFORMED:
        /// - Unit must not be null
        /// - Value must not be NaN or Infinity
        /// 
        /// These validations ensure robustness and prevent invalid states.
        /// </summary>
        public Quantity(double value, U unit)
        {
            if (unit == null)
            {
                throw new ArgumentException("Unit cannot be null");
            }

            if (double.IsNaN(value) || double.IsInfinity(value))
            {
                throw new ArgumentException("Invalid numeric value");
            }

            this.Value = value;
            this.Unit = unit;
        }

        /// <summary>
        /// Converts the current quantity into a different target unit.
        /// 
        /// PROCESS:
        /// 1. Convert current value into base unit
        /// 2. Convert base value into target unit
        /// 
        /// Example:
        /// - 1 Meter → 100 Centimeters
        /// 
        /// </summary>
        public Quantity<U> ConvertTo(U targetUnit)
        {
            if (targetUnit == null)
            {
                throw new ArgumentException("Invalid target unit");
            }

            double baseValue = Unit.ConvertToBaseUnit(this.Value);
            double convertedValue = targetUnit.ConvertFromBaseUnit(baseValue);

            return new Quantity<U>(convertedValue, targetUnit);
        }

        /// <summary>
        /// Validates operands before performing arithmetic operations.
        /// 
        /// CHECKS:
        /// - Second operand is not null
        /// - Both quantities belong to same measurement type
        /// - Values are valid (not NaN or Infinity)
        /// - Target unit is valid (if required)
        /// 
        /// This ensures safe and meaningful arithmetic operations.
        /// </summary>
        private void ValidateArithmeticOperands(Quantity<U> second, U targetUnit, bool targetRequired)
        {
            if (second == null)
            {
                throw new ArgumentException("Second operand cannot be null");
            }

            if (Unit.GetType() != second.Unit.GetType())
            {
                throw new ArgumentException("Cannot perform operation on different measurement categories");
            }

            if (double.IsNaN(Value) || double.IsInfinity(Value) ||
                double.IsNaN(second.Value) || double.IsInfinity(second.Value))
            {
                throw new ArgumentException("Invalid numeric value");
            }

            if (targetRequired && targetUnit == null)
            {
                throw new ArgumentException("Invalid target unit");
            }
        }

        /// <summary>
        /// Performs arithmetic operations in BASE UNIT.
        /// 
        /// STEPS:
        /// 1. Convert both values into base unit
        /// 2. Perform operation (Add/Subtract/Divide)
        /// 
        /// WHY BASE UNIT?
        /// - Ensures consistency
        /// - Avoids unit mismatch errors
        /// </summary>
        private double PerformBaseArithmetic(Quantity<U> second, MathematicalComputationActionIdentifier operation)
        {
            double firstBase = Unit.ConvertToBaseUnit(this.Value);
            double secondBase = second.Unit.ConvertToBaseUnit(second.Value);

            if (operation == MathematicalComputationActionIdentifier.COMBINE_VALUES_OPERATION)
            {
                return firstBase + secondBase;
            }
            else if (operation == MathematicalComputationActionIdentifier.DIFFERENCE_CALCULATION_OPERATION)
            {
                return firstBase - secondBase;
            }
            else if (operation == MathematicalComputationActionIdentifier.RATIO_EVALUATION_OPERATION)
            {
                return firstBase / secondBase;
            }

            throw new ArgumentException("Invalid arithmetic operation");
        }

        /// <summary>
        /// Adds two quantities and returns result in current unit.
        /// </summary>
        public Quantity<U> Add(Quantity<U> second)
        {
            Unit.ValidateOperationSupport("Addition");
            ValidateArithmeticOperands(second, this.Unit, true);

            double baseResult = PerformBaseArithmetic(second, MathematicalComputationActionIdentifier.COMBINE_VALUES_OPERATION);
            double resultValue = Unit.ConvertFromBaseUnit(baseResult);

            return new Quantity<U>(resultValue, this.Unit);
        }

        /// <summary>
        /// Adds two quantities and returns result in specified target unit.
        /// </summary>
        public Quantity<U> Add(Quantity<U> second, U targetUnit)
        {
            Unit.ValidateOperationSupport("Addition");
            ValidateArithmeticOperands(second, targetUnit, true);

            double baseResult = PerformBaseArithmetic(second, MathematicalComputationActionIdentifier.COMBINE_VALUES_OPERATION);
            double resultValue = targetUnit.ConvertFromBaseUnit(baseResult);

            return new Quantity<U>(resultValue, targetUnit);
        }

        /// <summary>
        /// Subtracts two quantities and returns result in current unit.
        /// </summary>
        public Quantity<U> Subtract(Quantity<U> second)
        {
            Unit.ValidateOperationSupport("Subtraction");
            ValidateArithmeticOperands(second, this.Unit, true);

            double baseResult = PerformBaseArithmetic(second, MathematicalComputationActionIdentifier.DIFFERENCE_CALCULATION_OPERATION);
            double resultValue = Unit.ConvertFromBaseUnit(baseResult);

            resultValue = Math.Round(resultValue, 2);

            return new Quantity<U>(resultValue, this.Unit);
        }

        /// <summary>
        /// Subtracts two quantities and returns result in target unit.
        /// </summary>
        public Quantity<U> Subtract(Quantity<U> second, U targetUnit)
        {
            Unit.ValidateOperationSupport("Subtraction");
            ValidateArithmeticOperands(second, targetUnit, true);

            double baseResult = PerformBaseArithmetic(second, MathematicalComputationActionIdentifier.DIFFERENCE_CALCULATION_OPERATION);
            double resultValue = targetUnit.ConvertFromBaseUnit(baseResult);

            resultValue = Math.Round(resultValue, 2);

            return new Quantity<U>(resultValue, targetUnit);
        }

        /// <summary>
        /// Divides two quantities and returns scalar result.
        /// </summary>
        public double Divide(Quantity<U> second)
        {
            Unit.ValidateOperationSupport("Division");
            ValidateArithmeticOperands(second, this.Unit, false);

            if (second.Value == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero quantity");
            }

            return PerformBaseArithmetic(second, MathematicalComputationActionIdentifier.DIFFERENCE_CALCULATION_OPERATION);
        }

        /// <summary>
        /// Compares two quantities using epsilon tolerance.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }

            if (obj == null || obj.GetType() != this.GetType())
            {
                return false;
            }

            Quantity<U> other = (Quantity<U>)obj;

            if (Unit.GetType() != other.Unit.GetType())
            {
                return false;
            }

            double firstBase = Unit.ConvertToBaseUnit(this.Value);
            double secondBase = other.Unit.ConvertToBaseUnit(other.Value);

            return Math.Abs(firstBase - secondBase) <= epsilon;
        }

        /// <summary>
        /// Generates hash code based on normalized value.
        /// </summary>
        public override int GetHashCode()
        {
            double baseValue = Unit.ConvertToBaseUnit(this.Value);
            double rounded = Math.Round(baseValue / epsilon) * epsilon;

            return rounded.GetHashCode();
        }

        /// <summary>
        /// Returns string representation of quantity.
        /// </summary>
        public override string ToString()
        {
            return $"{Value} {Unit.GetUnitName()}";
        }

        /// <summary>
        /// Returns numeric value of quantity.
        /// </summary>
        public double GetValue()
        {
            return Value;
        }
    }
}