using BowlingScores.Frames;
using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingScores.Games
{
    public class FivePin : RegularGame
    {

        //Test Comment

        public override int NumberOfPinsPerFrame => 5;
        public override int NumberOfFrames => 10;
        public override int NumberOfBonusFrames => 0;  //The last Frame of 5 pin always uses 3 balls so we don't need the bonus frame                                                       

        protected override string AllowedCharacters { get=> "^[HLRACS2-9\\|X\\/-]+$"; }

        public FivePin()
        {

        }

        public override (int, bool, bool) CalculateBallScoreAndStrikeOrSpare(char ball, int currentScore = 0)
        {
            var currentBallScore = 0;

            switch (ball)
            {
                case 'X':
                    currentBallScore = 15;
                    return (currentBallScore, true, false);
                case '/':
                    currentBallScore = 15 - currentScore;
                    return (currentBallScore, false, true);
                case '-':
                    currentBallScore = 0;
                    break;
                case 'H':  //HeadPin - only got the 5 pin
                    currentBallScore = 5;
                    break;
                case 'L': //CornerPin - only one of the 2 pins is left
                case 'R':
                    currentBallScore = 13;
                    break;
                case 'A':  //Ace - left both 2 pins
                    currentBallScore = 11;
                    break;
                case 'C':  //Chop - Headpin and one full side (both the 2 and 3 pins)
                    currentBallScore = 10;
                    break;
                case 'S':  //Split - HeadPin and one of the 3 pins
                    currentBallScore = 8;
                    break;
                default:
                    currentBallScore = (int)char.GetNumericValue(ball);
                    break;

            }
            return (currentBallScore, false, false);
        }
    }
}
