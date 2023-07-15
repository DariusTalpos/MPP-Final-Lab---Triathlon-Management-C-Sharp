using CompetitionModel.model;

namespace CompetitionPersistence.template
{
    public interface IRoundRepo: IGenericRepo<long,Round>
    {
        public Round findRoundWithName(String name);
    }
}
