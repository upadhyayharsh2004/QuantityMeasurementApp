using QuantityMeasurementApp;
namespace QuantityMeasurementApp.Tests
{
    [TestClass]
    public class QuantityLengthTests
    {
        // Verify equality when both values are in Feet and identical
        [TestMethod]
        public void TestEquality_FeetToFeet_SameValue_ReturnsTrue()
        {
            QuantityLength first = new QuantityLength(1.0, LengthUnit.Feet);
            QuantityLength second = new QuantityLength(1.0, LengthUnit.Feet);

            Assert.IsTrue(first.Equals(second));
        }

        // Verify equality when both values are in Inches and identical
        [TestMethod]
        public void TestEquality_InchToInch_SameValue_ReturnsTrue()
        {
            QuantityLength first = new QuantityLength(1.0, LengthUnit.Inch);
            QuantityLength second = new QuantityLength(1.0, LengthUnit.Inch);

            Assert.IsTrue(first.Equals(second));
        }

        // Verify inequality when values differ but units are both Feet
        [TestMethod]
        public void TestEquality_FeetToFeet_DifferentValue_ReturnsFalse()
        {
            QuantityLength first = new QuantityLength(1.0, LengthUnit.Feet);
            QuantityLength second = new QuantityLength(2.0, LengthUnit.Feet);

            Assert.IsFalse(first.Equals(second));
        }

        // Verify inequality when values differ but units are both Inches
        [TestMethod]
        public void TestEquality_InchToInch_DifferentValue_ReturnsFalse()
        {
            QuantityLength first = new QuantityLength(1.0, LengthUnit.Inch);
            QuantityLength second = new QuantityLength(2.0, LengthUnit.Inch);

            Assert.IsFalse(first.Equals(second));
        }

        // Verify equality between equivalent Feet and Inches (1 ft = 12 in)
        [TestMethod]
        public void TestEquality_FeetToInch_EquivalentValue_ReturnsTrue()
        {
            QuantityLength feet = new QuantityLength(1.0, LengthUnit.Feet);
            QuantityLength inches = new QuantityLength(12.0, LengthUnit.Inch);

            Assert.IsTrue(feet.Equals(inches));
        }

        // Verify equality between equivalent Inches and Feet (reverse comparison)
        [TestMethod]
        public void TestEquality_InchToFeet_EquivalentValue_ReturnsTrue()
        {
            QuantityLength inches = new QuantityLength(12.0, LengthUnit.Inch);
            QuantityLength feet = new QuantityLength(1.0, LengthUnit.Feet);

            Assert.IsTrue(inches.Equals(feet));
        }

        // Verify comparison with null returns false
        [TestMethod]
        public void TestEquality_NullComparison_ReturnsFalse()
        {
            QuantityLength first = new QuantityLength(12.0, LengthUnit.Feet);
            QuantityLength second = null;

            bool result = first.Equals(second);

            Assert.IsFalse(result);
        }

        // Verify comparison with an unrelated object type returns false
        [TestMethod]
        public void TestEquality_ComparedWithDifferentType_ReturnsFalse()
        {
            QuantityLength quantity = new QuantityLength(1.0, LengthUnit.Feet);

            Assert.IsFalse(quantity.Equals("Invalid"));
        }

        // Verify constructor throws exception when an invalid unit is used
        [TestMethod]
        public void TestEquality_InvalidUnit_ThrowsException()
        {
            Assert.Throws<Exception>(() =>
            {
                var invalid = new QuantityLength(5.0, (LengthUnit)10);
            });
        }

        // Verify equality when comparing the same object reference
        [TestMethod]
        public void TestEquality_SameReference_ReturnsTrue()
        {
            QuantityLength quantity = new QuantityLength(1.0, LengthUnit.Feet);

            Assert.IsTrue(quantity.Equals(quantity));
        }
    }
}