
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
}
