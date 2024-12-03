// Bead app!!
using System;

class Program
{
    static int cellsWide = 0; 
    static int cellsHigh = 0;

    static void Main()
    {
        Console.WriteLine("Welcome to Get Beading!");

        // Get cells wide
        Console.WriteLine("How many cells wide will your template be?");
        cellsWide = int.Parse(Console.ReadLine() ?? "0");

        // Get cells height
        Console.WriteLine("And how many cells high will your template be?");
        cellsHigh = int.Parse(Console.ReadLine() ?? "0");

        // Display the dimensions
        Console.WriteLine($"Your template will be {cellsHigh} cells tall and {cellsWide} cells wide. Is this correct? (Yes/No)");

        // Confirm the input
        string response = Console.ReadLine()?.Trim().ToLower();

        if (response == "yes")
        {
            Console.WriteLine("Printing template...");
            PrintTemplate();
        }
        else
        {
            Console.WriteLine("Let's start again.");
            Main();
        }
    }

    static void PrintTemplate()
    {
        Console.WriteLine($"Template with {cellsHigh} x {cellsWide} cells created!");
    }
}
