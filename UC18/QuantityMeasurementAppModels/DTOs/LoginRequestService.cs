using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuantityMeasurementAppModels.DTOs
{
    public class LoginRequestService
    {
        [Required(ErrorMessage = "User Email is required")]
        [EmailAddress(ErrorMessage = "User Email is in invalid email format")]
        public string LoginEmail { get; set; } = string.Empty;
 
        [Required(ErrorMessage = "User Login Password is required")]
        public string LoginPassword { get; set; } = string.Empty;
    }
}