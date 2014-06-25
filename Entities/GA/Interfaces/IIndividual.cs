using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Entities.ANN;

namespace Entities.GA
{
    public interface IIndividual : IComparable
    {
        int Fitness { get; set; }
        Network Net { get; set; }
        List<double> Genes { get; set; }
        void CalcFitness();
        void Mutate(int geneNumber);
    }
}
