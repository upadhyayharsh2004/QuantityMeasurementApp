using QuantityMeasurementAppModels.Validation;
///////CORRRRECT File not change
namespace QuantityMeasurementAppModels.DTOs
{
    [UnitValidation]
    public class QuantityDTOs
    {
        public double ValueDTOs { get; set; }
        public string UnitNameDTOs { get; set; }
        public string MeasurementTypeDTOs { get; set; }

        // Constructor
        public QuantityDTOs(double value, string unitName, string measurementType)
        {
            ValueDTOs = value;
            UnitNameDTOs = unitName;
            MeasurementTypeDTOs = measurementType;
        }

        public QuantityDTOs() { }

        // Override To String method
        public override string ToString()
        {
            return ValueDTOs + " " + UnitNameDTOs;
        }
    }
}
//shi hai file no change