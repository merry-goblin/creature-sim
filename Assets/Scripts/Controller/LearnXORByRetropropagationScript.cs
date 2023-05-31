using System;
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
        float learningRate = 5.0f;
        float errorMaxAllowed = 0.1f;
        float learningReductingRate = 0.1f;
        int nbIterationsToReduce = 25;
        this.algorithm = new GradientDescentAlgorithm(neuralNetwork, learningRate, errorMaxAllowed, learningReductingRate, nbIterationsToReduce);

        //  Train
        List<List<float>> inputValueSet = this.fillInputValues();
        List<List<float>> expectedOutputValueSet = this.fillOutputValues();

        Debug.Log(String.Join(", ", neuralNetwork.ExportWeights()));
        this.algorithm.Train(inputValueSet, expectedOutputValueSet);
        Debug.Log(String.Join(", ", neuralNetwork.ExportWeights()));
    }

    private List<List<float>> fillInputValues()
    {
        float[,] input = new float[4,2] { { 0.0f, 0.0f }, { 0.0f, 1.0f }, { 1.0f, 0.0f }, { 1.0f, 1.0f } };

        List<List<float>> inputValueSet = new List<List<float>>();
        List<float> inputList;
        for (int i = 0; i< 4; i++) {
            inputList = new List<float>();
            inputList.Add(input[i, 0]);
            inputList.Add(input[i, 1]);
            inputValueSet.Add(inputList);
        }
        
        return inputValueSet;
    }

    private List<List<float>> fillOutputValues()
    {
        float[,] output = new float[4,1] { { 0.0f }, { 1.0f }, { 1.0f }, { 0.0f } };

        List<List<float>> expectedOutputValueSet = new List<List<float>>();
        List<float> outputList;
        for (int i = 0; i< 4; i++) {
            outputList = new List<float>();
            outputList.Add(output[i, 0]);
            expectedOutputValueSet.Add(outputList);
        }
        
        return expectedOutputValueSet;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
