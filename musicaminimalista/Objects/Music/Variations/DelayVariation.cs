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
    public class DelayVariation : Variation
    {
        public DelayVariation(Duration delay)
        {
            this.delay = delay;
        }

        public override Motif variate(Motif motif)
        {
            Motif m = motif.Clone();
            m.delay(this.delay);
            return m;
        }

        [DataMember(Name = "Delay")]
        private Duration delay;
    }
}
