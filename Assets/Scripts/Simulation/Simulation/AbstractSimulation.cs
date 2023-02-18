
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

    protected bool active
    {
        get;
        set;
    }

    public AbstractSimulation()
    {
        this.worldList = new List<IWorld>();       // Loaded worlds
        this.worldListToLoad = new List<IWorld>(); // Not loaded yet

        this.active = false;
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
            this.worldListToLoad[i].OnNoMoreActiveSubjects += this.OnWorldIsNoMoreActive; // We will be informed any time one of this simulation's world dies
        }
        this.worldListToLoad.Clear();

        //  Simulation is active if at least one subject is active
        if (this.worldList.Count > 0)
        {
            this.active = true;
        }
    }

    public virtual void Unload()
    {
        this.active = false;
        for (int i = 0, nb = this.worldList.Count; i < nb; i++)
        {
            this.worldList[i].Unload();
        }
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

    public bool IsActive()
    {
        return this.active;
    }

    /**
     * One subject of this world is no more active
     * This method checks if there is still some subjets active
     * If not we call EndSimulationForThisWorldSubjects()
     * Could be overridden if needed by child class
     */
    protected virtual void OnWorldIsNoMoreActive()
    {
        bool oneWorldIsStillActive = false;
        for (int i = 0, nb = this.worldList.Count; i < nb; i++)
        {
            if (this.worldList[i].IsActive())
            {
                oneWorldIsStillActive = true;
                break;
            }
        }

        if (!oneWorldIsStillActive)
        {
            this.EndSimulationForCurrentGroupOfSubjects();
        }
    }

    protected void AddWorld(IWorld world)
    {
        this.worldListToLoad.Add(world);
    }

}

