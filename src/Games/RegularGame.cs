using BowlingScores;
using BowlingScores.Frames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BowlingScores.Games
{
    public abstract class RegularGame : IGame
    {
        public abstract int NumberOfPinsPerFrame { get; }
        public abstract int NumberOfFrames { get; }
        public abstract int NumberOfBonusFrames { get; }

        public List<IFrame> Frames { get => _frames; }
        private List<IFrame> _frames = new List<IFrame>();

        protected virtual string AllowedCharacters { get=> "^[1-9\\|X\\/-]+$"; }

        //These indicate how many balls after a strike or spare to get extra points for
        protected virtual int StrikeAdjustment { get => 2; }
        protected virtual int SpareAdjustment { get => 1; }

        public int GetTotalScore() => _frames.Sum(x => x.GetScore());
 
        protected virtual void VerifyInputString(string input)
        {            
            Regex myRegeX = new Regex(AllowedCharacters);
            var valid = myRegeX.Match(input);

            if (!valid.Success)
            {
                var wrongchars = Regex.Replace(input, AllowedCharacters, "");
                throw new InvalidOperationException($"Unknown character(s) `{wrongchars}` included in score data.");
            }

        }

        /// <summary>
        /// Main Function for inputing scores, validates input string, determines and validates the number of Frames, and calculates the score for each Frame.
        /// </summary>
        /// <param name="scoreBoard"></param>
        public virtual void ParseGameScore(string scoreBoard)
        {

            VerifyInputString(scoreBoard);
            string[] bonusFrames = new string[0];

            var regularVsBonusFrames = scoreBoard.Split("||");
            var regularFrames = regularVsBonusFrames[0].Split("|");

            if (regularVsBonusFrames.Length > 1) //won't always have bonus frames for all games ex. Five Pin
            {
                bonusFrames = regularVsBonusFrames[1].Split("|");
            }

            ValidateNumberOfFrames(regularFrames, bonusFrames);

            for (int x = 1; x <= regularFrames.Count(); x++)
            {
                var currentFrame = new RegularFrame(x);
                var frameData = regularFrames[x - 1];
                AddBallScoresToFrameAndMarkFrameForLaterBonusPoints(currentFrame, frameData);
                _frames.Add(currentFrame);
            }

            for (int x = 1; x <= bonusFrames.Count(); x++)
            {
                var currentFrame = new BonusFrame(x + NumberOfFrames);
                var frameData = bonusFrames[x-1];
                AddBallScoresToFrameAndMarkFrameForLaterBonusPoints(currentFrame, frameData);
                _frames.Add(currentFrame);
            }

        }

        /// <summary>
        /// Takes frame and ball roll data to apply the score, marks frame for future bonus points, adds scores to any frames marked to take bonus points
        /// </summary>
        /// <param name="currentFrame">The frame to add data too (usually empty)</param>
        /// <param name="frameData">string representation of teh ball data examples: 'X', '-3', '4/' </param>
        /// <returns>total score for the current balls</returns>
        public virtual int AddBallScoresToFrameAndMarkFrameForLaterBonusPoints(IFrame currentFrame, string frameData)
        {
            int totalScoreForFrame = 0;
            foreach (char ball in frameData)
            {
                int score = 0;
                bool strike, spare = false;

                //could create a "ball" class to store the score, strike and spare info, but since there is no need to pass
                //that information anywhere else, just handling this with a tuple.
                (score, strike, spare) = CalculateBallScoreAndStrikeOrSpare(ball, currentFrame.GetScore());

                currentFrame.AddBallScore(score, strike, spare);


                if (strike) { SetFrameToTakeBonusPoints(currentFrame, StrikeAdjustment); }
                if (spare) { SetFrameToTakeBonusPoints(currentFrame, SpareAdjustment); }

                AddBonusPointsToMarkedFrames(score);
                totalScoreForFrame += score;
            }

            return totalScoreForFrame;
        }

        protected virtual void AddBonusPointsToMarkedFrames(int pointsToAdd)
        {
            var framesToAddPointsTo = _frames.Where(f => f.ScoreNextNumBalls > 0);

            foreach (IFrame frameToUpdate in framesToAddPointsTo)
            {
                frameToUpdate.AddBonusPoints(pointsToAdd);
            }
        }

        public virtual void SetFrameToTakeBonusPoints(IFrame frame,int numberOfTimesToAddBonuses)
        {
            //Could potentially change this to add to the bonuses expected rather than assign a value;
            frame.ScoreNextNumBalls=numberOfTimesToAddBonuses;
        }

        public virtual bool ValidateNumberOfFrames(string[] regular, string[] bonus)
        {
            if (regular.Count() > NumberOfFrames || bonus.Count() > NumberOfBonusFrames)
            {
                throw new InvalidOperationException($"The input Game Score appears to have too many frames for the game");

            }
            return true;
        }


        /// <summary>
        /// Calculate a point value for the input character (X=strike, /=spare)
        /// </summary>
        /// <param name="ball">the character representation to interpret</param>
        /// <param name="currentScore">existing score in the frame after all previous balls have been evaluated</param>
        /// <returns>value of the ball, if it was a strike, if it was a spare</returns>
        public virtual (int, bool, bool) CalculateBallScoreAndStrikeOrSpare(char ball, int currentScore=0)
        {
            //could create a "ball" class to store the score, strike and spare info, but since there is no need to pass
            //that information anywhere else, just handling this with a tuple.

            var currentBallScore = 0;
            
            switch (ball)
            {
                case 'X':
                    currentBallScore = NumberOfPinsPerFrame;
                    return (currentBallScore, true, false);

                case '/':
                    currentBallScore = NumberOfPinsPerFrame - currentScore;
                    return (currentBallScore, false, true);

                case '-':
                    currentBallScore = 0;
                    break;
                default:
                    currentBallScore = (int)char.GetNumericValue(ball);
                    break;
            }
            return (currentBallScore, false, false);

        }

        public virtual int GetScoreForFirstNFrames(int numberOfFrames)
        {
            return _frames.Take(numberOfFrames).Sum(x => x.GetScore());
        }
    }
}
