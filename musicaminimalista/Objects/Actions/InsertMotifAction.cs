using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicaMinimalista.Objects.Music;

namespace MusicaMinimalista.Objects.Actions
{
    class InsertMotifAction : ProgramAction
    {
        private Controller controller;
        private int motifId;
        private int track;
        private Duration startTime;

        public InsertMotifAction(Controller controller, int motifId, int track, Duration startTime)
        {
            this.controller = controller;
            this.motifId = motifId;
            this.track = track;
            this.startTime = startTime;
        }

        public override void execute()
        {
            this.controller.insertMotif(motifId, track, startTime);
            this.controller.updatePlaylist();
        }

        public override void undo()
        {
            this.controller.removeMotif(track, startTime);
            this.controller.updatePlaylist();
        }
    }
}
