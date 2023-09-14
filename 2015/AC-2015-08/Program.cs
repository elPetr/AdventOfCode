/* --- Day 8: Matchsticks ---
Instructions:   Instructions-Day-08-Matchsticks.txt
InputFile:      adventofcode.com_2015_day_8_input.txt
Web:            https://adventofcode.com/2015/day/8    */

/*
 nápady:
raw string literals 
- dělají se přes troje úvozovky
- https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/strings/
 */

using System;
using System.IO;

internal class Program
{
    private static void Main(string[] args)
    {
        string fileName = @"test.txt";  //prohodit za adventofcode.com_2015_day_7_input-B.txt pro druhou půlku úkolu
        string[] lines = File.ReadAllLines(fileName);
        ReadTheFileByChar(fileName);
        CountStringSizes(fileName);
    }
    
    static void ReadTheFileByChar(string path)
    {
        int countCharsInCode = 0;
        int znak;
        try
        {
            using (StreamReader sr = new StreamReader(path))
            {
                while (sr.Peek() >= 0)
                {
                    znak = sr.Read();
                    //System.Console.WriteLine($"{znak},");
                    //Console.Write((char)sr.Read());

                    if (znak != 10) countCharsInCode++; // spocti vsechny znaky v souboru krome \n
                }
            }
            System.Console.WriteLine($"Number of characters in code representation: {countCharsInCode}");
        }
        catch (Exception e)
        {
            Console.WriteLine("The process failed: {0}", e.ToString());
        }
    }

    static void CountStringSizes(string path)
    {
        int totalLengthOfStrings = 0;
        string[] lines = File.ReadAllLines(path);
        foreach (var line in lines)
        {
            System.Console.WriteLine(line);
            totalLengthOfStrings += line.Replace(" ", "").Length;
        }
        System.Console.WriteLine($"Number of characters in strings: {totalLengthOfStrings}");

    }
}
