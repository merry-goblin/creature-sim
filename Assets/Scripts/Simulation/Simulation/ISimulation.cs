using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatureSim
{
    public interface ISimulation
    {
        public List<IWorld> worldList
        {
            get;
            set;
        }
        public List<IWorld> inactiveWorldList
        {
            get;
            set;
        }

        public delegate void SimulationEndsDelegate();
        public event SimulationEndsDelegate OnSimulationEnds;

        public delegate void NoMoreActiveWorldsDelegate();
        public event NoMoreActiveWorldsDelegate OnNoMoreActiveWorlds;

        void Load();

        void Unload();

        void Update();

        bool IsActive();
    }
}