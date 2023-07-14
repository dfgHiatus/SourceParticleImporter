using BaseX;
using CodeX;
using FrooxEngine;
using HarmonyLib;
using SourceParticleImporter.Model;
using SourceParticleImporter.Model.Types;
using System;
using System.Collections.Generic;
using System.IO;

[HarmonyPatch(typeof(UniversalImporter), "Import", typeof(AssetClass), typeof(IEnumerable<string>),
    typeof(World), typeof(float3), typeof(floatQ), typeof(bool))]
public class UniversalImporterPatch
{
    static bool Prefix(ref IEnumerable<string> files)
    {
        List<string> sourceParticles = new();
        List<string> nonSourceParticles = new();
        foreach (var file in files)
        {
            if (Path.GetExtension(file).ToLower().Equals(".pcf"))
                sourceParticles.Add(file);
            else
                nonSourceParticles.Add(file);
        }

        foreach (var sp in sourceParticles)
        {
            SetupNeosParticleSystem(Source2NeosParticle.SetupSourceParticle(sp).Result);
        }

        if (nonSourceParticles.Count <= 0) return false;
        files = nonSourceParticles.ToArray();
        return true;
    }

    private static void SetupNeosParticleSystem(SourceParticle sourceParticle)
    {

        foreach (var system in sourceParticle.ParticleSystemDefinitions)
        {
            var slot = Engine.Current.WorldManager.FocusedWorld.AddSlot();
            slot.PositionInFrontOfUser();
            slot.Name = system.Name;
            SetupParticleDefinitions(slot, system);
        }
    }

    private static void SetupParticleDefinitions(Slot slot, DmeParticleSystemDefinition particleSystem)
    {
        var ps = slot.AttachComponent<ParticleSystem>();
        var se = ps.AttachEmitter<SphereEmitter>();
        var style = ps.Style.Target;

        SetupInitializers(slot, particleSystem.Initializers);
        SetupOperators(slot, particleSystem.Operators);
        SetupConstraints(slot, particleSystem.Constraints);
        SetupRenderers(slot, particleSystem.Renderers);
        SetupEmitters(slot, particleSystem.Renderers);

        var allParticleSystems = slot.GetComponentsInChildren<ParticleSystem>(null, false, true);
        allParticleSystems.ForEach(p => p.MaxParticles.Value = particleSystem.MaxParticles);
    }

    private static void SetupEmitters(Slot slot, List<RendererData> emitters)
    {
        var emitterSlot = slot.AddSlot("Emitters");

        foreach (var emitter in emitters)
        {
            var newSlot = emitterSlot.AddSlot(emitter.Name);
            var ps = slot.AttachComponent<ParticleSystem>();
            var se = ps.AttachEmitter<SphereEmitter>();
            var style = ps.Style.Target;
        }
    }

    private static void SetupRenderers(Slot slot, List<RendererData> renderers)
    {
        var rendererSlot = slot.AddSlot("Renderers");

        foreach (var renderer in renderers)
        {
            var newSlot = rendererSlot.AddSlot(renderer.Name);
            var ps = slot.AttachComponent<ParticleSystem>();
            var se = ps.AttachEmitter<SphereEmitter>();
            var style = ps.Style.Target;
        }
    }

    private static void SetupConstraints(Slot slot, List<ConstraintData> constraints)
    {
        var constraintSlot = slot.AddSlot("Constraints");

        foreach (var constraint in constraints)
        {
            var newSlot = constraintSlot.AddSlot(constraint.Name);
            var ps = slot.AttachComponent<ParticleSystem>();
            var se = ps.AttachEmitter<SphereEmitter>();
            var style = ps.Style.Target;
        }
    }

    private static void SetupOperators(Slot slot, List<OperatorData> operators)
    {
        var operatorSlot = slot.AddSlot("Operators");

        foreach (var op in operators)
        {
            var newSlot = operatorSlot.AddSlot(op.Name);
            var ps = slot.AttachComponent<ParticleSystem>();
            var se = ps.AttachEmitter<SphereEmitter>();
            var style = ps.Style.Target;
        }
    }

    private static void SetupInitializers(Slot slot, List<InitializerData> initializers)
    {
        var initializerSlot = slot.AddSlot("Initializers");

        foreach (var initializer in initializers)
        {
            var newSlot = initializerSlot.AddSlot(initializer.Name);
            var ps = slot.AttachComponent<ParticleSystem>();
            var se = ps.AttachEmitter<SphereEmitter>();
            var style = ps.Style.Target;
        }
    }

    private static void SetupForces(Slot slot, List<InitializerData> initializers)
    {
        throw new NotImplementedException();
    }
}