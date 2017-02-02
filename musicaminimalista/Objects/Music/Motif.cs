using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Xml;

using MusicaMinimalista.Objects.Music.Variations;

namespace MusicaMinimalista.Objects.Music
{
    [DataContract]
    public class Motif
    {
        [DataMember(Name = "Id")]
        private int id;
        [DataMember(Name = "VoiceList")]
        private List<Voice> voiceList;
        [DataMember(Name = "ParentId")]
        private int parentId;
        [DataMember(Name = "Name")]
        private string name;
        [DataMember(Name = "Variation")]
        private Variation variation;
        [DataMember(Name = "Tonality")]
        private Tonality tonality;

        [DataMember(Name = "Harmony")]
        private SortedList<Harmony, int> harmonyList;

        /**
         * New motif
        **/
        public Motif()
        {
            this.voiceList = new List<Voice>();
            this.id = Tune.generateMotifID();
            this.parentId = -1;
            this.variation = new NullVariation();
            this.tonality = new Tonality();
            this.name = "";
            this.harmonyList = new SortedList<Harmony, int>();
        }

        private Motif(List<Voice> voiceList, int parentId, Variation variation, Tonality tonality, SortedList<Harmony, int> harmonyList)
        {
            this.voiceList = voiceList;
            this.id = Tune.generateMotifID();
            this.parentId = parentId;
            this.variation = variation;
            this.tonality = tonality;
            this.name = "";
            this.harmonyList = harmonyList;
        }

