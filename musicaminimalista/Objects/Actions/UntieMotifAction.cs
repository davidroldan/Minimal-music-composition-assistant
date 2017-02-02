using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicaMinimalista.Objects.Music;

namespace MusicaMinimalista.Objects.Actions
{
    class UntieMotifAction : ProgramAction
    {
        private Controller controller;
        private int oldMotifId;
        private Motif newMotif;
        private int track;
        private Duration startTime;

        public UntieMotifAction(Controller controller, int oldMotifId, Motif newMotif, int track, Duration startTime)
        {
            this.controller = controller;
            this.oldMotifId = oldMotifId;
            this.newMotif = newMotif;
            this.track = track;
            this.startTime = startTime;
        }

        public override void execute()
        {
            controller.addMotif(newMotif);
            controller.removeMotif(track, startTime);
            controller.insertMotif(newMotif.getId(), track, startTime);
            controller.updatePlaylist();
        }

        public override void undo()
        {
            controller.removeMotif(track, startTime);
            controller.insertMotif(oldMotifId, track, startTime);
            controller.deleteMotif(newMotif.getId());
            controller.updatePlaylist();
        }
    }
}
