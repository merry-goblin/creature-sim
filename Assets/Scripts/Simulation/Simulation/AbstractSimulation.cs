
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSimulation
{
    public List<IWorld> worldList;
    public const int AutoPlayMode = 1;
    public const int ManualPlayMode = 2;

    protected int playMode;

    public virtual void Load()
    {
        for (int i = 0, nb = this.worldList.Count; i < nb; i++)
        {
            this.worldList[i].Load();
        }
    }

    public void Update()
    {
        for (int i = 0, nb = worldList.Count; i < nb; i++)
        {
            this.worldList[i].Update();
        }
    }

    protected void AddWorld(IWorld world)
    {
        this.worldList.Add(world);
    }

}

