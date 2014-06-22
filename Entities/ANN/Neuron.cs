﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ANN
{
    public class Neuron
    {
        public double Shift { get; private set; }
        public double Value { get; private set; }
        public double Error { get; private set; }
        public double WeightedSum { get; private set; }
        public double FactorA { get; private set; }
        public List<Link> InputLinks { get; private set; }
        public List<Link> OutputLinks { get; private set; }

        public Neuron()
        {
            Shift = 0;
            Value = 0;
            Error = 0;
            WeightedSum = 0;
            FactorA = 1;
        }

        private double LogSigmoidFunc(double factorA, double weightedSum)
        {
            return (1 / (1 + Math.Exp( -1*factorA*weightedSum )));
        }

        public void ActivationFunc()
        {
            WeightedSum = 0;
            foreach (var link in InputLinks)
            {
                WeightedSum += link.Weight*link.SourceNeuron.Value;
            }
            Value = LogSigmoidFunc(FactorA, WeightedSum);
        }

        public void ErrorOutputLayer(double desiredOutput)
        {
            Error = FactorA*Value*(1 - Value)*(Value - desiredOutput);
        }

        public void ErrorInnerLayer()
        {
            double sum = OutputLinks.Sum(link => link.Weight*link.EndNeuron.Error);
            Error = FactorA*Value*(1-Value)*sum;
        }
    }
}