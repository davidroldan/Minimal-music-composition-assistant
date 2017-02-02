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
    public class ModulationVariation : Variation
    {
        public ModulationVariation(Tonality tonality)
        {
            this.tonality = tonality;
        }

        public override Motif variate(Motif motif)
        {
            Motif m = motif.Clone();
            m.modulate(this.tonality);
            return m;
        }

        [DataMember(Name = "Transport")]
        private Tonality tonality;
    }
}
