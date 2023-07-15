using CompetitionModel.model;

namespace CompetitionPersistence.template
{
    public interface IScoreRepo : IGenericRepo<long, Score>
    {
        public List<Score> FindAllWithPointsInRound(string roundName);
    }
}
