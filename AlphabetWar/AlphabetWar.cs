using System.Collections.Generic;
using System.Linq;

namespace AlphabetWar
{ 
    public class Player
    {
        private char priest = default(char);
        private char enemyPriest = default(char);
        private char[] letters = null;
        private char[] enemyLetters = null;

        public int points { private set; get; } = 0;

        /// <summary>
        /// Constructor for the player.
        /// </summary>
        /// <param name="letters">Letters for this player (the first letter will be the priest).</param>
        /// <param name="enemyLetters">Letters for the enemy player (the first letter will be the priest).</param>
        public Player(char[] letters, char[] enemyLetters)
        {
            priest = letters[0];
            enemyPriest = enemyLetters[0];
            this.letters = letters.Skip(1).ToArray();
            this.enemyLetters = enemyLetters.Skip(1).ToArray();
        }

        /// <summary>
        /// Method that computes the point of the current letter in the fight.
        /// </summary>
        /// <param name="fight">The fight string.</param>
        /// <param name="c">The indec of the letter currently considered in the fight.</param>
        /// <param name="switchLetter">A flag indicating if the letter needs to be switched between the player and the enemy.</param>
        public void ComputePoints(string fight, int c, bool switchLetter)
        {
            for (int i = 0; i < letters.Length; ++i)
            {
                char comparisonChar = switchLetter ? enemyLetters[i] : letters[i];
                if (comparisonChar == fight[c]) points += i + 1;
            }
        }

        /// <summary>
        /// Method that verifies if a letter at a given position in the fight string is between the two priests.
        /// </summary>
        /// <param name="fight">The fight string.</param>
        /// <param name="c">The position of the char to be considered within the fight string.</param>
        /// <returns>True if the letter at the position passed is between the two priests. False in the opposite case.</returns>
        private bool LetterBetweenTwoPriests(string fight, int c)
        {
            if (c == 0) return false;
            if (c == fight.Length - 1) return false;

            List<char> priestLetters = new List<char> { priest, enemyPriest };
            List<char> stringLetters = new List<char> { fight[c - 1], fight[c + 1] };

            return stringLetters.OrderBy(t => t).SequenceEqual(priestLetters.OrderBy(t => t));
        }

        /// <summary>
        /// Method that verifies if the letter at position given has to be switced between left and right players.
        /// </summary>
        /// <param name="fight">The fight string.</param>
        /// <param name="c">The position of the char to be considered within the fight string.</param>
        /// <returns>True if the letter at the position passed has to be switched between left and right players.</returns>
        public bool SwitchLetter(string fight, int c)
        {
            if (LetterBetweenTwoPriests(fight, c)) return false;

            if (c < fight.Length - 1 && fight[c + 1] == priest && enemyLetters.Contains(fight[c]))
            {
                return true;
            }

            if (c > 0 && fight[c - 1] == priest && enemyLetters.Contains(fight[c]))
            {
                return true;
            }

            return false;
        }
    }

    public class Kata
    {
        private static string MESSAGE_LEFT = "Left side wins!";
        private static string MESSAGE_RIGHT = "Right side wins!";
        private static string MESSAGE_TIE = "Let's fight again!";

        private static char[] leftLetters = new char[] { 't', 's', 'b', 'p', 'w' };
        private static char[] rightLetters = new char[] { 'j', 'z', 'd', 'q', 'm' };

        /// <summary>
        /// Method that just print the string with the winner (or the tie string).
        /// </summary>
        /// <param name="left">Points scored by Left player.</param>
        /// <param name="right">Points scored by Right player.</param>
        /// <returns>The string nominating the winner.</returns>
        private static string NominateWinner(int left, int right)
        {
            if (right == left) return MESSAGE_TIE;
            return (right > left) ? MESSAGE_RIGHT : MESSAGE_LEFT;
        }

        /// <summary>
        /// Method that performs a fight between the left and right player.
        /// </summary>
        /// <param name="fight">The fight string.</param>
        /// <returns>A string indicating the winner between the two players.</returns>
        public static string AlphabetWar(string fight)
        {
            Player left = new Player(leftLetters, rightLetters);
            Player right = new Player(rightLetters, leftLetters);

            for (int c = 0; c < fight.Length; c++)
            {
                bool switchLetter = left.SwitchLetter(fight, c) || right.SwitchLetter(fight, c);
                left.ComputePoints(fight, c, switchLetter);
                right.ComputePoints(fight, c, switchLetter);
            }

            return NominateWinner(left.points, right.points);
        }
    }
}