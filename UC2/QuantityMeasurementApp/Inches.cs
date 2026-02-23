namespace QuantityMeasurementApp
{
    public class Inches
    {
        // Stores the measurement value in inches (immutable)
        private readonly double value;

        // Constructor to initialize inches value
        public Inches(double value)
        {
            this.value = value;
        }

        // Overrides Equals() to compare two Inches objects by their value
        public override bool Equals(object obj)
        {
            // Return true if both references point to the same object
            if (this == obj)
                return true;

            // Return false if object is null or not of type Inches
            if (obj == null || obj.GetType() != this.GetType())
                return false;

            // Cast object safely to Inches type
            Inches inches = (Inches)obj;

            // Compare the stored values for equality
            return value.CompareTo(inches.value) == 0;
        }

        // Overrides GetHashCode() so equal objects produce same hash code
        public override int GetHashCode()
        {
            return this.value.GetHashCode();
        }
    }
}