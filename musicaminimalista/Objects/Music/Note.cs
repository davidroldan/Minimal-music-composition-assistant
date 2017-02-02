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
    public class Note : MusicItem
    {
        [DataMember(Name = "Duration")]
        private Duration duration;
        [DataMember(Name = "Pitch")]
        private int pitch;

        public Note(int pitch, Duration duration)
        {
            this.pitch = pitch;
            this.duration = duration;
        }

        public Note(int pitch)
        {
            this.pitch = pitch;
            this.duration = 1;
        }

        public override Duration getDuration()
        {
            return this.duration;
        }

        public override void shortenDurationFromBeginning(Duration duration)
        {
            this.duration = duration;
        }

        public override void shortenDurationFromEnd(Duration duration)
        {
            this.duration = duration;
        }

        public override int pitchMean()
        {
            return this.pitch;
        }

        public int getPitch()
        {
            return this.pitch;
        }

        public override void invert(int p, Tonality tonality)
        {
            if (this.isSilence()) return;

            int distance = this.pitch - p;
            bool inTonality = tonality.hasNote(p);
            Interval interval = new Interval(distance, inTonality);
            interval.invert(false, true, true);
            this.pitch = p + interval.toPitch();
        }

        public override void RachmaninoffInvert(int p, Tonality tonality)
        {
            if (this.isSilence()) return;

            int doubleDistance = 0;
            if (tonality.getMode() == Tonality.MAJOR)
            {
                doubleDistance = this.pitch * 2 - (p * 2 - 1);
            }
            else
            {
                doubleDistance = this.pitch * 2 - (p * 2 + 1);
            }

            this.pitch -= doubleDistance;
        }

        public override void tonaltransport(int gradeDistance, int[] scale)
        {
            if (this.isSilence()) return;

            int originalPitch = this.pitch;
            int grade = -1;
            bool onScale = false;

            for (int i = 0; i < NOTES; i++)
            {
                if (Note.isSameNote(originalPitch, scale[i]))
                {
                    grade = i;
                    onScale = true;
                    break;
                }
            }

            if (onScale)
            {
                this.pitch = Note.convertToClosestPitch(originalPitch, scale[(grade + gradeDistance) % NOTES]);
            }
            else //No pertenece a la tonalidad, transportar a una nota intermedia
            {
                for (int i = 0; i < NOTES; i++)
                {
                    if (Note.isSameNote(originalPitch+1, scale[i]))
                    {
                        grade = i;
                    }
                }
                this.pitch = Note.convertToClosestPitch(originalPitch, scale[(grade + gradeDistance) % NOTES] - 1);
            }

            //Asegurar que todas las notas asciendan en caso de cambiar a grado II-III-IV, y que todas las notas desciendan para V-VI-VII
            if (gradeDistance < 4 && this.pitch < originalPitch) pitch += Note.PITCH_OCTAVE;
            if (gradeDistance > 3 && this.pitch > originalPitch) pitch -= Note.PITCH_OCTAVE;
        }


        public override void modulate(Tonality oldTonality, int[] targetScale)
        {
            if (this.isSilence()) return;

            int[] oldScale = oldTonality.getScale();

            //Modificamos la altura de la nota solo si pertenecía a la tonalidad.
            for (int i = 0; i < oldScale.Length; i++)
            {
                if (Note.isSameNote(this.pitch, oldScale[i]))
                {
                    this.pitch = convertToClosestPitch(pitch, targetScale[i]);
                    return;
                }
            }               
        }

        public override void permutate(int[] triad)
        {
            for (int i = 0; i < triad.Length; i++)
            {
                if (Note.isSameNote(pitch, triad[i])) break;
                if (i == triad.Length - 1) return;
            }
            int value = RNG.generateModulo100();
            if (value < 50)
            {
                this.pitch = Note.convertToClosestPitch(pitch, triad[0]);
            }
            else if (value < 70)
            {
                this.pitch = Note.convertToClosestPitch(pitch, triad[1]);
            }
            else this.pitch = Note.convertToClosestPitch(pitch, triad[2]);
        }

        public override void transport(int p)
        {
            if (this.isSilence()) return;
            this.pitch += p;
        }

        public override void changeDuration(Duration multiplier)
        {
            this.duration *= multiplier;
        }

        public override MusicItem Clone()
        {
            return new Note(this.pitch, this.duration);
        }

        public override bool isSilence()
        {
            return this.pitch == Note.REST;
        }

        public override bool isChord()
        {
            return false;
        }

        // STATIC FUNCTIONS TO HANDLE NOTE DISTANCES

        public static NoteFigure getNoteFigure(int pitch)
        {
            if (pitch == -1) return NoteFigure.Silence;
            return (NoteFigure)(pitch % PITCH_OCTAVE);
        }

        public static bool isSameNote(int pitch1, int pitch2)
        {
            return (int)getNoteFigure(pitch1) == (int)getNoteFigure(pitch2);
        }

        public static int getNoteDistance(int pitch1, int pitch2)
        {
            while (pitch1 > 0) pitch1 -= PITCH_OCTAVE;
            while (pitch2 > 0) pitch2 -= PITCH_OCTAVE;
            int dist = Math.Abs(pitch1 - pitch2);
            return Math.Min(dist, PITCH_OCTAVE - dist);
        }

        public static int convertToClosestPitch(int reference, int pitchTarget)
        {
            int dist = getNoteDistance(reference, pitchTarget);
            if (isSameNote(reference - dist, pitchTarget))
                return reference - dist;
            else
                return reference + dist;
        }

        public static int[] getClosestPermutation(int[] origin, int[] source_dest)
        {
            //Solo hay 3 posibles permutaciones válidas: I-III-V, III-V-I y V-I-III. Cualquier otra posibilidad tendría
            //algún intervalo en la triada mayor que una cuarta.
            List<int[]> permutations = new List<int[]>();
            permutations.Add(source_dest); //triada sin permutar
            permutations.Add(new[] { source_dest[1], source_dest[2], source_dest[0] });
            permutations.Add(new[] { source_dest[2], source_dest[0], source_dest[1] });

            int dist = Int32.MaxValue;
            int selectedPermutation = 0;

            for (int i = 0; i < permutations.Count; i++)
            {
                int[] perm_i = permutations[i];
                int dist_aux = getNoteDistance(perm_i[0], origin[0])
                    + getNoteDistance(perm_i[1], origin[1])
                    + getNoteDistance(perm_i[2], origin[2]);
                if (dist_aux < dist)
                {
                    dist = dist_aux;
                    selectedPermutation = i;
                }
            }

            return permutations[selectedPermutation];
        }

        public const int PITCH_OCTAVE = 12;
        public const int NOTES = 7;
        public const int REST = -1;
    }
}
