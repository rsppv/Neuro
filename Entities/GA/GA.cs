using System;
using System.Collections.Generic;
using System.Linq;
using Entities.GA.Concrete;
using Entities.GA.Interfaces;

namespace Entities.GA
{
    public class Ga
    {

        #region Инициализация Готового решения
        //private readonly Gene[,] correctGenes = new Gene[4,8] {  { new Gene(1,1), new Gene(2,1), new Gene(1,2), new Gene(2,2), new Gene(1,1), new Gene(2,1), new Gene(1,2), new Gene(2,2) },
        //                                                { new Gene(1,1), new Gene(2,3), new Gene(2,2), new Gene(1,1), new Gene(1,1), new Gene(2,1), new Gene(1,2), new Gene(2,2) },
        //                                                { new Gene(2,3), new Gene(1,2), new Gene(2,4), new Gene(1,1), new Gene(1,1), new Gene(2,1), new Gene(2,1), new Gene(2,2) },
        //                                                { new Gene(1,1), new Gene(2,1), new Gene(1,2), new Gene(2,3), new Gene(1,2), new Gene(1,2), new Gene(2,4), new Gene(1,1) } };

        //public Individual solution = new Individual(new Gene[4,8]
        //{  
        //    { new Gene(1,1), new Gene(2,1), new Gene(1,2), new Gene(2,2), new Gene(1,1), new Gene(2,1), new Gene(1,2), new Gene(2,2) },
        //    { new Gene(1,1), new Gene(2,3), new Gene(2,2), new Gene(1,1), new Gene(1,1), new Gene(2,1), new Gene(1,2), new Gene(2,2) },
        //    { new Gene(2,3), new Gene(1,2), new Gene(2,4), new Gene(1,1), new Gene(1,1), new Gene(2,1), new Gene(2,1), new Gene(2,2) },
        //    { new Gene(1,1), new Gene(2,1), new Gene(1,2), new Gene(2,3), new Gene(1,2), new Gene(1,2), new Gene(2,4), new Gene(1,1) } 
        //});
        #endregion

        public int GenerationNumber { get; private set; }
        public int PopulationSize { get; private set; }
        public double CrossRate { get; private set; }
        public double MutationRate { get; private set; }
        public ICrossible Crossover { get; private set; }
        public ISelectable Selection { get; private set; }


        public Ga(int generationNumber, int populationSize, double crossRate, double mutationRate, ICrossible crossover, ISelectable selection)
        {
            if (mutationRate < 0 || mutationRate > 1) throw new ArgumentOutOfRangeException("mutationRate [0;1]");
            if (crossRate < 0 || crossRate > 1) throw new ArgumentOutOfRangeException("crossRate [0;1]");
            if (generationNumber == 0) throw new ArgumentException("Количество поколений должно быть больше нуля");
            if (populationSize % 2 == 1) throw new ArgumentException("Размер популяции должен быть четным");

            MutationRate = mutationRate;
            CrossRate = crossRate;
            PopulationSize = populationSize;
            GenerationNumber = generationNumber;
            Crossover = crossover;
            Selection = selection;
        }

        public void Start()
        {
            
            #region Формирование начальной популяции

            var pop = new Population(PopulationSize);
            pop.FillPopulation(); // Запоним рандомными значениями

            #endregion

            int currentGeneration = 0;
            int fitness = 1;
            var selectedIndividuals = new List<Individual>();
            //var childPop = new Population(PopulationSize);
            var comparer = new IndividualComparer();
            Random rand = new Random();

            while (currentGeneration < GenerationNumber || fitness != 0)
            {
                #region Поколение

                currentGeneration += 1;
 
                #region Оценивание популяции

                foreach (var individual in pop.Individuals)
                {
                    individual.CalcFitness();
                }

                pop.Individuals.Sort(comparer);
                fitness = pop.Individuals.First().Fitness;

                #endregion


                #region Селекция

                selectedIndividuals = Selection.Select(pop);

                #endregion


                #region Скрещивание
                pop.Individuals.Clear();
                while (pop.Size < PopulationSize)
                {
                    var parent1 = selectedIndividuals[rand.Next(1, selectedIndividuals.Count + 1)];
                    var parent2 = selectedIndividuals[rand.Next(1, selectedIndividuals.Count + 1)];
                    if (rand.NextDouble() < CrossRate)
                    {
                        var childs = Crossover.Cross(parent1, parent2);
                        pop.Add(childs[0]);
                        pop.Add(childs[1]);
                    }
                    else
                    {
                        pop.Add(parent1);
                        pop.Add(parent2);
                    }
                }

                #endregion


                #region Мутация

                foreach (var ind in pop.Individuals)
                {
                    foreach (var gene in ind.Genes)
                    {
                        if (rand.NextDouble() < MutationRate)
                        {
                            gene.Mutate();
                        }
                    }
                }
                
                #endregion

                // Либо так, 
                //либо getRange'ем скопировать элементы в pop,
                //либо ForEach делегатом пробежаться
                //pop = childPop;

                #endregion
            }




        }

    }
}
