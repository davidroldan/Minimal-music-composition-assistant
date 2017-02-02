using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Xml;

namespace MusicaMinimalista.Objects.Music.Variations
{
    [DataContract]
    public class NoteValueVariation : Variation
    {
        public NoteValueVariation(Duration multiplier)
        {
            this.multiplier = multiplier;
        }

        public override Motif variate(Motif motif)
        {
            Motif m = motif.Clone();
            m.changeDuration(this.multiplier);
            return m;
        }

        [DataMember(Name = "Multiplier")]
        private Duration multiplier;
    }
}
