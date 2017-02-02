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
    public class Interval
    {
        public enum IntervalMode
        {
            MINOR,
            MAJOR,
            DIMINISHED,
            AUGMENTED,
            PERFECT
        }

        public enum IntervalDirection
        {
            INCREASING,
            DECREASING
        }

        [DataMember(Name = "Distance")]
        private int distance;
        [DataMember(Name = "Mode")]
        private IntervalMode mode;
        [DataMember(Name = "Direction")]
        private IntervalDirection direction;

        public const int INTERVAL_OCTAVE = 7;

        public Interval(int distance, IntervalMode mode, IntervalDirection direction){
            this.distance = distance;
            this.mode = mode;
            this.direction = direction;
        }

        public Interval(int pitch)
        {
            Interval i = Interval.fromPitch(pitch, true);
            this.distance = i.distance;
            this.mode = i.mode;
            this.direction = i.direction;
        }

        public Interval(int pitch, bool isInTonality)
        {
            Interval i = Interval.fromPitch(pitch, isInTonality);
            this.distance = i.distance;
            this.mode = i.mode;
            this.direction = i.direction;
        }

        private static Interval fromPitch(int pitch, bool isInTonality)
        {
            //Calculate direction

            IntervalDirection direction = IntervalDirection.INCREASING;
            if (pitch < 0)
            {
                pitch = -pitch;
                direction = IntervalDirection.DECREASING;
            }

            //Reduce pitch change to 1 octave

            int octave = 0;
            while (pitch >= Note.PITCH_OCTAVE)
            {
                pitch -= Note.PITCH_OCTAVE;
                octave++;
            }

            switch (pitch)
            {
                case 0: return new Interval(
                                1 + octave * Interval.INTERVAL_OCTAVE,
                                IntervalMode.PERFECT,
                                direction
                        );
                case 1: return new Interval(
                                2 + octave * Interval.INTERVAL_OCTAVE,
                                IntervalMode.MINOR,
                                direction
                        );
                case 2: return new Interval(
                                2 + octave * Interval.INTERVAL_OCTAVE,
                                IntervalMode.MAJOR,
                                direction
                        );
                case 3: if (isInTonality)
                            return new Interval(
                                3 + octave * Interval.INTERVAL_OCTAVE,
                                IntervalMode.MINOR,
                                direction
                            );
                        else
                            return new Interval(
                                2 + octave * Interval.INTERVAL_OCTAVE,
                                IntervalMode.AUGMENTED,
                                direction
                            );
                case 4: if (isInTonality)
                            return new Interval(
                                3 + octave * Interval.INTERVAL_OCTAVE,
                                IntervalMode.MAJOR,
                                direction
                            );
                        else
                            return new Interval(
                                4 + octave * Interval.INTERVAL_OCTAVE,
                                IntervalMode.DIMINISHED,
                                direction
                            );
                case 5: return new Interval(
                                4 + octave * Interval.INTERVAL_OCTAVE,
                                IntervalMode.PERFECT,
                                direction
                        );
                case 6: return new Interval(
                                5 + octave * Interval.INTERVAL_OCTAVE,
                                IntervalMode.DIMINISHED,
                                direction
                        );
                case 7: return new Interval(
                                5 + octave * Interval.INTERVAL_OCTAVE,
                                IntervalMode.PERFECT,
                                direction
                        );
                case 8: if (isInTonality)
                            return new Interval(
                                6 + octave * Interval.INTERVAL_OCTAVE,
                                IntervalMode.MINOR,
                                direction
                            );
                        else
                            return new Interval(
                                5 + octave * Interval.INTERVAL_OCTAVE,
                                IntervalMode.AUGMENTED,
                                direction
                            );
                case 9: if (isInTonality)
                            return new Interval(
                                6 + octave * Interval.INTERVAL_OCTAVE,
                                IntervalMode.MAJOR,
                                direction
                            );
                        else
                            return new Interval(
                                7 + octave * Interval.INTERVAL_OCTAVE,
                                IntervalMode.DIMINISHED,
                                direction
                            );
                case 10: if (isInTonality)
                            return new Interval(
                                7 + octave * Interval.INTERVAL_OCTAVE,
                                IntervalMode.MINOR,
                                direction
                            );
                        else
                            return new Interval(
                                6 + octave * Interval.INTERVAL_OCTAVE,
                                IntervalMode.AUGMENTED,
                                direction
                            );
                case 11: if (isInTonality)
                            return new Interval(
                                7 + octave * Interval.INTERVAL_OCTAVE,
                                IntervalMode.MAJOR,
                                direction
                            );
                        else
                            return new Interval(
                                7 + octave * Interval.INTERVAL_OCTAVE,
                                IntervalMode.MAJOR,
                                direction
                            );
                default: return null;
            }
        }

        public void invert(bool invertDistance, bool invertMode, bool invertDirection)
        {
            if (invertDistance)
            {
                int octave = 0;
                int newDist = this.distance;

                //Reduce to 1 octave
                while (newDist > Interval.INTERVAL_OCTAVE + 1)
                {
                    newDist -= Interval.INTERVAL_OCTAVE;
                    octave++;
                }

                //Inversion:
                // - newDist : 1-7
                // - if 1: inverted interval is 1
                // - if 2-7: inverted is 7-2 (9-newDist)
                if (newDist > 1)
                {
                    newDist = 9 - newDist;
                }

                // Recover octave
                newDist += Interval.INTERVAL_OCTAVE * octave;

                this.distance = newDist;
            }

            if (invertMode)
            {
                switch (this.mode)
                {
                    case IntervalMode.DIMINISHED:
                        this.mode = IntervalMode.AUGMENTED;
                        break;
                    case IntervalMode.AUGMENTED:
                        this.mode = IntervalMode.DIMINISHED;
                        break;
                    case IntervalMode.MAJOR:
                        this.mode = IntervalMode.MINOR;
                        break;
                    case IntervalMode.MINOR:
                        this.mode = IntervalMode.MAJOR;
                        break;
                    case IntervalMode.PERFECT: //do nothing
                        break;
                }
            }

            if (invertDirection)
            {
                if (this.direction == IntervalDirection.DECREASING)
                    this.direction = IntervalDirection.INCREASING;
                else
                    this.direction = IntervalDirection.DECREASING;
            }
        }

        public int toPitch()
        {
            int pitch = 0;
            int octave = 0;
            int auxDistance = this.distance;
            while (auxDistance > Interval.INTERVAL_OCTAVE + 1)
            {
                auxDistance -= Interval.INTERVAL_OCTAVE;
            }

            switch (auxDistance)
            {
                case 1:
                    pitch = 0;
                    break;
                case 2:
                    if (this.mode == IntervalMode.DIMINISHED) pitch = 0;
                    else if (this.mode == IntervalMode.MINOR) pitch = 1;
                    else if (this.mode == IntervalMode.MAJOR) pitch = 2;
                    else if (this.mode == IntervalMode.AUGMENTED) pitch = 3;
                    break;
                case 3:
                    if (this.mode == IntervalMode.DIMINISHED) pitch = 2;
                    else if (this.mode == IntervalMode.MINOR) pitch = 3;
                    else if (this.mode == IntervalMode.MAJOR) pitch = 4;
                    else if (this.mode == IntervalMode.AUGMENTED) pitch = 5;
                    break;
                case 4:
                    if (this.mode == IntervalMode.DIMINISHED) pitch = 4;
                    else if (this.mode == IntervalMode.PERFECT) pitch = 5;
                    else if (this.mode == IntervalMode.AUGMENTED) pitch = 6;
                    break;
                case 5:
                    if (this.mode == IntervalMode.DIMINISHED) pitch = 6;
                    else if (this.mode == IntervalMode.PERFECT) pitch = 7;
                    else if (this.mode == IntervalMode.AUGMENTED) pitch = 8;
                    break;
                case 6:
                    if (this.mode == IntervalMode.DIMINISHED) pitch = 7;
                    else if (this.mode == IntervalMode.MINOR) pitch = 8;
                    else if (this.mode == IntervalMode.MAJOR) pitch = 9;
                    else if (this.mode == IntervalMode.AUGMENTED) pitch = 10;
                    break;
                case 7:
                    if (this.mode == IntervalMode.DIMINISHED) pitch = 9;
                    else if (this.mode == IntervalMode.MINOR) pitch = 10;
                    else if (this.mode == IntervalMode.MAJOR) pitch = 11;
                    else if (this.mode == IntervalMode.AUGMENTED) pitch = 12;
                    break;
            }

            pitch += Note.PITCH_OCTAVE * octave;
            if (this.direction == IntervalDirection.DECREASING)
            {
                pitch *= -1;
            }

            return pitch;
        }
    }
}
