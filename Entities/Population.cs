using System;
using System.Collections.Generic;

namespace Entities
{
    public class Population
    {
        public List<Individual> Individuals { get; set; }

        public Population(int populationSize)
        {
            var random = new Random();
            Individuals = new List<Individual>(populationSize);

            foreach (var individual in Individuals)
            {
                for (int i = 0; i < UPPER; i++)
                {
                    for (int j = 0; j < UPPER; j++)
                    {
                        individual.Genes[i,j].
                    }
                }
            }
        }

        public Population Select()
        {
            throw new NotImplementedException();
        }

        
    }
}
