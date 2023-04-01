using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Learning.GradientDescent;
using FeedForwardNeuralNetwork;

public class LearnXORByRetropropagationScript : MonoBehaviour
{
    private GradientDescentAlgorithm algorithm;

    // Start is called before the first frame update
    void Start()
    {
        IActivation activation = new SigmoidActivation();
        bool initSynaspesRandomly = true;
        NeuralNetwork neuralNetwork = new NeuralNetwork(2, 1, 1, 2, initSynaspesRandomly, ref activation);
        float learningRate = 0.1f;
        this.algorithm = new GradientDescentAlgorithm(neuralNetwork, learningRate);

        //  todo
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
