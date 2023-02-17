
using System;

public class TanhActivation : IActivation
{
    public float Filter(float value)
    {
        return (float) Math.Tanh((float) value);
    }
}
