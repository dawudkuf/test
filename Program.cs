using System;
using System.IO;
using System.Linq;

public class OfficeFileSummary
{
    public static void Main(string[] args)
    {
        string directoryName = "FileCollection";
        string resultsFileName = "results.txt";

        
        if (!Directory.Exists(directoryName))
        {
            Directory.CreateDirectory(directoryName);
            Console.WriteLine($"Created directory: {directoryName}");
        }

        
        var officeFiles = new[] { ".xlsx", ".docx", ".pptx" };
        int totalCount = 0;
        long totalSize = 0;

        var fileCounts = new Dictionary<string, int>();
        var fileSizes = new Dictionary<string, long>();

        foreach (var file in Directory.GetFiles(directoryName))
        {
            string extension = Path.GetExtension(file).ToLower();

            if (officeFiles.Contains(extension))
            {
                totalCount++;
                totalSize += new FileInfo(file).Length;

                if (!fileCounts.ContainsKey(extension))
                {
                    fileCounts[extension] = 0;
                    fileSizes[extension] = 0;
                }

                fileCounts[extension]++;
                fileSizes[extension] += new FileInfo(file).Length;
            }
        }

        
        using (StreamWriter writer = new StreamWriter(resultsFileName))
        {
            writer.WriteLine("Office File Summary:");
            writer.WriteLine($"Total Files: {totalCount}");
            writer.WriteLine($"Total Size: {totalSize} bytes");

            foreach (var extension in fileCounts.Keys)
            {
                writer.WriteLine($"- {extension} Files: {fileCounts[extension]}");
                writer.WriteLine($"  Size: {fileSizes[extension]} bytes");
            }
        }

        Console.WriteLine($"Summary written to {resultsFileName}");
    }
}
