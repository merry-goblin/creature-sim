
using System.Collections.Generic;

public class SampleSimulation1 : AbstractSimulation, ISimulation
{

    public SampleSimulation1(int playMode = SampleSimulation1.AutoPlayMode)
    {
        this.playMode = playMode;

        this.worldList = new List<IWorld>();
    }

    public override void Load()
    {
        SampleWorld1 world = new SampleWorld1();

        this.AddWorld(world);

        base.Load();
    }
}
