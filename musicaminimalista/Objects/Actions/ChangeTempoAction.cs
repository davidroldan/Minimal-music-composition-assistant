using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicaMinimalista.Objects.Actions
{
    class ChangeTempoAction : ProgramAction
    {
        private Controller controller;
        private int oldTempo;
        private int newTempo;

        public ChangeTempoAction(Controller controller, int oldTempo, int newTempo)
        {
            this.controller = controller;
            this.oldTempo = oldTempo;
            this.newTempo = newTempo;
        }

        public override void execute()
        {
            this.controller.setTempo(newTempo);
            this.controller.updateMainWindow();
        }

        public override void undo()
        {
            this.controller.setTempo(oldTempo);
            this.controller.updateMainWindow();
        }
    }
}
