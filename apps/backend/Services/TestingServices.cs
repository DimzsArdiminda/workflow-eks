using System;
using System.Diagnostics;
using System.Reflection;

namespace workflowEkstensi.backend.Services
{
    public class TestingServices
    {
        public object getHealth()
        {
            string status = "Healthy";
            string version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "Unknown";
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
            TimeSpan uptime = DateTime.UtcNow - Process.GetCurrentProcess().StartTime.ToUniversalTime();
            string formattedUptime = $"{uptime.Days}d {uptime.Hours}h {uptime.Minutes}m {uptime.Seconds}s";
                return new
            {
                status,
                version,
                environment,
                uptime = formattedUptime
            };
        }

        public string GetMessage()
        {
            return "Hello from TestingServices!";
        }

        public string GetGreeting(string name)
        {
            return $"Hello, {name}!";
        }

        public string CheckPalindrome(int input)
        {
            if(input < 0)
            {
                return "Negative numbers are not considered palindromes.";
            }

            string inputStr = input.ToString();
            char[] charArray = inputStr.ToCharArray();
            int i = 0, j = charArray.Length - 1;

            while (i < j)
            {
                if (charArray[i] != charArray[j])
                {
                    return "The number is not a palindrome.";
                }
                i++;
                j--;
            }

            return "The number is a palindrome.";
        }
    }
}