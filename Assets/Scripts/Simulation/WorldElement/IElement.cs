
using System.Collections.Generic;

namespace CreatureSim
{
    public interface IElement
    {
        public bool toUnload
        {
            get;
            set;
        }

        public void Load();

        public void Unload();

        public void Update();
    }
}
