using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : ISubject
{
    protected GameObject gameObject;
    protected NeuralNetwork neuralNetwork;

    public Creature(GameObject gameObject, NeuralNetwork neuralNetwork)
    {
        this.gameObject = gameObject;
        this.neuralNetwork = neuralNetwork;
    }

    public void CalculateOutput()
    {
        this.neuralNetwork.CalculateOutput();
    }

    public List<float> GetOutput()
    {
        return this.neuralNetwork.GetOutputValues();
    }
}