        public string getName()
        {
            return this.name;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public int getId()
        {
            return this.id;
        }

        public int getParentId()
        {
            return this.parentId;
        }

        public Variation getVariation()
        {
            return this.variation;
        }

        public void setTonality(Tonality t)
        {
            this.tonality = t;
        }

        public Tonality getTonality()
        {
            return this.tonality;
        }

        public Voice getVoice(int n)
        {
            return this.voiceList.ElementAt(n);
        }

        public void addVoice(Voice voice)
        {
            this.voiceList.Add(voice);
        }

        public int voiceCount()
        {
            return this.voiceList.Count;
        }

        public Duration getDuration()
        {
            Duration d = 0;
            foreach (Voice v in this.voiceList)
            {
                Duration d1 = v.getDuration();
                if (d1 > d) d = d1;
            }
            return d;
        }

        private int pitchMean()
        {
            int pitchSum = 0;
            foreach (Voice v in this.voiceList)
            {
                pitchSum += v.pitchMean();
            }
            return pitchSum / this.voiceList.Count;
        }

        private int getMaxNotes()
        {
            int notes = 0;
            foreach (Voice v in this.voiceList)
            {
                int aux = v.size();
                if (aux > notes)
                {
                    notes = aux;
                }
            }
            return notes;
        }

        private Duration getMaxNoteLength()
        {
            Duration length = 0;
            foreach (Voice v in this.voiceList)
            {
                Duration length_aux = v.getMaxNoteLength();
                if (v.getMaxNoteLength() > length)
                    length = v.getMaxNoteLength();
            }
            return length;
        }

        private List<Duration> getNotesStart(bool getSilences)
        {
            List<Duration> notes = new List<Duration>();
            foreach (Voice v in this.voiceList)
            {
                List<Duration> voicenotes = v.getNotesStart(getSilences);
                notes = notes.Union(voicenotes).ToList();
            }
            return notes;
        }

        public List<Motif> split()
        {
            List<Motif> motifList = new List<Motif>();

            foreach (Voice v in voiceList)
            {
                SortedList<Harmony, int> newHarmonyList = new SortedList<Harmony, int>();
                for (int i = 0; i < this.harmonyList.Count; i++)
                {
                    newHarmonyList.Add(this.harmonyList.Keys[i].Clone(), this.harmonyList.Values[i]);
                }

                Motif m = new Motif(new List<Voice>(), this.id, new NullVariation(), this.tonality.Clone(), newHarmonyList);
                m.addVoice(v.Clone());
                motifList.Add(m);
            }
            return motifList;
        }

        #region Variaciones

        internal void setVariation(Variation variation, int parentId)
        {
            this.variation = variation;
            this.parentId = parentId;
        }

        internal void transport(int semitones)
        {
            tonality.transport(semitones);
            foreach (Voice v in this.voiceList)
            {
                v.transport(semitones);
            }
        }

        internal void tonaltransport(int gradeOrigin, int gradeDest)
        {
            int gradeDistance = gradeDest - gradeOrigin;

            if (gradeDistance == 0) return;
            if (gradeDistance < 0) gradeDistance += Note.NOTES;
            
            int[] scale = this.tonality.getScale();
            foreach (Voice v in this.voiceList)
            {
                v.tonaltransport(gradeDistance, scale);
            }
        }

        internal void delay(Duration duration)
        {
            foreach (Voice v in this.voiceList)
            {
                v.delay(duration);
            }
        }

        internal void changeDuration(Duration multiplier)
        {
            foreach (Voice v in this.voiceList)
            {
                v.changeDuration(multiplier);
            }
        }

        internal void retrogradation()
        {
            Duration motifDuration = this.getDuration();
            foreach (Voice v in this.voiceList)
            {
                v.retrogradation(this.getDuration());
            }
        }

        internal void invert()
        {
            foreach (Voice v in this.voiceList)
            {
                v.invert(tonality);
            }
        }

        internal void RachmaninoffInvert()
        {
            foreach (Voice v in this.voiceList)
            {
                v.RachmaninoffInvert(tonality);
            }

            tonality = tonality.getRelative();
        }

        internal void modulate(Tonality newTonality)
        {
            Tonality oldTonality = this.tonality;
            this.tonality = newTonality;

            int[] oldScale = oldTonality.getScale();
            int[] newScale = newTonality.getScale();
            int[] oldTriad = oldTonality.getTriad();
            int[] newTriad = newTonality.getTriad();

            int[] permutatedTriad = Note.getClosestPermutation(oldTriad, newTriad);

            //Si una nota pertenecía a la tonalidad (por lo tanto estará en su escala oldScale[i]), la nota será reemplazada por targetScale[i]
            int[] targetScale = new int[newScale.Count()];

            if (permutatedTriad[0] == newTriad[1])
            {
                targetScale[0] = permutatedTriad[0];
                targetScale[1] = newScale[3];
                targetScale[2] = permutatedTriad[1];
                targetScale[3] = newScale[5];
                targetScale[4] = permutatedTriad[2];
                targetScale[5] = newScale[1];
                targetScale[6] = newScale[1];
                if (Note.getNoteDistance(oldScale[3], newScale[6]) < Note.getNoteDistance(oldScale[3], newScale[5]))
                {
                    targetScale[3] = newScale[6];
                }
            }
            else if (permutatedTriad[0] == newTriad[2])
            {
                targetScale[0] = permutatedTriad[0];
                targetScale[1] = newScale[5];
                targetScale[2] = permutatedTriad[1];
                targetScale[3] = newScale[1];
                targetScale[4] = permutatedTriad[2];
                targetScale[5] = newScale[3];
                targetScale[6] = newScale[3];
                if (Note.getNoteDistance(oldScale[1], newScale[6]) < Note.getNoteDistance(oldScale[1], newScale[5]))
                {
                    targetScale[1] = newScale[6];
                }
            }
            else //Si la triada está sin permutar (permutatedTriad[0] == newTriad[0])
            { 
                targetScale = newScale;
            }

            //Aplicamos el transporte voz a voz
            foreach (Voice v in this.voiceList)
            {
                v.modulate(oldTonality, targetScale);
            }          
        }

        internal void harmonizate()
        {
            foreach (Voice v in this.voiceList)
            {
                v.harmonizate(this.tonality);
            }
        }

        internal void elision()
        {
            foreach (Voice v in this.voiceList)
            {
                v.elision();
            }
        }

        internal void interpolate()
        {
            foreach (Voice v in this.voiceList)
            {
                v.interpolate(this.tonality);
            }
        }

        internal void permutate()
        {
            int[] triad = this.tonality.getTriad();
            foreach (Voice v in this.voiceList)
            {
                v.permutate(triad);
            }
        }

        internal void canonizate()
        {
            Duration duration = getDuration();
            List<Duration> noteStarts = getNotesStart(false);

            double optimal = double.MaxValue;
            int index = 0;

            for (int i = 1; i < noteStarts.Count - 1; i++)
            {
                if (noteStarts[i] * 2 > duration) break; //Limit: half motif
 
                double current = 0;
                foreach (Voice v in this.voiceList)
                {
                    current += v.canonizationValue(noteStarts[i]);
                }
                current /= voiceList.Count;
                if (current < optimal)
                {
                    optimal = current;
                    index = i;
                }
            }

            this.delay(noteStarts[index]);
        }

        internal void ornamentation(int pitch)
        {
            foreach (Voice v in this.voiceList)
            {
                v.ornamentation(pitch);
            }
        }

        public Motif Clone()
        {
            List<Voice> newVoiceList = new List<Voice>();
            for (int i = 0; i < this.voiceList.Count; i++)
            {
                newVoiceList.Add(this.voiceList[i].Clone());
            }

            SortedList<Harmony, int> newHarmonyList = new SortedList<Harmony, int>();
            if (this.harmonyList != null)
            {
                for (int i = 0; i < this.harmonyList.Count; i++)
                {
                    newHarmonyList.Add(this.harmonyList.Keys[i].Clone(), this.harmonyList.Values[i]);
                }
            }

            return new Motif(newVoiceList, this.parentId, variation, tonality.Clone(), newHarmonyList);
        }

        #endregion
    }
}
