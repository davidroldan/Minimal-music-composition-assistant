using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicaMinimalista.Objects.Music
{
    class Harmony
    {
        public enum HarmonyType
        {
            Major,
            Minor,
            Augmented,
            Diminished
        }

        public NoteFigure noteFigure;
        public HarmonyType harmonyType;

        public Harmony()
        {
            this.noteFigure = NoteFigure.C;
            this.harmonyType = HarmonyType.Major;
        }

        public Harmony(NoteFigure figure, HarmonyType hType)
        {
            this.noteFigure = figure;
            this.harmonyType = hType;
        }

        public Harmony parse(string s)
        {
            NoteFigure figure;

            if (s == "") return new Harmony();
            switch (s.ToLower()[0])
            {
                case 'a':
                    figure = NoteFigure.A; break;
                case 'b':
                    figure = NoteFigure.B; break;
                case 'c':
                    figure = NoteFigure.C; break;
                case 'd':
                    figure = NoteFigure.D; break;
                case 'e':
                    figure = NoteFigure.E; break;
                case 'f':
                    figure = NoteFigure.F; break;
                case 'g':
                    figure = NoteFigure.G; break;
                default:
                    return new Harmony();
            }

            s = s.Substring(1);
            if (s[0] == '#')
            {
                if (figure == NoteFigure.B) figure = NoteFigure.C;
                else figure++;
                s = s.Substring(1);
            }
            else if (s[0] == 'b')
            {
                if (figure == NoteFigure.C) figure = NoteFigure.B;
                else figure--;
                s = s.Substring(1);
            }

            s = s.Substring(1);
            if (s == "m")
            {
                return new Harmony(figure, HarmonyType.Minor);
            }

            s = s.ToLower();
            if (s == "min")
            {
                return new Harmony(figure, HarmonyType.Minor);
            }
            else if (s == "+" || s == "aug")
            {
                return new Harmony(figure, HarmonyType.Augmented);
            }
            else if (s == "-" || s == "º" || s == "dis" || s == "dim")
            {
                return new Harmony(figure, HarmonyType.Diminished);
            }
            else
            {
                return new Harmony(figure, HarmonyType.Major);
            }         
        }

        public override string ToString()
        {
            string str = "";

            switch ((int)noteFigure)
            {
                case 0: str = "C"; break;
                case 1: str = "C#"; break;
                case 2: str = "D"; break;
                case 3: str = "Eb"; break;
                case 4: str = "E"; break;
                case 5: str = "F"; break;
                case 6: str = "F#"; break;
                case 7: str = "G"; break;
                case 8: str = "Ab"; break;
                case 9: str = "A"; break;
                case 10: str = "Bb"; break;
                default: str = "B"; break; //11
            }

            switch (harmonyType)
            {
                case HarmonyType.Augmented: return str + "aug";
                case HarmonyType.Diminished: return str + "dim";
                case HarmonyType.Minor: return str + "m";
                default: return str; //major
            }
        }

        public Harmony Clone()
        {
            return new Harmony(this.noteFigure, this.harmonyType);
        }
    }
}
