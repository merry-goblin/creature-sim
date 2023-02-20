
using System.Collections.Generic;
using UnityEngine;

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
            //  Previous simulation
            INeuralNetwork previousNeuralNetwork = this.simulation.worldList[0].subjectList[0].neuralNetwork;
            List<float> exportedBrain = previousNeuralNetwork.ExportWeights();

            //  Learning
            List<float> brainToImport = this.ApplyGeneticAlgorithm(exportedBrain);

            //  Next simulation
            ISimulation simulation = new SimulationSample1(SimulationSample1.ManualPlayMode);
            this.NewCycle(simulation);
            base.Load();

            //  Apply learning
            INeuralNetwork nextNeuralNetwork = simulation.worldList[0].subjectList[0].neuralNetwork;
            nextNeuralNetwork.ImportWeights(ref brainToImport);
        }

        protected List<float> ApplyGeneticAlgorithm(List<float> exportedBrain)
        {


            return exportedBrain; // @todo: To change
        }

        public void OnOneCycleIsNoMoreActive()
        {
            Debug.Log("OnOneCycleIsNoMoreActive");
            this.LoadNextCycle();

            // @todo: finish Load method the call Load here
        }
    }
}