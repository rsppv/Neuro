namespace Entities.GA.Interfaces
{
    public interface ICrossible
    {
        Individual[] Cross(Individual parent1, Individual parent2);
    }
}