
using System;
using System.Collections.Generic;

public interface ISubject
{
    public delegate void LifeEndsDelegate();
    public event LifeEndsDelegate OnLifeEnds;

    public List<float> GetOutput();

    public void Load();

    public void Update();

    public bool IsActive();
}
