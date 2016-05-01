using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicPuzzleGame.Model;

namespace LogicPuzzleGameTestProject
{
    [TestClass]
    public class OveralTests
    {
        [TestMethod]
        public void NoMiddle()
        {
            Tank source = new SourceTank();
            Tank end = new Tank();
            end.ConnectTo(source);

            Assert.IsFalse(end.IsDirty);
        }
        [TestMethod]
        public void CleanMiddle()
        {
            Tank source = new SourceTank();
            Tank cleanTank = new Tank();
            cleanTank.ConnectTo(source);

            Tank endTank = new Tank();
            endTank.ConnectTo(cleanTank);

            Assert.IsFalse(endTank.IsDirty);

        }

        [TestMethod]
        public void DirtyMiddle()
        {
            Tank source = new SourceTank();
            Tank dirtyTank = new DirtyTank();
            dirtyTank.ConnectTo(source);

            Tank endTank = new Tank();
            endTank.ConnectTo(dirtyTank);

            Assert.IsTrue(endTank.IsDirty);
        }

        [TestMethod]
        public void CleanMultiMiddle()
        {
            Tank source = new SourceTank();
            Tank clean1 = new Tank();
            Tank clean2 = new Tank();
            clean1.ConnectTo(source);
            clean2.ConnectTo(source);

            Tank endTank = new Tank();
            endTank.ConnectTo(clean1);
            endTank.ConnectTo(clean2);

            Assert.IsFalse(endTank.IsDirty);
        }

        [TestMethod]
        public void DirtyMultiMiddle()
        {
            Tank source = new SourceTank();
            Tank clean1 = new Tank();
            Tank dirty = new DirtyTank();
            clean1.ConnectTo(source);
            dirty.ConnectTo(source);

            Tank endTank = new Tank();
            endTank.ConnectTo(clean1);
            endTank.ConnectTo(dirty);

            Assert.IsTrue(endTank.IsDirty);
        }
    }
}
