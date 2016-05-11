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

        public int X {
            get; set;
        }

        public int Y {
            get; set;
        }

        public bool IsDirty {
            get {
                if (!hasUpdated)
                    this.Update();
                return this.isDirty;
            }
        }

        public Pipe[] Inputs {
            get {
                return inputs.ToArray();
            }
        }

        public Pipe[] Outputs {
            get { return outputs.ToArray(); }
        }

        private List<Pipe> inputs = new List<Pipe>();
        private List<Pipe> outputs = new List<Pipe>();

        public void ConnectTo(Tank t) {
            Pipe p = new Pipe(t, this);
            t.outputs.Add(p);
            inputs.Add(p);
        }

        public void Update() {
            foreach (Pipe p in inputs) {
                isDirty |= p.isDirty;
            }
        }

        public override string ToString() {
            return "Tank(" + X + "," + Y + ")";
        }
    }
}
