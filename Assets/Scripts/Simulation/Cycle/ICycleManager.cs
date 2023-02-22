using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatureSim
{
    public interface ICycleManager
    {
        public delegate void OneCycleEndsDelegate();
        public event OneCycleEndsDelegate OnOneCycleEnds;

        void Start();

        void Update();

    }
}