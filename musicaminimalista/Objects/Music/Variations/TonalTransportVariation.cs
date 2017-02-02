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
    public class TonalTransportVariation : Variation
    {
        public TonalTransportVariation(int gradeOrigin, int gradeDest)
        {
            this.gradeOrigin = gradeOrigin;
            this.gradeDest = gradeDest;
        }

        public override Motif variate(Motif motif)
        {
            Motif m = motif.Clone();
            m.tonaltransport(this.gradeOrigin, this.gradeDest);
            return m;
        }

        [DataMember(Name = "GradeOrigin")]
        private int gradeOrigin;
        [DataMember(Name = "GradeDest")]
        private int gradeDest;
    }
}
