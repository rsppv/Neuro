using System.Collections.Generic;

namespace Entities.ANN
{
    public class Layer
    {
        public List<Neuron> Neurons { get; private set; }

        public Layer() : this(null)
        {
            Neurons = new List<Neuron>();
        }

        public Layer(IEnumerable<Neuron> neurons)
        {
            Neurons = new List<Neuron>(neurons);
        }

        public void AddNeuron(Neuron neuron)
        {
            Neurons.Add(neuron);
        }
    }
}
