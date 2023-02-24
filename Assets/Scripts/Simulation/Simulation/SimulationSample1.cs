
using System.Collections.Generic;
using UnityEngine;

namespace CreatureSim
{
    public class SimulationSample1 : AbstractSimulation, ISimulation
    {
        protected BrainExchangerSample1 brainExchanger;
        protected int numberOfWorldsToLoad = 5;

        public SimulationSample1(ref BrainExchangerSample1 brainExchanger, int playMode = SimulationSample1.AutoPlayMode) : base()
        {
            this.brainExchanger = brainExchanger;
            this.playMode = playMode;
            base.OnSimulationEnds += this.OnSimulationIsFinished;
        }

        public override void Load()
        {
            WorldSample1 world = new WorldSample1(ref this.brainExchanger);

            this.AddWorld(world);

            base.Load();
            Debug.Log("NumberOfWorldsToLoad: " + numberOfWorldsToLoad.ToString());
        }

        protected override bool CheckSimulationEnd()
        {
            bool simulationEnds = true;
            if (numberOfWorldsToLoad > 0)
            {
                simulationEnds = false;
                this.numberOfWorldsToLoad--;

                base.Unload();
                this.Load();
            }
            return simulationEnds;
        }

        public void OnSimulationIsFinished()
        {
            Debug.Log("OnSimulationIsFinished");
        }


    }
}