using System;
using System.Collections.Generic;
using System.Linq;
using Entities.ANN;
using Entities.GA;
using Entities.GA.Concrete;
using Entities.GA.Interfaces;
using Entities.Game;

namespace NeuroPlumber
{
    class Program
    {

        static void Main(string[] args)
        {
            GameEnvironment.Initialize(20, 20, 3, 5);

            int genarationCount = 10;
            int populationSize = 50;
            double crossRate = 0.9;
            double mutateRate = 0.9;
            double cutoff = 0.6;

            try
            {
                GameEnvironment game = new GameEnvironment();
                //game.Net.FindResult(new List<int>() { 1, 0, 0, 0 });
                //game.Net.Print();
                //Console.Out.WriteLine(game.CodeToSymbol(game.Net.Solution));
                //game.Net.FindResult(new List<int>() { 0, 0, 0, 1 });
                //game.Net.Print();
                //Console.Out.WriteLine(game.CodeToSymbol(game.Net.Solution));

                //game.Net.FindResult(new List<int>() { 0, 1, 0, 0 });
                //game.Net.Print();
                //Console.Out.WriteLine(game.CodeToSymbol(game.Net.Solution));

                Ga ga = new Ga(genarationCount, populationSize, crossRate, mutateRate, new OnePointСrossover(), new TruncationSelection(cutoff));
                ga.Start();
               
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadLine();
            }
        }
    }
}
