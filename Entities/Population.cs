using System;
using System.Collections.Generic;

namespace Entities
{
    public class Population
    {
        public List<Individual> Individuals { get; set; }

        public Population(int populationSize)
        {
            Individuals = new List<Individual>(populationSize);
        }

        public List<Individual> Initialize(Individual solution)
        {
            var random = new Random();

            foreach (var individual in Individuals)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        individual.Genes[i, j].Position = random.Next(1, individual.Genes[i,j].Type == 1 ? 2 : 4);
                    }
                }
            }
            return Individuals;
        }


        public void Evaluate()
        {
            throw new NotImplementedException();
        }
    }
}
