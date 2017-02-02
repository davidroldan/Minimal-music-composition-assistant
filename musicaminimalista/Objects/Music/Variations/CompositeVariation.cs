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
    public class CompositeVariation : Variation
    {
        public CompositeVariation(Variation v1, Variation v2)
        {
            this.variation1 = v1;
            this.variation2 = v2;
        }

        public override Motif variate(Motif motif)
        {
            Motif motif2 = this.variation1.variate(motif);
            return this.variation2.variate(motif2);
        }

        [DataMember(Name = "Variation1")]
        private Variation variation1;
        [DataMember(Name = "Variation2")]
        private Variation variation2;
    }
}
