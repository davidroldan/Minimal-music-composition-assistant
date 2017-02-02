using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicaMinimalista.Objects.Music;

namespace MusicaMinimalista.Objects.Utils
{
    public class AbcNoteParser
    {
        private Tonality tonality; //K
        private Duration unitNoteLength; //L
        private Accidental[] currentAccidentals;

        public AbcNoteParser()
        {
            this.tonality = new Tonality();
            this.unitNoteLength = new Duration(1, 4); //Default unit note length
            this.currentAccidentals = new Accidental[7];
            this.resetAccidentals();
        }

        public AbcNoteParser(Tonality tonality)
        {
            this.tonality = tonality;
            this.unitNoteLength = new Duration(1, 4); //Default unit note length
            this.currentAccidentals = new Accidental[7];
            this.resetAccidentals();
        }

        public void setTonality(Tonality tonality)
        {
            this.tonality = tonality;
        }

        public void setUnitNoteLength(Duration d)
        {
            this.unitNoteLength = d;
        }

        public void resetAccidentals()
        {
            for (int i = 0; i < currentAccidentals.Length; i++)
            {
                currentAccidentals[i] = Accidental.NONE;
            }
        }

        public Note parse(string note, Duration duration)
        {
            if (note == "z")
                return new Note(Note.REST, duration * unitNoteLength * 4);

            Accidental accidental = Accidental.NONE;
            int selectedNote = 0;
            int pitch = 0;

            //Parse accidental and remove it from the string
            if (note.Length > 1 && note.Substring(0, 2) == "__")
            {
                accidental = Accidental.DOUBLE_FLAT;
                note = note.Substring(2);
            }
            else if (note.Length > 1 && note.Substring(0, 2) == "^^")
            {
                accidental = Accidental.DOUBLE_SHARP;
                note = note.Substring(2);
            }
            else if (note[0] == '_')
            {
                accidental = Accidental.FLAT;
                note = note.Substring(1);
            }
            else if (note[0] == '=')
            {
                accidental = Accidental.NATURAL;
                note = note.Substring(1);
            }
            else if (note[0] == '^')
            {
                accidental = Accidental.SHARP;
                note = note.Substring(1);
            }

            //Parse note
            char charnote = note[0];
            note = note.Substring(1);

            if (char.IsUpper(charnote))
            {
                charnote = char.ToLower(charnote);
                pitch -= Note.PITCH_OCTAVE;
            }

            switch (charnote)
            {
                case 'f': pitch += (int)NoteFigure.F + C_5; selectedNote = 0; break;
                case 'c': pitch += (int)NoteFigure.C + C_5; selectedNote = 1; break;
                case 'g': pitch += (int)NoteFigure.G + C_5; selectedNote = 2; break;
                case 'd': pitch += (int)NoteFigure.D + C_5; selectedNote = 3; break;
                case 'a': pitch += (int)NoteFigure.A + C_5; selectedNote = 4; break;
                case 'e': pitch += (int)NoteFigure.E + C_5; selectedNote = 5; break;
                case 'b': pitch += (int)NoteFigure.B + C_5; selectedNote = 6; break;
            }

            //Update accidental
            if (accidental != Accidental.NONE) currentAccidentals[selectedNote] = accidental;

            //Update pitch
            switch (currentAccidentals[selectedNote])
            {
                case Accidental.DOUBLE_SHARP: pitch += 2; break;
                case Accidental.SHARP: pitch += 1; break;
                case Accidental.FLAT: pitch -= 1; break;
                case Accidental.DOUBLE_FLAT: pitch -= 2; break;
            }

            //Check tonality if no accidental applied
            if (currentAccidentals[selectedNote] == Accidental.NONE)
            {
                int tonalityAccidentals = tonality.getAccidentals();
                if (tonalityAccidentals > 0)
                {
                    if (tonalityAccidentals > selectedNote) pitch++;
                }
                else if (tonalityAccidentals < 0)
                {
                    if (tonalityAccidentals + 7 <= selectedNote) pitch--;
                }
            }

            //Octave
            for (int i = 0; i < note.Length; i++)
            {
                switch (note[i])
                {
                    case ',': pitch -= Note.PITCH_OCTAVE; break;
                    case '\'': pitch += Note.PITCH_OCTAVE; break;
                }
            }

            return new Note(pitch, duration * unitNoteLength * 4);
        }

        public string toABC(MusicItem mi)
        {
            if (mi is Note)
            {
                return toABC_Note((Note)mi);
            }
            else
            {
                return toABC_Chord((Chord)mi);
            }
        }

        private string toABC_Chord(Chord chord)
        {
            string result = "[";
            for (int i = 0; i < chord.size(); i++)
            {
                result += toABC_Note(chord.get(i));
            }
            return result + "]";
        }

        private string toABC_Note(Note note)
        {
            if (note.getDuration() == 1)
            {
                return pitchToString(note.getPitch());
            }
            else
            {
                return pitchToString(note.getPitch()) + note.getDuration();
            }
        }

