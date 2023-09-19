// --- Day 1: Calorie Counting ---
// https://adventofcode.com/2022/day/1

internal class Program
{
    private static void Main(string[] args)
    {
        string fileName = @"adventofcode.com_2022_day_1_input.txt";       
            // adventofcode.com_2022_day_1_input.txt
            // test.txt
            // trochu jsem si pomohl přidáním druhého prázdného řádku na konec souboru :-/
        int currentElfID = 1;
        int currentElfCalories = 0;
        int max1ElfID = 1;
        int max1ElfCalories = 0;
        int max2ElfID = 1;
        int max2ElfCalories = 0;
        int max3ElfID = 1;
        int max3ElfCalories = 0;

        string[] lines = File.ReadAllLines(fileName);

        foreach (var line in lines)
        {
            if (line.Length == 0)
            {
                if (currentElfCalories > max1ElfCalories)
                {
                    max3ElfCalories = max2ElfCalories;
                    max3ElfID = max2ElfID;

                    max2ElfCalories = max1ElfCalories;
                    max2ElfID = max1ElfID;

                    max1ElfCalories = currentElfCalories;
                    max1ElfID = currentElfID;
                }
                else if (currentElfCalories > max2ElfCalories)
                {
                    max3ElfCalories = max1ElfCalories;
                    max3ElfID = max1ElfID;

                    max2ElfCalories = currentElfCalories;
                    max2ElfID = currentElfID;
                }
                else if (currentElfCalories > max3ElfCalories)
                {
                    max3ElfCalories = currentElfCalories;
                    max3ElfID = currentElfID;
                }

                currentElfCalories = 0;
                currentElfID++;
            }
            else
            {
                currentElfCalories += int.Parse(line);
            }
        }
        Console.WriteLine("\n----------------------------------------------");
        Console.WriteLine($"First Max. Calories has Elf No.: {max1ElfID}");
        Console.WriteLine($"He has {max1ElfCalories} calories.");
        Console.WriteLine($"\nSecond Max. Calories has Elf No.: {max2ElfID}");
        Console.WriteLine($"He has {max2ElfCalories} calories.");
        Console.WriteLine($"\nThird Max. Calories has Elf No.: {max3ElfID}");
        Console.WriteLine($"He has {max3ElfCalories} calories.");
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine($"Total Calories of top 3 Elfs: {max1ElfCalories + max2ElfCalories + max3ElfCalories}");
        Console.WriteLine("----------------------------------------------");
    }
}
