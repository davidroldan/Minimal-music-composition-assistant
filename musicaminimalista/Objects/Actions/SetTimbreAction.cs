using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicaMinimalista.Objects.Music;

namespace MusicaMinimalista.Objects.Actions
{
    class SetTimbreAction : ProgramAction
    {
        private Timbre oldTimbre;
        private Timbre newTimbre;
        private int track;
        private Controller controller;

        public SetTimbreAction(Controller controller, int track, Timbre oldTimbre, Timbre newTimbre)
        {
            this.controller = controller;
            this.oldTimbre = oldTimbre;
            this.newTimbre = newTimbre;
            this.track = track;
        }

        public override void execute()
        {
            this.controller.setTimbre(newTimbre, track);
            this.controller.updatePlaylist();
        }

        public override void undo()
        {
            this.controller.setTimbre(oldTimbre, track);
            this.controller.updatePlaylist();
        }
    }
}
