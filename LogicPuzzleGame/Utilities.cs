using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicPuzzleGame
{
    public class Utilities
    {
        public static int BoundedInt(int trial, int min, int max)
        {
            if (trial <= max && trial >= min) return trial;
            if (trial > max) return max;
            if (trial < min) return min;
            throw new Exception("Logic Error!");
        }
    }
}
