using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.GA.Interfaces;

namespace Entities.GA.Concrete
{
    public class TruncationSelection : ISelectable
    {
        public double Cutoff { get; set; }

        public TruncationSelection(double cutoff)
        {
            if (cutoff < 0.0 || cutoff > 1.0) throw new ArgumentOutOfRangeException("Порог усечения должен лежать в диапазоне [0;1]");
            Cutoff = cutoff;
        }

        public List<IIndividual> SelectedIndividuals { get; private set; }

        public List<IIndividual> Select(Population population)
        {
            return population.Individuals.GetRange(0, (int) (population.Individuals.Count*Cutoff));
        }
    }
}
