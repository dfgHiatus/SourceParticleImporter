using Datamodel;
using SourceParticleImporter.Model.Types;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DM = Datamodel.Datamodel;

namespace SourceParticleImporter.Model;

internal class Source2NeosParticle
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filePath">The file path to parse into a Neos-ready particle system. Assumes one particle per file</param>
    /// <returns></returns>
    internal static async Task<SourceParticle> SetupSourceParticle(string filePath)
    {
        // Comment this out for testing
        var textPCF = await Utils.ConvertPCFToText(
                        filePath,
                        SourceParticleImporter.Config.GetValue(SourceParticleImporter.DMXConverterPath));

        SourceParticle dmeElement = new SourceParticle();

        using (FileStream fileStream = File.OpenRead(textPCF))
        {
            var dm = DM.Load(fileStream);

            // These should always be here
            dmeElement.Name = dm.Root.Name;
            dmeElement.Id = dm.Root.ID;

            // We assume the particle systems are always defined under "particleSystemDefinition".
            // This will throw an exception if this is not present
            var particleSystemDefinitions = dm.Root.Get<ElementArray>("particleSystemDefinitions");
            
            var def = SetupParticleDefinition(particleSystemDefinitions);
            dmeElement.ParticleSystemDefinitions = def;
        }

        return dmeElement;
    }

    private static List<DmeParticleSystemDefinition> SetupParticleDefinition(ElementArray sourceDefinition)
    {
        var particleSystemDefinitions = new List<DmeParticleSystemDefinition>();

        foreach (var DmeParticleSystemDefinition in sourceDefinition)
        {
            var particleSystemDefinition = new DmeParticleSystemDefinition();

            // These should always be here
            particleSystemDefinition.Name = DmeParticleSystemDefinition.Name;
            particleSystemDefinition.Id = DmeParticleSystemDefinition.ID;

            #region Possible particle settings (Auto-generated)

            if (DmeParticleSystemDefinition.TryGetValue("renderers", out object rendererArray))
                particleSystemDefinition.Renderers = Renderer.Setup((ElementArray)rendererArray);

            if (DmeParticleSystemDefinition.TryGetValue("operators", out object operatorArray))
                particleSystemDefinition.Operators = Operator.Setup((ElementArray)operatorArray);

            if (DmeParticleSystemDefinition.TryGetValue("initializers", out object initializerArray))
                particleSystemDefinition.Initializers = Initializer.Setup((ElementArray)initializerArray);

            if (DmeParticleSystemDefinition.TryGetValue("emitters", out object emitterArray))
                particleSystemDefinition.Emitters = Emitter.Setup((ElementArray)emitterArray);

            if (DmeParticleSystemDefinition.TryGetValue("children", out object childrenArray))
                particleSystemDefinition.Children = Children.Setup((ElementArray)childrenArray);

            if (DmeParticleSystemDefinition.TryGetValue("constraints", out object constraintArray))
                particleSystemDefinition.Constraints = Constraint.Setup((ElementArray)constraintArray);

            if (DmeParticleSystemDefinition.TryGetValue("preventNameBasedLookup", out object preventNameBasedLookup))
                particleSystemDefinition.PreventNameBasedLookup = (bool)preventNameBasedLookup;

            if (DmeParticleSystemDefinition.TryGetValue("max_particles", out object maxParticles))
                particleSystemDefinition.MaxParticles = (int)maxParticles;

            if (DmeParticleSystemDefinition.TryGetValue("initial_particles", out object initialParticles))
                particleSystemDefinition.InitialParticles = (int)initialParticles;

            if (DmeParticleSystemDefinition.TryGetValue("material", out object material))
                particleSystemDefinition.Material = (string)material;
            
            //if (DmeParticleSystemDefinition.TryGetValue("bounding_box_min", out object boundingBoxMin))
            //    particleSystemDefinition.BoundingBoxMin = (Vector3)boundingBoxMin;

            //if (DmeParticleSystemDefinition.TryGetValue("bounding_box_max", out object boundingBoxMax))
            //    particleSystemDefinition.BoundingBoxMax = (Vector3)boundingBoxMax;

            if (DmeParticleSystemDefinition.TryGetValue("cull_radius", out object cullRadius))
                particleSystemDefinition.CullRadius = (float)cullRadius;

            if (DmeParticleSystemDefinition.TryGetValue("cull_cost", out object cullCost))
                particleSystemDefinition.CullCost = (float)cullCost;

            if (DmeParticleSystemDefinition.TryGetValue("cull_control_point", out object cullControlPoint))
                particleSystemDefinition.CullControlPoint = (int)cullControlPoint;

            if (DmeParticleSystemDefinition.TryGetValue("cull_replacement_definition", out object cullReplacementDefinition))
                particleSystemDefinition.CullReplacementDefinition = (string)cullReplacementDefinition;

            if (DmeParticleSystemDefinition.TryGetValue("radius", out object radius))
                particleSystemDefinition.Radius = (float)radius;

            //if (DmeParticleSystemDefinition.TryGetValue("color", out object color))
            //    particleSystemDefinition.Color = (Vector4)color;

            if (DmeParticleSystemDefinition.TryGetValue("rotation", out object rotation))
                particleSystemDefinition.Rotation = (float)rotation;

            if (DmeParticleSystemDefinition.TryGetValue("rotation_speed", out object rotationSpeed))
                particleSystemDefinition.RotationSpeed = (float)rotationSpeed;

            if (DmeParticleSystemDefinition.TryGetValue("sequence_number", out object sequenceNumber))
                particleSystemDefinition.SequenceNumber = (int)sequenceNumber;

            if (DmeParticleSystemDefinition.TryGetValue("sequence_number 1", out object sequenceNumber1))
                particleSystemDefinition.SequenceNumber1 = (int)sequenceNumber1;

            if (DmeParticleSystemDefinition.TryGetValue("group id", out object groupId))
                particleSystemDefinition.GroupId = (int)groupId;

            if (DmeParticleSystemDefinition.TryGetValue("maximum time step", out object maximumTimeStep))
                particleSystemDefinition.MaximumTimeStep = (float)maximumTimeStep;

            if (DmeParticleSystemDefinition.TryGetValue("maximum sim tick rate", out object maximumSimTickRate))
                particleSystemDefinition.MaximumSimTickRate = (float)maximumSimTickRate;

            if (DmeParticleSystemDefinition.TryGetValue("minimum sim tick rate", out object minimumSimTickRate))
                particleSystemDefinition.MinimumSimTickRate = (float)minimumSimTickRate;

            if (DmeParticleSystemDefinition.TryGetValue("minimum rendered frames", out object minimumRenderedFrames))
                particleSystemDefinition.MinimumRenderedFrames = (int)minimumRenderedFrames;

            if (DmeParticleSystemDefinition.TryGetValue("control point to disable rendering if it is the camera", out object controlPointToDisableRenderingIfItIsTheCamera))
                particleSystemDefinition.ControlPointToDisableRenderingIfItIsTheCamera = (int)controlPointToDisableRenderingIfItIsTheCamera;

            if (DmeParticleSystemDefinition.TryGetValue("maximum draw distance", out object maximumDrawDistance))
                particleSystemDefinition.MaximumDrawDistance = (float)maximumDrawDistance;

            if (DmeParticleSystemDefinition.TryGetValue("time to sleep when not drawn", out object timeToSleepWhenNotDrawn))
                particleSystemDefinition.TimeToSleepWhenNotDrawn = (float)timeToSleepWhenNotDrawn;

            if (DmeParticleSystemDefinition.TryGetValue("Sort particles", out object sortParticles))
                particleSystemDefinition.SortParticles = (bool)sortParticles;

            if (DmeParticleSystemDefinition.TryGetValue("batch particle systems", out object batchParticleSystems))
                particleSystemDefinition.BatchParticleSystems = (bool)batchParticleSystems;

            if (DmeParticleSystemDefinition.TryGetValue("view model effect", out object viewModelEffect))
                particleSystemDefinition.ViewModelEffect = (bool)viewModelEffect;

            # endregion

            particleSystemDefinitions.Add(particleSystemDefinition);

        }

        return particleSystemDefinitions;
    }
}
