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

/* using System;
using System.IO;
using System.Text.RegularExpressions;

internal class Program
{
    private static void Main(string[] args)
    {
        string fileName = @"adventofcode.com_2015_day_8_input.txt";
        // střídám tu "test.txt" a finální "adventofcode.com_2015_day_8_input.txt"
        int odpoved = ReadTheFileByChar(fileName) - CountStringSizes(fileName);
        Console.WriteLine($"--------------------------\nThe answer is: {odpoved}\n--------------------------");
    }

    static int ReadTheFileByChar(string path)
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
            return countCharsInCode;
        }
        catch (Exception e)
        {
            Console.WriteLine("The process failed: {0}", e.ToString());
            return -1;
        }
    }

    static int CountStringSizes(string path)
    {
        int totalLengthOfStrings = 0;
        int alternativeCount = 0;
        string[] lines = File.ReadAllLines(path);
        Regex rx = new Regex(@"\\x([0-9a-fA-F]{1,2})");
        Regex rspace = new Regex(@"\\s");
        Regex 
        MatchCollection matches;
        MatchCollection matchSpaces;
        string tmpLine = "";
        string outputLines = "";
        foreach (var line in lines)
        {
            tmpLine = line;

            tmpLine = tmpLine.Remove(0, 1);
            tmpLine = tmpLine.Remove(tmpLine.Length - 1, 1);

            matches = rx.Matches(tmpLine);
            foreach (Match match in matches)
            {
                string unicodeStr = match.Groups[1].Value;
                char charValue = (char)Convert.ToInt32(unicodeStr, 16);
                tmpLine = tmpLine.Replace(match.Value, charValue.ToString()); // "" přehodit na přfvedený unicode znak
            }
            matchSpaces = rspace.Matches(tmpLine);
            foreach (Match matchedSpace in matchSpaces)
            {
                tmpLine = tmpLine.Replace(matchedSpace.Value, "");
            }

            tmpLine = tmpLine.Replace("\\\"", "\"");
            tmpLine = tmpLine.Replace("\\\\", "\\");




            // System.Console.WriteLine(tmpLine);
            totalLengthOfStrings += tmpLine.Length;
            alternativeCount += line.Length;
            outputLines += tmpLine + "\n";
        }

        string outputFileName = @"output.txt";
        File.WriteAllText(outputFileName, String.Empty);
        File.WriteAllText(outputFileName, outputLines);

        System.Console.WriteLine($"Code representation alternative count: {alternativeCount}");
        System.Console.WriteLine($"Number of characters in strings: {totalLengthOfStrings}");
        return totalLengthOfStrings;


    }
}
 */
/* Operace se stringem:
1 NAHRADIT: Unicode znaky mají backslash x a dvouciferné hexačíslo = (dva znaky číslice nabo a..f A..F)  Př. \x1a
  \\x([0-9a-fA-F]{1,2})
2 ODEBRAT backslash před úvozovkama
3 ODEBRAT úvozovky na začátku a na konci
4 NAHRADIT zdvojený backslash (ale bacha, co když někde budou 3 za sebou?)
*/


// Nakonec jsem to vzdal a řešení okopíroval z:
//https://www.reddit.com/r/adventofcode/comments/3vw32y/day_8_solutions/
// v c# od uživatele wafflepie
// ještě si to musím pořádně prostudovat, abych pochopil, co jsem vlastně dělal blbě v první části mělo vyjít 1342, já byl nejblíž 1344

using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day8
{
    internal class Program
    {
        private static void Main()
        {
            var words = File.ReadAllLines("adventofcode.com_2015_day_8_input.txt");
            Console.Out.WriteLine(words.Sum(w => w.Length - Regex.Replace(w.Trim('"').Replace("\\\"", "A").Replace("\\\\", "B"), "\\\\x[a-f0-9]{2}", "C").Length));
            Console.Out.WriteLine(words.Sum(w => w.Replace("\\", "AA").Replace("\"", "BB").Length + 2 - w.Length));
        }
    }
}