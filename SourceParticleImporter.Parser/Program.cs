using SourceParticleImporter.Model;

namespace SourceParticleImporter.Tests;

internal class Program
{
    static async Task Main(string[] args)
    {
        var files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.pcf", SearchOption.TopDirectoryOnly);

        foreach (var file in files)
        {
            var converted = await Source2NeosParticle.SetupSourceParticle(file);
            await Console.Out.WriteLineAsync(converted.Name);
            await Console.Out.WriteLineAsync(converted.Id.ToString());
            await Console.Out.WriteLineAsync(converted.ParticleSystemDefinitions.Count.ToString());
        }

        Console.ReadKey();
    }
}