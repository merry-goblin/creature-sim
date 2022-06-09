
using System.Collections.Generic;

public class Simulation : ISimulation
{
    public const int AutoPlayMode = 1;
    public const int ManualPlayMode = 2;

    protected int playMode;

    public List<IWorld> worldList;

    public Simulation(int playMode = Simulation.AutoPlayMode)
    {
        this.playMode = playMode;

        this.worldList = new List<IWorld>();
    }

    public void AddWorld(IWorld world)
    {
        this.worldList.Add(world);
    }

    public void Update()
    {
        for (int i=0, nb=worldList.Count; i < nb; i++)
        {
            this.worldList[i].Update();
        }
    }
}
