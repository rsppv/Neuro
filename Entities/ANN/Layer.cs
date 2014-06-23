using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities.ANN
{
    public class Layer
    {
        public List<Neuron> Neurons { get; private set; }

        public Layer()
        {
            Neurons = new List<Neuron>();
        }

        public Layer(IEnumerable<Neuron> neurons)
        {
            if (neurons == null) throw new ArgumentNullException("Ошибка при создании слоя. Список нейронов пуст");
            Neurons = new List<Neuron>(neurons);
        }

        public void AddNeuron(Neuron neuron)
        {
            Neurons.Add(neuron);
        }

        public override string ToString()
        {
            string str = "";
            return Neurons.Aggregate(str, (current, neuron) => current + (neuron.ToString() + "  "));
        }

        public List<double> GetInputWeights()
        {
            List<double> weights = new List<double>();
            foreach (var neuron in Neurons)
            {
                weights.AddRange(neuron.GetInputWeights());
            }
            return weights;
        }

        public List<double> GetOutputWeights()
        {
            List<double> weights = new List<double>();
            foreach (var neuron in Neurons)
            {
                weights.AddRange(neuron.GetOutputWeights());
            }
            return weights;
        }

        public double[] GetInputWeightsArray()
        {
            return GetInputWeights().ToArray();
        }

        public double[] GetOutputWeightsArray()
        {
            return GetOutputWeights().ToArray();
        }
    }
}
