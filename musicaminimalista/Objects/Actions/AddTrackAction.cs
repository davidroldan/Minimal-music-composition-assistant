using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicaMinimalista.Objects.Actions
{
    class AddTrackAction : ProgramAction
    {
        private Controller controller;
        private int newTrackIndex;

        public AddTrackAction(Controller controller, int newTrackIndex)
        {
            this.controller = controller;
            this.newTrackIndex = newTrackIndex;
        }

        public override void execute()
        {
            this.controller.addTrack();
            this.controller.updatePlaylist();
        }

        public override void undo()
        {
            this.controller.deleteTrack(newTrackIndex);
            this.controller.updatePlaylist();
        }
    }
}
