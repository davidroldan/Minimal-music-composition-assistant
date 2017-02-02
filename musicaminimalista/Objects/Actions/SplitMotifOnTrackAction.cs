using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicaMinimalista.Objects.Music;

namespace MusicaMinimalista.Objects.Actions
{
    class SplitMotifOnTrackAction : ProgramAction
    {
        private Controller controller;

        private List<Motif> splittedMotifs;

        private int track;
        private int motifId;
        private Duration startTime;

        public SplitMotifOnTrackAction(Controller controller, int motifId, int track, Duration startTime)
        {
            this.controller = controller;
            this.motifId = motifId;
            this.track = track;
            this.startTime = startTime;

            Motif motif = controller.getMotif(motifId);

            this.splittedMotifs = this.controller.splitMotif(motif);
        }

        public override void execute()
        {
            //Replace motif with first Voice
            Motif firstVoice = splittedMotifs.ElementAt(0);
            this.controller.removeMotif(track, startTime);
            this.controller.addMotif(firstVoice);
            this.controller.insertMotif(firstVoice.getId(), track, startTime);

            //Add other voices in new tracks
            for (int i = 1; i < splittedMotifs.Count; i++)
            {
                Motif m = splittedMotifs.ElementAt(i);
                Track t = new Track();
                t.setSettingsFrom(controller.getTrack(track));
                this.controller.addMotif(m);
                this.controller.addTrack(track + i, t);
                this.controller.insertMotif(m.getId(), track + i, startTime);
            }

            this.controller.updatePlaylist();
        }

        public override void undo()
        {
            //Eliminate tracks with other voices
            for (int i = 1; i < splittedMotifs.Count; i++)
            {
                Motif m = splittedMotifs.ElementAt(i);
                this.controller.deleteTrack(track + 1);
                this.controller.deleteMotif(m.getId());
            }

            //Replace first voice with original motif
            this.controller.removeMotif(track, startTime);
            this.controller.insertMotif(motifId, track, startTime);
            this.controller.deleteMotif(splittedMotifs[0].getId());

            this.controller.updatePlaylist();
        }
    }
}
