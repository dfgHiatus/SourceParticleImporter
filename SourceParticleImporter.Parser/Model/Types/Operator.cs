using Datamodel;
using System.Collections.Generic;

namespace SourceParticleImporter.Model.Types;

public class OperatorData
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string FunctionName { get; set; }
    public float OperatorStartFadeIn { get; set; }
    public float OperatorEndFadeIn { get; set; }
    public float OperatorStartFadeOut { get; set; }
    public float OperatorEndFadeOut { get; set; }
    public float OperatorFadeOscillate { get; set; }
    public int SpinRateDegrees { get; set; }
    public float SpinStopTime { get; set; }
    public int SpinRateMin { get; set; }
    public float EndFadeOutTime { get; set; }
    public float StartFadeOutTime { get; set; }
    public float EndFadeInTime { get; set; }
    public float StartFadeInTime { get; set; }
    public float EndAlpha { get; set; }
    public float StartAlpha { get; set; }
}

public class Operator
{
    public static List<OperatorData> Setup(ElementArray operatorArray)
    {
        List<OperatorData> result = new();
        foreach (var element in operatorArray)
        {
            OperatorData o = new OperatorData();

            #region Auto-generated
            if (element.TryGetValue("functionName", out object functionName))
                o.FunctionName = (string)functionName;
            if (element.TryGetValue("operator start fadein", out object operatorStartFadeIn))
                o.OperatorStartFadeIn = (float)operatorStartFadeIn;
            if (element.TryGetValue("operator end fadein", out object operatorEndFadeIn))
                o.OperatorEndFadeIn = (float)operatorEndFadeIn;
            if (element.TryGetValue("operator start fadeout", out object operatorStartFadeOut))
                o.OperatorStartFadeOut = (float)operatorStartFadeOut;
            if (element.TryGetValue("operator end fadeout", out object operatorEndFadeOut))
                o.OperatorEndFadeOut = (float)operatorEndFadeOut;
            if (element.TryGetValue("operator fade oscillate", out object operatorFadeOscillate))
                o.OperatorFadeOscillate = (float)operatorFadeOscillate;
            if (element.TryGetValue("spin rate degrees", out object spinRateDegrees))
                o.SpinRateDegrees = (int)spinRateDegrees;
            if (element.TryGetValue("spin stop time", out object spinStopTime))
                o.SpinStopTime = (float)spinStopTime;
            if (element.TryGetValue("spin rate min", out object spinRateMin))
                o.SpinRateMin = (int)spinRateMin;
            if (element.TryGetValue("end fadeout time", out object endFadeOutTime))
                o.EndFadeOutTime = (float)endFadeOutTime;
            if (element.TryGetValue("start fadeout time", out object startFadeOutTime))
                o.StartFadeOutTime = (float)startFadeOutTime;
            if (element.TryGetValue("end fadein time", out object endFadeInTime))
                o.EndFadeInTime = (float)endFadeInTime;
            if (element.TryGetValue("start fadein time", out object startFadeInTime))
                o.StartFadeInTime = (float)startFadeInTime;
            if (element.TryGetValue("end alpha", out object endAlpha))
                o.EndAlpha = (float)endAlpha;
            if (element.TryGetValue("start alpha", out object startAlpha))
                o.StartAlpha = (float)startAlpha;
            #endregion
        }

        return result;
    }
}
