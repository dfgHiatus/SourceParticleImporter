using BaseX;
using FrooxEngine;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace SourceParticleImporter
{
    internal static class DMXConverter
    {
        internal static async Task<string> ConvertToText(string inputFile, string dmxConverterPath)
        {
            if (!File.Exists(dmxConverterPath))
                throw new FileNotFoundException("Could not find DMXConverter");
            
            var outputFile = Path.Combine(Engine.Current.LocalDB.AssetCachePath, Path.GetFileName(inputFile) + "_converted.pcf");
            var process = new Process();
            process.StartInfo.FileName = dmxConverterPath;
            process.StartInfo.Arguments = 
                $"-i { inputFile } " +
                $"-o { outputFile }" +
                $"-oe keyvalues2";
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            process.Start();
            process.WaitForExit();

            UniLog.Log("PCF conversion complete!");

            return outputFile;
        }
    }
}
