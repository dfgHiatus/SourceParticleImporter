using Datamodel;


namespace SourceParticleImporter.Model;

internal static class ElementExtensions
{
    internal static T PCFToNeosValue<T>(this Element element, string key)
    {
        return element.TryGetValue(key, out object obj) ? (T)obj : default;
    }
}
