using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicPuzzleGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicPuzzleGame.Model.Tests
{
    [TestClass()]
    public class PipeTests
    {
        [TestMethod()]
        public void DirtyTest()
        {
            Tank source = new SourceTank();
            Tank dirty = new DirtyTank();

            Pipe cleanToDirty = new Pipe(source, dirty);
            Pipe dirtyToClean = new Pipe(dirty, source);

            Assert.IsFalse(cleanToDirty.isDirty);
            Assert.IsTrue(dirtyToClean.isDirty);
        }
    }
}