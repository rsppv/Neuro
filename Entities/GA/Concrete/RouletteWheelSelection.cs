using System;
using System.Collections.Generic;
using System.Linq;
using Entities.GA.Interfaces;

namespace Entities.GA.Concrete
{
    public class RouletteWheelSelection : ISelectable
    {
        public List<IIndividual> SelectedIndividuals { get; private set; }

        public int ListSize { get; private set; }

        /* Добавить параметры если необходимо
           Параметры будут задавать при создании Селекции */

        public RouletteWheelSelection (int listSize)
        {
            ListSize = listSize;
        }

        public List<IIndividual> Select(Population population)
        {
            int sumFitness = population.Individuals.Sum(ind => ind.Fitness);
            var rand = new Random();
            foreach (var ind in population.Individuals)
            {
                    float chance = ((float)ind.Fitness) / sumFitness;
                    if ((((float)rand.Next(0, 101)) / 100) < chance)
                    {
                        SelectedIndividuals.Add(ind);
                    }
            }

            throw new NotImplementedException();
            //return SelectedIndividuals;
        }
    }
}
