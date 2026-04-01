using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Services;
using QuantityMeasurementAppModels.DTOs;
using QuantityMeasurementAppRepositories.Repositories;
using QuantityMeasurementAppRepositories.Context;
using Microsoft.EntityFrameworkCore;

namespace QuantityMeasurementAppTests
{
    [TestClass]
    public class QuantityServiceTests
    {
        private QuantityImplService service;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DatabaseAppContext>()
                .UseInMemoryDatabase(databaseName: "TestDB")
                .Options;

            var context = new DatabaseAppContext(options);
            var repo = new QuantityRepository(context);

            service = new QuantityImplService(repo);
        }
        [TestMethod]
        public void GivenTwoLengths_WhenAdded_ShouldReturnCorrectResult()
        {
            var result = service.Combine(new QuantityDTOs(1, "Feet", "Length"), new QuantityDTOs(12, "Inch", "Length"), "Feet");
            Assert.AreEqual(2, result.ValueDTOs, 0.01);
        }
        public void GivenTwoLengths_WhenCompared_ShouldReturnTrue()
        {
            var result = service.Comparison(
            new QuantityDTOs(1, "Feet", "Length"),
            new QuantityDTOs(12, "Inch", "Length"));

            Assert.IsTrue(result);
        }
        [TestMethod]
        public void GivenTwoWeights_WhenAdded_ShouldReturnCorrectResult()
        {
            var result = service.Combine(
                new QuantityDTOs(1, "Kilogram", "Weight"),
                new QuantityDTOs(1000, "Gram", "Weight"),
                "Kilogram");

            Assert.AreEqual(2, result.ValueDTOs, 0.01);
        }
        [TestMethod]
        public void GivenTwoVolumes_WhenAdded_ShouldReturnCorrectResult()
        {
            var result = service.Combine(
                new QuantityDTOs(1, "Litre", "Volume"),
                new QuantityDTOs(1000, "Millilitre", "Volume"),
                "Litre");

            Assert.AreEqual(2, result.ValueDTOs, 0.01);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GivenTemperature_WhenArithmeticPerformed_ShouldThrowException()
        {
            service.Combine(
                new QuantityDTOs(100, "Celsius", "Temperature"),
                new QuantityDTOs(50, "Celsius", "Temperature"),
                "Celsius");
        }
        [TestMethod]
        public void GivenLength_WhenConverted_ShouldReturnCorrectValue()
        {
            var result = service.Conversion(
                new QuantityDTOs(1, "Feet", "Length"),
                "Inch");

            Assert.AreEqual(12, result.ValueDTOs, 0.01);
        }
        [TestMethod]
        public void GivenTwoLengths_WhenDivided_ShouldReturnCorrectResult()
        {
            var result = service.Divison(
                new QuantityDTOs(10, "Feet", "Length"),
                new QuantityDTOs(5, "Feet", "Length"));

            Assert.AreEqual(2, result);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GivenInvalidUnit_WhenConversion_ShouldThrowException()
        {
            service.Conversion(
                new QuantityDTOs(10, "InvalidUnit", "Length"),
                "Feet");
        }
        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void GivenZero_WhenDivided_ShouldThrowException()
        {
            service.Divison(
                new QuantityDTOs(10, "Feet", "Length"),
                new QuantityDTOs(0, "Feet", "Length"));
        }
        [TestMethod]
        public void GivenTwoLengths_WhenSubtracted_ShouldReturnCorrectResult()
        {
            var result = service.Difference(
                new QuantityDTOs(5, "Feet", "Length"),
                new QuantityDTOs(2, "Feet", "Length"),
                "Feet");

            Assert.AreEqual(3, result.ValueDTOs, 0.01);
        }
    }
}