using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicaMinimalista.Objects.Music;
using MusicaMinimalista.Objects.Utils;

public class AbcFileWriter
{
    public static void saveToFile(Tune tune, string fileName, int tempo, bool midi)
    {
        using (System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(fileName, false))
        {
            Tonality tonality = new Tonality(); //default tonality;
            int voiceCount = 0;
            List<string> voiceABC = new List<string>();
            AbcNoteParser anp = new AbcNoteParser(tonality);
            streamWriter.WriteLine("X:" + tune.getReferenceNumber());
            streamWriter.WriteLine("M:C");
            streamWriter.WriteLine("L:1/4");
            streamWriter.WriteLine("Q:1/4=" + tempo);
            streamWriter.WriteLine("K:" + tonality.ToString());
            for (int i1 = 0; i1 < tune.trackCount(); i1++)
            {
                Track track = tune.getTrack(i1);
                if (track.isEmpty()) continue;
                if (track.getVolume() == 0) continue;
                anp.resetAccidentals();
                voiceABC.Clear();
                Duration motifStart = 0;
                Duration previousMotifEnd = 0;
                for (int i = 0; i < track.motifCount(); i++)
                {
                    Motif motif = tune.getMotif(track.getMotifIdentificator(i));
                    Duration motifDuration = motif.getDuration();
                    motifStart = track.getStartTime(i);
                    for (int j = 0; j < motif.voiceCount(); j++)
                    {
                        Voice voice = motif.getVoice(j);
                        anp.resetAccidentals();
                        Duration voiceDuration = voice.getDuration();
                        if (j >= voiceABC.Count)
                        {
                            voiceABC.Add("");
                            if (motifStart > 0) voiceABC[j] += "z" + motifStart + " ";
                        }
                        else
                        {
                            Duration relativeStart = motifStart - previousMotifEnd;
                            if (relativeStart > 0) voiceABC[j] += "z" + relativeStart + " ";
                        }

                        for (int k = 0; k < voice.size(); k++)
                        {
                            voiceABC[j] += anp.toABC(voice.get(k)) + " ";
                        }
                    }
                    for (int j = motif.voiceCount(); j < voiceABC.Count; j++)
                    {
                        if (motifDuration > 0) voiceABC[j] += "z" + motifStart + " ";
                    }
                    previousMotifEnd = motifStart + motifDuration;
                }
                for (int i = 0; i < voiceABC.Count; i++)
                {
                    streamWriter.WriteLine("V:" + (voiceCount + i));
                    if (midi)
                    {
                        streamWriter.WriteLine("%%MIDI program " + (int)track.getTimbre());
                        switch (track.getVolume())
                        {
                            case 1: streamWriter.WriteLine("!ppp!");
                                break;
                            case 2: streamWriter.WriteLine("!pp!");
                                break;
                            case 3: streamWriter.WriteLine("!p!");
                                break;
                            case 4: streamWriter.WriteLine("!mp!");
                                break;
                            case 5: streamWriter.WriteLine("!mf!");
                                break;
                            case 6: streamWriter.WriteLine("!f!");
                                break;
                            case 7: streamWriter.WriteLine("!ff!");
                                break;
                            default: streamWriter.WriteLine("!fff!");
                                break;
                        }
                        streamWriter.Write(voiceABC[i]);
                        streamWriter.WriteLine("z");
                    }
                    else
                    {
                        streamWriter.WriteLine(voiceABC[i]);
                    }
                }
                voiceCount += voiceABC.Count;
            }
        }
    }

    public static void saveToFile(Motif motif, string fileName, bool midi)
    {
        using (System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(fileName, false))
        {
            Tonality tonality = motif.getTonality();
            AbcNoteParser anp = new AbcNoteParser(tonality);
            streamWriter.WriteLine("X:1");
            streamWriter.WriteLine("L:1/4");
            streamWriter.WriteLine("K:" + tonality.ToString());

            int voiceIterator = 0;
            for (int j = 0; j < motif.voiceCount(); j++)
            {
                streamWriter.WriteLine("V:" + voiceIterator);
                voiceIterator++;
                Voice v = motif.getVoice(j);
                anp.resetAccidentals();
                for (int k = 0; k < v.size(); k++)
                {
                    streamWriter.Write(anp.toABC(v.get(k)));
                    streamWriter.Write(" ");
                }
                if (midi) streamWriter.Write("z");
                streamWriter.WriteLine("|]");
            }
        }
    }

    public AbcFileWriter()
    {
        this.accidentals = 0;
    }

    public void setTonality(Tonality t)
    {
        this.accidentals = t.getAccidentals();
    }

    private int accidentals;
}
