
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSubject
{
    protected GameObject gameObject;
    protected NeuralNetwork neuralNetwork;

    public delegate void LifeEndsDelegate();
    public event LifeEndsDelegate LifeEnds;

    public virtual void Load()
    {

    }

    public virtual void Update()
    {
        this.CalculateOutput();
        this.ApplyOutput();
    }

    public List<float> GetOutput()
    {
        return this.neuralNetwork.GetOutputValues();
    }

    /**
     * End simulation for current subject
     * To use when a subject die or when the conditions for ending his simulation have been met
     * Will raise an event
     */
    public void EndSimulation()
    {
        if (this.LifeEnds != null)
        {
            this.LifeEnds();
        }
    }

    protected void CalculateOutput()
    {
        this.neuralNetwork.CalculateOutput();
    }

    /// <summary>
    /// Child class will decide what to do with the output.
    /// Is it a GameObject to move or a string to display somehow? It will be applied with this method.
    /// To get the subject's output values use: List<float> output = this.GetOutput();
    /// </summary>
    protected abstract void ApplyOutput();
}
