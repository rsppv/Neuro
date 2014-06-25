namespace Entities.ANN
{
    public class Link
    {
        public double Weight { get; set; }
        public Neuron SourceNeuron { get; private set; }
        public Neuron EndNeuron { get; private set; }

        public Link(double weight, Neuron sourceNeuron, Neuron endNeuron)
        {
            EndNeuron = endNeuron;
            SourceNeuron = sourceNeuron;
            Weight = weight;
        }
    }
}
