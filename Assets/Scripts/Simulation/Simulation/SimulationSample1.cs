
using System.Collections.Generic;

public class SimulationSample1 : AbstractSimulation, ISimulation
{

    public SimulationSample1(int playMode = SimulationSample1.AutoPlayMode) : base()
    {
        this.playMode = playMode;
    }

    public override void Load()
    {
        WorldSample1 world = new WorldSample1();

        this.AddWorld(world);

        base.Load();
    }


}
