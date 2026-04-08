using System.ComponentModel.DataAnnotations;
///////////CORRRRECT File not change

namespace QuantityMeasurementAppModels.DTOs
{
    public class ConvertRequestDTOs
    {
        [Required(ErrorMessage = "ThisQuantityDTO is required")]
        public QuantityDTOs ThisQuantityDTO { get; set; }

        [Required(ErrorMessage = "TargetUnitDTOs is required")]
        public string TargetUnitDTOs { get; set; }
    }
}
//shi hai file no change