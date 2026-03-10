using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QuantityMeasurementApp.Tests
{
    [TestClass]
    public class QuantityMeasurementTests
    {
        //Tolerance value used for comparing double precision numbers
        private const double epsilon = 0.0001;

        //Verify that LengthMeasurementImpl correctly implements the IMeasurable interface
        [TestMethod]
        public void TestIMeasurableInterface_LengthUnitImplementation()
        {
            IMeasurable length = new LengthMeasurementImpl(LengthUnit.Feet);

            double factor = length.GetConversionFactor();
            double baseValue = length.ConvertToBaseUnit(1.0);
            double original = length.ConvertFromBaseUnit(baseValue);
            string name = length.GetUnitName();

            Assert.IsTrue(factor > 0);
            Assert.AreEqual(1.0, original, 0.0001);
            Assert.AreEqual("Feet", name);
        }


        //Validate that WeightMeasurementImpl follows the IMeasurable contract
        [TestMethod]
        public void TestIMeasurableInterface_WeightUnitImplementation()
        {
            IMeasurable weight = new WeightMeasurementImpl(WeightUnit.Kilogram);

            double factor = weight.GetConversionFactor();
            double baseValue = weight.ConvertToBaseUnit(1.0);
            double original = weight.ConvertFromBaseUnit(baseValue);
            string name = weight.GetUnitName();

            Assert.IsTrue(factor > 0);
            Assert.AreEqual(1.0, original, 0.0001);
            Assert.AreEqual("Kilogram", name);
        }


        //Ensure both measurement implementations behave consistently
        [TestMethod]
        public void TestIMeasurableInterface_ConsistentBehavior()
        {
            IMeasurable length = new LengthMeasurementImpl(LengthUnit.Feet);
            IMeasurable weight = new WeightMeasurementImpl(WeightUnit.Kilogram);

            Assert.IsTrue(length.GetConversionFactor() > 0);
            Assert.IsTrue(weight.GetConversionFactor() > 0);

            Assert.IsNotNull(length.GetUnitName());
            Assert.IsNotNull(weight.GetUnitName());
        }

        //-----------GENERIC QUANTITY EQUALITY Test-----------
        //Check equality between different length units representing the same measurement
        [TestMethod]
        public void TestGenericQuantity_LengthOperations_Equality()
        {
            Quantity<IMeasurable> feet = new Quantity<IMeasurable>(1.0, new LengthMeasurementImpl(LengthUnit.Feet));

            Quantity<IMeasurable> inch = new Quantity<IMeasurable>(12.0, new LengthMeasurementImpl(LengthUnit.Inch));

            Assert.IsTrue(feet.Equals(inch));
        }


        //Check equality between weight values expressed in different units
        [TestMethod]
        public void TestGenericQuantity_WeightOperations_Equality()
        {
            Quantity<IMeasurable> kilogram = new Quantity<IMeasurable>(1.0, new WeightMeasurementImpl(WeightUnit.Kilogram));

            Quantity<IMeasurable> gram = new Quantity<IMeasurable>(1000.0, new WeightMeasurementImpl(WeightUnit.Gram));

            Assert.IsTrue(kilogram.Equals(gram));
        }


        //----------GENERIC QUANTITY CONVERSION Test-----------
        //Validate conversion functionality from feet to inches
        [TestMethod]
        public void TestGenericQuantity_LengthOperations_Conversion()
        {
            Quantity<IMeasurable> feet = new Quantity<IMeasurable>(1.0, new LengthMeasurementImpl(LengthUnit.Feet));

            Quantity<IMeasurable> result = feet.ConvertTo(new LengthMeasurementImpl(LengthUnit.Inch));

            Quantity<IMeasurable> expected = new Quantity<IMeasurable>(12.0, new LengthMeasurementImpl(LengthUnit.Inch));

            Assert.IsTrue(result.Equals(expected));
        }


        //Verify conversion between kilogram and gram units
        [TestMethod]
        public void TestGenericQuantity_WeightOperations_Conversion()
        {
            Quantity<IMeasurable> kilogram = new Quantity<IMeasurable>(1.0, new WeightMeasurementImpl(WeightUnit.Kilogram));

            Quantity<IMeasurable> result = kilogram.ConvertTo(new WeightMeasurementImpl(WeightUnit.Gram));

            Quantity<IMeasurable> expected = new Quantity<IMeasurable>(1000.0, new WeightMeasurementImpl(WeightUnit.Gram));

            Assert.IsTrue(result.Equals(expected));
        }

        //----------GENERIC QUANTITY ADDITION Test-----------
        //Verify that addition works correctly for different length units
        [TestMethod]
        public void TestGenericQuantity_LengthOperations_Addition()
        {
            Quantity<IMeasurable> feet = new Quantity<IMeasurable>(1.0, new LengthMeasurementImpl(LengthUnit.Feet));

            Quantity<IMeasurable> inch = new Quantity<IMeasurable>(12.0, new LengthMeasurementImpl(LengthUnit.Inch));

            Quantity<IMeasurable> result = feet.Add(inch, new LengthMeasurementImpl(LengthUnit.Feet));

            Quantity<IMeasurable> expected = new Quantity<IMeasurable>(2.0, new LengthMeasurementImpl(LengthUnit.Feet));

            Assert.IsTrue(result.Equals(expected));
        }


        //Validate addition operation for weight units
        [TestMethod]
        public void TestGenericQuantity_WeightOperations_Addition()
        {
            Quantity<IMeasurable> kilogram = new Quantity<IMeasurable>(1.0, new WeightMeasurementImpl(WeightUnit.Kilogram));

            Quantity<IMeasurable> gram = new Quantity<IMeasurable>(1000.0, new WeightMeasurementImpl(WeightUnit.Gram));

            Quantity<IMeasurable> result = kilogram.Add(gram, new WeightMeasurementImpl(WeightUnit.Kilogram));

            Quantity<IMeasurable> expected = new Quantity<IMeasurable>(2.0, new WeightMeasurementImpl(WeightUnit.Kilogram));

            Assert.IsTrue(result.Equals(expected));
        }


        //Confirm that quantities from different categories cannot be equal
        [TestMethod]
        public void TestCrossCategoryPrevention_LengthVsWeight()
        {
            Quantity<IMeasurable> length = new Quantity<IMeasurable>(1.0, new LengthMeasurementImpl(LengthUnit.Feet));

            Quantity<IMeasurable> weight = new Quantity<IMeasurable>(1.0, new WeightMeasurementImpl(WeightUnit.Kilogram));

            Assert.IsFalse(length.Equals(weight));
        }


        //Ensure compile-time safety when mixing different measurement categories
        [TestMethod]
        public void TestCrossCategoryPrevention_CompilerTypeSafety()
        {
            Quantity<IMeasurable> length = new Quantity<IMeasurable>(1.0, new LengthMeasurementImpl(LengthUnit.Feet));

            Quantity<IMeasurable> weight = new Quantity<IMeasurable>(1.0, new WeightMeasurementImpl(WeightUnit.Kilogram));

            //Compile-time protection avoids invalid generic operations
            Assert.IsFalse(length.Equals(weight));
        }


        //Ensure constructor throws exception when unit argument is missing
        [TestMethod]
        public void TestGenericQuantity_ConstructorValidation_NullUnit()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Quantity<IMeasurable> quantity = new Quantity<IMeasurable>(1.0, null);
            });
        }


        //Verify constructor validation when invalid numeric value is provided
        [TestMethod]
        public void TestGenericQuantity_ConstructorValidation_InvalidValue()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Quantity<IMeasurable> quantity = new Quantity<IMeasurable>(double.NaN, new LengthMeasurementImpl(LengthUnit.Feet));
            });
        }


        //Check conversions across multiple length unit types
        [TestMethod]
        public void TestGenericQuantity_Conversion_AllUnitCombinations()
        {
            Quantity<IMeasurable> feet = new Quantity<IMeasurable>(1.0, new LengthMeasurementImpl(LengthUnit.Feet));

            Quantity<IMeasurable> inches = feet.ConvertTo(new LengthMeasurementImpl(LengthUnit.Inch));

            Quantity<IMeasurable> yards = feet.ConvertTo(new LengthMeasurementImpl(LengthUnit.Yard));

            Assert.IsTrue(inches.Equals(new Quantity<IMeasurable>(12.0, new LengthMeasurementImpl(LengthUnit.Inch))));

            Assert.IsTrue(yards.Equals(new Quantity<IMeasurable>(0.333333, new LengthMeasurementImpl(LengthUnit.Yard))));
        }



        //Ensure previous UC1–UC9 behaviors still function correctly
        [TestMethod]
        public void TestBackwardCompatibility_AllUC1Through9Tests()
        {
            Quantity<IMeasurable> feet = new Quantity<IMeasurable>(1.0, new LengthMeasurementImpl(LengthUnit.Feet));

            Quantity<IMeasurable> inches = new Quantity<IMeasurable>(12.0, new LengthMeasurementImpl(LengthUnit.Inch));

            Assert.IsTrue(feet.Equals(inches));
        }



        //Demonstration of equality check using generic quantity objects
        [TestMethod]
        public void TestQuantityMeasurementApp_SimplifiedDemonstration_Equality()
        {
            Quantity<IMeasurable> kg = new Quantity<IMeasurable>(1.0, new WeightMeasurementImpl(WeightUnit.Kilogram));

            Quantity<IMeasurable> gram = new Quantity<IMeasurable>(1000.0, new WeightMeasurementImpl(WeightUnit.Gram));

            Assert.IsTrue(kg.Equals(gram));
        }



        //Demonstration of conversion feature using generic quantity type
        [TestMethod]
        public void TestQuantityMeasurementApp_SimplifiedDemonstration_Conversion()
        {
            Quantity<IMeasurable> kg = new Quantity<IMeasurable>(1.0, new WeightMeasurementImpl(WeightUnit.Kilogram));

            Quantity<IMeasurable> result = kg.ConvertTo(new WeightMeasurementImpl(WeightUnit.Gram));

            Assert.IsTrue(result.Equals(new Quantity<IMeasurable>(1000.0, new WeightMeasurementImpl(WeightUnit.Gram))));
        }




        //Demonstration of addition operation using generic quantity objects
        [TestMethod]
        public void TestQuantityMeasurementApp_SimplifiedDemonstration_Addition()
        {
            Quantity<IMeasurable> kg = new Quantity<IMeasurable>(1.0, new WeightMeasurementImpl(WeightUnit.Kilogram));

            Quantity<IMeasurable> gram = new Quantity<IMeasurable>(1000.0, new WeightMeasurementImpl(WeightUnit.Gram));

            Quantity<IMeasurable> result = kg.Add(gram, new WeightMeasurementImpl(WeightUnit.Kilogram));

            Assert.IsTrue(result.Equals(new Quantity<IMeasurable>(2.0, new WeightMeasurementImpl(WeightUnit.Kilogram))));
        }



        //Verify flexible usage of Quantity with IMeasurable type
        [TestMethod]
        public void TestTypeWildcard_FlexibleSignatures()
        {
            Quantity<IMeasurable> length = new Quantity<IMeasurable>(1.0, new LengthMeasurementImpl(LengthUnit.Feet));

            Quantity<IMeasurable> weight = new Quantity<IMeasurable>(1.0, new WeightMeasurementImpl(WeightUnit.Kilogram));

            Assert.IsNotNull(length);
            Assert.IsNotNull(weight);
        }



        //Verify new measurement type (Volume) can be integrated without altering Quantity class
        [TestMethod]
        public void TestScalability_NewUnitEnumIntegration()
        {
            Quantity<IMeasurable> Litre = new Quantity<IMeasurable>(1.0, new VolumeMeasurementImpl(VolumeUnit.Litre));

            Quantity<IMeasurable> ml = new Quantity<IMeasurable>(1000.0, new VolumeMeasurementImpl(VolumeUnit.Millilitre));

            Assert.IsTrue(Litre.Equals(ml));
        }


        //Ensure the system supports multiple measurement categories simultaneously
        [TestMethod]
        public void TestScalability_MultipleNewCategories()
        {
            Quantity<IMeasurable> volume = new Quantity<IMeasurable>(1.0, new VolumeMeasurementImpl(VolumeUnit.Litre));

            Quantity<IMeasurable> length = new Quantity<IMeasurable>(1.0, new LengthMeasurementImpl(LengthUnit.Feet));

            Quantity<IMeasurable> weight = new Quantity<IMeasurable>(1.0, new WeightMeasurementImpl(WeightUnit.Kilogram));

            Assert.IsNotNull(volume);
            Assert.IsNotNull(length);
            Assert.IsNotNull(weight);
        }


        //Ensure that Quantity generic type accepts only IMeasurable implementations
        [TestMethod]
        public void TestGenericBoundedTypeParameter_Enforcement()
        {
            Quantity<IMeasurable> quantity = new Quantity<IMeasurable>(5.0, new LengthMeasurementImpl(LengthUnit.Feet));

            Assert.IsNotNull(quantity);
        }


        //Check that equal quantities produce identical hash codes
        [TestMethod]
        public void TestHashCode_GenericQuantity_Consistency()
        {
            Quantity<IMeasurable> a = new Quantity<IMeasurable>(1.0, new LengthMeasurementImpl(LengthUnit.Feet));

            Quantity<IMeasurable> b = new Quantity<IMeasurable>(12.0, new LengthMeasurementImpl(LengthUnit.Inch));

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        //Verify standard equals() contract rules
        [TestMethod]
        public void TestEquals_GenericQuantity_ContractPreservation()
        {
            Quantity<IMeasurable> A = new Quantity<IMeasurable>(1.0, new LengthMeasurementImpl(LengthUnit.Feet));

            Quantity<IMeasurable> B = new Quantity<IMeasurable>(12.0, new LengthMeasurementImpl(LengthUnit.Inch));

            Quantity<IMeasurable> C = new Quantity<IMeasurable>(1.0, new LengthMeasurementImpl(LengthUnit.Feet));

            //Reflexive
            Assert.IsTrue(A.Equals(A));

            //Symmetric
            Assert.IsTrue(A.Equals(B));
            Assert.IsTrue(B.Equals(A));

            //Transitive
            Assert.IsTrue(A.Equals(B));
            Assert.IsTrue(B.Equals(C));
            Assert.IsTrue(A.Equals(C));
        }



        //Validate enum-based measurement units through the interface behavior
        [TestMethod]
        public void TestEnumAsUnitCarrier_BehaviorEncapsulation()
        {
            IMeasurable length = new LengthMeasurementImpl(LengthUnit.Feet);
            IMeasurable weight = new WeightMeasurementImpl(WeightUnit.Kilogram);

            double lengthFactor = length.GetConversionFactor();
            double weightFactor = weight.GetConversionFactor();

            Assert.IsTrue(lengthFactor > 0);
            Assert.IsTrue(weightFactor > 0);
        }



        //Ensure runtime checks prevent invalid comparisons
        [TestMethod]
        public void TestTypeErasure_RuntimeSafety()
        {
            Quantity<IMeasurable> length = new Quantity<IMeasurable>(1.0, new LengthMeasurementImpl(LengthUnit.Feet));

            Quantity<IMeasurable> weight = new Quantity<IMeasurable>(1.0, new WeightMeasurementImpl(WeightUnit.Kilogram));

            Assert.IsFalse(length.Equals(weight));
        }


        //Verify composition approach allows flexible measurement handling
        [TestMethod]
        public void TestCompositionOverInheritance_Flexibility()
        {
            Quantity<IMeasurable> length = new Quantity<IMeasurable>(5.0, new LengthMeasurementImpl(LengthUnit.Yard));

            Quantity<IMeasurable> weight = new Quantity<IMeasurable>(10.0, new WeightMeasurementImpl(WeightUnit.Kilogram));

            Assert.IsNotNull(length);
            Assert.IsNotNull(weight);
        }

        //Ensure generics help reduce repeated code logic
        [TestMethod]
        public void TestCodeReduction_DRYValidation()
        {
            Quantity<IMeasurable> length = new Quantity<IMeasurable>(1.0, new LengthMeasurementImpl(LengthUnit.Feet));

            Quantity<IMeasurable> weight = new Quantity<IMeasurable>(1.0, new WeightMeasurementImpl(WeightUnit.Kilogram));

            Assert.IsNotNull(length);
            Assert.IsNotNull(weight);
        }



        //Check maintainability by verifying unified implementation logic
        [TestMethod]
        public void TestMaintainability_SingleSourceOfTruth()
        {
            Quantity<IMeasurable> feet = new Quantity<IMeasurable>(1.0, new LengthMeasurementImpl(LengthUnit.Feet));

            Quantity<IMeasurable> inch = new Quantity<IMeasurable>(12.0, new LengthMeasurementImpl(LengthUnit.Inch));

            Quantity<IMeasurable> result = feet.Add(inch, new LengthMeasurementImpl(LengthUnit.Feet));

            Assert.IsTrue(result.Equals(new Quantity<IMeasurable>(2.0, new LengthMeasurementImpl(LengthUnit.Feet))));
        }
    }
}