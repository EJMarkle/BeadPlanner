using System;
using System.Collections.Generic;
using System.Linq;

namespace BeadApp
{
    // Pattern class, stores grid size and color data
    public class Pattern
    {
        public string Name { get; set; }
        public string[,] Grid { get; set; }

        public Pattern(string name, string[,] grid)
        {
            Name = name;
            Grid = grid;
        }
    }

    // The BeadConsole, handles CRUD operations
    public class BeadConsole
    {
        static public List<Pattern> PatternsList = new List<Pattern>();

        public static void Start()
        {
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
                        Console.WriteLine("Enter the pattern name to display:");
                        string patternName = Console.ReadLine()!;
                        PrintPattern(patternName);
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

        // Prints all pattern names
        static public void PrintAll()
        {
            Console.WriteLine("Listing all patterns:");
            if (PatternsList.Count == 0)
            {
                Console.WriteLine("No patterns available.");
            }
            else
            {
                foreach (var pattern in PatternsList)
                {
                    Console.WriteLine($"- {pattern.Name}");
                }
            }
        }

        // Prints a specific pattern by name
        static public void PrintPattern(string patternName)
        {
            // Find the pattern with the specified name
            var pattern = PatternsList.FirstOrDefault(p => p.Name.Equals(patternName, StringComparison.OrdinalIgnoreCase));

            // Check if the pattern was found
            if (pattern != null)
            {
                Console.WriteLine($"Printing pattern: {pattern.Name}");
                for (int i = 0; i < pattern.Grid.GetLength(0); i++)
                {
                    for (int j = 0; j < pattern.Grid.GetLength(1); j++)
                    {
                        Console.Write(pattern.Grid[i, j] + " ");
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine($"Pattern with the name '{patternName}' not found.");
            }
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
                return;
            }

            // Corrected grid initialization
            string[,] pattern = new string[patternHeight, patternWidth];

            Console.WriteLine("Time to color the design!");
            Console.WriteLine("Enter a default hex color for the entire pattern (e.g., #FF5733), or leave blank to set colors for each cell individually:");
            string defaultColor = Console.ReadLine()!;
            if (IsValidHexColor(defaultColor))
            {
                for (int i = 0; i < patternHeight; i++)
                    for (int j = 0; j < patternWidth; j++)
                        pattern[i, j] = defaultColor;
            }
            else
            {
                Console.WriteLine("Let's set the color for each cell individually...");
                for (int i = 0; i < patternHeight; i++)
                {
                    for (int j = 0; j < patternWidth; j++)
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

            // Create a new Pattern object and add it to the list
            PatternsList.Add(new Pattern(patternName, pattern));
            Console.WriteLine("Pattern added successfully!");
        }


        // prompt for pattern to update, then allows for grid size name and color changing
        static void UpdatePattern()
        {
            Console.WriteLine("Which pattern would you like to edit?");
            if (PatternsList.Count == 0)
            {
                Console.WriteLine("No patterns available.");
                return;
            }

            // Display available patterns
            foreach (var pattern in PatternsList)
            {
                Console.WriteLine($"- {pattern.Name}");
            }

            // Get the pattern name from the user
            string input = Console.ReadLine()!;
            var patternToUpdate = PatternsList.FirstOrDefault(p => p.Name.Equals(input, StringComparison.OrdinalIgnoreCase));

            if (patternToUpdate == null)
            {
                Console.WriteLine($"Pattern '{input}' not found.");
                return;
            }

            // Ask the user what they want to update
            Console.WriteLine($"Editing pattern: {patternToUpdate.Name}");
            Console.WriteLine("What would you like to update? NAME, SIZE, COLORS");
            string choice = Console.ReadLine()!.ToUpper();

            switch (choice)
            {
                case "NAME":
                    Console.WriteLine("Enter the new name for the pattern:");
                    string newName = Console.ReadLine()!;
                    if (!string.IsNullOrWhiteSpace(newName))
                    {
                        patternToUpdate.Name = newName;
                        Console.WriteLine("Pattern name updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid name. Update canceled.");
                    }
                    break;

                case "SIZE":
                    Console.WriteLine("Enter the new width of the pattern:");
                    int newWidth = int.Parse(Console.ReadLine()!);
                    Console.WriteLine("Enter the new height of the pattern:");
                    int newHeight = int.Parse(Console.ReadLine()!);

                    // Create a new grid with updated size
                    string[,] newGrid = new string[newHeight, newWidth];

                    // Copy existing colors to the new grid (if possible)
                    for (int i = 0; i < Math.Min(patternToUpdate.Grid.GetLength(0), newHeight); i++)
                    {
                        for (int j = 0; j < Math.Min(patternToUpdate.Grid.GetLength(1), newWidth); j++)
                        {
                            newGrid[i, j] = patternToUpdate.Grid[i, j];
                        }
                    }

                    // Update the grid
                    patternToUpdate.Grid = newGrid;
                    Console.WriteLine("Pattern size updated successfully.");
                    break;

                case "COLORS":
                    Console.WriteLine("Let's update the colors. You can set a default color or edit each cell individually.");
                    Console.WriteLine("Enter a default hex color (e.g., #FF5733), or leave blank to edit each cell:");

                    string defaultColor = Console.ReadLine()!;
                    if (IsValidHexColor(defaultColor))
                    {
                        for (int i = 0; i < patternToUpdate.Grid.GetLength(0); i++)
                        {
                            for (int j = 0; j < patternToUpdate.Grid.GetLength(1); j++)
                            {
                                patternToUpdate.Grid[i, j] = defaultColor;
                            }
                        }
                        Console.WriteLine("All cells updated with the default color.");
                    }
                    else
                    {
                        Console.WriteLine("Editing individual cells...");
                        for (int i = 0; i < patternToUpdate.Grid.GetLength(0); i++)
                        {
                            for (int j = 0; j < patternToUpdate.Grid.GetLength(1); j++)
                            {
                                Console.WriteLine($"Enter a hex color for cell ({i + 1}, {j + 1}):");
                                string color = Console.ReadLine()!;
                                if (IsValidHexColor(color))
                                {
                                    patternToUpdate.Grid[i, j] = color;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid color. Skipping this cell.");
                                }
                            }
                        }
                    }
                    Console.WriteLine("Pattern colors updated successfully.");
                    break;

                default:
                    Console.WriteLine("Invalid choice. Update canceled.");
                    break;
            }
        }

        // prompts for pattern to delete, then deletes it
        static public void RemovePattern()
        {
            // Check if the list is empty
            if (PatternsList.Count == 0)
            {
                Console.WriteLine("No patterns available to remove.");
                return;
            }

            // Display available patterns
            Console.WriteLine("Available patterns:");
            foreach (var pattern in PatternsList)
            {
                Console.WriteLine($"- {pattern.Name}");
            }
            // Ask user for the pattern name to remove
            Console.WriteLine("Enter the name of the pattern you want to remove:");
            string input = Console.ReadLine()!;

            // Find the pattern
            var patternToRemove = PatternsList.FirstOrDefault(p => p.Name.Equals(input, StringComparison.OrdinalIgnoreCase));

            // Check if the pattern was found
            if (patternToRemove != null)
            {
                PatternsList.Remove(patternToRemove); // Remove the pattern from the list
                Console.WriteLine($"Pattern '{patternToRemove.Name}' has been removed.");
            }
            else
            {
                Console.WriteLine($"Pattern with the name '{input}' not found.");
            }

        }

        // Checks if user input is a valid hex value
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
