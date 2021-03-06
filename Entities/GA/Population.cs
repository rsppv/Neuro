﻿using System;
using System.Collections.Generic;
using Entities.Game;

namespace Entities.GA
{
    public class Population
    {
        public List<IIndividual> Individuals { get; private set; }
        public int Size { get; private set; }

        public Population (int populationSize)
        {
            Individuals = new List<IIndividual>(populationSize);
            Size = populationSize;
        }

        public void Evaluate ()
        {
            throw new NotImplementedException();
        }
        public void Add(IIndividual individual)
        {
            Individuals.Add(individual);
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
