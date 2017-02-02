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
    public class Chord : MusicItem
    {
        [DataMember(Name = "NoteList")]
        private List<Note> noteList;

        public Chord()
        {
            this.noteList = new List<Note>();
        }

        public void add(Note n)
        {
            this.noteList.Add(n);
        }

        public Note get(int n)
        {
            return this.noteList.ElementAt(n);
        }

        public override void shortenDurationFromBeginning(Duration duration)
        {
            foreach (Note n in this.noteList)
            {
                Duration d = n.getDuration() - duration;
                if (d < 0)
                    d = 0;
                n.shortenDurationFromBeginning(d);
            }
        }

        public override void shortenDurationFromEnd(Duration duration)
        {
            foreach (Note n in this.noteList)
            {
                if (n.getDuration() > duration)
                    n.shortenDurationFromBeginning(duration);
            }
        }

        public override Duration getDuration()
        {
            Duration d = 0;
            foreach (Note n in this.noteList)
            {
                Duration d1 = n.getDuration();
                if (d1 > d) d = d1;
            }
            return d;
        }

        public override int pitchMean()
        {
            int pitchSum = 0;
            foreach (Note n in this.noteList)
            {
                pitchSum += n.pitchMean();
            }
            return pitchSum / noteList.Count;
        }

        public override void invert(int pitch, Tonality tonality)
        {
            foreach (Note n in this.noteList)
            {
                n.invert(pitch, tonality);
            }
        }

        public override void RachmaninoffInvert(int pitch, Tonality tonality)
        {
            foreach (Note n in this.noteList)
            {
                n.RachmaninoffInvert(pitch, tonality);
            }
        }

        public override bool isSilence()
        {
            return false;
        }

        public override bool isChord()
        {
            return true;
        }

        public int size()
        {
            return this.noteList.Count;
        }

        public override void tonaltransport(int gradeDistance, int[] scale)
        {
            foreach (Note n in this.noteList)
            {
                n.tonaltransport(gradeDistance, scale);
            }
        }

        public override void transport(int pitch)
        {
            foreach (Note n in this.noteList)
            {
                n.transport(pitch);
            }
        }

        public override void modulate(Tonality oldTonality, int[] targetScale)
        {
            foreach (Note n in this.noteList)
            {
                n.modulate(oldTonality, targetScale);
            }
        }

        public override void permutate(int[] triad)
        {
            foreach (Note n in this.noteList)
            {
                n.permutate(triad);
            }
        }

        public override void changeDuration(Duration multiplier)
        {
            foreach (Note n in this.noteList)
            {
                n.changeDuration(multiplier);
            }
        }

        public override MusicItem Clone()
        {
            Chord c = new Chord();
            for (int i = 0; i < this.size(); i++)
            {
                c.add((Note)this.get(i).Clone());
            }
            return c;
        }
    }
}
