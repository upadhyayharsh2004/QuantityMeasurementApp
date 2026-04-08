using System.ComponentModel.DataAnnotations;

namespace QuantityMeasurementAppModels.DTOs
{
    ///////CORRRRECT File not change
    public class QuantityInputRequestDTOs
    {
        [Required(ErrorMessage = "ThisQuantityDTO is required")]
        public QuantityDTOs ThisQuantityDTO { get; set; }

        [Required(ErrorMessage = "ThereQuantityDTO is required")]
        public QuantityDTOs ThereQuantityDTO { get; set; }
    }
}
//shi hai file no change