using System;
using Entities.GA.Interfaces;

namespace Entities.GA.Concrete
{
    public class OnePointСrossover : ICrossible
    {
        public Population ParentsPopulation;
        
        public OnePointСrossover()
        {
        }

        public Individual[] Cross(Individual parent1, Individual parent2)
        {
            /* Скрещивание между двумя родителями */
            throw new NotImplementedException();
        }

    }
}
