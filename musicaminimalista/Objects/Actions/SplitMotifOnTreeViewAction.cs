using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicaMinimalista.Objects.Music;

namespace MusicaMinimalista.Objects.Actions
{
    class SplitMotifOnTreeViewAction : ProgramAction
    {
        private Motif originalMotif;
        private List<Motif> splittedMotifs;
        private Controller controller;

        public SplitMotifOnTreeViewAction(Controller controller, Motif motif)
        {
            this.controller = controller;
            this.originalMotif = motif;
            this.splittedMotifs = this.controller.splitMotif(originalMotif);
        }

        public override void execute()
        {
            foreach (Motif m in splittedMotifs)
            {
                this.controller.addMotif(m);
            }
        }

        public override void undo()
        {
            foreach (Motif m in splittedMotifs)
            {
                this.controller.deleteMotif(m.getId());
            }
        }
    }
}
