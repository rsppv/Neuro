namespace Entities
{
    public interface ICrossible
    {
        Individual[] Cross(Individual parent1, Individual parent2);
        Population RunCross();
    }
}