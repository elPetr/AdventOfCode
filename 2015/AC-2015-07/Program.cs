/* 
https://adventofcode.com/2015/day/7
--- Day 7: Some Assembly Required ---
This year, Santa brought little Bobby Tables a set of wires and bitwise logic gates! Unfortunately, little Bobby is a little under the recommended age range, and he needs help assembling the circuit.

Each wire has an identifier (some lowercase letters) and can carry a 16-bit signal (a number from 0 to 65535). A signal is provided to each wire by a gate, another wire, or some specific value. Each wire can only get a signal from one source, but can provide its signal to multiple destinations. A gate provides no signal until all of its inputs have a signal.

The included instructions booklet describes how to connect the parts together: x AND y -> z means to connect wires x and y to an AND gate, and then connect its output to wire z.

For example:

123 -> x means that the signal 123 is provided to wire x.
x AND y -> z means that the bitwise AND of wire x and wire y is provided to wire z.
p LSHIFT 2 -> q means that the value from wire p is left-shifted by 2 and then provided to wire q.
NOT e -> f means that the bitwise complement of the value from wire e is provided to wire f.
Other possible gates include OR (bitwise OR) and RSHIFT (right-shift). If, for some reason, you'd like to emulate the circuit instead, almost all programming languages (for example, C, JavaScript, or Python) provide operators for these gates.

For example, here is a simple circuit:

123 -> x
456 -> y
x AND y -> d
x OR y -> e
x LSHIFT 2 -> f
y RSHIFT 2 -> g
NOT x -> h
NOT y -> i
After it is run, these are the signals on the wires:

d: 72
e: 507
f: 492
g: 114
h: 65412
i: 65079
x: 123
y: 456
In little Bobby's kit's instructions booklet (provided as your puzzle input), what signal is ultimately provided to wire a?
---------------------------------------------------------------------------------------------------------------------------
input file: d:\SOURCE\AdventOfCode\2015\AC-2015-07\adventofcode.com_2015_day_7_input.txt

Your puzzle answer was 956.

--- Part Two ---
Now, take the signal you got on wire a, override wire b to that signal, and reset the other wires (including wire a). What new signal is ultimately provided to wire a?
input file2: d:\SOURCE\AdventOfCode\2015\AC-2015-07\adventofcode.com_2015_day_7_input-B.txt
Your puzzle answer was 40149.
 */

using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;

void Main()
{
    string fileName = @"adventofcode.com_2015_day_7_input.txt";  //prohodit za adventofcode.com_2015_day_7_input-B.txt pro druhou půlku úkolu
    string[] lines = File.ReadAllLines(fileName);
    List<string> unsolvedLinesList = new List<string>(lines);
    List<string> solvedLinesList = new List<string>();
    Dictionary<string, int> components = new Dictionary<string, int>();

    do
    {
        for (int i = 0; i < unsolvedLinesList.Count; i++)
        {
            string line = unsolvedLinesList[i];
            try
            {
                ProcessLine(line, components);
                solvedLinesList.Add(line);
                unsolvedLinesList.RemoveAt(i);
    
            }
            catch (KeyNotFoundException)
            {
              // Console.WriteLine($"Line: {i} skipped.");
            }
            
        }
        Console.WriteLine($"Solved lines: {solvedLinesList.Count}");
        Console.WriteLine($"Unsolved lines: {unsolvedLinesList.Count}");
    } while (unsolvedLinesList.Count > 0);

    printComponents(components);
}

void ProcessLine(string line, Dictionary<string, int> components)
{
    string[] parsedLine = line.Split(' '); //tak bacha, tady to bude jinak
    int operandInt1;
    int operandInt2;

    switch ((parsedLine.Length))
    {
        case 3:
            {
                //Console.WriteLine($"{parsedLine[0]}\t{parsedLine[1]}\t{parsedLine[2]}");
                if (!(int.TryParse(parsedLine[0], out operandInt1)))
                {
                    operandInt1 = components[parsedLine[0]];
                }

                components[parsedLine[2]] = operandInt1;
                break;
            }
        case 4:
            {
                // Console.WriteLine($"{parsedLine[0]}\t{parsedLine[1]}\t{parsedLine[2]}\t{parsedLine[3]}");
               components[parsedLine[3]] = ~components[parsedLine[1]];
                break;
            }
        case 5:
            {
                //Console.WriteLine($"{parsedLine[0]}\t{parsedLine[1]}\t{parsedLine[2]}\t{parsedLine[3]}\t{parsedLine[4]}");
                if (!(int.TryParse(parsedLine[0], out operandInt1)))
                {
                    operandInt1 = components[parsedLine[0]];
                }
                if (!(int.TryParse(parsedLine[2], out operandInt2)))
                {
                    operandInt2 = components[parsedLine[2]];
                }
                switch (parsedLine[1])
                {
                    case "AND":
                        components[parsedLine[4]] = operandInt1 & operandInt2;
                        break;
                    case "OR":
                        components[parsedLine[4]] = operandInt1 | operandInt2;
                        break;
                    case "RSHIFT":
                        components[parsedLine[4]] = operandInt1 >> operandInt2;
                        break;
                    case "LSHIFT":
                        components[parsedLine[4]] = operandInt1 << operandInt2;
                        break;
                    default:
                        Console.WriteLine($"Nečekaný 5-ti člen: {parsedLine[0]}\t{parsedLine[1]}\t{parsedLine[2]}\t{parsedLine[3]}\t{parsedLine[4]}");
                    break;
                }
                break;
            }           
        default:
            break;
    }
}

void printComponents(Dictionary<string, int> components)
{
    Console.WriteLine($"Přehled komponentů a jejich hodnot:\n===================================");
    foreach (KeyValuePair<string, int> component in components)
    {
        Console.WriteLine($"{component.Key}\t{component.Value}");
    }
}

Main();
// postup
//nacist operace do pole, ale muzou mit ruzny pocet polozek
// napravo je vždy proměnná
// řešit zvlášť
// 3 prvky - proměnná nebo číslo = proměnná
// 4 prvky - NOT proměnná = proměnná
// 5 prvků - číslo/proměnná OPERATOR číslo/proměnná = proměnná
// postupně procházet pole a vyřešené řádky ukládat do Slovníku ve formátu proměnná, číslo