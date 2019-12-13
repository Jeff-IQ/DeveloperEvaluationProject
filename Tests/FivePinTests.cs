using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using BowlingScores.Games;

namespace Tests
{
    class FivePinTests
    {
        [Test]
        public void No_Score_Until_Strike_in_Last_Frame()
        {
            FivePin game = new FivePin();
            game.ParseGameScore("---|---|---|---|---|---|---|---|---|X35");
            Assert.AreEqual(23, game.GetTotalScore());
        }

        [Test]
        public void No_Score_Until_All_Strikes_in_Last_Frame()
        {
            FivePin game = new FivePin();
            game.ParseGameScore("---|---|---|---|---|---|---|---|---|XXX");
            Assert.AreEqual(45, game.GetTotalScore());
        }

        [Test]
        public void One_Missed_Strike()
        {
            FivePin game = new FivePin();

            game.ParseGameScore("X|X|X|X|X|X|X|X|X|X-X");
            Assert.AreEqual(420, game.GetTotalScore());
        }




        [Test]
        public void Perfect_Game()
        {
            FivePin game = new FivePin();
            game.ParseGameScore("X|X|X|X|X|X|X|X|X|XXX");
            Assert.AreEqual(450, game.GetTotalScore());
        }

        [Test]
        public void All_GutterBall_Game()
        {
            FivePin game = new FivePin();
            game.ParseGameScore("---|---|---|---|---|---|---|---|---|---");
            Assert.AreEqual(0, game.GetTotalScore());
        }


        [Test]
        public void Bonus_Frame_Invalid()
        {
            FivePin game = new FivePin();
            try
            {
                game.ParseGameScore("X|X|X|X|X|X|X|X|X|XXX||");
            }
            catch (Exception e)
            {
                Assert.AreEqual("The input Game Score appears to have too many frames for the game", e.Message);
            }
            
        }

        [Test]
        public void Eleven_Frames_Invalid()
        {
            FivePin game = new FivePin();
            try
            {
                game.ParseGameScore("X|X|X|X|X|X|X|X|X|X|X");
            }
            catch (Exception e)
            {
                Assert.AreEqual("The input Game Score appears to have too many frames for the game", e.Message);
            }

        }



        [Test]
        public void Random_Score_1()
        {
            FivePin game = new FivePin();
            game.ParseGameScore("-25|X|57-|-/|A--|382|C-5|L--|R-2|R/2");
            Assert.AreEqual(156, game.GetTotalScore());
        }

        [Test]
        public void Random_Score_2()
        {
            FivePin game = new FivePin();
            game.ParseGameScore("X|X|57-|-/|A--|382|C/|L/|R-2|R/2");
            Assert.AreEqual(212, game.GetTotalScore());
        }

        [Test]
        public void Strike_adds_only_next_two_balls_score()
        {
            FivePin game = new FivePin();
            game.ParseGameScore("X|223|-");
            Assert.AreEqual(19, game.Frames[0].GetScore());
            Assert.AreEqual(26,game.GetTotalScore());
        }

        [Test]
        public void Spare_adds_only_the_next_ball_to_score()
        {
            FivePin game = new FivePin();
            game.ParseGameScore("2/|223");

            Assert.AreEqual(17, game.Frames[0].GetScore());
            Assert.AreEqual(24, game.GetTotalScore());
        }

        [Test]
        public void All_spares()
        {
            FivePin game = new FivePin();
            game.ParseGameScore("2/|2/|3/|5/|H/|R/|L/|C/|A/|3/X");
            Assert.AreEqual(230, game.GetTotalScore());
        }

        [Test]
        public void All_Allowed_Characters()
        {
            FivePin game = new FivePin();
            game.ParseGameScore("23456789-HRLCA/X");           
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
                game.ParseGameScore("a");
            }
            catch (Exception e)
            {
                Assert.AreEqual("Unknown character(s) `a` included in score data.", e.Message);
            }
        }

        [Test]
        public void Verify_Balls_Return_Correct_Scores()
        {
            FivePin Game = new FivePin();
            Assert.AreEqual((2, false, false), Game.CalculateBallScoreAndStrikeOrSpare('2'));
            Assert.AreEqual((3, false, false), Game.CalculateBallScoreAndStrikeOrSpare('3'));
            Assert.AreEqual((4, false, false), Game.CalculateBallScoreAndStrikeOrSpare('4'));
            Assert.AreEqual((5, false, false), Game.CalculateBallScoreAndStrikeOrSpare('5'));
            Assert.AreEqual((6, false, false), Game.CalculateBallScoreAndStrikeOrSpare('6'));
            Assert.AreEqual((7, false, false), Game.CalculateBallScoreAndStrikeOrSpare('7'));
            Assert.AreEqual((8, false, false), Game.CalculateBallScoreAndStrikeOrSpare('8'));
            Assert.AreEqual((9, false, false), Game.CalculateBallScoreAndStrikeOrSpare('9'));
            Assert.AreEqual((15, true, false), Game.CalculateBallScoreAndStrikeOrSpare('X'));
            Assert.AreEqual((15, false, true), Game.CalculateBallScoreAndStrikeOrSpare('/'));
            Assert.AreEqual((5, false, true), Game.CalculateBallScoreAndStrikeOrSpare('/', 10));
            Assert.AreEqual((5, false, false), Game.CalculateBallScoreAndStrikeOrSpare('H'));
            Assert.AreEqual((13, false, false), Game.CalculateBallScoreAndStrikeOrSpare('L'));
            Assert.AreEqual((13, false, false), Game.CalculateBallScoreAndStrikeOrSpare('R'));
            Assert.AreEqual((10, false, false), Game.CalculateBallScoreAndStrikeOrSpare('C'));
            Assert.AreEqual((8, false, false), Game.CalculateBallScoreAndStrikeOrSpare('S'));
            Assert.AreEqual((11, false, false), Game.CalculateBallScoreAndStrikeOrSpare('A'));
        }

        [Test]
        public void ScoreFirst3Frames()
        {
            FivePin game = new FivePin();
            game.ParseGameScore("X|X|X|X|X|---|---|---|---|X35");
            Assert.AreEqual(135, game.GetScoreForFirstNFrames(3));
        }
    }
}
