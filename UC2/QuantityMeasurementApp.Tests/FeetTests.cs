using QuantityMeasurementApp;

namespace QuantityMeasurementApp.Tests
{
    [TestClass]
    public class FeetTests
    {
        // Checks whether two Feet objects having the same numeric value are equal
        [TestMethod]
        public void TestFeetEquality_WithSameValues_ReturnsTrue()
        {   
            // Arrange: initialize both objects with identical values
            Feet first = new Feet(1.0);
            Feet second = new Feet(1.0);

            // Act: perform equality comparison
            bool result = first.Equals(second);

            // Assert: result should be true
            Assert.IsTrue(result);
        }

        // Checks whether two Feet objects with different values are not equal
        [TestMethod]
        public void TestFeetEquality_WithDifferentValues_ReturnsFalse()
        {
            // Arrange: initialize objects with different values
            Feet first = new Feet(1.0);
            Feet second = new Feet(2.0);

            // Act
            bool result = first.Equals(second);

            // Assert: result should be false
            Assert.IsFalse(result);
        }

        // Verifies that comparing a Feet object with null returns false
        [TestMethod]
        public void TestFeetEquality_WhenComparedWithNull_ReturnsFalse()
        {
            // Arrange
            Feet first = new Feet(1.0);

            // Act
            bool result = first.Equals(null);

            // Assert
            Assert.IsFalse(result);
        }

        // Verifies that comparing Feet with an object of another type returns false
        [TestMethod]
        public void TestFeetEquality_WhenComparedWithNonNumericInput_ReturnsFalse()
        {
            // Arrange
            Feet first = new Feet(1.0);

            // Act: compare with a string instead of Feet object
            bool result = first.Equals("One");

            // Assert
            Assert.IsFalse(result);
        }

        // Verifies equality check when the same object reference is compared
        [TestMethod]
        public void TestFeetEquality_WithSameReference_ReturnsTrue()
        {
            // Arrange
            Feet first = new Feet(1.0);

            // Act: compare object with itself
            bool result = first.Equals(first);

            // Assert
            Assert.IsTrue(result);
        }
    }
}