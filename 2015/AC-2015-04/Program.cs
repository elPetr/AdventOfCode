/* https://adventofcode.com/2015/day/4
--- Day 4: The Ideal Stocking Stuffer ---
Santa needs help mining some AdventCoins (very similar to bitcoins) to use as gifts for all the economically forward-thinking little girls and boys.

To do this, he needs to find MD5 hashes which, in hexadecimal, start with at least five zeroes. The input to the MD5 hash is some secret key (your puzzle input, given below) followed by a number in decimal. To mine AdventCoins, you must find Santa the lowest positive number (no leading zeroes: 1, 2, 3, ...) that produces such a hash.

For example:

If your secret key is abcdef, the answer is 609043, because the MD5 hash of abcdef609043 starts with five zeroes (000001dbbfa...), and it is the lowest such number to do so.
If your secret key is pqrstuv, the lowest number it combines with to make an MD5 hash starting with five zeroes is 1048970; that is, the MD5 hash of pqrstuv1048970 looks like 000006136ef....

Your puzzle input is ckczppom.
---------------------------------------------------
5 zeroes:
00000FE1C139A2C710E9A5C03EC1AF03
ckczppom117946
6 zeroes
00000028023E3B4729684757F8DC3FBF
ckczppom3938038
---------------------------------------------------

 */
using System;
using System.Text;
using System.Security.Cryptography;

public class Example
{
    static string ComputeMD5(string s)
    {
        using (MD5 md5 = MD5.Create())
        {
            byte[] hashValue = md5.ComputeHash(Encoding.UTF8.GetBytes(s));
            return Convert.ToHexString(hashValue);
        }
    }

    public static void Main()
    {
        //int number = 1;
        for (int number = 0; number < 10000000; number++)
        {
            string hash = ComputeMD5("ckczppom" + number.ToString());
            if (hash.StartsWith("000000"))
            {
                Console.WriteLine(hash);
                Console.WriteLine("ckczppom" + number.ToString());
                break;
            }
        }

    }
}