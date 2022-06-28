
using System;
using System.Collections.Generic;

public interface ISubject
{
    public delegate void LifeEndsDelegate();
    public event LifeEndsDelegate OnLifeEnds;

    public List<float> GetOutput();

    void Load();

    public void Update();
}
