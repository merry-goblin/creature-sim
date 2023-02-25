
using System.Collections.Generic;
using UnityEngine;

namespace CreatureSim
{
    public abstract class AbstractElement
    {
        protected GameObject gameObject;
        protected static int currentId = 0;

        public bool toUnload
        {
            get;
            set;
        }

        public AbstractElement()
        {
            this.toUnload = false;
        }

        public virtual void Load()
        {

        }

        public virtual void Unload()
        {

        }

        public virtual void Update()
        {
        }

        protected static int PeekNewId()
        {
            return AbstractElement.currentId++;
        }
    }
}
