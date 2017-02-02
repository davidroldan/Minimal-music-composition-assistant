using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Xml;

namespace MusicaMinimalista.Objects.Music
{
    [DataContract]
    [KnownType(typeof(Chord))]
    [KnownType(typeof(Note))]
    public abstract class MusicItem
    {
        public abstract void tonaltransport(int gradeDistance, int[] scale);
        public abstract void transport(int pitch);
        public abstract void changeDuration(Duration multiplier);
        public abstract void modulate(Tonality oldTonality, int[] targetScale);
        public abstract void permutate(int[] triad);
        public abstract void shortenDurationFromBeginning(Duration duration);
        public abstract void shortenDurationFromEnd(Duration duration);
        public abstract Duration getDuration();
        public abstract MusicItem Clone();
        public abstract int pitchMean();
        public abstract void invert(int pitch, Tonality tonality);
        public abstract void RachmaninoffInvert(int pitch, Tonality tonality);
        public abstract bool isSilence();
        public abstract bool isChord();
    }
}
