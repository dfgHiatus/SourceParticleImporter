using DM = Datamodel.Datamodel;


namespace SourceParticleImporter.Model;

internal static class DatamodelExtensions
{
    internal static T PCFToNeosValue<T>(this DM dm, string key, T defaultValue = default)
    {
        return dm.Root.TryGetValue(key, out object obj) ? (T)obj : defaultValue;
    }
}
