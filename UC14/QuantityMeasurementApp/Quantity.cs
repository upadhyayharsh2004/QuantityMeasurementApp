using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuantityMeasurementApp
{
    //Generic class representing a measurable quantity with a value and unit
    public class Quantity<U> where U : IMeasurable
    {
        //Private fields storing the numeric value and its corresponding unit
        private readonly double Value;
        private readonly U Unit;

        //Tolerance value used for comparing floating-point numbers
        private const double epsilon = 0.0001;

        //Constructor to initialize quantity with value and unit
        public Quantity(double value, U unit)
        {
            //Ensure the unit object is not null
            if (unit == null)
            {
                throw new ArgumentException("Unit cannot be null");
            }

            //Validate numeric value (must not be NaN or Infinity)
            if (double.IsNaN(value) || double.IsInfinity(value))
            {
                throw new ArgumentException("Invalid numeric value");
            }

            this.Value = value;
            this.Unit = unit;
        }


        //Converts the current quantity to another specified unit
        public Quantity<U> ConvertTo(U targetUnit)
        {
            //Ensure the target unit is valid
            if (targetUnit == null)
            {
                throw new ArgumentException("Invalid target unit");
            }

            //Convert current value to the base unit
            double baseValue = Unit.ConvertToBaseUnit(this.Value);

            //Convert base unit value to the target unit
            double convertedValue = targetUnit.ConvertFromBaseUnit(baseValue);

            return new Quantity<U>(convertedValue, targetUnit);
        }


        //UC-13: Centralized arithmetic logic to follow DRY principle in quantity operations

        //Validates operands before performing arithmetic operations
        private void ValidateArithmeticOperands(Quantity<U> second, U targetUnit, bool targetRequired)
        {
            //Ensure second operand exists
            if (second == null)
            {
                throw new ArgumentException("Second operand cannot be null");
            }

            //Ensure both quantities belong to the same measurement category
            if (Unit.GetType() != second.Unit.GetType())
            {
                throw new ArgumentException("Cannot perform operation on different measurement categories");
            }

            //Validate numeric values of both quantities
            if (double.IsNaN(Value) || double.IsInfinity(Value) ||
                double.IsNaN(second.Value) || double.IsInfinity(second.Value))
            {
                throw new ArgumentException("Invalid numeric value");
            }

            //Check target unit if the operation requires it
            if (targetRequired && targetUnit == null)
            {
                throw new ArgumentException("Invalid target unit");
            }
        }


        //Performs arithmetic operations after converting both quantities to base units
        private double PerformBaseArithmetic(Quantity<U> second, ArithmeticOperation operation)
        {
            //Convert both quantities to base units
            double firstBase = Unit.ConvertToBaseUnit(this.Value);
            double secondBase = second.Unit.ConvertToBaseUnit(second.Value);

            //Perform the requested arithmetic operation
            if (operation == ArithmeticOperation.ADD)
            {
                return firstBase + secondBase;
            }
            else if (operation == ArithmeticOperation.SUBTRACT)
            {
                return firstBase - secondBase;
            }
            else if (operation == ArithmeticOperation.DIVIDE)
            {
                return firstBase / secondBase;
            }

            throw new ArgumentException("Invalid arithmetic operation");
        }


        //Adds two quantities and returns the result in the current unit
        public Quantity<U> Add(Quantity<U> second)
        {
            //Verify that the unit supports addition
            Unit.ValidateOperationSupport("Addition");

            //Validate operands before performing operation
            ValidateArithmeticOperands(second, this.Unit, true);

            //Perform addition using base units
            double baseResult = PerformBaseArithmetic(second, ArithmeticOperation.ADD);

            //Convert result back to the current unit
            double resultValue = Unit.ConvertFromBaseUnit(baseResult);

            return new Quantity<U>(resultValue, this.Unit);
        }


        //Adds two quantities and returns the result in the specified target unit
        public Quantity<U> Add(Quantity<U> second, U targetUnit)
        {
            //Verify that the unit supports addition
            Unit.ValidateOperationSupport("Addition");

            //Validate operands including target unit
            ValidateArithmeticOperands(second, targetUnit, true);

            //Perform addition in base unit
            double baseResult = PerformBaseArithmetic(second, ArithmeticOperation.ADD);

            //Convert result to the target unit
            double resultValue = targetUnit.ConvertFromBaseUnit(baseResult);

            return new Quantity<U>(resultValue, targetUnit);
        }

        //UC-12: Subtraction and division operations for quantity measurements

        //Subtracts one quantity from another and returns result in current unit
        public Quantity<U> Subtract(Quantity<U> second)
        {
            //Verify that subtraction is supported
            Unit.ValidateOperationSupport("Subtraction");

            //Validate operands
            ValidateArithmeticOperands(second, this.Unit, true);

            //Perform subtraction in base unit
            double baseResult = PerformBaseArithmetic(second, ArithmeticOperation.SUBTRACT);

            //Convert result back to the current unit
            double resultValue = Unit.ConvertFromBaseUnit(baseResult);

            //Round result to two decimal places
            resultValue = Math.Round(resultValue, 2);

            return new Quantity<U>(resultValue, this.Unit);
        }

        //Subtracts two quantities and returns the result in the specified target unit
        public Quantity<U> Subtract(Quantity<U> second, U targetUnit)
        {
            //Verify that subtraction is supported
            Unit.ValidateOperationSupport("Subtraction");

            //Validate operands including target unit
            ValidateArithmeticOperands(second, targetUnit, true);

            //Perform subtraction in base unit
            double baseResult = PerformBaseArithmetic(second, ArithmeticOperation.SUBTRACT);

            //Convert result to target unit
            double resultValue = targetUnit.ConvertFromBaseUnit(baseResult);

            //Round result to two decimal places
            resultValue = Math.Round(resultValue, 2);

            return new Quantity<U>(resultValue, targetUnit);
        }

        //Divides two quantities and returns a numeric ratio
        public double Divide(Quantity<U> second)
        {
            //Verify that division is supported
            Unit.ValidateOperationSupport("Division");

            //Validate operands
            ValidateArithmeticOperands(second, this.Unit, false);

            //Prevent division by zero
            if (second.Value == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero quantity");
            }

            //Perform division using base units
            return PerformBaseArithmetic(second, ArithmeticOperation.DIVIDE);
        }

        //Overrides Equals to compare two quantities logically
        public override bool Equals(object obj)
        {
            //Check if both references point to the same object
            if (this == obj)
            {
                return true;
            }

            //Ensure object is not null and of the same type
            if (obj == null || obj.GetType() != this.GetType())
            {
                return false;
            }

            //Safely cast object to Quantity
            Quantity<U> other = (Quantity<U>)obj;

            //Ensure both quantities belong to the same measurement category
            if (Unit.GetType() != other.Unit.GetType())
            {
                return false;
            }

            //Convert both values to base units
            double firstBase = Unit.ConvertToBaseUnit(this.Value);
            double secondBase = other.Unit.ConvertToBaseUnit(other.Value);

            //Compare values using epsilon tolerance
            return Math.Abs(firstBase - secondBase) <= epsilon;
        }


        //Overrides GetHashCode to maintain consistency with Equals
        public override int GetHashCode()
        {
            //Convert value to base unit and normalize using epsilon
            double baseValue = Unit.ConvertToBaseUnit(this.Value);
            double rounded = Math.Round(baseValue / epsilon) * epsilon;

            return rounded.GetHashCode();
        }


        //Returns a readable string representation of the quantity
        public override string ToString()
        {
            return $"{Value} {Unit.GetUnitName()}";
        }

        //Getter method to retrieve the stored value
        public double GetValue()
        {
            return Value;
        }
    }
}