using System;
using Entities.GA.Interfaces;
using Entities.Game;

namespace Entities.GA.Concrete
{
    public class OnePointСrossover : ICrossible
    {
        public IIndividual Child1 { get; set; }
        public IIndividual Child2 { get; set; }

        private Random rand = new Random();
        
        public OnePointСrossover()
        {
        }

        public void Cross(IIndividual parent1, IIndividual parent2)
        {
            /* Скрещивание между двумя родителями */
            int crossPoint = rand.Next(1, parent1.Genes.Count);

            Child1 = new GameEnvironment();
            Child2 = new GameEnvironment();

            Child1.Genes.Clear();
            Child2.Genes.Clear();

            Child1.Genes.AddRange(parent1.Genes.GetRange(0, crossPoint));
            Child1.Genes.AddRange(parent2.Genes.GetRange(crossPoint, parent2.Genes.Count - crossPoint));

            Child2.Genes.AddRange(parent2.Genes.GetRange(0, crossPoint));
            Child2.Genes.AddRange(parent1.Genes.GetRange(crossPoint, parent1.Genes.Count - crossPoint));

        }

    }
}
