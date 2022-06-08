using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : ISubject
{
    protected GameObject subjecGameObject;
    protected NeuralNetwork neuralNetwork;

    protected float subjectRotationModifier = 45.0f;
    protected float subjectSpeedModifier = 20.0f;

    public Creature(GameObject gameObject, NeuralNetwork neuralNetwork)
    {
        this.subjecGameObject = gameObject;
        this.neuralNetwork = neuralNetwork;
    }

    public void Update()
    {
        this.CalculateOutput();
        this.ApplyOutputOnSubject();
    }

    public List<float> GetOutput()
    {
        return this.neuralNetwork.GetOutputValues();
    }

    protected void CalculateOutput()
    {
        this.neuralNetwork.CalculateOutput();
    }

    protected void ApplyOutputOnSubject()
    {
        List<float> outputList = this.GetOutput();

        float subjectRotation = outputList[0];
        float subjectSpeed = outputList[1];

        this.subjecGameObject.transform.Rotate(0.0f, subjectRotation * Time.deltaTime * this.subjectRotationModifier, 0.0f, Space.Self);
        this.subjecGameObject.transform.Translate(Vector3.forward * subjectSpeed * Time.deltaTime * this.subjectSpeedModifier, Space.Self);
    }
}
