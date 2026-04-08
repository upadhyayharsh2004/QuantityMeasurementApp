using QuantityMeasurementAppModels.Entities;

namespace QuantityMeasurementAppRepositories.Interfaces
{
    public interface IPersonRepository
    {
        PersonEntity? SearchUserByEmail(string userEmail);
        PersonEntity? SearchUserById(long userId);

        //user=userEntity
        PersonEntity SaveUserRecords(PersonEntity userEntity);

        //user=updateUserData
        void UpdateUser(PersonEntity updateUserData);
    }
}