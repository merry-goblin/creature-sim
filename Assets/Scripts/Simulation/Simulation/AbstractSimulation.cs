
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSimulation
{
    public List<IWorld> worldList;
    public List<IWorld> worldListToLoad;
    public const int AutoPlayMode = 1;
    public const int ManualPlayMode = 2;

    protected int playMode;

    public event ISimulation.NoMoreActiveWorldsDelegate OnNoMoreActiveWorlds;

    public AbstractSimulation()
    {
        this.worldList = new List<IWorld>();       // Loaded worlds
        this.worldListToLoad = new List<IWorld>(); // Not loaded yet
    }

    /**
     * Tip: Override this method to add one or more worlds in it then call base.Load()
     */
    public virtual void Load()
    {
        for (int i = 0, nb = this.worldListToLoad.Count; i < nb; i++)
        {
            this.worldListToLoad[i].Load();
            this.worldList.Add(this.worldListToLoad[i]);
        }
        this.worldListToLoad.Clear();
    }

    public void Update()
    {
        for (int i = 0, nb = worldList.Count; i < nb; i++)
        {
            this.worldList[i].Update();
        }
    }

    /**
     * End simulation for current group of subjects
     * To use when no more subject are active
     * Will raise an event
     */
    public void EndSimulationForCurrentGroupOfSubjects()
    {
        if (this.OnNoMoreActiveWorlds != null)
        {
            this.OnNoMoreActiveWorlds(); // Event
        }
    }

    protected void AddWorld(IWorld world)
    {
        this.worldListToLoad.Add(world);
    }

}

