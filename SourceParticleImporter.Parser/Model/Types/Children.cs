using Datamodel;
using System.Collections.Generic;

namespace SourceParticleImporter.Model.Types;

public class ChildrenData
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string FunctionName { get; set; }
    public float OperatorStartFadein { get; set; }
    public float OperatorEndFadein { get; set; }
    public float OperatorStartFadeout { get; set; }
    public float OperatorEndFadeout { get; set; }
    public float OperatorFadeOscillate { get; set; }
    public int AlphaMin { get; set; }
    public int AlphaMax { get; set; }
    public float AlphaRandomExponent { get; set; }
    public float LifetimeMin { get; set; }
    public float LifetimeMax { get; set; }
    public float LifetimeRandomExponent { get; set; }
    public float DistanceMin { get; set; }
    public float DistanceMax { get; set; }
    // public Vector3 DistanceBias { get; set; }
    // public Vector3 DistanceBiasAbsoluteValue { get; set; }
    public bool BiasInLocalSystem { get; set; }
    public int ControlPointNumber { get; set; }
    public float SpeedMin { get; set; }
    public float SpeedMax { get; set; }
    public float SpeedRandomExponent { get; set; }
    // public Vector3 SpeedInLocalCoordinateSystemMin { get; set; }
    // public Vector3 SpeedInLocalCoordinateSystemMax { get; set; }
    public int CreateInModel { get; set; }
    public bool RandomlyDistributeToHighestSuppliedControlPoint { get; set; }
    public float RandomlyDistributionGrowthTime { get; set; }
    public float RadiusMin { get; set; }
    public float RadiusMax { get; set; }
    public float RadiusRandomExponent { get; set; }
    public float RotationInitial { get; set; }
    public float RotationOffsetMin { get; set; }
    public float RotationOffsetMax { get; set; }
    public float RotationRandomExponent { get; set; }
    // public Vector3 OffsetMin { get; set; }
    // public Vector3 OffsetMax { get; set; }
    public bool OffsetInLocalSpace { get; set; }
    public bool OffsetProportionalToRadius { get; set; }
    public float TintUpdateMovementThreshold { get; set; }
    // public Vector4 TintClampMax { get; set; }
    // public Vector4 TintClampMin { get; set; }
    public int TintControlPoint { get; set; }
    public float TintPerc { get; set; }
    // public Vector4 Color2 { get; set; }
    // public Vector4 Color1 { get; set; }
    public int SequenceMin { get; set; }
}

public class Children
{
    public static List<ChildrenData> Setup(ElementArray childrenArray) // TODO: Add vectors
    {
        List<ChildrenData> result = new();
        foreach (var element in childrenArray)
        {
            ChildrenData c = new ChildrenData();

            #region Auto-generated
            if (element.TryGetValue("functionName", out object functionName))
                c.FunctionName = (string)functionName;
            if (element.TryGetValue("operatorStartFadein", out object operatorStartFadein))
                c.OperatorStartFadein = (float)operatorStartFadein;
            if (element.TryGetValue("operatorEndFadein", out object operatorEndFadein))
                c.OperatorEndFadein = (float)operatorEndFadein;
            if (element.TryGetValue("operatorStartFadeout", out object operatorStartFadeout))
                c.OperatorStartFadeout = (float)operatorStartFadeout;
            if (element.TryGetValue("operatorEndFadeout", out object operatorEndFadeout))
                c.OperatorEndFadeout = (float)operatorEndFadeout;
            if (element.TryGetValue("operatorFadeOscillate", out object operatorFadeOscillate))
                c.OperatorFadeOscillate = (float)operatorFadeOscillate;
            if (element.TryGetValue("alphaMin", out object alphaMin))
                c.AlphaMin = (int)alphaMin;
            if (element.TryGetValue("alphaMax", out object alphaMax))
                c.AlphaMax = (int)alphaMax;
            if (element.TryGetValue("alphaRandomExponent", out object alphaRandomExponent))
                c.AlphaRandomExponent = (float)alphaRandomExponent;
            if (element.TryGetValue("lifetimeMin", out object lifetimeMin))
                c.LifetimeMin = (float)lifetimeMin;
            if (element.TryGetValue("lifetimeMax", out object lifetimeMax))
                c.LifetimeMax = (float)lifetimeMax;
            if (element.TryGetValue("lifetimeRandomExponent", out object lifetimeRandomExponent))
                c.LifetimeRandomExponent = (float)lifetimeRandomExponent;
            if (element.TryGetValue("distanceMin", out object distanceMin))
                c.DistanceMin = (float)distanceMin;
            if (element.TryGetValue("distanceMax", out object distanceMax))
                c.DistanceMax = (float)distanceMax;
            if (element.TryGetValue("biasInLocalSystem", out object biasInLocalSystem))
                c.BiasInLocalSystem = (bool)biasInLocalSystem;
            if (element.TryGetValue("controlPointNumber", out object controlPointNumber))
                c.ControlPointNumber = (int)controlPointNumber;
            if (element.TryGetValue("speedMin", out object speedMin))
                c.SpeedMin = (float)speedMin;
            if (element.TryGetValue("speedMax", out object speedMax))
                c.SpeedMax = (float)speedMax;
            if (element.TryGetValue("speedRandomExponent", out object speedRandomExponent))
                c.SpeedRandomExponent = (float)speedRandomExponent;
            if (element.TryGetValue("createInModel", out object createInModel))
                c.CreateInModel = (int)createInModel;
            if (element.TryGetValue("randomlyDistributeToHighestSuppliedControlPoint", out object randomlyDistributeToHighestSuppliedControlPoint))
                c.RandomlyDistributeToHighestSuppliedControlPoint = (bool)randomlyDistributeToHighestSuppliedControlPoint;
            if (element.TryGetValue("randomlyDistributionGrowthTime", out object randomlyDistributionGrowthTime))
                c.RandomlyDistributionGrowthTime = (float)randomlyDistributionGrowthTime;
            if (element.TryGetValue("radiusMin", out object radiusMin))
                c.RadiusMin = (float)radiusMin;
            if (element.TryGetValue("radiusMax", out object radiusMax))
                c.RadiusMax = (float)radiusMax;
            if (element.TryGetValue("radiusRandomExponent", out object radiusRandomExponent))
                c.RadiusRandomExponent = (float)radiusRandomExponent;
            if (element.TryGetValue("rotationInitial", out object rotationInitial))
                c.RotationInitial = (float)rotationInitial;
            if (element.TryGetValue("rotationOffsetMin", out object rotationOffsetMin))
                c.RotationOffsetMin = (float)rotationOffsetMin;
            if (element.TryGetValue("rotationOffsetMax", out object rotationOffsetMax))
                c.RotationOffsetMax = (float)rotationOffsetMax;
            if (element.TryGetValue("rotationRandomExponent", out object rotationRandomExponent))
                c.RotationRandomExponent = (float)rotationRandomExponent;
            if (element.TryGetValue("offsetInLocalSpace", out object offsetInLocalSpace))
                c.OffsetInLocalSpace = (bool)offsetInLocalSpace;
            if (element.TryGetValue("offsetProportionalToRadius", out object offsetProportionalToRadius))
                c.OffsetProportionalToRadius = (bool)offsetProportionalToRadius;
            #endregion
        }

        return result;
    }
}
