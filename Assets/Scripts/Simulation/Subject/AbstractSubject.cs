
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSubject
{
    protected GameObject gameObject;
    protected INeuralNetwork neuralNetwork;

    public event ISubject.LifeEndsDelegate OnLifeEnds;

    protected bool active
    {
        get;
        set;
    }

    public AbstractSubject()
    {
        this.active = false;
    }

    public virtual void Load()
    {
        this.active = true;
    }

    public virtual void Unload()
    {
        this.active = false;
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
     * To use when a subject dies or when the conditions for ending his simulation have been met (subject is no more active)
     * Will raise an event
     */
    public void EndSimulationForSubject()
    {
        this.active = false;
        if (this.OnLifeEnds != null)
        {
            this.OnLifeEnds(); // Event
        }
    }

    public bool IsActive()
    {
        return this.active;
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
