using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Xml;
using MusicaMinimalista.Objects.Utils;

namespace MusicaMinimalista.Objects.Music
{
    [DataContract]
    public class Voice
    {
        [DataMember(Name = "MusicIList")]
        private List<MusicItem> musicItemList;

        public Voice()
        {
            this.musicItemList = new List<MusicItem>();
        }

        public void add(MusicItem musicItem)
        {
            this.musicItemList.Add(musicItem);
        }

        public MusicItem get(int n)
        {
            return this.musicItemList.ElementAt(n);
        }

        public int size()
        {
            return this.musicItemList.Count;
        }

        public Duration getDuration()
        {
            Duration d = 0;
            foreach (MusicItem mi in this.musicItemList)
            {
                d += mi.getDuration();
            }
            return d;
        }

        public int pitchMean()
        {
            int pitchSum = 0;
            int noteCount = 0;
            foreach (MusicItem mi in this.musicItemList)
            {
                if (!mi.isSilence())
                {
                    pitchSum += mi.pitchMean();
                    noteCount++;
                }
            }
            return pitchSum / noteCount;
        }

        public void invert(Tonality tonality)
        {
            int mean = this.pitchMean();

            int tonicPitch = tonality.getTonicPitch();
            
            if (tonality.getMode() == Tonality.MINOR)
            {
                tonicPitch += 3;
            }

            tonicPitch = Note.convertToClosestPitch(mean, tonicPitch);

            //Apply inversion
            foreach (MusicItem mi in this.musicItemList)
            {
                mi.invert(tonicPitch, tonality);
            }
        }

        internal void RachmaninoffInvert(Tonality tonality)
        {
            int mean = this.pitchMean();

            int thirdPitch = tonality.getScale()[2];

            thirdPitch = Note.convertToClosestPitch(mean, thirdPitch);

            //Aplicar inversion
            foreach (MusicItem mi in this.musicItemList)
            {
                mi.RachmaninoffInvert(thirdPitch, tonality);
            }
        }

        public void modulate(Tonality oldTonality, int[] targetScale)
        {
            foreach (MusicItem mi in this.musicItemList)
            {
                mi.modulate(oldTonality, targetScale);
            }
        }

        public void permutate(int[] triad)
        {
            foreach (MusicItem mi in this.musicItemList)
            {
                mi.permutate(triad);
            }
        }

        public List<Duration> getNotesStart(bool getSilences)
        {
            List<Duration> notes = new List<Duration>();
            Duration noteStart = 0;
            for (int i = 0; i < musicItemList.Count; i++)
            {
                MusicItem mi = musicItemList[i];
                if (getSilences || !mi.isSilence())
                    notes.Add(noteStart);
                noteStart += mi.getDuration();
            }
            return notes;
        }

        public Duration getMaxNoteLength()
        {
            Duration duration = 0;
            foreach (MusicItem mi in musicItemList)
            {
                if (mi.getDuration() > duration)
                {
                    duration = mi.getDuration();
                }
            }
            return duration;
        }

        public List<int> getNonSilentNotes(){
            List<int> nonSilentNotes = new List<int>();
            for (int i = 0; i < musicItemList.Count; i++)
            {
                MusicItem mi = musicItemList[i];
                if (!mi.isSilence()) nonSilentNotes.Add(i);
            }
            return nonSilentNotes;
        }

        #region Variations

        internal void delay(Duration duration)
        {
            this.musicItemList.Insert(0, new Note(Note.REST, duration));
        }

        internal void transport(int pitch)
        {
            foreach (MusicItem mi in this.musicItemList)
            {
                mi.transport(pitch);
            }
        }

        internal void tonaltransport(int gradeDistance, int[] scale)
        {
            foreach (MusicItem mi in this.musicItemList)
            {
                mi.tonaltransport(gradeDistance, scale);
            }
        }

        internal void changeDuration(Duration multiplier)
        {
            foreach (MusicItem mi in this.musicItemList)
            {
                mi.changeDuration(multiplier);
            }
        }

        internal void retrogradation(Duration motifDuration)
        {
            this.musicItemList.Reverse();
            Duration voiceDuration = this.getDuration();
            if (voiceDuration < motifDuration)
            {
                this.musicItemList.Insert(0, new Note(Note.REST, motifDuration - voiceDuration));
            }
        }

        internal void harmonizate(Tonality tonality)
        {
            int[] scale = tonality.getScale();
            for (int i = 0; i < this.musicItemList.Count; i++)
            {
                MusicItem mi = musicItemList[i];
                if (mi.isChord()) continue;
                Note high = (Note)mi;
                Chord chord = new Chord();
                chord.add(high);
                int found = 0;
                int pitch = high.getPitch() - 2; // Substracting 2 to avoid 2-intervals
                while (found < 2 && pitch > 0)
                {
                    pitch--;
                    if (tonality.isinTriad(pitch))
                    {
                        chord.add(new Note(pitch, high.getDuration()));
                        found++;
                    }
                }
                musicItemList[i] = chord;
            }
        }

        internal void elision()
        {
            int percentage = RNG.generate(10, 30);

            List<int> nonSilentNotes = getNonSilentNotes();

            if (nonSilentNotes.Count == 0) return;

            int number = Math.Max(percentage * nonSilentNotes.Count / 100, 1);
            for (int i = 0; i < number; i++)
            {
                int element = RNG.generate(0, nonSilentNotes.Count - 1);
                Note note = new Note(Note.REST, musicItemList[nonSilentNotes[element]].getDuration());
                musicItemList[nonSilentNotes[element]] = note;
                nonSilentNotes = getNonSilentNotes(); //update
            }
        }

