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
    public GameObject creatureGameObject;
    public GameObject headRotation;
    public GameObject head;

    public float creatureStepRotationY = 45.0f;
    public float headStepRotationY = 180.0f;
    public float creatureSpeed = 20.0f;

    private Simulation simulation;

    // Start is called before the first frame update
    void Start()
    {
        this.InitSimulation();

        /*this.neuralNetwork.CalculateOutput();
        Debug.Log(this.neuralNetwork.Debug());*/

        /*this.creatureRotationModifier = this.neuralNetwork.outputLayer[0].outputValue;
        //this.headRotationModifier = this.neuralNetwork.outputLayer[1].outputValue;
        this.creatureSpeedDirection = this.neuralNetwork.outputLayer[1].outputValue;*/

        /*Debug.Log("output1: "+this.neuralNetwork.outputLayer[0].outputValue+", output2: " + this.neuralNetwork.outputLayer[1].outputValue + ", output3: " + this.neuralNetwork.outputLayer[2].outputValue);
        */
        /*DirectionProgram program = new DirectionProgram();

        program.Start();*/

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

    private void InitSimulation()
    {
        this.simulation = new Simulation(Simulation.ManualPlayMode);

        Creature creature = this.BuildACreature();
        this.simulation.AddSubject(creature);
    }

    private Creature BuildACreature()
    {
        NeuralNetwork neuralNetwork = new NeuralNetwork(2, 2, 4, 5, true);
        neuralNetwork.inputLayer[0].outputValue = 0.25f;
        neuralNetwork.inputLayer[1].outputValue = -0.5f;

        return new Creature(this.creatureGameObject, neuralNetwork);
    }

    // Update is called once per frame
    void Update()
    {
        this.simulation.Update();

        //ISubject creature = this.simulation.subjectList[0];

        /*Vector3 headRotationCenter = this.headRotation.transform.position;
        Vector3 headOffset = new Vector3(1.5f, 0.0f, 0.0f);

        this.creatureGameObject.transform.Rotate(0.0f, this.creatureStepRotationY * this.creatureRotationModifier * Time.deltaTime, 0.0f, Space.Self);
        this.creatureGameObject.transform.Translate(Vector3.forward * Time.deltaTime * this.creatureSpeedDirection * this.creatureSpeed, Space.Self);
        //this.head.transform.RotateAround(headRotationCenter, Vector3.up, this.headStepRotationY * this.headRotationModifier * Time.deltaTime);*/
    }
}
