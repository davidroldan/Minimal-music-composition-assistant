using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicaMinimalista.Objects.Music;

namespace MusicaMinimalista.Objects.Actions
{
    class RenameMotifAction : ProgramAction
    {
        private Controller controller;
        private int motifId;
        private string oldName;
        private string newName;

        public RenameMotifAction(Controller controller, Motif motif, string newName)
        {
            this.controller = controller;
            this.motifId = motif.getId();
            this.oldName = motif.getName();
            this.newName = newName;
        }

        public override void execute()
        {
            this.controller.renameMotif(motifId, newName);
            this.controller.updatePlaylist();
        }

        public override void undo()
        {
            this.controller.renameMotif(motifId, oldName);
            this.controller.updatePlaylist();
        }
    }
}
