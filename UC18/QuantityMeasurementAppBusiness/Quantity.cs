using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuantityMeasurementAppBusiness.Interfaces;
using QuantityMeasurementAppModels.Enums;

namespace QuantityMeasurementAppBusiness
{
    public class Quantity<U> where U : IMeasurable
    {
        private readonly double updatedValue;
        private readonly U updatedUnitValue;
        private const double epsilonValue = 0.0001;
        private void ValidateArithmeticOperands(Quantity<U> secondValue, U updatedTargetUnit, bool updatedTargetRequired)
        {
            if (secondValue == null)
            {
                throw new ArgumentException("Second Value For Converison of Quantity Cannot be null");
            }
            if (updatedUnitValue.GetType() != secondValue.updatedUnitValue.GetType())
            {
                throw new ArgumentException("Cannot Perform Arithemtic Operation on Different-Different Units");
            }
            if (double.IsNaN(updatedValue) || double.IsInfinity(updatedValue) ||double.IsNaN(secondValue.updatedValue) || double.IsInfinity(secondValue.updatedValue))
            {
                throw new ArgumentException("Invalid Integer or Numeric Value Passed For Arithmetic Operations");
            }
            if (updatedTargetRequired && updatedTargetUnit == null)
            {
                throw new ArgumentException("Invalid Target Unit Passed For Checking The Result After Arithmetic Operations");
            }
        }
        public Quantity<U> ChangeTo(U updatedTargetUnit)
        {
            if (updatedTargetUnit == null)
            {
                throw new ArgumentException("Invalid Updated Target Unit Value Passed For Conversion");
            }
            double updatedBaseValue = updatedUnitValue.NormalizeToBaseUnit(this.updatedValue);

            double updatedConvertedValue = updatedTargetUnit.NormalizeFromBaseUnit(updatedBaseValue);

            return new Quantity<U>(updatedConvertedValue, updatedTargetUnit);
        }
        private double PerformBaseArithmetic(Quantity<U> secondValue, DoubleOperandOperation checkingOperation)
        {
            double firstArithmeticValue = updatedUnitValue.NormalizeToBaseUnit(this.updatedValue);

            double secondArithmeticValue = secondValue.updatedUnitValue.NormalizeToBaseUnit(secondValue.updatedValue);

            if (checkingOperation == DoubleOperandOperation.ADD)
            {
                return firstArithmeticValue + secondArithmeticValue;
            }
            else if (checkingOperation == DoubleOperandOperation.SUBTRACT)
            {
                return firstArithmeticValue - secondArithmeticValue;
            }
            else if (checkingOperation == DoubleOperandOperation.DIVIDE)
            {
                return firstArithmeticValue / secondArithmeticValue;
            }

            throw new ArgumentException("Invalid Performed Based Operation as a Arithmetic operation");
        }
        public double Divide(Quantity<U> second)
        {
            updatedUnitValue.ValidateOperationSupport("Division");

            ValidateArithmeticOperands(second, this.updatedUnitValue, false);

            if (second.updatedValue == 0)
            {
                throw new DivideByZeroException("Divison By Zerp Quantity Cannot Be Allowed Throws Exception");
            }

            return PerformBaseArithmetic(second, DoubleOperandOperation.DIVIDE);
        }
        //change
        public Quantity<U> Add(Quantity<U> secondValue)
        {
            updatedUnitValue.ValidateOperationSupport("Addition");

            ValidateArithmeticOperands(secondValue, this.updatedUnitValue, true);

            double basePerformedResult = PerformBaseArithmetic(secondValue, DoubleOperandOperation.ADD);

            double basedResultValue = updatedUnitValue.NormalizeFromBaseUnit(basePerformedResult);

            return new Quantity<U>(basedResultValue, this.updatedUnitValue);
        }

        public Quantity<U> Add(Quantity<U> secondValue, U updatedTargetUnit)
        {
            updatedUnitValue.ValidateOperationSupport("Addition");

            ValidateArithmeticOperands(secondValue, updatedTargetUnit, true);

            double basePerformedResult = PerformBaseArithmetic(secondValue, DoubleOperandOperation.ADD);

            double basedResultValue = updatedTargetUnit.NormalizeFromBaseUnit(basePerformedResult);

            return new Quantity<U>(basedResultValue, updatedTargetUnit);
        }

        //no change
        public Quantity<U> Subtract(Quantity<U> secondValue, U updatedTargetUnit)
        {
            updatedUnitValue.ValidateOperationSupport("Subtraction");

            ValidateArithmeticOperands(secondValue, updatedTargetUnit, true);

            double basePerformedResult = PerformBaseArithmetic(secondValue, DoubleOperandOperation.SUBTRACT);

            double basedResultValue = updatedTargetUnit.NormalizeFromBaseUnit(basePerformedResult);

            basedResultValue = Math.Round(basedResultValue, 2);

            return new Quantity<U>(basedResultValue, updatedTargetUnit);
        }
        public override int GetHashCode()
        {
            double updatedBaseValue = updatedUnitValue.NormalizeToBaseUnit(this.updatedValue);

            double rounded = Math.Round(updatedBaseValue / epsilonValue) * epsilonValue;

            return rounded.GetHashCode();
        }
        public override string ToString()
        {
            return $"{updatedValue} {updatedUnitValue.FetchUnitName()}";
        }
        public Quantity(double convertedvalue, U convertedunit)
        {
            if (convertedunit == null)
            {
                throw new ArgumentException("Unit Value Used For Conversion cannot be null");
            }

            if (double.IsNaN(convertedvalue) || double.IsInfinity(convertedvalue))
            {
                throw new ArgumentException("Invalid Unit Value Used For Conversion");
            }

            this.updatedValue = convertedvalue;

            this.updatedUnitValue = convertedunit;
        }
        public double GetValue()
        {
            return updatedValue;
        }
        public override bool Equals(object objectMeasure)
        {
            if (this == objectMeasure)
            {
                return true;
            }
            if (objectMeasure == null || objectMeasure.GetType() != this.GetType())
            {
                return false;
            }
            Quantity<U> otherObject = (Quantity<U>)objectMeasure;

            if (updatedUnitValue.GetType() != otherObject.updatedUnitValue.GetType())
            {
                return false;
            }
            double firstBaseValue = updatedUnitValue.NormalizeToBaseUnit(this.updatedValue);

            double secondBaseValue = otherObject.updatedUnitValue.NormalizeToBaseUnit(otherObject.updatedValue);

            return Math.Abs(firstBaseValue - secondBaseValue) <= epsilonValue;
        }
        public Quantity<U> Subtract(Quantity<U> secondValue)
        {
            updatedUnitValue.ValidateOperationSupport("Subtraction");

            ValidateArithmeticOperands(secondValue, this.updatedUnitValue, true);

            double basePerformedResult = PerformBaseArithmetic(secondValue, DoubleOperandOperation.SUBTRACT);

            double basedResultValue = updatedUnitValue.NormalizeFromBaseUnit(basePerformedResult);

            basedResultValue = Math.Round(basedResultValue, 2);

            return new Quantity<U>(basedResultValue, this.updatedUnitValue);
        }
    }
}