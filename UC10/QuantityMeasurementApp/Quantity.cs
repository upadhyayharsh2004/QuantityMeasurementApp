using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuantityMeasurementApp
{
    // Generic Quantity class that works with any unit implementing IMeasurable
    public class Quantity<U> where U : IMeasurable
    {
        // Encapsulated fields storing value and unit
        private readonly double Value;
        private readonly U Unit;

        // Epsilon used for floating-point comparison tolerance
        private const double epsilon = 0.0001;

        // Constructor to initialize quantity value and unit
        public Quantity(double value, U unit)
        {
            // Validate unit
            if (unit == null)
            {
                throw new ArgumentNullException(nameof(unit));
            }

            // Validate numeric value
            if (double.IsNaN(value) || double.IsInfinity(value))
            {
                throw new ArgumentException("Invalid numeric value");
            }

            this.Value = value;
            this.Unit = unit;
        }

        // Converts the current quantity to a target unit
        public Quantity<U> ConvertTo(U targetUnit)
        {
            if (targetUnit == null)
            {
                throw new ArgumentException("Invalid target unit");
            }

            // Convert value to base unit first
            double baseValue = Unit.ConvertToBaseUnit(this.Value);

            // Convert base unit value to target unit
            double convertedValue = targetUnit.ConvertFromBaseUnit(baseValue);

            return new Quantity<U>(convertedValue, targetUnit);
        }

        // Adds two quantities and returns result in the current unit
        public Quantity<U> Add(Quantity<U> second)
        {
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

        // Adds two quantities and returns result in specified target unit
        public Quantity<U> Add(Quantity<U> second, U targetUnit)
        {
            if (second == null)
            {
                throw new ArgumentException("Second operand cannot be null");
            }

            if (targetUnit == null)
            {
                throw new ArgumentException("Invalid target unit");
            }

            // Convert both quantities to base unit
            double firstBase = Unit.ConvertToBaseUnit(this.Value);
            double secondBase = second.Unit.ConvertToBaseUnit(second.Value);

            // Add base values
            double sumBase = firstBase + secondBase;

            // Convert result to target unit
            double resultValue = targetUnit.ConvertFromBaseUnit(sumBase);

            return new Quantity<U>(resultValue, targetUnit);
        }

        // Checks equality between two Quantity objects

        public override bool Equals(object obj)
        {
            //Check same reference
            if (this == obj)
            {
                return true;
            }

            //Check null or different type
            if (obj == null || obj.GetType() != this.GetType())
            {
                return false;
            }

            Quantity<U> other = (Quantity<U>)obj;

            //Prevent comparison between different measurement categories
            if (this.Unit.GetType() != other.Unit.GetType())
            {
                return false;
            }

            double firstBase = Unit.ConvertToBaseUnit(this.Value);
            double secondBase = other.Unit.ConvertToBaseUnit(other.Value);

            return Math.Abs(firstBase - secondBase) <= epsilon;
        }

        // Generates hash code based on normalized base unit value
        public override int GetHashCode()
        {
            double baseValue = Unit.ConvertToBaseUnit(this.Value);
            double rounded = Math.Round(baseValue / epsilon) * epsilon;

            return rounded.GetHashCode();
        }

        // Returns readable representation of the quantity
        public override string ToString()
        {
            return $"{Value} {Unit.GetUnitName()}";
        }
    }
}