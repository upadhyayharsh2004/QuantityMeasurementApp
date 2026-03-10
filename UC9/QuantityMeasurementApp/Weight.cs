using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantityMeasurementApp
{
    // Class representing a weight with value and unit
    public class Weight
    {
        // Stores the numeric value of the weight
        private readonly double value;

        // Stores the unit of the weight (Kg, Gram, Pound)
        private readonly WeightUnit unit;

        // Constructor to initialize weight value and unit
        public Weight(double value, WeightUnit unit)
        {
            // Validate numeric value
            if (double.IsNaN(value) || double.IsInfinity(value))
            {
                throw new ArgumentException("Invalid numeric value");
            }

            // Validate unit
            if (!Enum.IsDefined(typeof(WeightUnit), unit))
            {
                throw new ArgumentException("Invalid unit");
            }

            this.value = value;
            this.unit = unit;
        }

        // Converts the current weight to the base unit (Kilogram)
        private double ToBase()
        {
            return WeightUnitExtensions.ConvertToBaseUnit(unit, value);
        }

        // Converts the current weight to a target unit
        public Weight ConvertTo(WeightUnit targetUnit)
        {
            // Validate target unit
            if (!Enum.IsDefined(typeof(WeightUnit), targetUnit))
            {
                throw new ArgumentException("Invalid target unit");
            }

            double baseValue = ToBase();
            double converted = WeightUnitExtensions.ConvertFromBaseUnit(targetUnit, baseValue);

            return new Weight(converted, targetUnit);
        }

        // Adds two weights and returns the result in the current object's unit
        public Weight Add(Weight other)
        {
            // Check if other weight is null
            if (other == null)
            {
                throw new ArgumentException("Other weight cannot be null");
            }

            double sumBase = this.ToBase() + other.ToBase();
            double result = WeightUnitExtensions.ConvertFromBaseUnit(this.unit, sumBase);

            return new Weight(result, this.unit);
        }

        // Adds two weights and returns the result in a specified target unit
        public Weight Add(Weight other, WeightUnit targetUnit)
        {
            if (other == null)
            {
                throw new ArgumentException("Other weight cannot be null");
            }

            // Validate target unit
            if (!Enum.IsDefined(typeof(WeightUnit), targetUnit))
            {
                throw new ArgumentException("Invalid target unit");
            }

            double sumBase = this.ToBase() + other.ToBase();
            double result = WeightUnitExtensions.ConvertFromBaseUnit(targetUnit, sumBase);

            return new Weight(result, targetUnit);
        }

        // Checks equality between two weight objects
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj.GetType() != typeof(Weight))
            {
                return false;
            }

            Weight other = (Weight)obj;

            // Calculate difference in base units
            double difference = Math.Abs(this.ToBase() - other.ToBase());

            // Allow small tolerance for floating point comparison
            if (difference < 0.0001)
            {
                return true;
            }

            return false;
        }

        // Returns hash code based on base unit value
        public override int GetHashCode()
        {
            return ToBase().GetHashCode();
        }

        // Returns string representation of the weight
        public override string ToString()
        {
            return value + " " + unit;
        }
    }
}