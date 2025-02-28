using System;
using System.IO;

class Program
{
    static void Main()
    {
        Console.WriteLine("===== Office File Organizer =====");

        // Get user input with null checks
        Console.Write("Enter your name: ");
        string studentName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(studentName))
        {
            Console.WriteLine("Student name cannot be empty.");
            return;
        }

        Console.Write("Enter your student ID: ");
        string studentID = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(studentID))
        {
            Console.WriteLine("Student ID cannot be empty.");
            return;
        }

        Console.Write("Enter the directory path to organize: ");
        string baseDirectory = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(baseDirectory) || !Directory.Exists(baseDirectory))
        {
            Console.WriteLine("Invalid directory path. Please enter a valid path.");
            return;
        }

        Console.WriteLine("\nOrganizing files...");

        // Step 1: Generate Random Office Files
        GenerateRandomFiles(baseDirectory, 5);

        // Step 2 & 3: Identify and Move Files
        OrganizeFiles(baseDirectory);

        // Step 4: Generate Summary Report
        GenerateSummaryReport(baseDirectory, studentName, studentID);

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

            // Simulate file content
            File.WriteAllText(filePath, $"Sample {fileType} file content.");
        }

        Console.WriteLine("Random Office files created.\n");
    }

    static void OrganizeFiles(string directory)
    {
        // Create subdirectories
        string wordDir = Path.Combine(directory, "WordFiles");
        string excelDir = Path.Combine(directory, "ExcelFiles");
        string pptDir = Path.Combine(directory, "PPTFiles");

        Directory.CreateDirectory(wordDir);
        Directory.CreateDirectory(excelDir);
        Directory.CreateDirectory(pptDir);

        // Move files
        foreach (string file in Directory.GetFiles(directory))
        {
            string extension = Path.GetExtension(file);
            string destination = extension switch
            {
                ".docx" => Path.Combine(wordDir, Path.GetFileName(file)),
                ".xlsx" => Path.Combine(excelDir, Path.GetFileName(file)),
                ".pptx" => Path.Combine(pptDir, Path.GetFileName(file)),
                _ => null
            };

            if (destination != null)
            {
                File.Move(file, destination);
                Console.WriteLine($"Moved: {Path.GetFileName(file)} -> {Path.GetFileName(destination)}");
            }
        }
    }

    static void GenerateSummaryReport(string directory, string studentName, string studentID)
    {
        string reportPath = Path.Combine(directory, "SummaryReport.txt");
        using (StreamWriter writer = new StreamWriter(reportPath))
        {
            writer.WriteLine("===== Office File Organizer Summary Report =====");
            writer.WriteLine($"Date: {DateTime.Now}\n");
            writer.WriteLine($"Student: {studentName} (ID: {studentID})\n");
            writer.WriteLine("Organized Files:");

            string[] categories = { "WordFiles", "ExcelFiles", "PPTFiles" };
            foreach (string category in categories)
            {
                string subdirectory = Path.Combine(directory, category);
                var files = Directory.GetFiles(subdirectory);

                foreach (string file in files)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    string fileType = category.Replace("Files", "");
                    writer.WriteLine($"- {fileInfo.Name} ({fileType}, {fileInfo.Length / 1024} KB, Created: {fileInfo.CreationTime:yyyy-MM-dd})");
                }
            }
        }

        Console.WriteLine($"Summary report created: {reportPath}");
    }
}
