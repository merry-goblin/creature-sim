
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSubject
{
    protected GameObject subjectGameObject;
    protected NeuralNetwork neuralNetwork;

    public void Update()
    {
        this.CalculateOutput();
        this.ApplyOutput();
    }

    public List<float> GetOutput()
    {
        return this.neuralNetwork.GetOutputValues();
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
