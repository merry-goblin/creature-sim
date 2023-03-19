
using System;

public class SigmoidActivation : IActivation
{
    public float Filter(float value)
    {
        return ToolBox.Sigmoid(value);
    }

    public float UnfilterDerivative(float value)
    {
        return ToolBox.SigmoidDerivative(value);
    }

}
