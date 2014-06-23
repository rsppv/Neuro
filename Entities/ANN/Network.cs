﻿using System;
using System.Collections.Generic;
using System.Linq;


namespace Entities.ANN
{
    public class Network
    {
        public List<Layer> Layers = new List<Layer>();
        public double Speed = 0.8;
        public int InnerNeuronCount { get; private set; }
        public int InnerLayerCount { get; private set; }
        public int Solution { get; set; }
        private Random rand = new Random();
        private const int INPUTS_COUNT = 4;

        public Network(int innerNeuronCount, int innerLayerCount)
        {
            InnerLayerCount = innerLayerCount;
            InnerNeuronCount = innerNeuronCount;
            CreateNet();
        }

        private void CreateNet()
        {

            #region Формирование входного слоя

            /*  4 входных нейрона: Сигналы препятствий с 4х сторон
             *  Один нейрон получает специализацию, 
             *  как отвечающий за сторону откуда пришла труба
             */
            var inputNeurons = new List<Neuron>() { 
                new Neuron(), 
                new Neuron(), 
                new Neuron(), 
                new Neuron()
            };

            Layer inputLayer = new Layer(inputNeurons);
            Layers.Add(inputLayer);

            #endregion

            #region Формирование скрытых слоев

            for (int i = 0; i < InnerLayerCount; i++)
            {
                Layer innerLayer = new Layer();
                for (int j = 0; j < InnerNeuronCount; j++)
                {
                    innerLayer.AddNeuron(new Neuron());
                }
                Layers.Add(innerLayer);
            }

            #endregion

            #region Формирование выходного слоя

            /*Формируем выходной слой: 6 нейронов. Каждый нейрон это символ/кусок трубы*/

            Layer outputLayer = new Layer(
                new List<Neuron>()
                {
                    new Neuron(), 
                    new Neuron(), 
                    new Neuron(), 
                    new Neuron(),
                    new Neuron(),
                    new Neuron()
                });
            Layers.Add(outputLayer);

            #endregion

            #region Формирование связей

            for (int i = 0; i < Layers.Count - 1; i++)
            {
                foreach (var neuron1 in Layers[i].Neurons)
                {
                    foreach (var neuron2 in Layers[i + 1].Neurons)
                    {
                        double nextWeight = -0.1 + ((double) rand.Next(21))/100;
                        Link link = new Link(nextWeight, neuron1, neuron2);
                        neuron1.OutputLinks.Add(link);
                        neuron2.InputLinks.Add(link);
                    }
                }
            }

            #endregion

        }

        public void FindResult(List<int> inputs)
        {
            if (inputs.Count != INPUTS_COUNT)
                throw new ArgumentOutOfRangeException("В передаваемом списке должно быть "+INPUTS_COUNT+" значения");

            Layer inputLayer = Layers[0];
            List<Neuron> inputNeurons = inputLayer.Neurons;
            for (int i = 0; i < inputs.Count; i++)
            {
                inputNeurons[i].Value = inputs[i];
            }
            Activate();
            Solution = Layers.Last().Neurons.FindIndex(n => n.Value == 1);
        }

        private void Activate()
        {
            for (int i = 1; i < Layers.Count; i++)
            {
                var layer = Layers[i];
                foreach (var neuron in layer.Neurons)
                {
                    neuron.Activate();
                }
            }
        }

        public void CalculateErrors()
        {
            throw new NotImplementedException();
        }

        public void CorrectWeights()
        {
            throw new NotImplementedException();
        }

        public void GetNetError()
        {
            throw new NotImplementedException();
        }

        public static Network RandomNetwork()
        {
            throw new NotImplementedException();
        }

        public void Print()
        {
            int num = 0;
            foreach (var layer in Layers)
            {
                Console.Out.WriteLine();
                num += 1;
                Console.Out.WriteLine("Слой " + num + ":  " + layer.ToString());
                double[] mas = layer.GetOutputWeightsArray();
                int count = mas.Count();
                for (int i = 0; i < count; i++)
                {
                    Console.Out.Write(mas[i].ToString() + " ");
                }
                Console.Out.WriteLine();
            }
        }

        public String SolutionToString()
        {
            /* Символы для трубы
             * { "007С |", "2014 -", "2308 Г", "2309 верз прав угол", "230A ниж лев", "230B ниж прав" };
             * Консоль не выводит эти символы почему то
             */
            switch (Solution)
            {
                case 0:
                    return "!";
                case 1:
                    return "—";
                case 2:
                    return "Г";
                case 3:
                    return "7";
                case 4:
                    return "L";
                case 5:
                    return "j";
                default:
                    return "";
            }
        }
    }
}
