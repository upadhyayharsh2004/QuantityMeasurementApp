using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuantityMeasurementApp
{
    //Generic Quantity class representing a measurable value with its unit
    public class Quantity<U> where U : IMeasurable
    {
        //Private fields storing the numeric value and its corresponding unit
        private readonly double Value;
        private readonly U Unit;

        //Tolerance value used when comparing floating point numbers
        private const double epsilon = 0.0001;

        //Constructor to initialize quantity with value and unit
        public Quantity(double value, U unit)
        {
            //Ensure unit object is not null
            if (unit == null)
            {
                throw new ArgumentException("Unit cannot be null");
            }

            //Ensure numeric value is valid (not NaN or Infinity)
            if (double.IsNaN(value) || double.IsInfinity(value))
            {
                throw new ArgumentException("Invalid numeric value");
            }

            this.Value = value;
            this.Unit = unit;
        }


        //Converts current quantity into another unit of the same category
        public Quantity<U> ConvertTo(U targetUnit)
        {
            //Ensure target unit is valid
            if (targetUnit == null)
            {
                throw new ArgumentException("Invalid target unit");
            }

            //Convert the current value to its base unit
            double baseValue = Unit.ConvertToBaseUnit(this.Value);

            //Convert the base unit value into the target unit
            double convertedValue = targetUnit.ConvertFromBaseUnit(baseValue);

            return new Quantity<U>(convertedValue, targetUnit);
        }


        //Centralized arithmetic logic to reduce code duplication for operations

        //Validates operands before performing arithmetic operations
        private void ValidateArithmeticOperands(Quantity<U> second, U targetUnit, bool targetRequired)
        {
            //Check if second operand exists
            if (second == null)
            {
                throw new ArgumentException("Second operand cannot be null");
            }

            //Ensure both quantities belong to the same measurement category
            if (Unit.GetType() != second.Unit.GetType())
            {
                throw new ArgumentException("Cannot perform operation on different measurement categories");
            }

            //Validate numeric values of both operands
            if (double.IsNaN(Value) || double.IsInfinity(Value) ||
                double.IsNaN(second.Value) || double.IsInfinity(second.Value))
            {
                throw new ArgumentException("Invalid numeric value");
            }

            //Check if target unit is required and valid
            if (targetRequired && targetUnit == null)
            {
                throw new ArgumentException("Invalid target unit");
            }
        }


        //Performs arithmetic operations after converting both quantities to base units
        private double PerformBaseArithmetic(Quantity<U> second, ArithmeticOperation operation)
        {
            //Convert both quantities to their base units
            double firstBase = Unit.ConvertToBaseUnit(this.Value);
            double secondBase = second.Unit.ConvertToBaseUnit(second.Value);

            //Execute the requested arithmetic operation
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


        //Adds two quantities and returns result in the current unit
        public Quantity<U> Add(Quantity<U> second)
        {
            //Validate operands before performing addition
            ValidateArithmeticOperands(second, this.Unit, true);

            //Perform addition using base units
            double baseResult = PerformBaseArithmetic(second, ArithmeticOperation.ADD);

            //Convert result back to the current unit
            double resultValue = Unit.ConvertFromBaseUnit(baseResult);

            return new Quantity<U>(resultValue, this.Unit);
        }


        //Adds two quantities and returns result in the specified target unit
        public Quantity<U> Add(Quantity<U> second, U targetUnit)
        {
            //Validate operands including target unit
            ValidateArithmeticOperands(second, targetUnit, true);

            //Perform addition in base unit
            double baseResult = PerformBaseArithmetic(second, ArithmeticOperation.ADD);

            //Convert result to target unit
            double resultValue = targetUnit.ConvertFromBaseUnit(baseResult);

            return new Quantity<U>(resultValue, targetUnit);
        }


        //Subtraction and division operations for quantity measurements

        //Subtracts one quantity from another and returns result in current unit
        public Quantity<U> Subtract(Quantity<U> second)
        {
            //Validate operands
            ValidateArithmeticOperands(second, this.Unit, true);

            //Perform subtraction in base unit
            double baseResult = PerformBaseArithmetic(second, ArithmeticOperation.SUBTRACT);

            //Convert result to current unit
            double resultValue = Unit.ConvertFromBaseUnit(baseResult);

            //Round result to two decimal places
            resultValue = Math.Round(resultValue, 2);

            return new Quantity<U>(resultValue, this.Unit);
        }

        //Subtracts quantities and returns result in target unit
        public Quantity<U> Subtract(Quantity<U> second, U targetUnit)
        {
            //Validate operands and target unit
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
            //Validate operands (target unit not required for division)
            ValidateArithmeticOperands(second, this.Unit, false);

            //Prevent division by zero
            if (second.Value == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero quantity");
            }

            //Perform division in base units
            return PerformBaseArithmetic(second, ArithmeticOperation.DIVIDE);
        }

        //Override Equals to compare two quantities logically
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

            //Safely cast object to Quantity type
            Quantity<U> other = (Quantity<U>)obj;

            //Ensure both quantities belong to same measurement category
            if (Unit.GetType() != other.Unit.GetType())
            {
                return false;
            }

            //Convert both quantities to base units
            double firstBase = Unit.ConvertToBaseUnit(this.Value);
            double secondBase = other.Unit.ConvertToBaseUnit(other.Value);

            //Compare values within epsilon tolerance
            return Math.Abs(firstBase - secondBase) <= epsilon;
        }


        //Override GetHashCode for consistent hashing with Equals
        public override int GetHashCode()
        {
            //Convert to base unit and normalize using epsilon
            double baseValue = Unit.ConvertToBaseUnit(this.Value);
            double rounded = Math.Round(baseValue / epsilon) * epsilon;

            return rounded.GetHashCode();
        }


        //Return readable representation of quantity
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