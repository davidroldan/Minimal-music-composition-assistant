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
    public class NullVariation : Variation
    {
        public NullVariation()
        {

        }

        public override Motif variate(Motif motif)
        {
            return motif;
        }
    }
}
