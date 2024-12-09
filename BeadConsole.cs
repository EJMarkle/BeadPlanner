using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeadApp
{
    public class BeadConsole
    {
        public static int GridWidth = 10;
        public static int GridHeight = 10;
        
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

        static public void PrintAll()
        {
            Console.WriteLine("Prints all patterns");
        }
        static public void PrintPattern()
        {
            Console.WriteLine("Prints a specific design");
        }
        static public void CreatePattern()
        {
            Console.WriteLine("Starts pattern creation...");
        }
        static void UpdatePattern()
        {
            Console.WriteLine("Opens a given pattern to edit...");
        }
        static public void RemovePattern()
        {
            Console.WriteLine("Removes a given pattern.");
        }
    }
}