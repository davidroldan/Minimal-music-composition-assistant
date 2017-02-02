using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicaMinimalista.Objects.Music;

namespace MusicaMinimalista.Objects.Actions
{
    class DeleteTrackAction : ProgramAction
    {
        private Controller controller;
        private Track deletedTrack;
        private int trackIndex;

        public DeleteTrackAction(Controller controller, int trackIndex, Track track)
        {
            this.controller = controller;
            this.trackIndex = trackIndex;
            this.deletedTrack = track;
        }

        public override void execute()
        {
            this.controller.deleteTrack(trackIndex);
            this.controller.updatePlaylist();
        }

        public override void undo()
        {
            this.controller.addTrack(trackIndex, deletedTrack);
            this.controller.updatePlaylist();
        }
    }
}