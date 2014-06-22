using System;
using System.Collections.Generic;

namespace Entities.GA
{
    public class Population
    {
        public int Size { get; private set; }
        public List<Individual> Individuals { get; private set; }

        public Population (int populationSize)
        {
            Individuals = new List<Individual>(populationSize);
            Size = Individuals.Count;
        }

        public void Evaluate ()
        {
            throw new NotImplementedException();
        }
        public void Add(Individual individual)
        {
            Individuals.Add(individual);
            Size = Individuals.Count;
        }

        public void FillPopulation()
        {
            throw new NotImplementedException();
        }
    }
}
