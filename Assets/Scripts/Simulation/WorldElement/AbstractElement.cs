
using System.Collections.Generic;
using UnityEngine;

namespace CreatureSim
{
    public abstract class AbstractElement
    {
        protected GameObject gameObject;

        public virtual void Load()
        {

        }

        public virtual void Unload()
        {

        }

        public virtual void Update()
        {
        }
    }
}
