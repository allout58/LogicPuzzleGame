using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicPuzzleGame.Model
{
    public class DirtyTank : Tank
    {
        public DirtyTank() {
            isDirty = true;
            hasUpdated = true;
        }

        public new void Update() {

        }

        public override string ToString() {
            return "Dirty" + base.ToString();
        }
    }
}
