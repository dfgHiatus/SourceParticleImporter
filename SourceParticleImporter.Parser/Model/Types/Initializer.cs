using Datamodel;
using System.Collections.Generic;

namespace SourceParticleImporter.Model.Types;

public class InitializerData
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
    public int SequenceMax { get; set; }
}


public class Initializer
{
    public static List<InitializerData> Setup(ElementArray initializerArray)
    {
        List<InitializerData> result = new();
        foreach (var element in initializerArray)
        {
            InitializerData i = new InitializerData();

            #region Auto-generated
            if (element.TryGetValue("functionName", out object functionName))
                i.FunctionName = (string)functionName;
            if (element.TryGetValue("operatorStartFadein", out object operatorStartFadein))
                i.OperatorStartFadein = (float)operatorStartFadein;
            if (element.TryGetValue("operatorEndFadein", out object operatorEndFadein))
                i.OperatorEndFadein = (float)operatorEndFadein;
            if (element.TryGetValue("operatorStartFadeout", out object operatorStartFadeout))
                i.OperatorStartFadeout = (float)operatorStartFadeout;
            if (element.TryGetValue("operatorEndFadeout", out object operatorEndFadeout))
                i.OperatorEndFadeout = (float)operatorEndFadeout;
            if (element.TryGetValue("operatorFadeOscillate", out object operatorFadeOscillate))
                i.OperatorFadeOscillate = (float)operatorFadeOscillate;
            if (element.TryGetValue("alphaMin", out object alphaMin))
                i.AlphaMin = (int)alphaMin;
            if (element.TryGetValue("alphaMax", out object alphaMax))
                i.AlphaMax = (int)alphaMax;
            if (element.TryGetValue("alphaRandomExponent", out object alphaRandomExponent))
                i.AlphaRandomExponent = (float)alphaRandomExponent;
            if (element.TryGetValue("lifetimeMin", out object lifetimeMin))
                i.LifetimeMin = (float)lifetimeMin;
            if (element.TryGetValue("lifetimeMax", out object lifetimeMax))
                i.LifetimeMax = (float)lifetimeMax;
            if (element.TryGetValue("lifetimeRandomExponent", out object lifetimeRandomExponent))
                i.LifetimeRandomExponent = (float)lifetimeRandomExponent;
            if (element.TryGetValue("distanceMin", out object distanceMin))
                i.DistanceMin = (float)distanceMin;
            if (element.TryGetValue("distanceMax", out object distanceMax))
                i.DistanceMax = (float)distanceMax;
            // if (element.TryGetValue("distanceBias", out object distanceBias))
            //     i.DistanceBias = (Vector3)distanceBias;
            // if (element.TryGetValue("distanceBiasAbsoluteValue", out object distanceBiasAbsoluteValue))
            //     i.DistanceBiasAbsoluteValue = (Vector3)distanceBiasAbsoluteValue;
            if (element.TryGetValue("biasInLocalSystem", out object biasInLocalSystem))
                i.BiasInLocalSystem = (bool)biasInLocalSystem;
            if (element.TryGetValue("controlPointNumber", out object controlPointNumber))
                i.ControlPointNumber = (int)controlPointNumber;
            if (element.TryGetValue("speedMin", out object speedMin))
                i.SpeedMin = (float)speedMin;
            if (element.TryGetValue("speedMax", out object speedMax))
                i.SpeedMax = (float)speedMax;
            if (element.TryGetValue("speedRandomExponent", out object speedRandomExponent))
                i.SpeedRandomExponent = (float)speedRandomExponent;
            // if (element.TryGetValue("speedInLocalCoordinateSystemMin", out object speedInLocalCoordinateSystemMin))
            //     i.SpeedInLocalCoordinateSystemMin = (Vector3)speedInLocalCoordinateSystemMin;
            // if (element.TryGetValue("speedInLocalCoordinateSystemMax", out object speedInLocalCoordinateSystemMax))
            //     i.SpeedInLocalCoordinateSystemMax = (Vector3)speedInLocalCoordinateSystemMax;
            if (element.TryGetValue("createInModel", out object createInModel))
                i.CreateInModel = (int)createInModel;
            if (element.TryGetValue("randomlyDistributeToHighestSuppliedControlPoint", out object randomlyDistributeToHighestSuppliedControlPoint))
                i.RandomlyDistributeToHighestSuppliedControlPoint = (bool)randomlyDistributeToHighestSuppliedControlPoint;
            if (element.TryGetValue("randomlyDistributionGrowthTime", out object randomlyDistributionGrowthTime))
                i.RandomlyDistributionGrowthTime = (float)randomlyDistributionGrowthTime;
            if (element.TryGetValue("radiusMin", out object radiusMin))
                i.RadiusMin = (float)radiusMin;
            if (element.TryGetValue("radiusMax", out object radiusMax))
                i.RadiusMax = (float)radiusMax;
            if (element.TryGetValue("radiusRandomExponent", out object radiusRandomExponent))
                i.RadiusRandomExponent = (float)radiusRandomExponent;
            if (element.TryGetValue("rotationInitial", out object rotationInitial))
                i.RotationInitial = (float)rotationInitial;
            if (element.TryGetValue("rotationOffsetMin", out object rotationOffsetMin))
                i.RotationOffsetMin = (float)rotationOffsetMin;
            if (element.TryGetValue("rotationOffsetMax", out object rotationOffsetMax))
                i.RotationOffsetMax = (float)rotationOffsetMax;
            if (element.TryGetValue("rotationRandomExponent", out object rotationRandomExponent))
                i.RotationRandomExponent = (float)rotationRandomExponent;
            // if (element.TryGetValue("offsetMin", out object offsetMin))
            //     i.OffsetMin = (Vector3)offsetMin;
            // if (element.TryGetValue("offsetMax", out object offsetMax))
            //     i.OffsetMax = (Vector3)offsetMax;
            if (element.TryGetValue("offsetInLocalSpace", out object offsetInLocalSpace))
                i.OffsetInLocalSpace = (bool)offsetInLocalSpace;
            if (element.TryGetValue("offsetProportionalToRadius", out object offsetProportionalToRadius))
                i.OffsetProportionalToRadius = (bool)offsetProportionalToRadius;
            #endregion
        }

        return result;
    }
}