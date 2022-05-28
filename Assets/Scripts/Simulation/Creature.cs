
using UnityEngine;

public class Creature
{
    protected GameObject gameObject;
    protected NeuralNetwork neuralNetwork;

    public Creature(GameObject gameObject, NeuralNetwork neuralNetwork)
    {
        this.gameObject = gameObject;
        this.neuralNetwork = neuralNetwork;
    }
}
