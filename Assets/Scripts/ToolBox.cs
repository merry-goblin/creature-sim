
using System;

public class ToolBox
{
    private static Random random = null;

    public static float GetRandomFloat(float start, float end)
    {
        //  Init random static property if needed
        ToolBox.InitRandom();

        float randomValue = (float) ToolBox.random.NextDouble();
        randomValue *= (end - start);
        randomValue += start;

        return randomValue;
    }
    
    private static void InitRandom()
    {
        if (random == null)
        {
            ToolBox.random = new Random();
        }
    }

    public static float Sigmoid(float value)
    {
        return 1.0f / (1.0f + (float)Math.Exp((float)-value));
    }

    public static float SigmoidDerivative(float value)
    {
        float sigmoid = ToolBox.Sigmoid(value);
        return sigmoid * (1 - sigmoid);
    }
}
