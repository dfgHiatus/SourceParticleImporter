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
            Source2NeosParticle.SetupSourceParticle(sp);
        }

        if (nonSourceParticles.Count <= 0) return false;
        files = nonSourceParticles.ToArray();
        return true;
    }
}