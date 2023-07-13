using FrooxEngine;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace SourceParticleImporter;

internal static class Utils
{
    internal static async Task<string> ConvertPCFToText(string inputFile, string dmxConverterPath)
    {
        if (!File.Exists(inputFile))
            throw new FileNotFoundException($"Could not find input file: {inputFile}");

        if (!File.Exists(dmxConverterPath))
            throw new FileNotFoundException($"Could not find DMXConverter: {dmxConverterPath}");
        
        var outputFile = Path.Combine(Engine.Current.LocalDB.AssetCachePath, Path.GetFileName(inputFile) + "_converted.pcf");
        var process = new Process();
        process.StartInfo.FileName = dmxConverterPath;
        process.StartInfo.Arguments = 
            $"-i { inputFile } " +
            $"-o { outputFile }" +
            $"-oe keyvalues2";
        process.StartInfo.UseShellExecute = true;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        process.Start();
        process.WaitForExit();

        return outputFile;
    }
}
