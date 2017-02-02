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
    public class OrnamentationVariation : Variation
    {
        public OrnamentationVariation(int pitch)
        {
            this.pitch = pitch;
        }

        public override Motif variate(Motif motif)
        {
            Motif m = motif.Clone();
            m.ornamentation(pitch);
            return m;
        }

        [DataMember(Name = "Pitch")]
        private int pitch;
    }
}
