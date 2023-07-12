using BaseX;
using CodeX;
using Datamodel;
using FrooxEngine;
using HarmonyLib;
using NeosModLoader;
using System;
using System.Collections.Generic;
using System.IO;
using DM = Datamodel.Datamodel;
using System.IO.Pipes;
using System.Runtime.InteropServices.ComTypes;

namespace SourceParticleImporter
{
    public class SourceParticleImporter : NeosMod
    {
        public override string Name => "SourceParticleImporter";
        public override string Author => "dfgHiatus";
        public override string Version => "1.0.0";
        public override string Link => "";

        private static ModConfiguration config;

        public override void DefineConfiguration(ModConfigurationDefinitionBuilder builder)
        {
            builder
                .Version(new Version(1, 0, 0))
                .AutoSave(true); 
        }

        public override void OnEngineInit()
        {
            new Harmony("net.dfgHiatus.SourceParticleImporter").PatchAll();
            config = GetConfiguration();
        }


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
                    SetupSourceParticle(sp);
                }

                if (nonSourceParticles.Count <= 0) return false;
                files = nonSourceParticles.ToArray();
                return true;
            }
        }

        private static async void SetupSourceParticle(string filePath)
        {
            var slot = Engine.Current.WorldManager.FocusedWorld.AddSlot(
                       Path.GetFileNameWithoutExtension(filePath));
            slot.PositionInFrontOfUser();

            await default(ToBackground);
            var textPCF = await DMXConverter.ConvertToText(filePath, "");
            // We now enter Regex hell

            using (FileStream fileStream = File.OpenRead(textPCF))
            {
                var dm = DM.Load(fileStream);
            }

            await default(ToWorld);
        }
    }
}