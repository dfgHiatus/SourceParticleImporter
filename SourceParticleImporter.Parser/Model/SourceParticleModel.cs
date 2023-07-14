using SourceParticleImporter.Model.Types;
using System;
using System.Collections.Generic;

public class SourceParticle
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<DmeParticleSystemDefinition> ParticleSystemDefinitions { get; set; }
}

public class DmeParticleSystemDefinition
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<RendererData> Renderers { get; set; }
    public List<OperatorData> Operators { get; set; }
    public List<InitializerData> Initializers { get; set; }
    public List<EmitterData> Emitters { get; set; }
    public List<ChildrenData> Children { get; set; }
    public List<ConstraintData> Constraints { get; set; }
    // public List<ForceData> Forces { get; set; }
    public bool PreventNameBasedLookup { get; set; }
    public int MaxParticles { get; set; }
    public int InitialParticles { get; set; }
    public string Material { get; set; }
    // public Vector3 BoundingBoxMin { get; set; }
    // public Vector3 BoundingBoxMax { get; set; }
    public float CullRadius { get; set; }
    public float CullCost { get; set; }
    public int CullControlPoint { get; set; }
    public string CullReplacementDefinition { get; set; }
    public float Radius { get; set; }
    // public Vector4 Color { get; set; }
    public float Rotation { get; set; }
    public float RotationSpeed { get; set; }
    public int SequenceNumber { get; set; }
    public int SequenceNumber1 { get; set; }
    public int GroupId { get; set; }
    public float MaximumTimeStep { get; set; }
    public float MaximumSimTickRate { get; set; }
    public float MinimumSimTickRate { get; set; }
    public int MinimumRenderedFrames { get; set; }
    public int ControlPointToDisableRenderingIfItIsTheCamera { get; set; }
    public float MaximumDrawDistance { get; set; }
    public float TimeToSleepWhenNotDrawn { get; set; }
    public bool SortParticles { get; set; }
    public bool BatchParticleSystems { get; set; }
    public bool ViewModelEffect { get; set; }
}
