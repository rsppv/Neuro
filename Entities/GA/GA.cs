using System;
using System.Collections.Generic;
using System.Linq;
using Entities.GA.Concrete;
using Entities.GA.Interfaces;

namespace Entities.GA
{
    public class Ga
    {
        public int GenerationNumber { get; private set; }
        public int PopulationSize { get; private set; }
        public double CrossRate { get; private set; }
        public double MutationRate { get; private set; }
        public ICrossible Crossover { get; private set; }
        public ISelectable Selection { get; private set; }
        public Population Population { get; set; }
 


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
            Population = new Population(PopulationSize);
        }


        public void Start()
        {
            
            #region Формирование начальной популяции

            
            Population.FillPopulation(); // Запоним рандомными значениями

            #endregion

            int currentGeneration = 0;
            int fitness = 1;
            var selectedIndividuals = new List<IIndividual>();
            //var childPop = new Population(PopulationSize);
            Random rand = new Random();

            while (currentGeneration < GenerationNumber || fitness != 0)
            {
                #region Поколение

                currentGeneration += 1;
 
                #region Оценивание популяции

                foreach (var individual in Population.Individuals)
                {
                    individual.CalcFitness();
                }

                Population.Individuals.Sort();
                fitness = Population.Individuals.First().Fitness;

                #endregion


                #region Селекция

                selectedIndividuals = Selection.Select(Population);

                #endregion


                #region Скрещивание
                Population.Individuals.Clear();
                while (Population.IndividualCount < PopulationSize)
                {
                    var parent1 = selectedIndividuals[rand.Next(1, selectedIndividuals.Count + 1)];
                    var parent2 = selectedIndividuals[rand.Next(1, selectedIndividuals.Count + 1)];
                    if (rand.NextDouble() < CrossRate)
                    {
                        Crossover.Cross(parent1, parent2);
                        Population.Add(Crossover.Child1);
                        Population.Add(Crossover.Child2);
                    }
                    else
                    {
                        Population.Add(parent1);
                        Population.Add(parent2);
                    }
                }

                #endregion


                #region Мутация

                foreach (var ind in Population.Individuals)
                {
                    for (int i = 0; i < ind.Genes.Count; i++)
                    {
                        if (rand.NextDouble() < MutationRate)
                        {
                            ind.Mutate(i);
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

        public IIndividual GetBest()
        {
            Population.Individuals.Sort();
            return Population.Individuals.First();
        }

    }
}
