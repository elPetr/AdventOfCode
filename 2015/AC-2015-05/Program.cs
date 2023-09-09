/* 
https://adventofcode.com/2015/day/5
--- Day 5: Doesn't He Have Intern-Elves For This? ---
Santa needs help figuring out which strings in his text file are naughty or nice.

A nice string is one with all of the following properties:

It contains at least three vowels (aeiou only), like aei, xazegov, or aeiouaeiouaeiou.
It contains at least one letter that appears twice in a row, like xx, abcdde (dd), or aabbccdd (aa, bb, cc, or dd).
It does not contain the strings ab, cd, pq, or xy, even if they are part of one of the other requirements.
For example:

ugknbfddgicrmopn is nice because it has at least three vowels (u...i...o...), a double letter (...dd...), and none of the disallowed substrings.
aaa is nice because it has at least three vowels and a double letter, even though the letters used by different rules overlap.
jchzalrnumimnmhp is naughty because it has no double letter.
haegwjzuvuyypxyu is naughty because it contains the string xy.
dvszwmarrgswjxmb is naughty because it contains only one vowel.
How many strings are nice?

--- Part Two ---
Realizing the error of his ways, Santa has switched to a better model of determining whether a string is naughty or nice. None of the old rules apply, as they are all clearly ridiculous.

Now, a nice string is one with all of the following properties:

It contains a pair of any two letters that appears at least twice in the string without overlapping, like xyxy (xy) or aabcdefgaa (aa), but not like aaa (aa, but it overlaps).
It contains at least one letter which repeats with exactly one letter between them, like xyx, abcdefeghi (efe), or even aaa.
For example:

qjhvhtzxzqqjkmpb is nice because is has a pair that appears twice (qj) and a letter that repeats with exactly one letter between them (zxz).
xxyxx is nice because it has a pair that appears twice and a letter that repeats with one between, even though the letters used by each rule overlap.
uurcxstgmygtbstg is naughty because it has a pair (tg) but no repeat with a single letter between them.
ieodomkazucvgmuy is naughty because it has a repeating letter with one between (odo), but no pair that appears twice.
How many strings are nice under these new rules?

-------------------------------------------------

input file: d:\SOURCE\AdventOfCode\AC-2015-05\adventofcode.com_2015_day_5_input.txt
 */
using System;
using System.IO;
using System.Text.RegularExpressions;

string fileName = @"adventofcode.com_2015_day_5_input.txt";
string[] lines = File.ReadAllLines(fileName);
int nastyWordsCount = 0;
int niceWordsCount = 0;
string searchThreeWovels = @"[aeiou].*[aeiou].*[aeiou]";
string searchDoubleLetter = @"(.)\1";
string searchBadStrings = @"(ab|cd|pq|xy)";

foreach (string line in lines)
{
    // tip od Matějena optimalizaci, regex umí být náročný, u většího projektu by se vyplatilo
    // to z hasThreeWovels při false rovnou pustit do "nasty" a ušetřit si další regexy
    // to samé hasDoubleLetter ... a sařadit je od nejméně náročného k náročnému
    bool hasThreeWovels = Regex.IsMatch(line, searchThreeWovels); 
    bool hasDoubleLetter = Regex.IsMatch(line, searchDoubleLetter);
    bool doesNotHaveBadStrings = !Regex.IsMatch(line, searchBadStrings);
    if (hasThreeWovels && hasDoubleLetter && doesNotHaveBadStrings)
    { 
        niceWordsCount++; 
    }
    else 
    {   
        nastyWordsCount++;
    }
}

Console.WriteLine("Under old rules:");
Console.WriteLine($"Number of NICE words: {niceWordsCount}");
Console.WriteLine($"Number of NASTY words: {nastyWordsCount}");

nastyWordsCount = 0;
niceWordsCount = 0;
string searchTwoDoubles = @".*(..).*(\1).*";
string searchXyX = @"([a-z]).\1";
// tenhle by to dovedl najít najednou: ^(?=.*([a-z][a-z])\1)(?=.*([a-z]).\2).+$

foreach (string line in lines)
{
    bool hasTwoDoubles = Regex.IsMatch(line, searchTwoDoubles);
    bool hasXyX = Regex.IsMatch(line, searchXyX);

    if (hasTwoDoubles && hasXyX)
    { 
        niceWordsCount++;
        Console.WriteLine($"NICE word: {line}"); 
    }
    else 
    {   
        nastyWordsCount++;
    }
}

Console.WriteLine("Under new rules:");
Console.WriteLine($"Number of NICE words: {niceWordsCount}");
Console.WriteLine($"Number of NASTY words: {nastyWordsCount}");