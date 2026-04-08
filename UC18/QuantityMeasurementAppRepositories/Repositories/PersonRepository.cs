using QuantityMeasurementAppModels.Entities;
using QuantityMeasurementAppRepositories.Interfaces;
using QuantityMeasurementAppRepositories.Context;

namespace QuantityMeasurementAppRepositories.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly DatabaseAppContext dbContext;

        public PersonRepository(DatabaseAppContext dataContext)
        {
            this.dbContext = dataContext;
        }
        public PersonEntity? SearchUserByEmail(string userEmail)
        {
            return dbContext.UsersEntity.FirstOrDefault(u => u.EntityEmail == userEmail);
        }
        public PersonEntity? SearchUserById(long userId)
        {
            return dbContext.UsersEntity.FirstOrDefault(u => u.EntityId == userId);
        }
        public PersonEntity SaveUserRecords(PersonEntity userEntity)
        {
            dbContext.UsersEntity.Add(userEntity);

            dbContext.SaveChanges();

            return userEntity;
        }
        public void UpdateUser(PersonEntity userEntity)
        {
            dbContext.UsersEntity.Update(userEntity);
            dbContext.SaveChanges();
        }
    }
}

