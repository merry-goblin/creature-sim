
using System.Collections.Generic;
using UnityEngine;

class CycleManagerSample1 : AbstractCycleManager, ICycleManager
{
    public CycleManagerSample1() : base()
    {
        base.OnOneCycleEnds += this.OnOneCycleIsNoMoreActive;
    }

    protected override void LoadFirstCycle()
    {
        ISimulation simulation = new SimulationSample1(SimulationSample1.ManualPlayMode);
        this.NewCycle(simulation);

        base.Load();
    }

    protected override void LoadNextCycle()
    {
        this.LoadFirstCycle();
    }

    public void OnOneCycleIsNoMoreActive()
    {
        Debug.Log("OnOneCycleIsNoMoreActive");
        this.LoadNextCycle();

        // @todo: finish Load method the call Load here
    }
}
