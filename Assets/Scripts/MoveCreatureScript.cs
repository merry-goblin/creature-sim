using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GeneticSharp.Domain;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Terminations;

public class MoveCreatureScript : MonoBehaviour
{
    public GameObject creature;
    public GameObject headRotation;
    public GameObject head;

    public float creatureStepRotationY = 45.0f;
    public float headStepRotationY = 180.0f;

    private NeuralNetwork neuralNetwork;

    private float creatureRotationModifier = 1.0f;
    private float headRotationModifier = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        this.neuralNetwork = new NeuralNetwork(2, 2, 4, 5, true);
        this.neuralNetwork.inputLayer[0].outputValue = 0.25f;
        this.neuralNetwork.inputLayer[1].outputValue = -0.5f;

        this.neuralNetwork.CalculateOutput();
        Debug.Log(this.neuralNetwork.Debug());

        this.creatureRotationModifier = this.neuralNetwork.outputLayer[0].outputValue;
        this.headRotationModifier = this.neuralNetwork.outputLayer[1].outputValue;

        Debug.Log("output1: "+this.neuralNetwork.outputLayer[0].outputValue+", output2: " + this.neuralNetwork.outputLayer[1].outputValue);

        DirectionProgram program = new DirectionProgram();

        program.Start();

        /*AdditionProgram program = new AdditionProgram();

        program.Start();*/

        /*var selection = new EliteSelection();
        var crossover = new OnePointCrossover(0);
        var mutation = new UniformMutation(true);
        var fitness = new Issue1Fitness();
        var chromosome = new Issue1Chromosome();
        var population = new Population(50, 50, chromosome);

        var ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
        ga.Termination = new GenerationNumberTermination(100);

        Debug.Log("GA running...");
        ga.Start();
        Debug.Log(String.Concat("GA done in ", ga.GenerationsNumber.ToString(), " generations."));

        var bestChromosome = ga.BestChromosome as Issue1Chromosome;
        Debug.Log(String.Concat("Best solution found is X:", bestChromosome.X.ToString(), " Y:", bestChromosome.Y.ToString(), " with ", bestChromosome.Fitness.ToString(), " fitness."));*/
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 headRotationCenter = this.headRotation.transform.position;
        Vector3 headOffset = new Vector3(1.5f, 0.0f, 0.0f);

        this.creature.transform.Rotate(0.0f, this.creatureStepRotationY * this.creatureRotationModifier * Time.deltaTime, 0.0f, Space.Self);
        this.head.transform.RotateAround(headRotationCenter, Vector3.up, this.headStepRotationY * this.headRotationModifier * Time.deltaTime);
    }
}
