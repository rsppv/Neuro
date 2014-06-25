using System.Collections.Generic;

namespace Entities.GA.Interfaces
{
    public interface ISelectable
    {
        List<IIndividual> Select(Population population);
    }
}