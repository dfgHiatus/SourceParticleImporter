﻿using Datamodel;
using Datamodel.XAML;
using System.Collections.Generic;

namespace SourceParticleImporter.Model.Types;

public class ConstraintData
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string FunctionName { get; set; }
    public float OperatorStartFadein { get; set; }
    public float OperatorEndFadein { get; set; }
    public float OperatorStartFadeout { get; set; }
    public float OperatorEndFadeout { get; set; }
    public float OperatorFadeOscillate { get; set; }
    public int CollisionMode { get; set; }
    public float AmountOfBounce { get; set; }
    public float AmountOfSlide { get; set; }
    public float RadiusScale { get; set; }
    public bool BrushOnly { get; set; }
    public string CollisionGroup { get; set; }
    // public Vector3 ControlPointOffsetForFastCollisions { get; set; }
    public float ControlPointMovementDistanceTolerance { get; set; }
    public bool KillParticleOnCollision { get; set; }
    public float TraceAccuracyTolerance { get; set; }
}

public class Constraint
{
    public static List<ConstraintData> Setup(ElementArray constraintArray)
    {
        List<ConstraintData> result = new();
        foreach (var element in constraintArray) 
        {
            ConstraintData c = new ConstraintData();
            c.Name = element.Name;

            #region Auto-generated
            if (element.TryGetValue("functionName", out object functionName))
                c.FunctionName = (string)functionName;
            if (element.TryGetValue("operator start fadein", out object operatorStartFadein))
                c.OperatorStartFadein = (float)operatorStartFadein;
            if (element.TryGetValue("operator end fadein", out object operatorEndFadein))
                c.OperatorEndFadein = (float)operatorEndFadein;
            if (element.TryGetValue("operator start fadeout", out object operatorStartFadeout))
                c.OperatorStartFadeout = (float)operatorStartFadeout;
            if (element.TryGetValue("operator end fadeout", out object operatorEndFadeout))
                c.OperatorEndFadeout = (float)operatorEndFadeout;
            if (element.TryGetValue("operator fade oscillate", out object operatorFadeOscillate))
                c.OperatorFadeOscillate = (float)operatorFadeOscillate;
            if (element.TryGetValue("collision mode", out object collisionMode))
                c.CollisionMode = (int)collisionMode;
            if (element.TryGetValue("amount of bounce", out object amountOfBounce))
                c.AmountOfBounce = (float)amountOfBounce;
            if (element.TryGetValue("amount of slide", out object amountOfSlide))
                c.AmountOfSlide = (float)amountOfSlide;
            if (element.TryGetValue("radius scale", out object radiusScale))
                c.RadiusScale = (float)radiusScale;
            if (element.TryGetValue("brush only", out object brushOnly))
                c.BrushOnly = (bool)brushOnly;
            if (element.TryGetValue("collision group", out object collisionGroup))
                c.CollisionGroup = (string)collisionGroup;
            //if (element.TryGetValue("control point offset for fast collisions", out object controlPointOffsetForFastCollisions))
            //    c.ControlPointOffsetForFastCollisions = (Vector3)controlPointOffsetForFastCollisions;
            if (element.TryGetValue("control point movement distance tolerance", out object controlPointMovementDistanceTolerance))
                c.ControlPointMovementDistanceTolerance = (float)controlPointMovementDistanceTolerance;
            if (element.TryGetValue("kill particle on collision", out object killParticleOnCollision))
                c.KillParticleOnCollision = (bool)killParticleOnCollision;
            if (element.TryGetValue("trace accuracy tolerance", out object traceAccuracyTolerance))
                c.TraceAccuracyTolerance = (float)traceAccuracyTolerance;
            #endregion

            result.Add(c);
        }

        return result;
    }
}