        internal void interpolate(Tonality tonality)
        {
            int percentage = RNG.generate(10, 50);

            List<int> nonSilentNotes = getNonSilentNotes();

            int number = Math.Max(percentage * nonSilentNotes.Count / 100, 1);
            for (int i = 0; i < number; i++)
            {
                int element = RNG.generate(0, nonSilentNotes.Count - 1);
                MusicItem mi = musicItemList[nonSilentNotes[element]];

                int newNotePitch = Note.convertToClosestPitch(mi.pitchMean(), tonality.getRandomPitchFromScale(70));
                
                /*
                 * SplitNote = true: Divide note length by half and add a random note after it
                 * SplitNote = false: Substitute a silence with a random note.
                */
                bool splitNote = true;
                if (element != musicItemList.Count - 1 && musicItemList[nonSilentNotes[element] + 1].isSilence() && RNG.generateBool())
                    splitNote = false;

                if (splitNote)
                {
                    mi.changeDuration(new Duration(1, 2));
                    musicItemList.Insert(nonSilentNotes[element] + 1, new Note(newNotePitch, mi.getDuration()));
                }
                else
                {
                    musicItemList[nonSilentNotes[element] + 1] = new Note(newNotePitch, musicItemList[nonSilentNotes[element] + 1].getDuration());
                }

                nonSilentNotes = getNonSilentNotes();
            }
        }

        internal void removeFromBeginning(Duration duration)
        {
            while (duration > 0)
            {
                MusicItem mi = musicItemList[0];
                if (mi.getDuration() <= duration)
                {
                    musicItemList.RemoveAt(0);
                    duration -= mi.getDuration();
                }
                else
                {
                    mi.shortenDurationFromBeginning(duration);
                    duration = 0;
                }
            }
        }

        internal void removeFromEnd(Duration duration)
        {
            while (duration > 0)
            {
                MusicItem mi = musicItemList[musicItemList.Count - 1];
                if (mi.getDuration() <= duration)
                {
                    musicItemList.RemoveAt(musicItemList.Count - 1);
                    duration -= mi.getDuration();
                }
                else
                {
                    mi.shortenDurationFromEnd(duration);
                    duration = 0;
                }
            }
        }

        internal void ornamentation(int pitch)
        {
            List<MusicItem> oldList = this.musicItemList;
            this.musicItemList = new List<MusicItem>();
            foreach (MusicItem mi in oldList)
            {
                if (mi.isSilence())
                {
                    musicItemList.Add(mi);
                    continue;
                }

                mi.changeDuration(new Duration(1, 3));
                musicItemList.Add(mi.Clone());
                MusicItem aux = mi.Clone();
                aux.transport(pitch);
                musicItemList.Add(aux);
                musicItemList.Add(mi.Clone());
            }
        }

        internal double canonizationValue(Duration delay)
        {
            Voice nondelayVoice = this.Clone();
            Voice delayVoice = this.Clone();

            nondelayVoice.removeFromBeginning(delay);
            delayVoice.removeFromEnd(delay);

            Duration totalDuration = delayVoice.getDuration(); //both delayVoice and nondelayVoice should have same duration

            List<Duration> nondelayList = nondelayVoice.getNotesStart(true);
            List<Duration> delayList = delayVoice.getNotesStart(true);
            List<Duration> unionList = nondelayList.Union(delayList).ToList();
            unionList.Sort();

            Duration totalDisonance = 0;
            for (int i = 0; i < unionList.Count; i++)
            {
                Duration position = unionList[i];
                int a = nondelayList.BinarySearch(position);
                int b = delayList.BinarySearch(position);

                // Binary Search returns a negative int if it doesn't find the item.
                // The negative int is the position where the item would be inserted + 1,
                // so we just multiply by -1 and substract 2 to get what we want.
                if (a < 0) a = -a - 2;
                if (b < 0) b = -b - 2;

                int disonance = getDisonance(nondelayVoice.get(a), delayVoice.get(b));

                Duration disonanceDuration = 0;
                if (i < unionList.Count - 1)
                {
                    disonanceDuration = unionList[i + 1] - unionList[i];
                }
                else
                {
                    disonanceDuration = totalDuration - unionList[i];
                }
                totalDisonance += (disonance * disonanceDuration);
            }

            totalDisonance /= totalDuration;

            return totalDisonance.ToDouble();
        }

        private int getDisonance(MusicItem mi1, MusicItem mi2)
        {
            if (mi1.isSilence() || mi2.isSilence()) return 0;

            Chord a, b;

            if (mi1.isChord())
            {
                a = (Chord)mi1;
            }
            else
            {
                a = new Chord();
                a.add((Note)mi1);
            }

            if (mi2.isChord())
            {
                b = (Chord)mi2;
            }
            else
            {
                b = new Chord();
                b.add((Note)mi2);
            }

            int totalDisonance = 0;
            for (int i = 0; i < a.size(); i++)
            {
                for (int j = 0; j < b.size(); j++)
                {
                    totalDisonance += getDisonance(a.get(i), b.get(j));
                }
            }

            return totalDisonance / (a.size() * b.size());
        }

        private int getDisonance(Note n1, Note n2)
        {
            //http://quod.lib.umich.edu/cgi/p/pod/dod-idx/consonance-dissonance-algorithm-for-intervals.pdf?c=icmc;idno=bbp2372.1995.169
            int pitchDist = Note.getNoteDistance(n1.getPitch(), n2.getPitch());
            switch (pitchDist)
            {
                case 0: return 25;
                case 1: return 240;
                case 2: return 72;
                case 3: return 30;
                case 4: return 20;
                case 5: return 12;
                default: return 1440; //case 6
            }
        }



        public Voice Clone()
        {
            Voice v = new Voice();
            for (int i = 0; i < this.size(); i++)
            {
                v.add(this.get(i).Clone());
            }
            return v;
        }
        #endregion

    }
}
