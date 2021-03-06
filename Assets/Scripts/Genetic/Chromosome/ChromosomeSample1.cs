
using GeneticSharp.Domain.Chromosomes;

public class ChromosomeSample1 : FloatingPointChromosome
{
    public double lifespan { get; set; }
    public int foodFound { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:GeneticSharp.Domain.Chromosomes.FloatingPointChromosome"/> class.
    /// </summary>
    /// <param name="minValue">Minimum value.</param>
    /// <param name="maxValue">Max value.</param>
    /// <param name="totalBits">Total bits.</param>
    /// <param name="fractionDigits">Fraction digits.</param>
    /// /// <param name="geneValues">Gene values.</param>
    public ChromosomeSample1(double[] minValue, double[] maxValue, int[] totalBits, int[] fractionDigits, double[] geneValues)
        : base(minValue, maxValue, totalBits, fractionDigits, geneValues)
    {
        this.lifespan = 0.0;
        this.foodFound = 0;
    }
}
