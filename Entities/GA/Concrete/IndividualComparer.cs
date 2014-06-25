using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.GA.Concrete
{
    public sealed class IndividualComparer : IComparer<IIndividual>
    {
        public int Compare(IIndividual x, IIndividual y)
        {
            if (x == null)
            {
                if (y == null) { return 0; }
                else { return -1; }
            }
            else
            {
                if (y == null) return 1;
                else
                {
                    if (x.Fitness > y.Fitness) { return 1; }
                    if (x.Fitness == y.Fitness) { return 0; }
                    return -1;
                }
            }
        }
    }
}
