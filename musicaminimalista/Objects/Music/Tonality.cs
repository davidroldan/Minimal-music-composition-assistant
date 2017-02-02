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
    public class Tonality
    {
        [DataMember(Name = "Accidentals")]
        private int accidentals;
        [DataMember(Name = "Mode")]
        private int mode;
        [DataMember(Name = "Scale")]
        private int[] scale;

        //Default tonality: C
        public Tonality()
        {
            this.accidentals = 0;
            this.mode = MAJOR;
            this.generateScale();
        }

        public Tonality(int accidentals, int mode)
        {
            this.accidentals = accidentals;
            this.mode = mode;
            this.generateScale();
        }

        public void transport(int semitones)
        {
            //Convert semitones to accidentals
            int newacc = semitones * 7;

            //Set accidentals within -7 ; 7 as close as possible to original number of accidentals
            while (newacc > this.accidentals) newacc -= 12;
            while (newacc < this.accidentals) newacc += 12;
            if (newacc > 7)
            {
                this.accidentals = newacc - 12;
            }
            else if (newacc - 12 < -7)
            {
                this.accidentals = newacc;
            }
            else
            {
                if (Math.Abs(this.accidentals - newacc) <= Math.Abs(this.accidentals - (newacc - 12)))
                {
                    this.accidentals = newacc;
                }
                else
                {
                    this.accidentals = newacc - 12;
                }
            }
            this.generateScale();
        }

        private void generateScale()
        {
            this.scale = new int[Note.NOTES];

            this.scale[0] = this.generateTonicPitch();

            if (this.mode == Tonality.MAJOR)
            {
                this.scale[1] = scale[0] + 2;
                this.scale[2] = scale[1] + 2;
                this.scale[3] = scale[2] + 1;
                this.scale[4] = scale[3] + 2;
                this.scale[5] = scale[4] + 2;
                this.scale[6] = scale[5] + 2;
            }

            if (this.mode == Tonality.MINOR)
            {
                this.scale[1] = scale[0] + 2;
                this.scale[2] = scale[1] + 1;
                this.scale[3] = scale[2] + 2;
                this.scale[4] = scale[3] + 2;
                this.scale[5] = scale[4] + 1;
                this.scale[6] = scale[5] + 2;
            }
        }

        public Tonality getRelative()
        {
            int relativeAcc;
            if (this.mode == Tonality.MAJOR)
            {
                relativeAcc = this.accidentals - 3;
                if (relativeAcc < -7) relativeAcc += 12;
                return new Tonality(relativeAcc, Tonality.MINOR);
            }
            else
            {
                relativeAcc = this.accidentals + 3;
                if (relativeAcc > 7) relativeAcc -= 12;
                return new Tonality(relativeAcc, Tonality.MAJOR);
            }
        }

        private int generateTonicPitch()
        {
            int pitch = 0;
            int acc = this.accidentals;
            if (acc < -1) //Remove the b
            {
                acc += Note.NOTES;
                pitch--;
            }
            if (acc > 5) //Remove the #
            {
                acc -= Note.NOTES;
                pitch++;
            }

            switch (acc)
            {
                case -1: pitch += (int)NoteFigure.F; break;
                case 0: pitch += (int)NoteFigure.C; break;
                case 1: pitch += (int)NoteFigure.G; break;
                case 2: pitch += (int)NoteFigure.D; break;
                case 3: pitch += (int)NoteFigure.A; break;
                case 4: pitch += (int)NoteFigure.E; break;
                case 5: pitch += (int)NoteFigure.B; break;
            }

            if (this.mode == MINOR)
                pitch -= 3; //Example Cminor = EbMajor
            return pitch % Note.PITCH_OCTAVE;
        }


        public int getAccidentals()
        {
            return this.accidentals;
        }

        public int getMode()
        {
            return this.mode;
        }

        public int getTonicPitch(){
            if (scale == null) this.generateScale();
            return this.scale[0];
        }

        public int[] getTriad()
        {
            if (scale == null) this.generateScale();
            return new int[] { scale[0], scale[2], scale[4] };
        }

        public int[] getScale()
        {
            if (scale == null) this.generateScale();
            return scale;
        }

        public bool hasNote(int pitch)
        {
            if (scale == null) this.generateScale();
            for (int i = 0; i < scale.Count(); i++)
            {
                if (Note.isSameNote(pitch, scale[i]))
                    return true;
            }
            return false;
        }

        public bool isinTriad(int pitch)
        {
            int[] triad = getTriad();
            for (int i = 0; i < triad.Count(); i++)
            {
                if (Note.isSameNote(pitch, triad[i]))
                    return true;
            }
            return false;
        }

        public int getRandomPitchFromScale()
        {
            if (scale == null) this.generateScale();
            return scale[RNG.generate(0, Note.NOTES - 1)];
        }

        public int getRandomPitchFromScale(int triadProbability)
        {
            if (scale == null) this.generateScale();

            if (triadProbability > RNG.generateModulo100())
            {
                return getTriad()[RNG.generate(0, 2)];
            }

            int nonTriadNote = RNG.generate(0, 3);

            switch (nonTriadNote)
            {
                case 0: return scale[1];
                case 1: return scale[3];
                case 2: return scale[5];
                default: return scale[6];
            }
        }

        public static Tonality parse(string str)
        {
            str = str.ToLower();

            if (str.Length < 1 || str.Length > 3) return new Tonality(); //Fail, returns default tonality

            int mode = MAJOR, accidentals = 0;
            if (str[str.Length - 1] == 'm')
            {
                mode = MINOR;
            }
            switch (str[0])
            {
                case 'f': accidentals = -1; break;
                case 'c': accidentals = 0; break;
                case 'g': accidentals = 1; break;
                case 'd': accidentals = 2; break;
                case 'a': accidentals = 3; break;
                case 'e': accidentals = 4; break;
                case 'b': accidentals = 5; break;
                default: return new Tonality(); //Fail, returns default tonality
            }

            if (str.Length == 2)
            {
                if (str[1] == '#') accidentals += Note.NOTES;
                if (str[1] == 'b') accidentals -= Note.NOTES;
            }

            //Minor mode, -3
            if (mode == MINOR)
            {
                accidentals -= 3; //Example: Cm = 3 flats ; C = 0 flats / sharps
            }

            //Range is -7 to 7
            if (accidentals < -7) accidentals += 12; //Example: Cb = B
            if (accidentals > 7) accidentals -= 12;

            return new Tonality(accidentals, mode);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            Tonality t = obj as Tonality;
            if ((Tonality)t == null)
                return false;

            return (this.accidentals == t.accidentals && this.mode == t.mode);
        }

        public override int GetHashCode()
        {
            return this.accidentals + this.mode * 10;
        }

        public Tonality Clone()
        {
            return new Tonality(this.accidentals, this.mode);
        }

        public override string ToString()
        {
            string str;
            bool sharp = false;
            bool flat = false;
            int acc = this.accidentals;
            if (mode == MINOR) acc += 3;
            if (acc > 5)
            {
                sharp = true;
                acc -= Note.NOTES;
            }
            else if (acc < -1)
            {
                flat = true;
                acc += Note.NOTES;
            }
            switch (acc)
            {
                case -1: str = "F"; break;
                case 0: str = "C"; break;
                case 1: str = "G"; break;
                case 2: str = "D"; break;
                case 3: str = "A"; break;
                case 4: str = "E"; break;
                case 5: str = "B"; break;
                default: return "C"; //should never happen
            }
            if (sharp) str += "#";
            if (flat) str += "b";
            if (this.mode == MINOR) str += "m";
            return str;
        }

        public const int MAJOR = 0;
        public const int MINOR = 1;
    }  
}
