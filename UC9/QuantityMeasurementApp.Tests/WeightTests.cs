using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuantityMeasurementApp;

namespace QuantityMeasurementTests
{
    // Test class for verifying Weight functionality
    [TestClass]
    public class WeightTests
    {
        // Test equality when both weights are in kilograms and same value
        [TestMethod]
        public void testEquality_KilogramToKilogram_SameValue()
        {
            Weight a = new Weight(1.0, WeightUnit.KILOGRAM);
            Weight b = new Weight(1.0, WeightUnit.KILOGRAM);
            Assert.IsTrue(a.Equals(b));
        }

        // Test equality when kilogram values are different
        [TestMethod]
        public void testEquality_KilogramToKilogram_DifferentValue()
        {
            Weight a = new Weight(1.0, WeightUnit.KILOGRAM);
            Weight b = new Weight(2.0, WeightUnit.KILOGRAM);
            Assert.IsFalse(a.Equals(b));
        }

        // Test equality between kilogram and gram with equivalent value
        [TestMethod]
        public void testEquality_KilogramToGram_EquivalentValue()
        {
            Weight a = new Weight(1.0, WeightUnit.KILOGRAM);
            Weight b = new Weight(1000.0, WeightUnit.GRAM);
            Assert.IsTrue(a.Equals(b));
        }

        // Test equality between gram and kilogram with equivalent value
        [TestMethod]
        public void testEquality_GramToKilogram_EquivalentValue()
        {
            Weight a = new Weight(1000.0, WeightUnit.GRAM);
            Weight b = new Weight(1.0, WeightUnit.KILOGRAM);
            Assert.IsTrue(a.Equals(b));
        }

        // Test comparison between Weight and Length objects
        [TestMethod]
        public void testEquality_WeightVsLength_Incompatible()
        {
            Weight weight = new Weight(1.0, WeightUnit.KILOGRAM);
            Length length = new Length(1.0, LengthUnit.FEET);
            Assert.IsFalse(weight.Equals(length));
        }

        // Test comparison with null
        [TestMethod]
        public void testEquality_NullComparison()
        {
            Weight a = new Weight(1.0, WeightUnit.KILOGRAM);
            Assert.IsFalse(a.Equals(null));
        }

        // Test equality when comparing the same reference
        [TestMethod]
        public void testEquality_SameReference()
        {
            Weight a = new Weight(1.0, WeightUnit.KILOGRAM);
            Assert.IsTrue(a.Equals(a));
        }

        // Test invalid unit handling
        [TestMethod]
        public void testEquality_NullUnit()
        {
            try
            {
                Weight a = new Weight(1.0, (WeightUnit)(-1));
                Assert.Fail("Expected ArgumentException");
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(true);
            }
        }

        // Test transitive property of equality
        [TestMethod]
        public void testEquality_TransitiveProperty()
        {
            Weight a = new Weight(1.0, WeightUnit.KILOGRAM);
            Weight b = new Weight(1000.0, WeightUnit.GRAM);
            Weight c = new Weight(1.0, WeightUnit.KILOGRAM);

            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(b.Equals(c));
            Assert.IsTrue(a.Equals(c));
        }

        // Test equality when both weights are zero
        [TestMethod]
        public void testEquality_ZeroValue()
        {
            Weight a = new Weight(0.0, WeightUnit.KILOGRAM);
            Weight b = new Weight(0.0, WeightUnit.GRAM);
            Assert.IsTrue(a.Equals(b));
        }

        // Test equality with negative weight values
        [TestMethod]
        public void testEquality_NegativeWeight()
        {
            Weight a = new Weight(-1.0, WeightUnit.KILOGRAM);
            Weight b = new Weight(-1000.0, WeightUnit.GRAM);
            Assert.IsTrue(a.Equals(b));
        }

        // Test equality with large values
        [TestMethod]
        public void testEquality_LargeWeightValue()
        {
            Weight a = new Weight(1000000.0, WeightUnit.GRAM);
            Weight b = new Weight(1000.0, WeightUnit.KILOGRAM);
            Assert.IsTrue(a.Equals(b));
        }

