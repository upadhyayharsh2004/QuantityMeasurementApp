using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuantityMeasurementApp
{
    // Generic Quantity class that works with any measurement unit implementing IMeasurable
    public class Quantity<U> where U : IMeasurable
    {
        // Encapsulated fields storing the numeric value and its unit
        private readonly double Value;
        private readonly U Unit;

        // Tolerance value used for floating-point comparison
        private const double epsilon = 0.0001;

        // Constructor used to initialize quantity value and unit
        public Quantity(double value, U unit)
        {
            // Validate unit object
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

        // Converts the current quantity to a specified target unit
        public Quantity<U> ConvertTo(U targetUnit)
        {
            // Validate target unit
            if (targetUnit == null)
            {
                throw new ArgumentException("Invalid target unit");
            }

            // Convert current value to base unit
            double baseValue = Unit.ConvertToBaseUnit(this.Value);

            // Convert base value to the target unit
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

            // Convert result back to the first operand's unit
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

            // Convert result to target unit
            double resultValue = targetUnit.ConvertFromBaseUnit(sumBase);

            return new Quantity<U>(resultValue, targetUnit);
        }

        // Override Equals to compare quantities based on their base unit values
        public override bool Equals(object obj)
        {
            // Check if both references are the same
            if (this == obj)
            {
                return true;
            }

            // Check for null or different object type
            if (obj == null || obj.GetType() != this.GetType())
            {
                return false;
            }

            // Safe type casting
            Quantity<U> other = (Quantity<U>)obj;

            // Ensure both quantities belong to the same measurement category
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

        // Override GetHashCode based on normalized base unit value
        public override int GetHashCode()
        {
            // Normalize value using epsilon before hashing
            double baseValue = Unit.ConvertToBaseUnit(this.Value);
            double rounded = Math.Round(baseValue / epsilon) * epsilon;

            return rounded.GetHashCode();
        }

        // Returns a readable string representation of the quantity
        public override string ToString()
        {
            return $"{Value} {Unit.GetUnitName()}";
        }

        // Getter method for retrieving the stored numeric value
        public double GetValue()
        {
            return Value;
        }
    }
}