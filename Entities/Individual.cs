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

        public Individual Mutate(Individual individual)
        {
            throw new NotImplementedException();
        }
    }
}