        // Test equality with small values
        [TestMethod]
        public void testEquality_SmallWeightValue()
        {
            Weight a = new Weight(0.001, WeightUnit.KILOGRAM);
            Weight b = new Weight(1.0, WeightUnit.GRAM);
            Assert.IsTrue(a.Equals(b));
        }

        // Test conversion from pound to kilogram
        [TestMethod]
        public void testConversion_PoundToKilogram()
        {
            Weight pound = new Weight(2.20462, WeightUnit.POUND);
            Weight result = pound.ConvertTo(WeightUnit.KILOGRAM);
            Weight expected = new Weight(1.0, WeightUnit.KILOGRAM);

            Assert.IsTrue(result.Equals(expected));
        }

        // Test conversion from kilogram to pound
        [TestMethod]
        public void testConversion_KilogramToPound()
        {
            Weight kg = new Weight(1.0, WeightUnit.KILOGRAM);
            Weight result = kg.ConvertTo(WeightUnit.POUND);
            Weight expected = new Weight(2.20462, WeightUnit.POUND);

            Assert.IsTrue(result.Equals(expected));
        }

        // Test conversion when unit is the same
        [TestMethod]
        public void testConversion_SameUnit()
        {
            Weight a = new Weight(5.0, WeightUnit.KILOGRAM);
            Weight result = a.ConvertTo(WeightUnit.KILOGRAM);

            Assert.IsTrue(a.Equals(result));
        }

        // Test conversion of zero value
        [TestMethod]
        public void testConversion_ZeroValue()
        {
            Weight a = new Weight(0.0, WeightUnit.KILOGRAM);
            Weight result = a.ConvertTo(WeightUnit.GRAM);
            Weight expected = new Weight(0.0, WeightUnit.GRAM);

            Assert.IsTrue(result.Equals(expected));
        }

        // Test conversion with negative value
        [TestMethod]
        public void testConversion_NegativeValue()
        {
            Weight a = new Weight(-1.0, WeightUnit.KILOGRAM);
            Weight result = a.ConvertTo(WeightUnit.GRAM);
            Weight expected = new Weight(-1000.0, WeightUnit.GRAM);

            Assert.IsTrue(result.Equals(expected));
        }

        // Test round-trip conversion (kg → g → kg)
        [TestMethod]
        public void testConversion_RoundTrip()
        {
            Weight a = new Weight(1.5, WeightUnit.KILOGRAM);
            Weight grams = a.ConvertTo(WeightUnit.GRAM);
            Weight back = grams.ConvertTo(WeightUnit.KILOGRAM);

            Assert.IsTrue(a.Equals(back));
        }

        // Test addition when both weights are kilograms
        [TestMethod]
        public void testAddition_SameUnit_KilogramPlusKilogram()
        {
            Weight a = new Weight(1.0, WeightUnit.KILOGRAM);
            Weight b = new Weight(2.0, WeightUnit.KILOGRAM);

            Weight result = a.Add(b);
            Weight expected = new Weight(3.0, WeightUnit.KILOGRAM);

            Assert.IsTrue(result.Equals(expected));
        }

        // Test addition across different units
        [TestMethod]
        public void testAddition_CrossUnit_KilogramPlusGram()
        {
            Weight a = new Weight(1.0, WeightUnit.KILOGRAM);
            Weight b = new Weight(1000.0, WeightUnit.GRAM);

            Weight result = a.Add(b);
            Weight expected = new Weight(2.0, WeightUnit.KILOGRAM);

            Assert.IsTrue(result.Equals(expected));
        }

        // Test addition of pound and kilogram
        [TestMethod]
        public void testAddition_CrossUnit_PoundPlusKilogram()
        {
            Weight pound = new Weight(2.20462, WeightUnit.POUND);
            Weight kg = new Weight(1.0, WeightUnit.KILOGRAM);

            Weight result = pound.Add(kg);
            Weight expected = new Weight(4.40924, WeightUnit.POUND);

            Assert.IsTrue(result.Equals(expected));
        }
    }
}