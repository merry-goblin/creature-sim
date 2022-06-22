
using System;
using System.Collections.Generic;
using UnityEngine;

class SubjectSample1 : AbstractSubject, ISubject
{
    protected static GameObject prefab;
    protected static bool prefabIsLoaded = false;

    protected static int nbInputNeurons = 2;
    protected static int nbOutputNeurons = 2;
    protected static int nbHiddenLayers = 4; // must be > 0
    protected static int nbHiddenNeuronsByLayer = 5;

    protected float subjectRotationModifier = 45.0f;
    protected float subjectSpeedModifier = 20.0f;

    protected float energy = 100.0f;

    public SubjectSample1()
    {
    }

    public static void addPrefab(GameObject prefab)
    {
        SubjectSample1.prefab = prefab;
        SubjectSample1.prefabIsLoaded = true;
    }

    public override void Load()
    {
        if (!SubjectSample1.prefabIsLoaded)
        {
            throw new Exception("SubjectSample1.addPrefabs must be called before calling Load");
        }

        //  Unity game object
        this.gameObject = UnityEngine.Object.Instantiate(SubjectSample1.prefab, new Vector3(0, 0, 0), Quaternion.identity);

        //  Subject neural network
        NeuralNetwork neuralNetwork = new NeuralNetwork(
            SubjectSample1.nbInputNeurons, 
            SubjectSample1.nbOutputNeurons, 
            SubjectSample1.nbHiddenLayers, 
            SubjectSample1.nbHiddenNeuronsByLayer, 
            true
        );
        neuralNetwork.inputLayer[0].outputValue = 0.25f; // no real input for this subject so far
        neuralNetwork.inputLayer[1].outputValue = -0.5f; // no real input for this subject so far
        this.neuralNetwork = neuralNetwork;
    }

    protected override void ApplyOutput()
    {
        Debug.Log(this.energy);

        if (this.energy > 0)
        {
            List<float> outputList = this.GetOutput();

            float subjectRotation = outputList[0];
            float subjectSpeed = outputList[1];

            this.gameObject.transform.Rotate(0.0f, subjectRotation * Time.deltaTime * this.subjectRotationModifier, 0.0f, Space.Self);
            this.gameObject.transform.Translate(Vector3.forward * subjectSpeed * Time.deltaTime * this.subjectSpeedModifier, Space.Self);

            float consomption = (Math.Abs(subjectSpeed) + Math.Abs(subjectRotation) / 5) * Time.deltaTime * 5;
            this.energy -= consomption;
            if (this.energy < 0)
            {
                this.energy = 0;
            }
        }
    }
}
