using System;
using System.Collections.Generic;
using Entities.Game;

namespace Entities.GA
{
    public class Population
    {
        public int IndividualCount { get; private set; }
        public List<IIndividual> Individuals { get; private set; }
        public int Size { get; private set; }

        public Population (int populationSize)
        {
            Individuals = new List<IIndividual>(populationSize);
            IndividualCount = Individuals.Count;
            Size = populationSize;
        }

        public void Evaluate ()
        {
            throw new NotImplementedException();
        }
        public void Add(IIndividual individual)
        {
            Individuals.Add(individual);
            IndividualCount = Individuals.Count;
        }

        public void FillPopulation()
        {
            for (int i = 0; i < Size; i++)
            {
                Individuals.Add(new GameEnvironment());
            }
        }
    }
}
