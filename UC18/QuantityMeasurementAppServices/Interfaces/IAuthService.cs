using QuantityMeasurementAppModels.DTOs;

namespace QuantityMeasurementAppServices.Interfaces
{
    public interface IAuthService
    {
        AuthenticationResponse SignUp(RegisterRequestService request);
        AuthenticationResponse SignIn(LoginRequestService request);
    }
}