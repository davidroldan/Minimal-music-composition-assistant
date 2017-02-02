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
    public class TransportVariation : Variation
    {
        public TransportVariation(int transport)
        {
            this.transport = transport;
        }

        public override Motif variate(Motif motif)
        {
            Motif m = motif.Clone();
            m.transport(this.transport);
            return m;
        }

        [DataMember(Name = "Transport")]
        private int transport;
    }
}
