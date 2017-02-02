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
    public class Track
    {
        [DataMember(Name = "TrackElemList")]
        private SortedList<Duration, int> trackElemList;
        [DataMember(Name = "Timbre")]
        private Timbre timbre;
        [DataMember(Name = "Volume")]
        private int volume;

        public const int MAX_VOLUME = 8;

        public Track()
        {
            this.trackElemList = new SortedList<Duration, int>();
            this.timbre = Timbre.Acoustic_Grand_Piano;
            this.volume = MAX_VOLUME / 2;
        }

        public int motifCount()
        {
            return this.trackElemList.Count;
        }

        public int getMotifIdentificator(int index)
        {
            return this.trackElemList.ElementAt(index).Value;
        }

        public void setTimbre(Timbre timbre)
        {
            this.timbre = timbre;
        }

        public Timbre getTimbre(){
            return this.timbre;
        }

        public void setVolume(int volume)
        {
            this.volume = volume;
        }

        public int getVolume()
        {
            return this.volume;
        }

        public bool isEmpty()
        {
            return this.trackElemList.Count == 0;
        }

        public void setSettingsFrom(Track track)
        {
            this.timbre = track.timbre;
            this.volume = track.volume;
        }

        /**
         * Returns selected Motif. If key does not exist, returns key to the left.
        **/
        public int getMotifIndex(Duration selected)
        {
            for (int i = this.motifCount() - 1; i >= 0; i--)
            {
                if (selected >= this.getStartTime(i)) return i;
            }
            return -1;
        }

        public Duration getStartTime(int index)
        {
            return this.trackElemList.ElementAt(index).Key;
        }

        public void setStartTime(Duration duration, int index)
        {
            int motifid = this.trackElemList.ElementAt(index).Value;
            this.trackElemList.RemoveAt(index);
            this.trackElemList.Add(duration, motifid);
        }

        public void insert(int motifIndex, Duration start)
        {
            this.trackElemList.Add(start, motifIndex);
        }

        public void remove(int index)
        {
            this.trackElemList.RemoveAt(index);
        }
    }
}
