
public class Synapse
{
    public Neuron axon; // presynaptic button
    public Neuron dendrite; // postsynaptic button
    public float weight;

    public Synapse(bool initRandomly)
    {
        this.weight = (initRandomly) ? ToolBox.GetRandomFloat(-0.99f, 0.99f) : 0.0f;
    }
}
