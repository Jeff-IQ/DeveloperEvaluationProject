using NUnit.Framework;
using BowlingScores.Games;
using BowlingScores.Frames;
using System;
using System.Linq;

namespace Tests
{
    class TenPinTests
    {
        [Test]
        public void Verify_Frame_Amount_Limits()
        {
            var Game = new TenPin();
            Assert.IsTrue(Game.ValidateNumberOfFrames(new string[10], new string[1]));
            try
            {
                Game.ValidateNumberOfFrames(new string[11], new string[1]);
            }
            catch (Exception e)
            {
                Assert.AreEqual("The input Game Score appears to have too many frames for the game", e.Message);
            }
            try
            {
                Game.ValidateNumberOfFrames(new string[10], new string[2]);
            }
            catch (Exception e)
            {
                Assert.AreEqual("The input Game Score appears to have too many frames for the game", e.Message);
            }
        }

        [Test]
        public void Create_New_Game()
        {
            var Game = new TenPin();
            Assert.IsNotNull(Game);
            Assert.AreEqual(0, Game.GetTotalScore());
        }

        [Test]
        public void Verify_Strikes_Return_Correct_Values()
        {
            var Game = new TenPin();
            Assert.AreEqual((10, true, false), Game.CalculateBallScoreAndStrikeOrSpare('X', 0));

        }

        [Test]
        public void Verify_Spare_after_Blank_Return_Correct_Values()
        {
            var Game = new TenPin();
            Assert.AreEqual((10, false, true), Game.CalculateBallScoreAndStrikeOrSpare('/', 0));

        }

        [Test]
        public void Verify_Spare_after_5_Return_Correct_Values()
        {
            var Game = new TenPin();
            Assert.AreEqual((5, false, true), Game.CalculateBallScoreAndStrikeOrSpare('/', 5));

        }

        [Test]
        public void Verify_Spare_after_9_Return_Correct_Values()
        {
            var Game = new TenPin();
            Assert.AreEqual((1, false, true), Game.CalculateBallScoreAndStrikeOrSpare('/', 9));
        }

        [Test]
        public void Verify_Blank_Return_Correct_Scores()
        {
            var Game = new TenPin();
            Assert.AreEqual((0, false, false), Game.CalculateBallScoreAndStrikeOrSpare('-'));

        }

        [Test]
        public void Create_Ten_Frame_Game_with_No_Strikes_Or_Spares()
        {
            var Game = new TenPin();
            Game.ParseGameScore("54|54|54|54|54|54|54|54|54|54||");
            Assert.AreEqual(11, Game.Frames.Count);
            Assert.AreEqual(90, Game.GetTotalScore());

        }

        [Test]
        public void Verify_Spare_Frame_Gets_BonusPoints_From_Next_Ball()
        {
            var Game = new TenPin();
            Game.ParseGameScore("1/|1-||");
            Assert.AreEqual(11, Game.Frames.FirstOrDefault().GetScore());
            Assert.AreEqual(1, Game.Frames[1].GetScore());
            Assert.AreEqual(12, Game.GetTotalScore());
        }

        [Test]
        public void Verify_Strike_Frame_Gets_BonusPoints_From_Next_Balls()
        {
            var Game = new TenPin();
            Game.ParseGameScore("X|11||");
            Assert.AreEqual(12, Game.Frames.FirstOrDefault().GetScore());
            Assert.AreEqual(2, Game.Frames[1].GetScore());
            Assert.AreEqual(14, Game.GetTotalScore());
        }

        [Test]
        public void All_Frames_Score_9()
        {
            var Game = new TenPin();
            Game.ParseGameScore("54|54|54|54|54|54|54|54|54|54||");
            for (int x = 0; x < 10; x++)
            {
                Assert.AreEqual(9, Game.Frames[x].GetScore());
            }
        }


        public void Verify_Multiple_Strikes_Add_All_Bonus_Points()
        {
            var Game = new TenPin();
            Game.ParseGameScore("X|X|X||");
            Assert.AreEqual(30, Game.Frames.FirstOrDefault().GetScore());
            Assert.AreEqual(20, Game.Frames[1].GetScore());
            Assert.AreEqual(60, Game.GetTotalScore());
        }

        [Test]
        public void All_Strikes_With_Blank_Bonus_Frame()
        {
            IGame game = new TenPin();
            game.ParseGameScore("X|X|X|X|X|X|X|X|X|X||--");
            Assert.AreEqual(270, game.GetTotalScore());

        }

        [Test]
        public void All_Strikes()
        {
            IGame game = new TenPin();
            game.ParseGameScore("X|X|X|X|X|X|X|X|X|X||XX");
            Assert.AreEqual(300, game.GetTotalScore());

        }

        [Test]
        public void All_Spares_On_Second_Ball()
        {
            IGame game = new TenPin();
            game.ParseGameScore("-/|-/|-/|-/|-/|-/|-/|-/|-/|-/||-");
            Assert.AreEqual(100, game.GetTotalScore());
        }

        [Test]
        public void All_Spares_On_Second_Ball_With_BonusStrike()
        {
            IGame game = new TenPin();
            game.ParseGameScore("-/|-/|-/|-/|-/|-/|-/|-/|-/|-/||X");
            Assert.AreEqual(110, game.GetTotalScore());
        }

        [Test]
        public void All_Spares__With_BonusStrike()
        {
            IGame game = new TenPin();
            game.ParseGameScore("-/|-/|-/|-/|-/|-/|-/|-/|-/|-/||X");
            Assert.AreEqual(110, game.GetTotalScore());
        }

