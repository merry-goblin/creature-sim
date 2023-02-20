
using System;
using System.Collections.Generic;

namespace CreatureSim
{
    public interface ISubject
    {
        public INeuralNetwork neuralNetwork
        {
            get;
        }

        public delegate void LifeEndsDelegate();
        public event LifeEndsDelegate OnLifeEnds;

        public List<float> GetOutput();

        public void Load();

        public void Unload();

        public void Update();

        public bool IsActive();

        public float Fitness();
    }
}
