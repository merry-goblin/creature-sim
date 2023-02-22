
using System.Collections.Generic;
using UnityEngine;

namespace CreatureSim
{
    public class SimulationSample1 : AbstractSimulation, ISimulation
    {
        protected int numberOfWorlToLoad = 5;

        public SimulationSample1(int playMode = SimulationSample1.AutoPlayMode) : base()
        {
            this.playMode = playMode;
            base.OnSimulationEnds += this.OnSimulationIsFinished;
        }

        public override void Load()
        {
            WorldSample1 world = new WorldSample1();

            this.AddWorld(world);

            base.Load();
        }

        protected override bool CheckSimulationEnd()
        {
            bool simulationEnds = true;
            /*if (numberOfWorlToLoad > 0)
            {
                simulationEnds = false;
                this.numberOfWorlToLoad--;

                base.Unload();
                this.Load();
            }*/
            return simulationEnds;
        }

        public void OnSimulationIsFinished()
        {
            Debug.Log("OnSimulationIsFinished");
        }


    }
}