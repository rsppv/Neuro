using System;
using System.Collections.Generic;

namespace Entities.ANN
{
    public class Network
    {
        public List<Layer> Layers = new List<Layer>();
        public double[][] TrainingSample;
        public double Speed = 0.8;
        public int InnerNeuronCount { get; private set; }
        public int InnerLayerCount { get; private set; }

        public Network(int innerNeuronCount, int innerLayerCount)
        {
            InnerLayerCount = innerLayerCount;
            InnerNeuronCount = innerNeuronCount;
            CreateNet();
        }

        public void CreateNet()
        {
            var inneLayer = new Layer();
            // создаем входные нейроны
            throw new NotImplementedException();
        }

        public double FindResult()
        {
            throw new NotImplementedException();
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
    }
}
