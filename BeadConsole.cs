using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace BeadApp
{
    // The BeadConsole, handles CRUD operations
    public class BeadConsole
    {
        static public List<string[,]> PatternsList = new List<string[,]>();
   
        public static void Start()
        {
            // Console loop
            while (true)
            {
                Console.WriteLine("Welcome to BeadApp!");
                Console.WriteLine("ALL DISPLAY NEW UPDATE REMOVE EXIT");
                string input = Console.ReadLine()!.ToUpper();
                switch (input)
                {
                    case "ALL":
                        PrintAll();
                        break;
                    case "DISPLAY":
                        PrintPattern();
                        break;
                    case "NEW":
                        CreatePattern();
                        break;
                    case "UPDATE":
                        UpdatePattern();
                        break;
                    case "REMOVE":
                        RemovePattern();
                        break;
                    case "EXIT":
                        Console.WriteLine("See ya!");
                        return;
                    default:
                        Console.WriteLine("Invalid command. Please try again.");
                        break;
                }
            }
        }
        // todo
        static public void PrintAll()
        {
            Console.WriteLine("Prints all patterns");
            Console.WriteLine(PatternsList);
        }
        // todo
        static public void PrintPattern()
        {
            Console.WriteLine("Prints a specific design");
        }
        // Asks user for pattern name and dimensions
        // Sets hexadecimal color code per cell
        static public void CreatePattern()
        {
            int patternWidth = 10;
            int patternHeight = 10;
            Console.WriteLine("Starting pattern creation...");
            Console.WriteLine("What is your design name?");
            string patternName = Console.ReadLine()!;
            Console.WriteLine("How many beads wide will your design be?");
            patternWidth = int.Parse(Console.ReadLine()!);
            Console.WriteLine("And how many beads high will your design be?");
            patternHeight = int.Parse(Console.ReadLine()!);
            Console.WriteLine($"Your {patternName} design is {patternWidth} beads wide and {patternHeight} beads high.");
            Console.WriteLine("Is this correct? YES/NO");
            if (Console.ReadLine()!.ToUpper() == "NO")
            {
                CreatePattern();
            }

            string[,] pattern = new string[patternWidth, patternHeight];

            Console.WriteLine("Time to color the design!");
            // cell population
            Console.WriteLine("Enter a default hex color for the entire pattern (e.g., #FF5733), or leave blank to set colors for each cell individually:");
            string defaultColor = Console.ReadLine()!;
            if (IsValidHexColor(defaultColor))
            {
                for (int i = 0; i < patternWidth; i++)
                    for (int j = 0; j < patternHeight; j++)
                        pattern[i, j] = defaultColor;
            }
            else
            {
                Console.WriteLine("Let's set the color for each cell individually...");
                for (int i = 0; i < patternWidth; i++)
                {
                    for (int j = 0; j < patternHeight; j++)
                    {
                        while (true)
                        {
                            Console.WriteLine($"Enter a hex color for cell ({i + 1}, {j + 1}):");
                            string color = Console.ReadLine()!;
                            if (IsValidHexColor(color))
                            {
                                pattern[i, j] = color;
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid hex color. Please enter a value like #FF5733.");
                            }
                        }
                    }
                }
            }
            // Add pattern to master list
            PatternsList.Add(pattern);
            Console.WriteLine("Pattern added successfully!");
        }
        // todo
        static void UpdatePattern()
        {
            Console.WriteLine("Opens a given pattern to edit...");
        }
        // todo
        static public void RemovePattern()
        {
            Console.WriteLine("Removes a given pattern.");
        }
        // Checks if user input is valid hex value
        static bool IsValidHexColor(string color)
        {
            if (string.IsNullOrWhiteSpace(color) || color.Length != 7 || color[0] != '#')
                return false;

            for (int i = 1; i < 7; i++)
            {
                char c = color[i];
                if (!((c >= '0' && c <= '9') || (c >= 'A' && c <= 'F') || (c >= 'a' && c <= 'f')))
                    return false;
            }
            return true;
        }

    }
}