using System;
using System.Collections.Generic;
using System.Text;
using BowlingScores.Games;

namespace BowlingScores
{
    public static class BowlingScore
    {

        public static int ScoreGame(string input, GamesEnum gameType = GamesEnum.TenPin)
        {
            IGame myGame = BuildGame(gameType);
            myGame.ParseGameScore(input);
            return myGame.GetTotalScore();
        }

        private static IGame BuildGame(GamesEnum gameType)
        {
            switch (gameType)
            {
                case GamesEnum.FivePin:
                    return new FivePin();
                default:
                    return new TenPin();
            }
        }

    }
}
