using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class GA
    {
        private readonly Gene[,] correctGenes = new Gene[4,8] {  { new Gene(1,1), new Gene(2,1), new Gene(1,2), new Gene(2,2), new Gene(1,1), new Gene(2,1), new Gene(1,2), new Gene(2,2) },
                                                        { new Gene(1,1), new Gene(2,3), new Gene(2,2), new Gene(1,1), new Gene(1,1), new Gene(2,1), new Gene(1,2), new Gene(2,2) },
                                                        { new Gene(2,3), new Gene(1,2), new Gene(2,4), new Gene(1,1), new Gene(1,1), new Gene(2,1), new Gene(2,1), new Gene(2,2) },
                                                        { new Gene(1,1), new Gene(2,1), new Gene(1,2), new Gene(2,3), new Gene(1,2), new Gene(1,2), new Gene(2,4), new Gene(1,1) } };

        public Individual solution;

        public GA()
        {
           solution = new Individual(correctGenes); 
        }
        

    }
}
