using System;
using System.ComponentModel.DataAnnotations;
using QuantityMeasurementAppModels.DTOs;
using QuantityMeasurementAppModels.Enums;

namespace QuantityMeasurementAppModels.Validation
{
    public class UnitValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            QuantityDTOs Quantitydto = value as QuantityDTOs;

            if (Quantitydto == null)
            {
                return ValidationResult.Success;
            }
            if (Quantitydto.MeasurementTypeDTOs == null || Quantitydto.UnitNameDTOs == null)
            {
                return ValidationResult.Success;
            }

            string QuantityDtotype = Quantitydto.MeasurementTypeDTOs.ToLower();

            string QuantityDtounit = Quantitydto.UnitNameDTOs;

            bool isValidOperation = false;

            if (QuantityDtotype == "length")
            {
                isValidOperation = Enum.IsDefined(typeof(ConversionUnit), QuantityDtounit);
            }
            else if (QuantityDtotype == "weight")
            {
                isValidOperation = Enum.IsDefined(typeof(MassUnit), QuantityDtounit);
            }
            else if (QuantityDtotype == "volume")
            {
                isValidOperation = Enum.IsDefined(typeof(VolumeUnitType), QuantityDtounit);
            }
            else if (QuantityDtotype == "temperature")
            {
                isValidOperation = Enum.IsDefined(typeof(ThermalTemperatureUnit), QuantityDtounit);
            }
            if (!isValidOperation)
            {
                return new ValidationResult("Invalid given unit for the given measurement type");
            }

            return ValidationResult.Success;
        }
    }
}