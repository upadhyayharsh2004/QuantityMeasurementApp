using Microsoft.AspNetCore.Identity;
using QuantityMeasurementAppModels.DTOs;
using QuantityMeasurementAppModels.Entities;
using QuantityMeasurementAppRepositories.Interfaces;
using QuantityMeasurementAppServices.Interfaces;

namespace QuantityMeasurementAppServices.Services
{
    public class AuthenticationService : IAuthService
    {
        private readonly IPersonRepository personRepository;

        // jwt service
        private readonly JsonWebTokenService jsonWebTokenService;

        // Constructor
        public AuthenticationService(IPersonRepository personJwtRepository, JsonWebTokenService userJwtService)
        {
            this.personRepository = personJwtRepository;
            this.jsonWebTokenService = userJwtService;
        }
        public AuthenticationResponse SignUp(RegisterRequestService requestUserService)
        {
            PersonEntity existing = personRepository.SearchUserByEmail(requestUserService.UserEmail);
            if (existing != null)
            {
                throw new InvalidOperationException("With This Email Id User Has Been Already Registered Directly Log in Through Email Id.");
            }
            string UserPasswordHash = BCrypt.Net.BCrypt.HashPassword(requestUserService.UserPassword);

            // Build user entity
            PersonEntity newAuthUser = new PersonEntity
            {
                EntityName = requestUserService.UserName,
                EntityEmail = requestUserService.UserEmail,
                EntityHashPassword = UserPasswordHash,
                EntityCreatedAt = DateTime.UtcNow,
                EntityLastActiveAt = DateTime.UtcNow
            };

            personRepository.SaveUserRecords(newAuthUser);
            // reload from DB
            var savedUser = personRepository.SearchUserByEmail(newAuthUser.EntityEmail);
            string token = jsonWebTokenService.GenerateToken(savedUser);
            return CreateUsersResponse(savedUser, token);
        }

        // Method to Login existing user
        public AuthenticationResponse SignIn(LoginRequestService requestLoginService)
        {
            PersonEntity userLogin = personRepository.SearchUserByEmail(requestLoginService.LoginEmail);
            if (userLogin == null)
            {
                throw new UnauthorizedAccessException("Unmatched email or password.Pls try again");
            }
            bool passwordMatch = BCrypt.Net.BCrypt.Verify(requestLoginService.LoginPassword, userLogin.EntityHashPassword);
            if (!passwordMatch)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }
            userLogin.EntityLastActiveAt = DateTime.UtcNow;
            personRepository.UpdateUser(userLogin);

            // Issue token and return
            string token = jsonWebTokenService.GenerateToken(userLogin);
            return CreateUsersResponse(userLogin, token);
        }
        private AuthenticationResponse CreateUsersResponse(PersonEntity userEntity, string tokenEntity)
        {
            return new AuthenticationResponse
            {
                AuthorizationId = tokenEntity,
                AuthorizationType = "Bearer",
                ValidIn = jsonWebTokenService.GetExpirySeconds(),
                PersonId = userEntity.EntityId,
                PersonEmail = userEntity.EntityEmail,
                PersonName = userEntity.EntityName,
                GrantedAt = DateTime.UtcNow.ToString("o")
            };
        }
    }
}