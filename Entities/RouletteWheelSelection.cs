using System;
using System.Linq;

namespace Entities
{
    public class RouletteWheelSelection : ISelectable
    {
        public Population SelectedPopulation { get; set; }

        /* Добавить параметры если необходимо
           Параметры будут задавать при создании Селекции */

        public RouletteWheelSelection (Population population)
        {
            SelectedPopulation = population;
        }

        public Population Select()
        {
            /*Если что, переделать для foreach*/
            if (SelectedPopulation.Individuals.Any())
            {
                throw new NotImplementedException();
            }
            return SelectedPopulation;
        }
    }
}
