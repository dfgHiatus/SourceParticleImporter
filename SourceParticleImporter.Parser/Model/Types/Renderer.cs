using Datamodel;
using System.Collections.Generic;

namespace SourceParticleImporter.Model.Types;

public class RendererData
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string FunctionName { get; set; }
    public float OperatorStartFadein { get; set; }
    public float OperatorEndFadein { get; set; }
    public float OperatorStartFadeout { get; set; }
    public float OperatorEndFadeout { get; set; }
    public float OperatorFadeOscillate { get; set; }
    public int VisibilityProxyInputControlPointNumber { get; set; }
    public float VisibilityProxyRadius { get; set; }
    public float VisibilityInputMinimum { get; set; }
    public float VisibilityInputMaximum { get; set; }
    public float VisibilityAlphaScaleMinimum { get; set; }
    public float VisibilityAlphaScaleMaximum { get; set; }
    public float VisibilityRadiusScaleMinimum { get; set; }
    public float VisibilityRadiusScaleMaximum { get; set; }
    public float AnimationRate { get; set; }
    public bool AnimationFitLifetime { get; set; }
    public int OrientationType { get; set; }
    public int OrientationControlPoint { get; set; }
    public float SecondSequenceAnimationRate { get; set; }
    public bool UseAnimationRateAsFps { get; set; }
    public float VisibilityCameraDepthBias { get; set; }
}

public class Renderer
{
    internal static List<RendererData> Setup(ElementArray constraintArray)
    {
        List<RendererData> result = new();
        foreach (var element in constraintArray)
        {
            RendererData r = new RendererData();

            # region Auto-generated
            if (element.TryGetValue("functionName", out object functionName))
                r.FunctionName = (string)functionName;
            if (element.TryGetValue("operator start fadein", out object operatorStartFadein))
                r.OperatorStartFadein = (float)operatorStartFadein;
            if (element.TryGetValue("operator end fadein", out object operatorEndFadein))
                r.OperatorEndFadein = (float)operatorEndFadein;
            if (element.TryGetValue("operator start fadeout", out object operatorStartFadeout))
                r.OperatorStartFadeout = (float)operatorStartFadeout;
            if (element.TryGetValue("operator end fadeout", out object operatorEndFadeout))
                r.OperatorEndFadeout = (float)operatorEndFadeout;
            if (element.TryGetValue("operator fade oscillate", out object operatorFadeOscillate))
                r.OperatorFadeOscillate = (float)operatorFadeOscillate;
            if (element.TryGetValue("Visibility Proxy Input Control Point Number", out object visibilityProxyInputControlPointNumber))
                r.VisibilityProxyInputControlPointNumber = (int)visibilityProxyInputControlPointNumber;
            if (element.TryGetValue("Visibility Proxy Radius", out object visibilityProxyRadius))
                r.VisibilityProxyRadius = (float)visibilityProxyRadius;
            if (element.TryGetValue("Visibility input minimum", out object visibilityInputMinimum))
                r.VisibilityInputMinimum = (float)visibilityInputMinimum;
            if (element.TryGetValue("Visibility input maximum", out object visibilityInputMaximum))
                r.VisibilityInputMaximum = (float)visibilityInputMaximum;
            if (element.TryGetValue("Visibility Alpha Scale minimum", out object visibilityAlphaScaleMinimum))
                r.VisibilityAlphaScaleMinimum = (float)visibilityAlphaScaleMinimum;
            if (element.TryGetValue("Visibility Alpha Scale maximum", out object visibilityAlphaScaleMaximum))
                r.VisibilityAlphaScaleMaximum = (float)visibilityAlphaScaleMaximum;
            if (element.TryGetValue("Visibility Radius Scale minimum", out object visibilityRadiusScaleMinimum))
                r.VisibilityRadiusScaleMinimum = (float)visibilityRadiusScaleMinimum;
            if (element.TryGetValue("Visibility Radius Scale maximum", out object visibilityRadiusScaleMaximum))
                r.VisibilityRadiusScaleMaximum = (float)visibilityRadiusScaleMaximum;
            if (element.TryGetValue("animation rate", out object animationRate))
                r.AnimationRate = (float)animationRate;
            if (element.TryGetValue("animation_fit_lifetime", out object animationFitLifetime))
                r.AnimationFitLifetime = (bool)animationFitLifetime;
            if (element.TryGetValue("orientation_type", out object orientationType))
                r.OrientationType = (int)orientationType;
            if (element.TryGetValue("orientation control point", out object orientationControlPoint))
                r.OrientationControlPoint = (int)orientationControlPoint;
            if (element.TryGetValue("second sequence animation rate", out object secondSequenceAnimationRate))
                r.SecondSequenceAnimationRate = (float)secondSequenceAnimationRate;
            if (element.TryGetValue("use animation rate as FPS", out object useAnimationRateAsFPS))
                r.UseAnimationRateAsFps = (bool)useAnimationRateAsFPS;
            if (element.TryGetValue("Visibility Camera Depth Bias", out object visibilityCameraDepthBias))
                r.VisibilityCameraDepthBias = (float)visibilityCameraDepthBias;
            # endregion

            result.Add(r);
        }

        return result;
    }
}
