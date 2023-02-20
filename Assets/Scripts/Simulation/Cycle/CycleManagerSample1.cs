
using System.Collections.Generic;
using UnityEngine;
using GA = Learning.GeneticAlgorithm;

namespace CreatureSim
{
    class CycleManagerSample1 : AbstractCycleManager, ICycleManager
    {
        public CycleManagerSample1() : base()
        {
            base.OnOneCycleEnds += this.OnOneCycleIsNoMoreActive;
        }

        protected override void LoadFirstCycle()
        {
            ISimulation simulation = new SimulationSample1(SimulationSample1.ManualPlayMode);
            this.NewCycle(simulation);
            base.Load();
        }

        protected override void LoadNextCycle()
        {
            GA.GeneticAlgorithm geneticAlgorithm = this.InitializeGeneticAlgorithm();

            //  Next simulation
            ISimulation simulation = new SimulationSample1(SimulationSample1.ManualPlayMode);
            this.NewCycle(simulation);
            base.Load();

            this.ApplyGeneticAlgorithm(geneticAlgorithm);

            /*//  Apply learning
            INeuralNetwork nextNeuralNetwork = simulation.worldList[0].subjectList[0].neuralNetwork;
            nextNeuralNetwork.ImportWeights(ref brainToImport);*/
        }

        protected GA.GeneticAlgorithm InitializeGeneticAlgorithm()
        {
            //  Simulation
            ISubject simSubject = this.simulation.worldList[0].subjectList[0];
            INeuralNetwork previousNeuralNetwork = simSubject.neuralNetwork;

            //  Genetic Algorithm
            GA.FloatChromosome gaChromosome = new GA.FloatChromosome(previousNeuralNetwork.ExportWeights());

            GA.Subject gaSubject = new GA.Subject();
            gaSubject.score = simSubject.Fitness();
            gaSubject.chromosome = gaChromosome;

            GA.Population gaPopulation = new GA.Population();
            gaPopulation.AddSubject(gaSubject);

            GA.RouletteWheelSelection gaSelection = new GA.RouletteWheelSelection();

            GA.Crossover gaCrossover = new GA.Crossover();

            GA.GeneticAlgorithm geneticAlgorithm = new GA.GeneticAlgorithm(gaPopulation, gaSelection, gaCrossover);

            return geneticAlgorithm;
        }

        protected void ApplyGeneticAlgorithm(GA.GeneticAlgorithm geneticAlgorithm)
        {
            for (int worldIndex = 0, worldNb = this.simulation.worldList.Count; worldIndex < worldNb; worldIndex++)
            {
                for (int subjectIndex = 0, subjectNb = this.simulation.worldList[worldIndex].subjectList.Count; subjectIndex < subjectNb; subjectIndex++)
                {
                    List<float> brainToImport = geneticAlgorithm.BreedANewSubject();
                    this.simulation.worldList[worldIndex].subjectList[subjectIndex].neuralNetwork.ImportWeights(brainToImport);
                }
            }
        }

        public void OnOneCycleIsNoMoreActive()
        {
            Debug.Log("OnOneCycleIsNoMoreActive");
            this.LoadNextCycle();

            // @todo: finish Load method the call Load here
        }
    }
}