using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace Entities.GA
{
    public class Individual
    {
        public int Fitness { get; private set; }
        public List<Gene> Genes { get; private set; }

        public Individual(List<Gene> genes)
        {
            Genes = genes;
            Fitness = 1;
        }

        public int CalcFitness()
        {
            throw new NotImplementedException();
        }

    }
}
