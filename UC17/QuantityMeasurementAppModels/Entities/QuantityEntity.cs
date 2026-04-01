using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuantityMeasurementAppModels.Entities
{
    //[Table("quantity_measurements")]  as [Table("quantity_measurements_entity")]
    [Table("quantity_measurements_tables_conversion")]
    public class QuantityEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long EntityId { get; set; }

        [Column("entity_result_value")]
        public double EntityResultValue { get; set; }

        [Column("entity_measurement_type")]
        public string? EntityMeasurementType { get; set; }

        [Column("entity_operation")]
        public string EntityOperation { get; set; }

        [Column("entity_second_unit")]
        public string? EntitySecondUnit { get; set; }

        [Column("entity_created_at")]
        public DateTime EntityCreatedAt { get; set; }

        [Column("entity_updated_at")]
        public DateTime EntityUpdatedAt { get; set; }
        public QuantityEntity()
        {
            EntityCreatedAt = DateTime.UtcNow;
            EntityUpdatedAt = DateTime.UtcNow;
        }
        [Column("entity_is_error")]
        public bool IsEntityError { get; set; }

        [Column("entity_error_message")]
        public string? EntityErrorMessage { get; set; }

        [Column("entity_first_value")]
        public double EntityFirstValue { get; set; }

        [Column("entity_first_unit")]
        public string? EntityFirstUnit { get; set; }

        [Column("entity_second_value")]
        public double EntitySecondValue { get; set; }


        // Using Constructor in Entities for convert method in QuantityMeasurementAppModels for two-operand operation
        public QuantityEntity(
            string UpdatedOperation,
            double UpdatedFirstValue, string UpdatedFirstUnit,
            double UpdatedSecondValue, string UpdatedSecondUnit,
            double UpdatedResultValue,
            string UpdatedMeasurementType)
        {
            EntityOperation = UpdatedOperation;
            EntityFirstValue = UpdatedFirstValue;
            EntityFirstUnit = UpdatedFirstUnit;
            EntitySecondValue = UpdatedSecondValue;
            EntitySecondUnit = UpdatedSecondUnit;
            EntityResultValue = UpdatedResultValue;
            EntityMeasurementType = UpdatedMeasurementType;
            IsEntityError = false;
            EntityCreatedAt = DateTime.UtcNow;
            EntityUpdatedAt = DateTime.UtcNow;
        }

        // Using Constructor in Entities for convert method in QuantityMeasurementAppModels for single operand operation
        public QuantityEntity(
            string Extendedoperation,
            double ExtendedInputValue, string ExtendedInputUnit,
            double ExtendedResultValue,
            string ExtendedMeasurementType)
        {
            EntityOperation = Extendedoperation;
            EntityFirstValue = ExtendedInputValue;
            EntityFirstUnit = ExtendedInputUnit;
            EntityResultValue = ExtendedResultValue;
            EntityMeasurementType = ExtendedMeasurementType;
            IsEntityError = false;
            EntityCreatedAt = DateTime.UtcNow;
            EntityUpdatedAt = DateTime.UtcNow;
        }

        // Using Constructor in Entities for convert method in QuantityMeasurementAppModels for single operand operation
        public QuantityEntity(string AnotherOperation, string NewErrorMessage)
        {
            EntityOperation = AnotherOperation;
            IsEntityError = true;
            EntityErrorMessage = NewErrorMessage;
            EntityCreatedAt = DateTime.UtcNow;
            EntityUpdatedAt = DateTime.UtcNow;
        }

        public override string ToString()
        {
            return IsEntityError
                ? $"{{ Operation: \"{EntityOperation}\", Status: \"Error\", Message: \"{EntityErrorMessage}\" }}"
                : $"{{ Operation: \"{EntityOperation}\", Input: \"{EntityFirstValue} {EntityFirstUnit}\", Result: \"{EntityResultValue}\" }}";
        }
    }
}