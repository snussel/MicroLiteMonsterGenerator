using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Globalization;

namespace MicroLiteMonsterGenerator
{
    class Program
    {
        //Enum of the available attack types
        enum AttackTypes { Bite, Claw, Slam, Gore, Sting, Tentacle, Shock, Broadsword, Battleaxe, Club, Glaive, Spear, Falchion, Dagger }

        //Random number 
        private static Random _rand = new Random();

        //The public dictionary
        private static Dictionary<char, string[]> np = new Dictionary<char, string[]>();

        static void Main(string[] args)
        {
            //Populate the dictionary with data
            np = PopulateDictinary();

            //Get the HD from the user
            Console.Write("Enter number of hit dice(int): ");
            int.TryParse(Console.ReadLine(), out int numDie);

            //Create the monster
            CreateMonster(numDie);            
        }

        /// <summary>
        /// Method to create a monster from scratch
        /// </summary>
        /// <param name="hD">The number of Hit Die that the user specified</param>
        private static void CreateMonster(int hD)
        {
            Array values = Enum.GetValues(typeof(AttackTypes));

            Console.WriteLine($"Name: {GetWord()}");
            Console.WriteLine($"HD: {hD} ({(int)(Math.Floor(hD * 4.5)) + _rand.Next(hD * 4)}hp)");
            Console.WriteLine($"AC: {_rand.Next(5) + hD + 10}");
            Console.WriteLine($"Attack: {values.GetValue(_rand.Next(values.Length))} + {hD + _rand.Next(4)} ({_rand.Next(2) + 1}d{2 * (_rand.Next(6) + 1)})");
            Console.WriteLine($"");
            Console.WriteLine($"Done");
        }

        /// <summary>
        /// Method to Create the title of the monster
        /// </summary>
        /// <returns>A semi random string</returns>
        private static string GetWord()
        {            
            TextInfo ti = new CultureInfo("en-us", false).TextInfo;
            string word = "W";
            Regex rgEx = new Regex(@"[A-Z]");
            GroupCollection matches;

            while (word.Length < 40)
            {
                Match match = rgEx.Match(word);

                if (match.Success)
                    matches = match.Groups;                
                else
                    return ti.ToTitleCase(word);

                word = word.Replace(matches[0].Value, np[Convert.ToChar(matches[0].Value)][_rand.Next(np[Convert.ToChar(matches[0].Value)].Length)]);
            }            

            return ti.ToTitleCase(word);
        }

        /// <summary>
        /// Method to populate a Dictinary with values
        /// </summary>
        /// <returns>A Populated Dictinary</returns>
        private static Dictionary<char, string[]> PopulateDictinary()
        {
            Dictionary<char, string[]> temp = new Dictionary<char, string[]>();

            temp.Add('W', new string[] { "CT", "CT", "CX", "CDF", "CVFT", "CDFU", "CTU", "IT", "ITC", "A" });
            temp.Add('A', new string[] { "KVKVtion" });
            temp.Add('K', new string[] { "b", "c", "d", "f", "g", "j", "l", "m", "n", "p", "qu", "r", "s", "t", "v", "sP" });
            temp.Add('I', new string[] { "ex", "in", "un", "re", "de" });
            temp.Add('T', new string[] { "VF", "VEe" });
            temp.Add('U', new string[] { "er", "ish", "ly", "en", "ing", "ness", "ment", "able", "ive" });
            temp.Add('C', new string[] { "b", "c", "ch", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "qu", "r", "s", "sh", "t", "th", "v", "w", "y", "sP", "Rr", "Ll" });
            temp.Add('E', new string[] { "b", "c", "ch", "d", "f", "g", "dg", "l", "m", "n", "p", "r", "s", "t", "th", "v", "z" });
            temp.Add('F', new string[] { "b", "tch", "d", "ff", "g", "gh", "ck", "ll", "m", "n", "n", "ng", "p", "r", "ss", "sh", "t", "tt", "th", "x", "y", "zz", "rR", "sP", "lL" });
            temp.Add('P', new string[] { "p", "t", "k", "c" });
            temp.Add('Q', new string[] { "b", "d", "g" });
            temp.Add('L', new string[] { "b", "f", "k", "p", "s" });
            temp.Add('R', new string[] { "P", "Q", "f", "th", "sh" });
            temp.Add('V', new string[] { "a", "e", "i", "o", "u" });
            temp.Add('D', new string[] { "aw", "ei", "ow", "ou", "ie", "ea", "ai", "oy" });
            temp.Add('X', new string[] { "e", "i", "o", "aw", "ow", "oy"});

            return temp;
        }
    }
}
