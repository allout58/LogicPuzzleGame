using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicPuzzleGame.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicPuzzleGame.Controller.Tests
{
    [TestClass()]
    public class GameBoardTests
    {
        [TestMethod()]
        public void GenerateBoardTest()
        {
            GameBoard gb = new GameBoard(5,5);
            gb.GenerateBoard();

            Assert.Inconclusive();
        }
    }
}