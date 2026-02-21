
using Microsoft.VisualStudio.TestTools.UnitTesting;

using QuantityMeasurementApp;

namespace QuantityMeasurementAppTests
{
    [TestClass]
    public class QuantityMeasurementTests
    {
        // Test same value equality
        [TestMethod]
        public void TestEquality_SameValue()
        {
            var firstFeet = new Feet(1.0);
            
            var secondFeet = new Feet(1.0);

            bool result = firstFeet.Equals(secondFeet);

            Assert.IsTrue(result, "1.0 ft should equal 1.0 ft");
        }

        // Test different value inequality
        [TestMethod]
        public void TestEquality_DifferentValue()
        {
            var firstFeet = new Feet(1.0);

            var secondFeet = new Feet(2.0);

            bool result = firstFeet.Equals(secondFeet);

            Assert.IsFalse(result, "1.0 ft should not equal 2.0 ft");
        }

        // Test null comparison
        [TestMethod]
        public void TestEquality_NullComparison()
        {
            var firstFeet = new Feet(1.0);

            bool result = firstFeet.Equals(null);

            Assert.IsFalse(result, "Feet should not equal null");
        }

        // Test reflexive property
        [TestMethod]
        public void TestEquality_SameReference()
        {
            var firstFeet = new Feet(1.0);

            bool result = firstFeet.Equals(firstFeet);

            Assert.IsTrue(result, "Same reference must be equal");
        }

        // Test type safety
        [TestMethod]
        public void TestEquality_NonNumericInput()
        {
            var firstFeet = new Feet(1.0);

            object invalidFeet = "Not a Feet object";

            bool result = firstFeet.Equals(invalidFeet);

            Assert.IsFalse(result, "Feet should not equal non-numeric input");
        }
    }
}