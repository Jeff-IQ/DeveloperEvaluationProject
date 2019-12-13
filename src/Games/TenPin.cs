using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingScores.Games
{
    public class TenPin : RegularGame
    {
        public override int NumberOfPinsPerFrame => 10;
        public override int NumberOfFrames => 10;
        public override int NumberOfBonusFrames => 1;
        
        public TenPin()
        {

        }
    }
}
