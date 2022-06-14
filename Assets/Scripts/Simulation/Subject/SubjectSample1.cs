
using System.Collections.Generic;
using UnityEngine;

class SubjectSample1 : AbstractSubject, ISubject
{
    protected float subjectRotationModifier = 45.0f;
    protected float subjectSpeedModifier = 20.0f;

    public SubjectSample1(GameObject subjectPrefab, NeuralNetwork neuralNetwork)
    {
        this.subjectGameObject = UnityEngine.Object.Instantiate(subjectPrefab, new Vector3(0, 0, 0), Quaternion.identity); ;
        this.neuralNetwork = neuralNetwork;
    }

    protected override void ApplyOutput()
    {
        List<float> outputList = this.GetOutput();

        float subjectRotation = outputList[0];
        float subjectSpeed = outputList[1];

        this.subjectGameObject.transform.Rotate(0.0f, subjectRotation * Time.deltaTime * this.subjectRotationModifier, 0.0f, Space.Self);
        this.subjectGameObject.transform.Translate(Vector3.forward * subjectSpeed * Time.deltaTime * this.subjectSpeedModifier, Space.Self);
    }
}
