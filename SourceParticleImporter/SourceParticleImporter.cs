using HarmonyLib;
using NeosModLoader;
using System;

namespace SourceParticleImporter
{
    public class SourceParticleImporter : NeosMod
    {
        public override string Name => "SourceParticleImporter";
        public override string Author => "dfgHiatus";
        public override string Version => "1.0.0";
        public override string Link => "";

        public static ModConfiguration Config;

        public static readonly ModConfigurationKey<string> DMXConverterPath 
            = new ("SourceParticleImporter", "dmxConverterPath", () => "");

        public override void DefineConfiguration(ModConfigurationDefinitionBuilder builder)
        {
            builder
                .Version(new Version(1, 0, 0))
                .AutoSave(true); 
        }

        public override void OnEngineInit()
        {
            new Harmony("net.dfgHiatus.SourceParticleImporter").PatchAll();
            Config = GetConfiguration();
        }
    }
}