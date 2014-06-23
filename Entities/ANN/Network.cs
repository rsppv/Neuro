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

        public Network(int innerNeuronCount, int innerLayerCount)
        {
            InnerLayerCount = innerLayerCount;
            InnerNeuronCount = innerNeuronCount;
            CreateNet();
        }

        public void CreateNet()
        {

            #region Формирование входного слоя

            /*Три входных нейрона: Х, У, сторона пришедшей трубы*/
            var inputNeurons = new List<Neuron>() { 
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
                        Link link = new Link((-0.1 + rand.Next(21)/100), neuron1, neuron2);
                        neuron1.OutputLinks.Add(link);
                        neuron2.InputLinks.Add(link);
                    }
                }
            }

            #endregion

        }

        public void FindResult(List<int> inputs)
        {
            if (inputs.Count != 3) 
                throw new ArgumentOutOfRangeException("В передаваемом списке должно быть три значения");

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
            for (int i = 0; i < Layers.Count; i++)
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

        public String DisplayResult()
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
