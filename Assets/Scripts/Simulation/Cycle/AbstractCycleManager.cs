
using System.Collections.Generic;

/**
 * Handle simulation cycles
 */
public abstract class AbstractCycleManager
{
    public List<ISimulation> simulationListHistory; 
    public ISimulation simulation; // Current simulation
    public ISimulation simulationToLoad; // Not loaded yet

    public event ICycleManager.SimulationEndsDelegate OnOneCycleEnds;

    private bool isStarted = false;

    public AbstractCycleManager()
    {
        this.simulationListHistory = new List<ISimulation>();
    }

    public void Start()
    {
        if (this.isStarted)
        {
            throw new System.Exception("Start cannot be called a second time");
        }
        this.isStarted = true;

        this.LoadFirstCycle();
    }

    protected void Load()
    {
        if (this.simulationToLoad != null)
        {
            this.simulation = this.simulationToLoad;
            this.simulation.Load();
            this.simulation.OnNoMoreActiveWorlds += this.OnSimulationIsNoMoreActive; // We will be informed when a simulation ends
            this.simulationToLoad = null;
        }
    }

    public void Update()
    {
        if (this.simulation != null)
        {
            this.simulation.Update();
        }
    }

    /**
     * First cycle expects a simulation without background,
     * so this one will be build differently
     * It is expected to call at least NewCycle then Load in this method
     */
    protected abstract void LoadFirstCycle();

    /**
     * Other cycles expect a simulation with a background,
     * so those next simulations will be build based on what has been learned in previous simulations
     * It is expected to call at least NewCycle then Load in this method
     */
    protected abstract void LoadNextCycle();

    /**
     * One subject of this world is no more active
     * This method checks if there is still some subjets active
     * If not we call EndSimulationForThisWorldSubjects()
     * Could be overridden if needed by child class
     */
    protected virtual void OnSimulationIsNoMoreActive()
    {
        if (this.OnOneCycleEnds != null)
        {
            this.OnOneCycleEnds(); // Event
        }
    }

    protected void NewCycle(ISimulation simulation)
    {
        //  Previous simulation is stored in history
        if (this.simulation != null)
        {
            this.simulationListHistory.Add(this.simulation);
            this.simulation.Unload();
            this.simulation = null;
        }

        //  A new simulation is ready to be loaded
        this.simulationToLoad = simulation;
    }

}
