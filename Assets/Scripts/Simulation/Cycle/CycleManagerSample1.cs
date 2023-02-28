
using System.Collections.Generic;
using UnityEngine;
using GA = Learning.GeneticAlgorithm;

namespace CreatureSim
{
    class CycleManagerSample1 : AbstractCycleManager, ICycleManager
    {
        protected BrainExchangerSample1 brainExchanger;

        public CycleManagerSample1() : base()
        {
            base.OnOneCycleEnds += this.OnOneCycleIsNoMoreActive;
        }

        protected override void LoadFirstCycle()
        {
            this.brainExchanger = new BrainExchangerSample1(); // Won't contain any history to help building efficiant brains
            ISimulation simulation = new SimulationSample1(ref this.brainExchanger, SimulationSample1.ManualPlayMode);
            this.NewCycle(simulation);
            base.Load();
        }

        protected override void LoadNextCycle()
        {
            this.brainExchanger = new BrainExchangerSample1(); // Retrieves brains of the simulation which has juste end in order to build more efficiant brains
            ISimulation previousSimulation = this.simulation;

            //  Next simulation
            ISimulation simulation = new SimulationSample1(ref this.brainExchanger, SimulationSample1.ManualPlayMode);
            this.NewCycle(simulation);

            //  Take any brains of unloaded subjects and export them
            this.FeedBrainExchangerWithPreviousSimulation(previousSimulation);

            //  Load a new simulation
            base.Load();
        }

        protected void FeedBrainExchangerWithPreviousSimulation(ISimulation previousSimulation)
        {
            for (int worldIndex = 0, worldNb = previousSimulation.inactiveWorldList.Count; worldIndex < worldNb; worldIndex++)
            {
                for (int subjectIndex = 0, subjectNb = previousSimulation.inactiveWorldList[worldIndex].inactiveSubjectList.Count; subjectIndex < subjectNb; subjectIndex++)
                {
                    this.brainExchanger.AddSimSubject((SubjectSample1)previousSimulation.inactiveWorldList[worldIndex].inactiveSubjectList[subjectIndex]);
                }
            }
        }
        /*protected GA.GeneticAlgorithm InitializeGeneticAlgorithm()
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

            GA.Mutation gaMutation = new GA.Mutation();

            GA.GeneticAlgorithm geneticAlgorithm = new GA.GeneticAlgorithm(gaPopulation, gaSelection, gaCrossover, gaMutation);

            return geneticAlgorithm;
        }*/


        public void OnOneCycleIsNoMoreActive()
        {
            //Debug.Log("OnOneCycleIsNoMoreActive");
            this.LoadNextCycle();

            // @todo: finish Load method the call Load here
        }
    }
}