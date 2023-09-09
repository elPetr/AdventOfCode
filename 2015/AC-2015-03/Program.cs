/* 
--- Day 3: Perfectly Spherical Houses in a Vacuum ---
Santa is delivering presents to an infinite two-dimensional grid of houses.

He begins by delivering a present to the house at his starting location, and then an elf at the North Pole calls him via radio and tells him where to move next. Moves are always exactly one house to the north (^), south (v), east (>), or west (<). After each move, he delivers another present to the house at his new location.

However, the elf back at the north pole has had a little too much eggnog, and so his directions are a little off, and Santa ends up visiting some houses more than once. How many houses receive at least one present?

For example:

> delivers presents to 2 houses: one at the starting location, and one to the east.
^>v< delivers presents to 4 houses in a square, including twice to the house at his starting/ending location.
^v^v^v^v^v delivers a bunch of presents to some very lucky children at only 2 houses.

--- Part Two ---
The next year, to speed up the process, Santa creates a robot version of himself, Robo-Santa, to deliver presents with him.

Santa and Robo-Santa start at the same location (delivering two presents to the same starting house), then take turns moving based on instructions from the elf, who is eggnoggedly reading from the same script as the previous year.

This year, how many houses receive at least one present?

For example:

^v delivers presents to 3 houses, because Santa goes north, and then Robo-Santa goes south.
^>v< now delivers presents to 3 houses, and Santa and Robo-Santa end up back where they started.
^v^v^v^v^v now delivers presents to 11 houses, with Santa going one direction and Robo-Santa going the other.

 input file: d:\SOURCE\AdventOfCode\AC-2015-03\adventofcode.com_2015_day_3_input.txt 
 */

using System.Diagnostics;
using System.IO;

string inputFileName = @"d:\SOURCE\AdventOfCode\AC-2015-03\adventofcode.com_2015_day_3_input.txt";
string text = File.ReadAllText(inputFileName);
char[] instructions = text.ToCharArray();
string outputFileName = @"d:\SOURCE\AdventOfCode\AC-2015-03\output.txt";


int[,] mapOfHouses = new int[110, 90];
// zjistil jsem tím, že jsem si to jednou pustil na sucho, předělat na něco elegantnějšího
// takto inicializované je rovnou naplněné nulama, nemusím ho předvyplňovat

// zjisti jak daleko od stredu se dostaneme
int horizontalSanta = 1;
int verticalSanta = 0;
int horizontalRobo = 1;
int verticalRobo = 0;

int leftSize = 0;
int rightSize = 0;
int upSize = 0;
int downSize = 0;
int horizontalOffset = 55;
int verticalOffset = 25;
int pocetDomuSAlesponJednimDarkem = 0;
int steps = 0;

mapOfHouses[horizontalSanta - 1 + horizontalOffset, verticalSanta + verticalOffset]++; // Santa vyráží z domu, kde už jeden dárek rozdal
mapOfHouses[horizontalRobo - 1 + horizontalOffset, verticalRobo + verticalOffset]++; // Santa vyráží z domu, kde už jeden dárek rozdal
foreach (char instruction in instructions)
{
    steps++;
    //if (leftSize < 200 || rightSize > 200 || upSize > 200 || downSize < 200)
    //    Console.WriteLine($"Bacha málo místa\n leftSize:\t{leftSize}\nrightSize:\t{rightSize}\nupSize:\t{upSize}\ndownSize:\t{downSize}\n");

    switch (instruction)
    {
        case '<':
            if (steps % 2 != 0)
            {
                horizontalSanta--;
                if (horizontalSanta < leftSize) leftSize--;
                mapOfHouses[horizontalSanta + horizontalOffset, verticalSanta + verticalOffset]++;
            }
            else
            {
                horizontalRobo--;
                if (horizontalRobo < leftSize) leftSize--;
                mapOfHouses[horizontalRobo + horizontalOffset, verticalRobo + verticalOffset]++;
            }
            break;

        case '^':
            if (steps % 2 != 0)
            {
                verticalSanta++;
                if (verticalSanta > upSize) upSize++;
                mapOfHouses[horizontalSanta + horizontalOffset, verticalSanta + verticalOffset]++;
            }
            else
            {
                verticalRobo++;
                if (verticalRobo > upSize) upSize++;
                mapOfHouses[horizontalRobo + horizontalOffset, verticalRobo + verticalOffset]++;
            }
            break;

        case '>':
            if (steps % 2 != 0)
            {
                horizontalSanta++;
                if (horizontalSanta > rightSize) rightSize++;
                mapOfHouses[horizontalSanta + horizontalOffset, verticalSanta + verticalOffset]++;
            }
            else
            {
                horizontalRobo++;
                if (horizontalRobo > rightSize) rightSize++;
                mapOfHouses[horizontalRobo + horizontalOffset, verticalRobo + verticalOffset]++;
            }
            break;

        case 'v':
            if (steps % 2 != 0)
            {
                verticalSanta--;
                if (verticalSanta < downSize) downSize--;
                mapOfHouses[horizontalSanta + horizontalOffset, verticalSanta + verticalOffset]++;
            }
            else
            {
                verticalRobo--;
                if (verticalRobo < downSize) downSize--;
                mapOfHouses[horizontalRobo + horizontalOffset, verticalRobo + verticalOffset]++;
            }
            break;

        default:
            Console.WriteLine("Tenhle znak jsem v souboru nečekal!");
            break;
    }
}

Console.WriteLine($"Size of the map:");
Console.WriteLine($"\t{upSize}");
Console.WriteLine($"{leftSize}\tx\t{rightSize}");
Console.WriteLine($"\t{downSize}");


File.WriteAllText(outputFileName, String.Empty);
Console.WriteLine("Mapa Santovo návštěv:\n");
File.AppendAllText(outputFileName, "Mapa Santovo návštěv:\n");

int charCounter = 0;
int presentsCounter = 0;


for (int i = 0; i < mapOfHouses.GetLength(0); i++)
{
    charCounter = 0;
    for (int j = 0; j < mapOfHouses.GetLength(1); j++)
    {
        Console.Write($"|{mapOfHouses[i, j]}");
        File.AppendAllText(outputFileName, $"|{mapOfHouses[i, j]}");
        presentsCounter += mapOfHouses[i, j];

        charCounter++;
        if (mapOfHouses[i, j] > 0) pocetDomuSAlesponJednimDarkem++;
    }

    Console.WriteLine($"| {charCounter}");
    File.AppendAllText(outputFileName, $"| {charCounter}\n");
}
Console.WriteLine($"\n\nPočet domů s alespoň jedním dárkemje: {pocetDomuSAlesponJednimDarkem}");
Console.WriteLine($"Santa podnikl {steps} návštěv.");
Console.WriteLine($"Santa rozdal {presentsCounter} dárků.");