namespace Entities.GA.Interfaces
{
    public interface ICrossible
    {
        IIndividual Child1 { get; set; }
        IIndividual Child2 { get; set; }
        void Cross(IIndividual parent1, IIndividual parent2);
    }
}