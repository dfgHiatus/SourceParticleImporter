using Datamodel;
using DM = Datamodel.Datamodel;

namespace SourceParticleImporter.Tests;

internal class Program
{
    static void Main(string[] args)
    {
        // Get all files that end with .pcf in the current directory
        var files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.pcf", SearchOption.TopDirectoryOnly);
        // Read the first file
        foreach (var file in files)
        {
            using (FileStream fileStream = File.OpenRead(file))
            {
                var dm = DM.Load(fileStream);
            }
        }
    }
}