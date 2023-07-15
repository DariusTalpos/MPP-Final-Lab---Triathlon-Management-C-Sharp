using CompetitionModel.model;

namespace CompetitionPersistence.template
{
    public interface IGenericRepo<ID, E> where E : Entity<ID>
    {
        E Save(E entity);
        E Update(E entity);
        E Delete(ID id);
        E FindOne(ID id);
        List<E> FindAll();
    }
}
