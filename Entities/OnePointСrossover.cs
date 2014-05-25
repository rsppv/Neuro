using System;

namespace Entities
{
    public class OnePointСrossover : ICrossible
    {
        public Population ParentsPopulation;
        public OnePointСrossover(Population selected)
        {
            ParentsPopulation = selected;
        }

        public Individual[] Cross(Individual parent1, Individual parent2)
        {
            /* Скрещивание между двумя родителями */
            throw new NotImplementedException();
        }

        public Population RunCross()
        {
            /* Здесь запускать скрещивание для всей популяции */
            throw new NotImplementedException();
        }
    }
}
