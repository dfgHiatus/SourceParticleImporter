using FrooxEngine;
using System.IO;
using DM = Datamodel.Datamodel;

namespace SourceParticleImporter.Model;

internal class Source2NeosParticle
{
    internal static async void SetupSourceParticle(string filePath)
    {
        var textPCF = await Utils.ConvertPCFToText(filePath, "");
        using (FileStream fileStream = File.OpenRead(textPCF))
        {
            var dm = DM.Load(fileStream);
            var slot = Engine.Current.WorldManager.FocusedWorld.AddSlot(dm.Root.Name);
            slot.PositionInFrontOfUser();

            object max = null;
            dm.Root.TryGetValue("max_particles", out max); // May need to get first child

            foreach (var item in dm.AllElements)
            {
                slot.AddSlot(item.Name);

                var ps = slot.AttachComponent<ParticleSystem>();
                var style = ps.Style.Target;
                item.

                var emitter = ps.AttachEmitter<SphereEmitter>();
                emitter.Radius.Value = dm.PCFToNeosValue<float>("radius");


                if (max != null) ps.MaxParticles.Value = (int) max;
            }
        }
    }
}
