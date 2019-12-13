using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BowlingScores.Frames
{
    public class RegularFrame : IFrame
    {
        private List<int> BallScores = new List<int>();

        public int FrameNumber { get; private set; }
        public bool IsStrike { get; private set; }
        public bool IsSpare { get; private set; }

        public int ScoreNextNumBalls {get; set;}

        public int BonusPoints { get; private set; }
        
        public int GetScore() => BallScores.Sum() + BonusPoints;

        public RegularFrame(int frameNumber)
        {
            SetFrameNumber(frameNumber);
        }
          
        private void SetFrameNumber(int frameNumber)
        {
            FrameNumber = frameNumber;
        }

        public void AddBallScore(int score, bool strike = false, bool spare = false)
        {
            BallScores.Add(score);
            IsStrike = strike;            
            IsSpare = spare;           
        }

        public void AddBonusPoints(int bonus)
        {
            //considered adding a check here to see if ScoreNextNumBalls was greater than 0
            //but left that up to the game classes to enforce to allow for more flexibility
            BonusPoints += bonus;
                ScoreNextNumBalls = Math.Max(0, ScoreNextNumBalls - 1);            
        }

    }
}
