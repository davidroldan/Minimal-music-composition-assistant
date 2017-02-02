using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicaMinimalista.Objects.Music;

namespace MusicaMinimalista.Objects.Actions
{
    class SetVolumeAction : ProgramAction
    {
        private int oldVolume;
        private int newVolume;
        private int track;
        private Controller controller;

        public SetVolumeAction(Controller controller, int track, int oldVolume, int newVolume)
        {
            this.controller = controller;
            this.oldVolume = oldVolume;
            this.newVolume = newVolume;
            this.track = track;
        }

        public override void execute()
        {
            this.controller.setVolume(newVolume, track);
            this.controller.updatePlaylist();
        }

        public override void undo()
        {
            this.controller.setVolume(oldVolume, track);
            this.controller.updatePlaylist();
        }
    }
}
