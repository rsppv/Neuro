using System.Collections.Generic;

namespace Entities.GA.Interfaces
{
    public interface ISelectable
    {
        List<Individual> Select(Population population);
    }
}