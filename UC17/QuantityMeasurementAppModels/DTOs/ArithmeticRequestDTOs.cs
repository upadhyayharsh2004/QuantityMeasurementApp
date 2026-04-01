using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace QuantityMeasurementAppModels.DTOs
{
    public class ArithmeticRequestDTOs
    {
        [Required(ErrorMessage = "ThisQuantityDTO is required")]
        public QuantityDTOs ThisQuantityDTO { get; set; }

        [Required(ErrorMessage = "ThereQuantityDTO is required")]
        public QuantityDTOs ThereQuantityDTO { get; set; }

        [Required(ErrorMessage = "TargetUnitDTOs is required")]
        public string TargetUnitDTOs { get; set; }
    }
}