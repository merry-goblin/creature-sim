
using System;
using System.Collections.Generic;
using UnityEngine;

public class WorldSample1 : AbstractWorld, IWorld
{
    protected static GameObject subjectPrefab;
    protected static GameObject foodPrefab;
    protected static bool prefabsAreLoaded = false;

    public WorldSample1()
    {
        this.subjectList = new List<ISubject>();
    }

    public static void addPrefabs(GameObject subjectPrefab, GameObject foodPrefab)
    {
        WorldSample1.subjectPrefab = subjectPrefab;
        WorldSample1.foodPrefab = foodPrefab;
        WorldSample1.prefabsAreLoaded = true;
    }

    public override void Load()
    {
        if (!WorldSample1.prefabsAreLoaded)
        {
            throw new Exception("SampleWorld1.addPrefabs must be called before calling Load");
        }

        NeuralNetwork neuralNetwork = new NeuralNetwork(2, 2, 4, 5, true);
        neuralNetwork.inputLayer[0].outputValue = 0.25f;
        neuralNetwork.inputLayer[1].outputValue = -0.5f;

        SubjectSample1 subject = new SubjectSample1(WorldSample1.subjectPrefab, neuralNetwork);
        this.AddSubject(subject);

        base.Load();
    }
}
