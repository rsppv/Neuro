using System.IO;

namespace Entities
{
    public class GA
    {
        #region Инициализация Готового решения
        //private readonly Gene[,] correctGenes = new Gene[4,8] {  { new Gene(1,1), new Gene(2,1), new Gene(1,2), new Gene(2,2), new Gene(1,1), new Gene(2,1), new Gene(1,2), new Gene(2,2) },
        //                                                { new Gene(1,1), new Gene(2,3), new Gene(2,2), new Gene(1,1), new Gene(1,1), new Gene(2,1), new Gene(1,2), new Gene(2,2) },
        //                                                { new Gene(2,3), new Gene(1,2), new Gene(2,4), new Gene(1,1), new Gene(1,1), new Gene(2,1), new Gene(2,1), new Gene(2,2) },
        //                                                { new Gene(1,1), new Gene(2,1), new Gene(1,2), new Gene(2,3), new Gene(1,2), new Gene(1,2), new Gene(2,4), new Gene(1,1) } };
        #endregion

        public int GenerationNumber { get; private set; }
        public int PopulationSize { get; private set; }
        public int CrossingChance { get; private set; }
        public int MutationChance { get; private set; }

        public Individual solution = new Individual(new Gene[4,8]
        {  
            { new Gene(1,1), new Gene(2,1), new Gene(1,2), new Gene(2,2), new Gene(1,1), new Gene(2,1), new Gene(1,2), new Gene(2,2) },
            { new Gene(1,1), new Gene(2,3), new Gene(2,2), new Gene(1,1), new Gene(1,1), new Gene(2,1), new Gene(1,2), new Gene(2,2) },
            { new Gene(2,3), new Gene(1,2), new Gene(2,4), new Gene(1,1), new Gene(1,1), new Gene(2,1), new Gene(2,1), new Gene(2,2) },
            { new Gene(1,1), new Gene(2,1), new Gene(1,2), new Gene(2,3), new Gene(1,2), new Gene(1,2), new Gene(2,4), new Gene(1,1) } 
        });

        public GA(int generationNumber, int populationSize, int crossingChance, int mutationChance)
        {
            MutationChance = mutationChance;
            CrossingChance = crossingChance;
            PopulationSize = populationSize;
            GenerationNumber = generationNumber;
        }

        public void Start()
        {
            var initialPopulation = new Population(PopulationSize);
            initialPopulation.Evaluate();
            ISelectable selection = new RouletteWheelSelection(initialPopulation);
            var selected = selection.Select();
            ICrossible crossover = new OnePointСrossover(selected);
            var nextPopulation = crossover.RunCross();
            foreach (var individual in nextPopulation.Individuals)
            {
                individual.Mutate();
            }
        }

    }
}
