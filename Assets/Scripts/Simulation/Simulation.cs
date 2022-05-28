
using System.Collections.Generic;

public class Simulation
{
    public const int AutoPlayMode = 1;
    public const int ManualPlayMode = 2;

    protected int playMode;

    public List<Creature> creatureList;

    public Simulation(int playMode = Simulation.AutoPlayMode)
    {
        this.playMode = playMode;

        this.creatureList = new List<Creature>();
    }

    public void AddCreature(Creature creature)
    {
        creatureList.Add(creature);
    }

}
