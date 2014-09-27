using System;
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
                        double nextWeight = -0.1 + ((double) rand.Next(201))/1000;
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
                inputNeurons[i].Value = (double) inputs[i];
            }
            Activate();
            List<double> list = Layers.Last().GetValues();
            double maxOutput = list.Max();
            Solution = list.IndexOf(maxOutput);
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

        public List<double> GetWeights()
        {
            var list = new List<double>();
            foreach (var layer in Layers)
            {
                list.AddRange(layer.GetInputWeights()); 
            }
            return list;
        }

        public double[] GetWeightsArray()
        {
            return GetWeights().ToArray();
        }

        public void SetWeights(List<double> weights)
        {
            int l = 0;
            for (int i = 1; i < Layers.Count; i++)
            {
                var layer = Layers[i];
                for (int j = 0; j < layer.Neurons.Count; j++)
                {
                    var neuron = layer.Neurons[j];
                    for (int k = 0; k < neuron.InputLinks.Count; k++)
                    {
                        neuron.InputLinks[k].Weight = weights[l];
                        l++;
                    }
                }
            }
        }

        public void SetWeights(double[] weights)
        {
            int l = 0;
            foreach (var layer in Layers)
            {
                foreach (var neuron in layer.Neurons)
                {
                    foreach (Link link in neuron.InputLinks)
                    {
                        link.Weight = weights[l];
                        l++;
                    }
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
            //Если время останется
            throw new NotImplementedException();
        }

        public void Print()
        {
            int num = 0;
            foreach (var layer in Layers)
            {
                Console.Out.WriteLine();
                num += 1;
                Console.Out.WriteLine("Слой " + num + ":");
                Console.Out.WriteLine(layer.ToString());
                double[] mas = layer.GetOutputWeightsArray();
                int count = mas.Count();
                for (int i = 0; i < count; i++)
                {
                    if (i % 5 == 0) Console.Out.WriteLine();
                    Console.Out.Write(" "+mas[i].ToString() + " ");
                }
                Console.Out.WriteLine();
            }
        }
    }
}
