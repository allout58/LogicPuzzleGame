using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicPuzzleGame.Model
{
    public class Tank
    {
        protected bool hasUpdated = false;
        protected bool isDirty = false;

        public bool IsDirty
        {
            get
            {
                if (!hasUpdated)
                    this.Update();
                return this.isDirty;
            }
        }

        private List<Pipe> inputs = new List<Pipe>();
//        private List<Pipe> outputs = new List<Pipe>();

        public void ConnectTo(Tank t)
        {
            Pipe p = new Pipe(t, this);
//            outputs.Add(p);
            inputs.Add(p);
        }

        public void Update()
        {
            foreach (Pipe p in inputs) {
                isDirty |= p.isDirty;
            }
        }


    }
}
