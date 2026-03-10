using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantityMeasurementApp
{
    // Class representing a length value with its unit
    public class Length
    {
        // Stores the numeric value of the length
        private readonly double value;

        // Stores the unit of the length (Feet, Inch, Yard, Centimeter)
        private readonly LengthUnit unit;

        // Constructor to initialize length value and unit
        public Length(double value, LengthUnit unit)
        {
            // Validate numeric value
            if (double.IsNaN(value) || double.IsInfinity(value))
            {
                throw new ArgumentException("Invalid numeric value");
            }

            // Validate the unit
            if (!Enum.IsDefined(typeof(LengthUnit), unit))
            {
                throw new ArgumentException("Invalid unit");
            }

            this.value = value;
            this.unit = unit;
        }

        // Converts the current length to the base unit (Feet)
        private double ToBase()
        {
            return LengthUnitExtensions.ConvertToBaseUnit(unit, value);
        }

        // Converts the current length to a target unit
        public Length ConvertTo(LengthUnit targetUnit)
        {
            // Validate target unit
            if (!Enum.IsDefined(typeof(LengthUnit), targetUnit))
            {
                throw new ArgumentException("Invalid target unit");
            }

            double baseValue = ToBase();
            double converted = LengthUnitExtensions.ConvertFromBaseUnit(targetUnit, baseValue);

            return new Length(converted, targetUnit);
        }

        // Adds two lengths and returns the result in the current unit
        public Length Add(Length other)
        {
            // Check if the other length is null
            if (other == null)
            {
                throw new ArgumentException("Other length cannot be null");
            }

            double sumBase = this.ToBase() + other.ToBase();
            double result = LengthUnitExtensions.ConvertFromBaseUnit(this.unit, sumBase);

            return new Length(result, this.unit);
        }

        // Adds two lengths and returns the result in a specified target unit
        public Length Add(Length other, LengthUnit targetUnit)
        {
            if (other == null)
            {
                throw new ArgumentException("Other length cannot be null");
            }

            // Validate target unit
            if (!Enum.IsDefined(typeof(LengthUnit), targetUnit))
            {
                throw new ArgumentException("Invalid target unit");
            }

            double sumBase = this.ToBase() + other.ToBase();
            double result = LengthUnitExtensions.ConvertFromBaseUnit(targetUnit, sumBase);

            return new Length(result, targetUnit);
        }

        // Checks equality between two length objects
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj.GetType() != typeof(Length))
            {
                return false;
            }

            Length other = (Length)obj;

            // Compare values after converting to base unit
            double difference = Math.Abs(this.ToBase() - other.ToBase());

            // Allow small tolerance for floating-point comparison
            if (difference < 0.0001)
            {
                return true;
            }

            return false;
        }

        // Generates a hash code based on the base unit value
        public override int GetHashCode()
        {
            return ToBase().GetHashCode();
        }

        // Returns a readable string representation of the length
        public override string ToString()
        {
            return value + " " + unit;
        }
    }
}