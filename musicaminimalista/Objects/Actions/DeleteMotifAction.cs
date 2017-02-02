using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicaMinimalista.Objects.Music;
using MusicaMinimalista.Objects.Music.Variations;

namespace MusicaMinimalista.Objects.Actions
{
    class DeleteMotifAction : ProgramAction
    {
        private Motif motif;
        private Controller controller;
        private Dictionary<int, Variation> childVariationList;
        private List<Duration>[] motifInstances;


        public DeleteMotifAction(Controller controller, Motif motif){
            this.controller = controller;
            this.motif = motif;
            childVariationList = this.controller.getChildVariationList(motif.getId());
            int trackCount = controller.trackCount();
            this.motifInstances = new List<Duration>[trackCount];
            for (int i = 0; i < trackCount; i++)
            {
                this.motifInstances[i] = controller.getMotifInstances(motif.getId(), i);
            }
        }

        public override void execute()
        {
            this.controller.deleteMotif(motif.getId());
            this.controller.updatePlaylist();
        }

        public override void undo()
        {
            this.controller.restoreMotif(motif, childVariationList, motifInstances);
            this.controller.updatePlaylist();
        }
    }
}
