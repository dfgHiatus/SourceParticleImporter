using BaseX;
using CodeX;
using FrooxEngine;
using HarmonyLib;
using SourceParticleImporter.Model;
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

    private static void SetupNeosParticleSystem(DmeElement dmeElement)
    {
    //    var slot = Engine.Current.WorldManager.FocusedWorld.AddSlot(dmeElement.Name);
    //    slot.PositionInFrontOfUser();

    //    object max = null;
    //    dm.Root.TryGetValue("max_particles", out max); // May need to get first child

    //    var ps = slot.AttachComponent<ParticleSystem>();
    //    var style = ps.Style.Target;

    //    float @in = item.PCFToNeosValue("start_alpha", 0f);
    //    float @out = item.PCFToNeosValue("end_alpha", 1f);
    //    style.SetupAlphaFadeInFadeOut(@in, @out);

    //    var emitter = ps.AttachEmitter<SphereEmitter>();
    //    emitter.Radius.Value = dm.PCFToNeosValue("radius", emitter.Radius.Value);
    //    emitter.Rate.Value = item.PCFToNeosValue("emission_rate", emitter.Radius.Value);

    //    if (max != null) ps.MaxParticles.Value = (int)max;
    }
}