// See https://aka.ms/new-console-template for more information

using System;
using System.Globalization;
using System.Threading;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Write("Enter a future time (HH:MM) or type 'exit' to quit: ");
            string? userInput = Console.ReadLine();
            
            if (userInput?.ToLower() == "exit")
            {
                Console.WriteLine("Exiting program.");
            }
            
            if (!ValidateTimeFormat(userInput))
            {
                Console.WriteLine("Invalid time format. Please enter in HH:MM format.");
                continue;
            }
            
            DateTime? targetTime = GetTargetTime(userInput);
            if (targetTime.HasValue)
            {
                Countdown(targetTime.Value);
            }
        }
    }

    static bool ValidateTimeFormat(string? timeStr)
    {
        return DateTime.TryParseExact(timeStr, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None,out _ );
    }

    static DateTime? GetTargetTime(string userTime)
    {
        DateTime now = DateTime.Now;
        if (DateTime.TryParseExact(userTime, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime targetTime))
        {
            targetTime = new DateTime(now.Year, now.Month, now.Day, targetTime.Hour, targetTime.Minute, 0);
            if (targetTime < now)
            {
                Console.WriteLine("The time has already elapsed. Please enter a future time.");
                return null;
            }
            return targetTime;
        }
        return null;
    }

    static void Countdown(DateTime targetTime)
    {
        while (true)
        {
            TimeSpan remainingTime = targetTime - DateTime.Now;
            if (remainingTime.TotalSeconds == 0)
            {
                Console.WriteLine("Time's up!");
                break;
            }
            Console.Write($"Time remaining: {remainingTime.Minutes} minutes, {remainingTime.Seconds} seconds\r");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }
}