        private string pitchToString(int pitch)
        {
            if (pitch == Note.REST) return "z";
            string pitch_str = "";
            if (pitch >= C_5) //Lower case pitch
            {
                while (pitch > C_5 + (int)NoteFigure.B)
                {
                    pitch_str += '\'';
                    pitch -= Note.PITCH_OCTAVE;
                }
                return pitchNoteToString(pitch, true) + pitch_str;
            }
            else
            {
                while (pitch < C_4)
                {
                    pitch_str += ',';
                    pitch += Note.PITCH_OCTAVE;
                }
                return pitchNoteToString(pitch, false) + pitch_str;
            }
        }

        private string pitchNoteToString(int pitch, bool lowercase)
        {
            int acc = this.tonality.getAccidentals();

            if (lowercase) pitch -= Note.PITCH_OCTAVE;

            string pitch_str = "";
            switch (pitch)
            {
                case C_4:
                    pitch_str = naturalNote(0); break;
                case C_4 + 1:
                    pitch_str = accidentalNote(0); break;
                case C_4 + 2:
                    pitch_str = naturalNote(1); break;
                case C_4 + 3:
                    pitch_str = accidentalNote(1); break;
                case C_4 + 4:
                    pitch_str = naturalNote(2); break;
                case C_4 + 5:
                    pitch_str = naturalNote(3); break;
                case C_4 + 6:
                    pitch_str = accidentalNote(3); break;
                case C_4 + 7: 
                    pitch_str = naturalNote(4); break;
                case C_4 + 8:
                    pitch_str = accidentalNote(4); break;
                case C_4 + 9:
                    pitch_str = naturalNote(5); break;
                case C_4 + 10:
                    pitch_str = accidentalNote(5); break;
                case C_4 + 11:
                    pitch_str = naturalNote(6); break;
            }

            if (lowercase) return pitch_str.ToLower();
            else return pitch_str;
        }

        private string naturalNote(int scaleNote)
        {
            int acc = this.tonality.getAccidentals();
            int minAccidental = 0;
            int maxAccidental = 0;
            string[] notestr = { "C", "D", "E", "F", "G", "A", "B" };

            switch (scaleNote)
            {
                case 0: //C
                    maxAccidental = 2; break;
                case 1: //D
                    maxAccidental = 4; break;
                case 2: //E
                    maxAccidental = 6; break;
                case 3: //F
                    maxAccidental = 1; break;
                case 4: //G
                    maxAccidental = 3; break;
                case 5: //A
                    maxAccidental = 5; break;
                case 6: //B
                    maxAccidental = 7; break;
            }

            minAccidental = maxAccidental - 8; //Example C# default if acc >= 2, Cb if acc <= -6

            if (this.currentAccidentals[scaleNote] == Accidental.NATURAL)
            {
                return notestr[scaleNote];
            }
            else if (this.currentAccidentals[scaleNote] == Accidental.NONE)
            {
                if (acc <= minAccidental || acc >= maxAccidental)
                {
                    currentAccidentals[scaleNote] = Accidental.NATURAL;
                    return "=" + notestr[scaleNote];
                }
                else return notestr[scaleNote];
            }
            else
            {
                currentAccidentals[scaleNote] = Accidental.NATURAL;
                return "=" + notestr[scaleNote];
            }
        }

        private string accidentalNote(int previousNaturalNote)
        {
            int acc = this.tonality.getAccidentals();
            int minAccidental = 0;
            int maxAccidental = 0;
            string[] notestr = { "C", "D", "E", "F", "G", "A", "B" };
            switch (previousNaturalNote)
            {
                case 0: // C# or Db
                    maxAccidental = 2; //C# default if acc >= 2
                    break;
                case 1: // D# or Eb
                    maxAccidental = 4;
                    break;
                case 3: // F# or Gb
                    maxAccidental = 1;
                    break;
                case 4: // G# or Ab
                    maxAccidental = 3;
                    break;
                case 5: // A# or Bb
                    maxAccidental = 5;
                    break;
            }
            minAccidental = maxAccidental - 6; //Example: C# is default if accidentals >= 2. Db is default if accidentals is <= -4;

            if (acc >= 0)
            {
                if (this.currentAccidentals[previousNaturalNote] == Accidental.SHARP)
                    return notestr[previousNaturalNote];
                else if (this.currentAccidentals[0] == Accidental.NONE && acc >= maxAccidental)
                    return notestr[previousNaturalNote];
                else
                {
                    this.currentAccidentals[previousNaturalNote] = Accidental.SHARP;
                    return "^" + notestr[previousNaturalNote];
                }
            }
            else
            {
                if (this.currentAccidentals[previousNaturalNote + 1] == Accidental.FLAT)
                    return notestr[previousNaturalNote + 1];
                else if (this.currentAccidentals[previousNaturalNote + 1] == Accidental.NONE && acc <= minAccidental)
                    return notestr[previousNaturalNote + 1];
                else
                {
                    this.currentAccidentals[previousNaturalNote + 1] = Accidental.FLAT;
                    return "_" + notestr[previousNaturalNote + 1];
                }
            }
        }

        private const int C_5 = 72;
        private const int C_4 = 60;
    }
}
