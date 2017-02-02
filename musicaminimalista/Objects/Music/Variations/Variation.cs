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
    [KnownType(typeof(CanonizationVariation))]
    [KnownType(typeof(CompositeVariation))]
    [KnownType(typeof(DelayVariation))]
    [KnownType(typeof(ElisionVariation))]
    [KnownType(typeof(HarmonizationVariation))]
    [KnownType(typeof(InterpolationVariation))]
    [KnownType(typeof(InversionVariation))]
    [KnownType(typeof(ModulationVariation))]
    [KnownType(typeof(NoteValueVariation))]
    [KnownType(typeof(NullVariation))]
    [KnownType(typeof(PermutationVariation))]
    [KnownType(typeof(RachmaninoffInversionVariation))]
    [KnownType(typeof(RetrogradeVariation))]
    [KnownType(typeof(OrnamentationVariation))]
    [KnownType(typeof(TonalTransportVariation))]
    [KnownType(typeof(TransportVariation))]
    [KnownType(typeof(NullVariation))]

    public abstract class Variation
    {
        public abstract Motif variate(Motif motif);
    }
}