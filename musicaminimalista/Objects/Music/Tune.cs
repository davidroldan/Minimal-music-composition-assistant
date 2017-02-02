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
    public class Tune
    {
        [DataMember(Name = "RefNumber")]
        private int referenceNumber;
        [DataMember(Name = "Title")]
        public string title;
        [DataMember(Name = "Tempo")]
        public int tempo;
        [DataMember(Name = "MotifList")]
        private Dictionary<int, Motif> motifList;
        [DataMember(Name = "TrackList")]
        private List<Track> trackList;
        private static int nextMotifId;

        public Tune()
        {
            this.referenceNumber = 1;
            this.tempo = 120;
            this.motifList = new Dictionary<int, Motif>();
            this.trackList = new List<Track>();
            this.title = "Tune1";
            this.addTrack(); //Default track count = 1

            nextMotifId = 0;
        }

        public Tune(int referenceNumber, string title)
        {
            this.referenceNumber = referenceNumber;
            this.tempo = 120;
            this.motifList = new Dictionary<int, Motif>();
            this.trackList = new List<Track>();
            this.title = title;

            nextMotifId = 0;
        }

        public void setReferenceNumber(int referenceNumber)
        {
            this.referenceNumber = referenceNumber;
        }

        public int getReferenceNumber()
        {
            return this.referenceNumber;
        }

        public void addMotif(Motif motif)
        {
            this.motifList.Add(motif.getId(), motif);
        }

        public void deleteMotif(int id)
        {
            this.motifList.Remove(id);
        }

        public void addTrack()
        {
            this.trackList.Add(new Track());
        }

        public void addTrack(int index, Track track)
        {
            this.trackList.Insert(index, track);
        }

        public void deleteTrack(int track)
        {
            if (trackList.Count > 0) this.trackList.RemoveAt(track);
        }

        public Motif getMotif(int id)
        {
            Motif m;
            this.motifList.TryGetValue(id, out m);
            return m;
        }

        public Motif getMotifFromList(int i)
        {
            return this.motifList.ElementAt(i).Value;
        }

        public Track getTrack(int index)
        {
            return this.trackList.ElementAt(index);
        }

        public static int generateMotifID()
        {
            return nextMotifId++;
        }

        public int motifCount()
        {
            return this.motifList.Count;
        }

        public int trackCount()
        {
            return this.trackList.Count;
        }

        public int getTempo()
        {
            return this.tempo;
        }

        public void setTempo(int tempo)
        {
            this.tempo = tempo;
        }

        public void recalculateNextId()
        {
            //Use only when loading a project.
            int maxKey = 0;
            foreach (KeyValuePair<int, Motif> pair in this.motifList)
            {
                if (pair.Key > maxKey)
                    maxKey = pair.Key;
            }
            Tune.nextMotifId = maxKey + 1;
        }
    }
}
