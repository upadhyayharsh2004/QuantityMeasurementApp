using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuantityMeasurementApp
{
    // Generic Quantity class that works with any measurement type implementing IMeasurable
    public class Quantity<U> where U : IMeasurable
    {
        // Encapsulated fields storing the numeric value and its unit
        private readonly double Value;
        private readonly U Unit;

        // Tolerance value used for floating-point comparison
        private const double epsilon = 0.0001;

        // Constructor used to initialize the quantity value and unit
        public Quantity(double value, U unit)
        {
            // Validate that unit is not null
            if (unit == null)
            {
                throw new ArgumentException("Unit cannot be null");
            }

            // Validate numeric value
            if (double.IsNaN(value) || double.IsInfinity(value))
            {
                throw new ArgumentException("Invalid numeric value");
            }

            this.Value = value;
            this.Unit = unit;
        }

        // Converts the current quantity to the specified target unit
        public Quantity<U> ConvertTo(U targetUnit)
        {
            // Validate target unit
            if (targetUnit == null)
            {
                throw new ArgumentException("Invalid target unit");
            }

            // Convert current value to base unit
            double baseValue = Unit.ConvertToBaseUnit(this.Value);

            // Convert base unit to target unit
            double convertedValue = targetUnit.ConvertFromBaseUnit(baseValue);

            return new Quantity<U>(convertedValue, targetUnit);
        }

        // Adds two quantities and returns the result in the unit of the first operand
        public Quantity<U> Add(Quantity<U> second)
        {
            // Validate second operand
            if (second == null)
            {
                throw new ArgumentException("Second operand cannot be null");
            }

            // Convert both quantities to base unit
            double firstBase = Unit.ConvertToBaseUnit(this.Value);
            double secondBase = second.Unit.ConvertToBaseUnit(second.Value);

            // Add values in base unit
            double sumBase = firstBase + secondBase;

            // Convert result back to the unit of the first operand
            double resultValue = Unit.ConvertFromBaseUnit(sumBase);

            return new Quantity<U>(resultValue, this.Unit);
        }

        // Adds two quantities and returns the result in a specified target unit
        public Quantity<U> Add(Quantity<U> second, U targetUnit)
        {
            // Validate second operand
            if (second == null)
            {
                throw new ArgumentException("Second operand cannot be null");
            }

            // Validate target unit
            if (targetUnit == null)
            {
                throw new ArgumentException("Invalid target unit");
            }

            // Convert both quantities to base unit
            double firstBase = Unit.ConvertToBaseUnit(this.Value);
            double secondBase = second.Unit.ConvertToBaseUnit(second.Value);

            // Add values in base unit
            double sumBase = firstBase + secondBase;

            // Convert result to the target unit
            double resultValue = targetUnit.ConvertFromBaseUnit(sumBase);

            return new Quantity<U>(resultValue, targetUnit);
        }

        // ---------------- UC-12 OPERATIONS ----------------

        // Subtracts one quantity from another and returns result in current unit
        public Quantity<U> Subtract(Quantity<U> second)
        {
            // Validate second operand
            if (second == null)
            {
                throw new ArgumentException("Second operand cannot be null");
            }

            // Prevent subtraction between different measurement categories
            if (Unit.GetType() != second.Unit.GetType())
            {
                throw new ArgumentException("Cannot subtract quantities of different measurement categories");
            }

            // Convert both quantities to base unit
            double firstBase = Unit.ConvertToBaseUnit(this.Value);
            double secondBase = second.Unit.ConvertToBaseUnit(second.Value);

            // Perform subtraction
            double result = firstBase - secondBase;

            // Convert result back to current unit
            double converted = Unit.ConvertFromBaseUnit(result);

            // Round result to two decimal places
            converted = Math.Round(converted, 2);

            return new Quantity<U>(converted, this.Unit);
        }

        // Subtracts two quantities and returns the result in a specified target unit
        public Quantity<U> Subtract(Quantity<U> second, U targetUnit)
        {
            // Validate second operand
            if (second == null)
            {
                throw new ArgumentException("Second operand cannot be null");
            }

            // Validate target unit
            if (targetUnit == null)
            {
                throw new ArgumentException("Invalid target unit");
            }

            // Convert both quantities to base unit
            double firstBase = Unit.ConvertToBaseUnit(this.Value);
            double secondBase = second.Unit.ConvertToBaseUnit(second.Value);

            // Perform subtraction in base unit
            double resultBase = firstBase - secondBase;

            // Convert result to target unit
            double resultValue = targetUnit.ConvertFromBaseUnit(resultBase);

            // Round result to two decimal places
            resultValue = Math.Round(resultValue, 2);

            return new Quantity<U>(resultValue, targetUnit);
        }

        // Divides one quantity by another and returns a numeric ratio
        public double Divide(Quantity<U> second)
        {
            // Validate second operand
            if (second == null)
            {
                throw new ArgumentException("Second operand cannot be null");
            }

            // Prevent division by zero
            if (second.Value == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero quantity");
            }

            // Prevent division between different measurement categories
            if (Unit.GetType() != second.Unit.GetType())
            {
                throw new ArgumentException("Cannot divide quantities of different measurement categories");
            }

            // Convert both quantities to base unit
            double firstBase = Unit.ConvertToBaseUnit(this.Value);
            double secondBase = second.Unit.ConvertToBaseUnit(second.Value);

            // Return ratio of the two values
            return firstBase / secondBase;
        }

        // Override Equals to compare quantities using base unit values
        public override bool Equals(object obj)
        {
            // Check if both references point to same object
            if (this == obj)
            {
                return true;
            }

            // Check for null or different object type
            if (obj == null || obj.GetType() != this.GetType())
            {
                return false;
            }

            // Safe casting
            Quantity<U> other = (Quantity<U>)obj;

            // Ensure quantities belong to the same measurement category
            if (Unit.GetType() != other.Unit.GetType())
            {
                return false;
            }

            // Convert both values to base unit
            double firstBase = Unit.ConvertToBaseUnit(this.Value);
            double secondBase = other.Unit.ConvertToBaseUnit(other.Value);

            // Compare values using epsilon tolerance
            return Math.Abs(firstBase - secondBase) <= epsilon;
        }

        // Generate hash code based on normalized base unit value
        public override int GetHashCode()
        {
            double baseValue = Unit.ConvertToBaseUnit(this.Value);
            double rounded = Math.Round(baseValue / epsilon) * epsilon;

            return rounded.GetHashCode();
        }

        // Returns string representation of the quantity
        public override string ToString()
        {
            return $"{Value} {Unit.GetUnitName()}";
        }

        // Getter method to retrieve stored value
        public double GetValue()
        {
            return Value;
        }
    }
}