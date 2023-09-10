﻿/*
--- Day 6: Probably a Fire Hazard ---
Because your neighbors keep defeating you in the holiday house decorating contest year after year, you've decided to deploy one million lights in a 1000x1000 grid.

Furthermore, because you've been especially nice this year, Santa has mailed you instructions on how to display the ideal lighting configuration.

Lights in your grid are numbered from 0 to 999 in each direction; the lights at each corner are at 0,0, 0,999, 999,999, and 999,0. The instructions include whether to turn on, turn off, or toggle various inclusive ranges given as coordinate pairs. Each coordinate pair represents opposite corners of a rectangle, inclusive; a coordinate pair like 0,0 through 2,2 therefore refers to 9 lights in a 3x3 square. The lights all start turned off.

To defeat your neighbors this year, all you have to do is set up your lights by doing the instructions Santa sent you in order.

For example:

turn on 0,0 through 999,999 would turn on (or leave on) every light.
toggle 0,0 through 999,0 would toggle the first line of 1000 lights, turning off the ones that were on, and turning on the ones that were off.
turn off 499,499 through 500,500 would turn off (or leave off) the middle four lights.
After following the instructions, how many lights are lit?
------------------------------------------------------------------------------------------------------------------
--- Part Two ---
You just finish implementing your winning light pattern when you realize you mistranslated Santa's message from Ancient Nordic Elvish.

The light grid you bought actually has individual brightness controls; each light can have a brightness of zero or more. The lights all start at zero.

The phrase turn on actually means that you should increase the brightness of those lights by 1.

The phrase turn off actually means that you should decrease the brightness of those lights by 1, to a minimum of zero.

The phrase toggle actually means that you should increase the brightness of those lights by 2.

What is the total brightness of all lights combined after following Santa's instructions?

For example:

turn on 0,0 through 0,0 would increase the total brightness by 1.
toggle 0,0 through 999,999 would increase the total brightness by 2000000.
------------------------------------------------------------------------------------------------------------------------

input file: d:\SOURCE\AdventOfCode\AC-2015-06\adventofcode.com_2015_day_6_input.txt 
 */

using System;
using System.Diagnostics;
using System.IO;

string fileName = @"adventofcode.com_2015_day_6_input.txt";
string[] lines = File.ReadAllLines(fileName);
string[] parsedLine;
int[,] lightsGrid = new int[1000,1000];
int x1,x2,y1,y2;
x1=x2=y1=y2=0;
string lightsAction = "";
string lineTMP;
foreach (string line in lines)
{
    lineTMP = line.Replace("turn ",""); // turn on -> on, turn off -> off
    lineTMP = lineTMP.Replace(" through "," "); // zůstane 5 polí oddělených mezerami a čárkami
    parsedLine = lineTMP.Split(' ',','); //parsedLine[0-on/off/toggle, 1-x1 , 2-y1 , 3-x2, 4-y2]
    lightsAction = parsedLine[0];
    //if (lightsAction != "on" || lightsAction != "off" || lightsAction != "toggle")
    x1 = int.Parse(parsedLine[1]);
    y1 = int.Parse(parsedLine[2]);
    x2 = int.Parse(parsedLine[3]);
    y2 = int.Parse(parsedLine[4]);

    for (int x = x1; x <= x2; x++)
    {
        for (int y = y1; y <= y2; y++)
        {
            switch (lightsAction)
            {
                case "on":
                {
                    lightsGrid[x,y] += 1;
                    break;
                }
                
                case "off":
                {
                    if (lightsGrid[x,y] > 0) lightsGrid[x,y] -= 1;
                    break;
                }
                
                case "toggle":
                {
                    lightsGrid[x,y] += 2;
                    // lightsGrid[x,y] = (lightsGrid[x,y] == 1) ? 0 : 1; // pravidlo z casti 1
                    break;
                }
                default:
                {
                    Console.WriteLine($"Neočekávaná hodnota {lightsAction}");
                    break;
                }
            }
        }
    }
}

string outputFileName = @"output.txt";
File.WriteAllText(outputFileName, String.Empty);
// zapis do souboru jsem udělal, ale hodně to zdržuje, budu muset optimalizovat, psát po řádcích, nebo stream??
int numberOfLightsOn = 0;

for (int i = 0; i < lightsGrid.GetLength(0); i++)
{
    for (int j = 0; j < lightsGrid.GetLength(1); j++)
    {
        File.AppendAllText(outputFileName , $"{lightsGrid[i,j]},");
        numberOfLightsOn += lightsGrid[i,j];
    }
    File.AppendAllText(outputFileName , "\n");
}

Console.WriteLine($"Number of lights ON: {numberOfLightsOn}");