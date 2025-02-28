using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        string baseDirectory = Path.Combine(Directory.GetCurrentDirectory(), "OfficeFiles");
        Directory.CreateDirectory(baseDirectory);

        // Step 1: Generate Random Office Files
        GenerateRandomFiles(baseDirectory, 10);

        // Step 2 & 3: Identify and Move Files
        OrganizeFiles(baseDirectory);

        // Step 4: Generate Summary Report
        GenerateSummaryReport(baseDirectory);

        Console.WriteLine("\nFile organization complete. Check the summary report.");
    }

    static void GenerateRandomFiles(string directory, int count)
    {
        Random random = new Random();
        string[] extensions = { ".docx", ".xlsx", ".pptx" };

        for (int i = 1; i <= count; i++)
        {
            string fileType = extensions[random.Next(extensions.Length)];
            string fileName = $"File_{i}{fileType}";
            string filePath = Path.Combine(directory, fileName);

            // Create an empty file
            File.WriteAllText(filePath, $"This is a sample {fileType} file.");
        }

        Console.WriteLine("Random Office files generated successfully.\n");
    }

    static void OrganizeFiles(string directory)
    {
        string[] fileTypes = { "Word", "Excel", "PowerPoint" };

        foreach (string type in fileTypes)
        {
            Directory.CreateDirectory(Path.Combine(directory, type));
        }

        var files = Directory.GetFiles(directory).Where(f => f.EndsWith(".docx") || f.EndsWith(".xlsx") || f.EndsWith(".pptx"));

        foreach (string file in files)
        {
            string extension = Path.GetExtension(file);
            string subdirectory = extension switch
            {
                ".docx" => "Word",
                ".xlsx" => "Excel",
                ".pptx" => "PowerPoint",
                _ => "Others"
            };

            string destinationPath = Path.Combine(directory, subdirectory, Path.GetFileName(file));
            File.Move(file, destinationPath);
        }

        Console.WriteLine("Files organized into respective folders.\n");
    }

    static void GenerateSummaryReport(string directory)
    {
        string reportPath = Path.Combine(directory, "SummaryReport.txt");
        using (StreamWriter writer = new StreamWriter(reportPath))
        {
            writer.WriteLine("Office File Organizer Summary Report");
            writer.WriteLine("=====================================");
            writer.WriteLine($"Date: {DateTime.Now}\n");
            writer.WriteLine("Student Name: Dawud kabir");
            writer.WriteLine("Student ID: 10499\n");

            string[] categories = { "Word", "Excel", "PowerPoint" };
            foreach (string category in categories)
            {
                string subdirectory = Path.Combine(directory, category);
                var files = Directory.GetFiles(subdirectory);
                writer.WriteLine($"{category} Files ({files.Length}):");

                foreach (string file in files)
                {
                    writer.WriteLine($"- {Path.GetFileName(file)} (Size: {new FileInfo(file).Length} bytes)");
                }

                writer.WriteLine();
            }
        }

        Console.WriteLine("Summary report generated successfully.\n");
    }
}
