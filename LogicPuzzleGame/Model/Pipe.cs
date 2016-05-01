using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicPuzzleGame.Model
{
    public class Pipe
    {
        public Tank entranceTank
        {
            get;
            protected set;
        }

        public Tank exitTank
        {
            get;
            protected set;
        }

        public bool isDirty
        {
            get
            {
                return (entranceTank != null) ? entranceTank.IsDirty : false;
            }
        }

        public Pipe(Tank input, Tank output)
        {
            this.entranceTank = input;
            this.exitTank = output;
        }
    }
}
