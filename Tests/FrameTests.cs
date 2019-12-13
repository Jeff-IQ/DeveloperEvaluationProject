using NUnit.Framework;
using BowlingScores;
using System;
using BowlingScores.Frames;

namespace Tests
{
    public class FrameTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Successful_Empty_First_Frame()
        {
            RegularFrame myFrame = new RegularFrame(1);

            Assert.IsNotNull(myFrame);
            Assert.IsTrue(myFrame.FrameNumber == 1);
        }

        [Test]
        public void Successful_Empty_Tenth_Frame()
        {
            RegularFrame myFrame = new RegularFrame(10);

            Assert.IsNotNull(myFrame);
            Assert.IsTrue(myFrame.FrameNumber == 10);
        }

        [Test]
        public void Frame_Zero_Invalid()
        {
            try
            {
                RegularFrame myFrame = new RegularFrame(0);
            }

            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("Invalid frame number 0, must be between 1 and 10"));
            }
            
        }

        [Test]
        public void Frame_Eleven_Invalid()
        {
            try
            {
                RegularFrame myFrame = new RegularFrame(11);
            }

            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("Invalid frame number 11, must be between 1 and 10"));
            }

        }

       
        [Test]
        public void Add_Rolls_To_Existing_Frame()
        {
            RegularFrame myFrame = new RegularFrame(1);
            myFrame.AddBallScore(5);
            myFrame.AddBallScore(5);
            Assert.AreEqual(10, myFrame.GetScore());
        }


        [Test]
        public void Strike()
        {
            RegularFrame myFrame = new RegularFrame(10);
            myFrame.AddBallScore(10, strike: true);
            Assert.IsTrue(myFrame.IsStrike);
        }

        [Test]
        public void Spare()
        {
            RegularFrame myFrame = new RegularFrame(10);
            myFrame.AddBallScore(4);
            myFrame.AddBallScore(6,spare: true);
            Assert.IsTrue(myFrame.IsSpare);
        }

        [Test]
        public void Add_Bonus_Points_To_Strike()
        {
            RegularFrame myFrame = new RegularFrame(10);
            myFrame.AddBallScore(10, strike: true);
            myFrame.AddBonusPoints(10);
            Assert.AreEqual(20, myFrame.GetScore());
        }

        [Test]
        public void Add_Bonus_Points_To_Spare()
        {
            RegularFrame myFrame = new RegularFrame(10);
            myFrame.AddBallScore(5);
            myFrame.AddBallScore(5, spare: true);
            myFrame.AddBonusPoints(6);
            Assert.AreEqual(16, myFrame.GetScore());
        }

        [Test]
        public void Adding_Points_to_Bonus_Frame_Adds_Zero()
        {
            BonusFrame myFrame = new BonusFrame(11);
            myFrame.AddBallScore(10);
            myFrame.AddBonusPoints(5);
            Assert.AreEqual(0, myFrame.GetScore());
            
        }
        

    }
}