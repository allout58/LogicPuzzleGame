using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicPuzzleGame.Model
{
    public class SourceTank: Tank
    {
        public SourceTank()
        {
            isDirty = false;
            hasUpdated = true;
        }


        public new void Update()
        {

        }
    }
}
