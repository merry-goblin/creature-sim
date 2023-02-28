using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface INeuralNetwork
{
    public void ApplyInputValues(List<float> values);

    public List<float> GetOutputValues();

    public void CalculateOutput();

    public List<float> ExportWeights();

    public List<float> ImportWeights(List<float> importList);
}
