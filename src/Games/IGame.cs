using BowlingScores.Frames;
using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingScores.Games
{
    public interface IGame
    {
        public int NumberOfPinsPerFrame { get;  }
        public int NumberOfFrames { get;}
        public int NumberOfBonusFrames { get; }
        public List<IFrame> Frames { get; }
        
        public int GetTotalScore();

        /// <summary>
        /// Main Function for inputing scores, validates input string, determines and validates the number of Frames, and calculates the score for each Frame.
        /// </summary>
        /// <param name="scoreBoard">string input of scorecard ex. 22|X|4/||81</param>
        public void ParseGameScore(string scoreCard);

        public int GetScoreForFirstNFrames(int numberOfFramesToScore);
    }
}
