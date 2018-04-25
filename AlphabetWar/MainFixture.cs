using System;
using AlphabetWar;

namespace AlphabetWar
{
    class MainFixture
    {
        private static string GenerateRandomString(int length)
        {
            var chars = "abcdefghijklmnopqrstuvwxyz";
            var stringChars = new char[length];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new String(stringChars);
        }

        public static void Main(string[] args)
        {
            string fight = (args.Length > 1) ? fight = args[1] : GenerateRandomString(4);
            string result = Kata.AlphabetWar(fight);

            Console.WriteLine("Fight: {}, result: {}", fight, result);
            Console.ReadLine();
        }
    }
}
