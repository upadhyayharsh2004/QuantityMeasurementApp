
namespace QuantityMeasurementApp
{
    // Main class as required by UC1

    // Inner class Feet

    public class Feet
    {
        // private readonly ensures encapsulation and immutability
        private readonly double value;

            // Constructor initializes value
        public Feet(double value)
        {
            this.value = value;
        }

        // Override Equals method for value-based equality
        public override bool Equals(object? obj)
        {
            // Reflexive property
            if (this == obj)
            {
                return true;
            }

            // Null check and type safety
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }

            // Safe casting
            Feet other = (Feet)obj;

            // Floating-point comparison using CompareTo
            return this.value.CompareTo(other.value) == 0;
        }
        // Override GetHashCode (required when Equals overridden)
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }
    }
}