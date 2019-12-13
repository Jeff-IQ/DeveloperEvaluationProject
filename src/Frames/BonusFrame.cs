using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BowlingScores.Frames
{
    public class BonusFrame : IFrame
    {
        private List<int> BallScores=new List<int>();

        public int FrameNumber { get; private set; }
        public bool IsStrike{ get; private set; }
        public bool IsSpare{ get; private set; }
                
        public int ScoreNextNumBalls { get; set; }
        public int BonusPoints { get; private set; }

        public int GetScore() => BallScores.Sum() + BonusPoints;

        //Bonus frames could have multiple "Strikes"
        public int NumberOfStrikes=0;

        public BonusFrame(int frameNumber)
        {
            setFrameNumber(frameNumber);
        }

        private void setFrameNumber(int frameNumber)
        {
            FrameNumber = frameNumber;
        }

        public void AddBallScore(int score, bool strike = false, bool spare = false)
        {                        
            //Bonus frames don't get scores, but can keep track of spares and strikes
            if (strike)
            {
                IsStrike = strike;
                NumberOfStrikes++;  
            }            
            IsSpare = spare;
        }

        public void AddBonusPoints(int bonus)
        {               
            //Bonus frames don't get Bonus Points
        }

    }
}
