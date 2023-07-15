using CompetitionModel.model;

namespace CompetitionPersistence.template
{
    public interface IUserRepo : IGenericRepo<long, User>
    {
        public User findUserWithNameAndPassword(string username, string password);
    }
}
