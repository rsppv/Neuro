using System;
using System.Collections.Generic;

namespace Entities
{
    public class Individual
    {
        public Gene[,] Genes { get; set; }

        public Individual ( Gene[,] genes )
        {
            Genes = genes;
        }

        public int Fitness()
        {
            return 0;
        }

        public Individual Mutate()
        {
            throw new NotImplementedException();
        }
    }
}
