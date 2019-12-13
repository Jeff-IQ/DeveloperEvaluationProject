using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingScores.Frames
{
    public interface IFrame
    {

        public int FrameNumber { get;  }
        public bool IsStrike { get; }
        public bool IsSpare { get; }

        //indicates that we need to add bonus points on the next x Balls thrown
        public int ScoreNextNumBalls { get; set; }
        public int GetScore();

        public void AddBonusPoints(int bonus);
        public void AddBallScore(int score,bool isStrike, bool isSpare);

    }
}