        [Test]
        public void Throw_Error_On_Bad_Data()
        {
            IGame game = new TenPin();
            try
            {
                game.ParseGameScore("x");
            }
            catch (Exception e)
            {
                Assert.AreEqual("Unknown character(s) `x` included in score data.", e.Message);
            }
            try
            {
                game.ParseGameScore("\\");
            }
            catch (Exception e)
            {
                Assert.AreEqual("Unknown character(s) `\\` included in score data.", e.Message);
            }
        }

        [Test]
        public void No_Score_Until_Strike_in_Last_Frame()
        {
            TenPin game = new TenPin();
            game.ParseGameScore("--|--|--|--|--|--|--|--|--|X||24");
            Assert.AreEqual(16, game.GetTotalScore());
        }

        [Test]
        public void Verify_Balls_Return_Correct_Scores()
        {
            var Game = new TenPin();
            Assert.AreEqual((1, false, false), Game.CalculateBallScoreAndStrikeOrSpare('1'));
            Assert.AreEqual((2, false, false), Game.CalculateBallScoreAndStrikeOrSpare('2'));
            Assert.AreEqual((3, false, false), Game.CalculateBallScoreAndStrikeOrSpare('3'));
            Assert.AreEqual((4, false, false), Game.CalculateBallScoreAndStrikeOrSpare('4'));
            Assert.AreEqual((5, false, false), Game.CalculateBallScoreAndStrikeOrSpare('5'));
            Assert.AreEqual((6, false, false), Game.CalculateBallScoreAndStrikeOrSpare('6'));
            Assert.AreEqual((7, false, false), Game.CalculateBallScoreAndStrikeOrSpare('7'));
            Assert.AreEqual((8, false, false), Game.CalculateBallScoreAndStrikeOrSpare('8'));
            Assert.AreEqual((9, false, false), Game.CalculateBallScoreAndStrikeOrSpare('9'));
        }

        [Test]
        public void ScoreFirst3Frames()
        {
            TenPin game = new TenPin();
            game.ParseGameScore("X|X|X|X|X|--|--|--|--|X||25");
            Assert.AreEqual(90, game.GetScoreForFirstNFrames(3));
        }

        [Test]
        public void ScoreFirst3FramesButOnlyLoad1()
        {
            TenPin game = new TenPin();
            game.ParseGameScore("X");
            Assert.AreEqual(10, game.GetScoreForFirstNFrames(3));
        }

        [Test]
        public void Score_And_Mark_Frame1()
        {
            int expectedValue = 10;
            IFrame frame = new RegularFrame(1);
            TenPin game = new TenPin();
            int score=game.AddBallScoresToFrameAndMarkFrameForLaterBonusPoints(frame, "X");
            Assert.AreEqual(expectedValue, score);
            Assert.AreEqual(expectedValue, frame.GetScore());
            Assert.IsTrue(frame.ScoreNextNumBalls == 2);
        }

        [Test]
        public void Score_And_Mark_Frame2()
        {            
            int expectedValue = 20;
            IFrame frame = new RegularFrame(1);
            TenPin game = new TenPin();
            int score = game.AddBallScoresToFrameAndMarkFrameForLaterBonusPoints(frame, "XX");
            Assert.AreEqual(expectedValue, score);
            Assert.AreEqual(expectedValue, frame.GetScore()); 
            //because the frame isn't actually part of the collection of frames in the game it doesn't get the "bonus points" for the second ball
            Assert.IsTrue(frame.ScoreNextNumBalls == 2);
        }

        [Test]
        public void Score_And_Mark_Frame3()
        {
            int expectedValue = 10;
            IFrame frame = new RegularFrame(1);
            TenPin game = new TenPin();
            int score = game.AddBallScoresToFrameAndMarkFrameForLaterBonusPoints(frame, "-/");
            Assert.AreEqual(expectedValue, score);
            Assert.AreEqual(expectedValue, frame.GetScore());
            Assert.IsTrue(frame.ScoreNextNumBalls == 1);
        }

        [Test]
        public void Score_And_Mark_Frame4()
        {
            int expectedValue = 10;
            IFrame frame = new RegularFrame(1);
            TenPin game = new TenPin();
            int score = game.AddBallScoresToFrameAndMarkFrameForLaterBonusPoints(frame, "/");
            Assert.AreEqual(expectedValue, score);
            Assert.AreEqual(expectedValue, frame.GetScore());
            Assert.IsTrue(frame.ScoreNextNumBalls == 1);
        }

        [Test]
        public void Score_And_Mark_Frame5()
        {
            int expectedValue = 4;
            IFrame frame = new RegularFrame(1);
            TenPin game = new TenPin();
            int score = game.AddBallScoresToFrameAndMarkFrameForLaterBonusPoints(frame, "4-");
            Assert.AreEqual(expectedValue, score);
            Assert.AreEqual(expectedValue, frame.GetScore());
            Assert.IsTrue(frame.ScoreNextNumBalls == 0);
        }

        [Test]
        public void Score_And_Mark_Frame6()
        {
            int expectedValue = 10;
            IFrame frame = new RegularFrame(1);
            TenPin game = new TenPin();
            int score = game.AddBallScoresToFrameAndMarkFrameForLaterBonusPoints(frame, "46");
            //the scorecard SHOULD have marked this as "4/" for the spare, but we want to make sure that unless it is
            //specifically marked as a spare don't set it to add in bonus points later
            Assert.AreEqual(expectedValue, score);
            Assert.AreEqual(expectedValue, frame.GetScore());
            Assert.IsTrue(frame.ScoreNextNumBalls == 0);
        }
    }
}
