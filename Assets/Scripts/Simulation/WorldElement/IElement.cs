
using System.Collections.Generic;

namespace CreatureSim
{
    public interface IElement
    {
        public void Load();

        public void Unload();

        public void Update();
    }
}
