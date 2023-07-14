using Datamodel;
using System.Collections.Generic;

namespace SourceParticleImporter.Model.Types;

public class EmitterData
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string FunctionName { get; set; }
    public float OperatorStartFadein { get; set; }
    public float OperatorEndFadein { get; set; }
    public float OperatorStartFadeout { get; set; }
    public float OperatorEndFadeout { get; set; }
    public float OperatorFadeOscillate { get; set; }
    public float EmissionStartTime { get; set; }
    public float EmissionRate { get; set; }
    public float EmissionDuration { get; set; }
    public float ScaleEmissionToUsedControlPoints { get; set; }
}

public class Emitter
{
    public static List<EmitterData> Setup(ElementArray emitterArray)
    {
        List<EmitterData> result = new();
        foreach (var element in emitterArray)
        {
            EmitterData e = new EmitterData();

            #region Auto-generated
            if (element.TryGetValue("functionName", out object functionName))
                e.FunctionName = (string)functionName;
            if (element.TryGetValue("operator start fadein", out object operatorStartFadein))
                e.OperatorStartFadein = (float)operatorStartFadein;
            if (element.TryGetValue("operator end fadein", out object operatorEndFadein))
                e.OperatorEndFadein = (float)operatorEndFadein;
            if (element.TryGetValue("operator start fadeout", out object operatorStartFadeout))
                e.OperatorStartFadeout = (float)operatorStartFadeout;
            if (element.TryGetValue("operator end fadeout", out object operatorEndFadeout))
                e.OperatorEndFadeout = (float)operatorEndFadeout;
            if (element.TryGetValue("operator fade oscillate", out object operatorFadeOscillate))
                e.OperatorFadeOscillate = (float)operatorFadeOscillate;
            if (element.TryGetValue("emission start time", out object emissionStartTime))
                e.EmissionStartTime = (float)emissionStartTime;
            if (element.TryGetValue("emission rate", out object emissionRate))
                e.EmissionRate = (float)emissionRate;
            if (element.TryGetValue("emission duration", out object emissionDuration))
                e.EmissionDuration = (float)emissionDuration;
            if (element.TryGetValue("scale emission to used control points", out object scaleEmissionToUsedControlPoints))
                e.ScaleEmissionToUsedControlPoints = (float)scaleEmissionToUsedControlPoints;
            #endregion
        }

        return result;
    }
}
