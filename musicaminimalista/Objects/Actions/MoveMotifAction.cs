using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicaMinimalista.Objects.Music;

namespace MusicaMinimalista.Objects.Actions
{
    class MoveMotifAction : ProgramAction
    {
        private Controller controller;
        private KeyValuePair<int, Duration> initialPosition;
        private KeyValuePair<int, Duration> finalPosition;
        private int motifId;

        public MoveMotifAction(Controller controller, int motifId, KeyValuePair<int, Duration> initialPosition, KeyValuePair<int, Duration> finalPosition)
        {
            this.controller = controller;
            this.motifId = motifId;
            this.initialPosition = initialPosition;
            this.finalPosition = finalPosition;
        }

        public override void execute()
        {
            this.controller.moveMotif(motifId, initialPosition, finalPosition);
            this.controller.updatePlaylist();
        }

        public override void undo()
        {
            this.controller.moveMotif(motifId, finalPosition, initialPosition);
            this.controller.updatePlaylist();
        }
    }
}
