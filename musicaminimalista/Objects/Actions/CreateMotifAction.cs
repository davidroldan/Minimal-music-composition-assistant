using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicaMinimalista.Objects.Music;

namespace MusicaMinimalista.Objects.Actions
{
    class CreateMotifAction : ProgramAction
    {
        private Motif motif;
        private Controller controller;

        public CreateMotifAction(Controller controller, Motif motif)
        {
            this.controller = controller;
            this.motif = motif;
        }

        public override void execute()
        {
            this.controller.addMotif(motif);
        }

        public override void undo()
        {
            this.controller.deleteMotif(motif.getId());
        }
    }
}
