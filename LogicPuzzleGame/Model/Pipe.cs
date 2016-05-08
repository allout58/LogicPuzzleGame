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

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) {
                return false;
            }
            
            return entranceTank.Equals(((Pipe)obj).entranceTank) && exitTank.Equals(((Pipe)obj).exitTank);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
