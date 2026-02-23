namespace QuantityMeasurementApp
{
    public class QuantityLength
    {
        // Private fields to store the numeric length and its unit
        private readonly double value;
        private readonly LengthUnit unit;

        // Initializes a new length with validation for a valid unit
        public QuantityLength(double value, LengthUnit unit)
        {
            if (!Enum.IsDefined(typeof(LengthUnit), unit))
                throw new ArgumentException("Invalid unit type");
            this.value = value;
            this.unit = unit;
        }

        // Converts the stored value into the base unit (feet)
        private double ConvertToBaseUnit()
        {
            switch (unit)
            {
                case LengthUnit.Feet:
                    return value;

                case LengthUnit.Inch:
                    return value / 12.0;

                default:
                    throw new ArgumentException("Invalid Unit Type");
            }
        }

        // Determines equality by comparing values after converting to the same base unit
        public override bool Equals(object obj)
        {
            // Return true if both references point to the same object
            if (this == obj)
            {
                return true;
            }

            // Return false if the object is null or of a different type
            if (obj == null || obj.GetType() != this.GetType())
            {
                return false;
            }

            // Cast the object safely to QuantityLength
            QuantityLength other = (QuantityLength)obj;

            // Compare normalized (base unit) values
            return this.ConvertToBaseUnit() == other.ConvertToBaseUnit();
        }

        // Generates hash code based on the normalized base unit value
        public override int GetHashCode()
        {
            return this.ConvertToBaseUnit().GetHashCode();
        }
    }
}