using System;
using System.Text.RegularExpressions;

class Program
{
    public static string ReverseDateFormat(string inputDate)
    {
        // Set a reasonable timeout for regex operations
        TimeSpan regexTimeout = TimeSpan.FromSeconds(1);
        string result = inputDate;  // Default to return the input if invalid or regex fails

        // Define the regex pattern with named groups
        string pattern = @"^(?<mon>\d{1,2})/(?<day>\d{1,2})/(?<year>\d{2,4})$";
        
        try
        {
            // Perform regex match with timeout
            Regex regex = new Regex(pattern, RegexOptions.None, regexTimeout);
            Match match = regex.Match(inputDate);

            if (match.Success)
            {
                // Extract the month, day, and year using named groups
                string month = match.Groups["mon"].Value;
                string day = match.Groups["day"].Value;
                string year = match.Groups["year"].Value;

                // If year is 2 digits, convert it to 4 digits (assuming it's 20xx)
                if (year.Length == 2)
                {
                    year = "20" + year;
                }

                // Format the date as yyyy-mm-dd
                result = $"{year}-{month.PadLeft(2, '0')}-{day.PadLeft(2, '0')}";
            }
        }
        catch (RegexMatchTimeoutException)
        {
            Console.WriteLine("Regex operation timed out. Returning original input.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        return result;
    }

    static void Main(string[] args)
    {
        // Loop to keep asking the user for input
        while (true)
        {
            Console.Write("Enter a date (mm/dd/yyyy): ");
            string input = Console.ReadLine();

            // Exit condition
            if (string.IsNullOrWhiteSpace(input))
            {
                break;
            }

            // Validate if the input is a valid date
            if (DateTime.TryParse(input, out DateTime validDate))
            {
                // If valid, attempt to convert and display the result
                string result = ReverseDateFormat(input);
                Console.WriteLine($"Converted date: {result}");
            }
            else
            {
                // If the input is invalid
                Console.WriteLine("Invalid date format. Please enter a valid date.");
            }
        }
    }
}
