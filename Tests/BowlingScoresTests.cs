using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using BowlingScores;

namespace Tests
{
    class BowlingScoresTests
    {
        [Test]
        public void Score_Test_1()
        {
            var input = "X|X|X|X|X|X|X|X|X|X||XX";
            Assert.AreEqual(300, BowlingScore.ScoreGame(input));
        }
        
        [Test]
        public void Score_Test_2()
        {
            var input = "9-|9-|9-|9-|9-|9-|9-|9-|9-|9-||";
            Assert.AreEqual(90, BowlingScore.ScoreGame(input, BowlingScores.Games.GamesEnum.TenPin));
        }

        [Test]
        public void Score_Test_3()
        {
            var input = "5/|5/|5/|5/|5/|5/|5/|5/|5/|5/||5";
            Assert.AreEqual(150, BowlingScore.ScoreGame(input, BowlingScores.Games.GamesEnum.TenPin));
        }

        [Test]
        public void Score_Test_4()
        {
            var input = "X|7/|9-|X|-8|8/|-6|X|X|X||81";
            Assert.AreEqual(167, BowlingScore.ScoreGame(input, BowlingScores.Games.GamesEnum.TenPin));
        }

        [Test]
        public void Score_Test_5_Five_Pin()
        {
            var input = "---|X|X|X|X|X|X|X|X|XXX";
            Assert.AreEqual(405, BowlingScore.ScoreGame(input, BowlingScores.Games.GamesEnum.FivePin));
        }
    }
}
