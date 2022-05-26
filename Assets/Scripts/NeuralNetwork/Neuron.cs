
using System;
using System.Collections.Generic;

public class Neuron
{
    public float weightedSum = float.NaN;
    public float outputValue;
    public List<Synapse> dendrites; // input
    public List<Synapse> axons; // output
    public IActivation activation;

    public Neuron(IActivation activation)
    {
        this.dendrites = new List<Synapse>();
        this.axons = new List<Synapse>();
        this.activation = activation;
    }


}
