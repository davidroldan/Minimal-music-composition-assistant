using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using MusicaMinimalista.Objects.Music;
using MusicaMinimalista.Objects.Music.Variations;
using MusicaMinimalista.Objects.Actions;
using MusicaMinimalista.Objects.Utils;
using MusicaMinimalista.Controls;

namespace MusicaMinimalista.Objects
{
    public class Controller
    {
        private Tune tune;
        private MainWindow mainWindow;
        private MotifTreeView motifTreeView;
        private PlayList playlist;
        private List<ProgramAction> actionList;
        private int actionIndex;
        private const int MAX_ACTIONS = 20;
        private ProjectUtil projectUtil;
        private MediaPlayer mediaPlayer;
        private bool executingAction;

        public Controller(Tune tune, AxWMPLib.AxWindowsMediaPlayer wmpTune, AxWMPLib.AxWindowsMediaPlayer wmpMotif, MotifTreeView mtv, PlayList playlist, MusicaMinimalista.MainWindow mainWindow)
        {
            this.tune = tune;
            this.mainWindow = mainWindow;
            this.motifTreeView = mtv;
            this.motifTreeView.AfterSelect += motifTreeView_AfterSelect;
            this.playlist = playlist;
            this.actionList = new List<ProgramAction>();
            this.actionIndex = 0;
            this.projectUtil = new ProjectUtil();
            this.mediaPlayer = new MediaPlayer(wmpTune, wmpMotif, playlist);
            this.executingAction = false;
        }

        #region Action
        public Dictionary<int, Variation> getChildVariationList(int motifId)
        {
            Dictionary<int, Variation> dict = new Dictionary<int, Variation>();
            for (int i = 0; i < this.motifCount(); i++)
            {
                Motif m = this.tune.getMotifFromList(i);
                if (m.getParentId() == motifId)
                {
                    dict.Add(m.getId(), m.getVariation());
                }
            }
            return dict;
        }     
        public List<Duration> getMotifInstances(int id, int track)
        {
            List<Duration> list = new List<Duration>();
            Track t = tune.getTrack(track);
            for (int i = 0; i < t.motifCount(); i++)
            {
                if (t.getMotifIdentificator(i) == id)
                {
                    list.Add(t.getStartTime(i));
                }
            }
            return list;
        }
        public void addMotif(Motif motif)
        {
            this.tune.addMotif(motif);
            this.treeview_addMotif(motif.getName(), motif.getParentId());
        }
        public void restoreMotif(Motif motif, Dictionary<int, Variation> childVariationList, List<Duration>[] motifInstances)
        {
            int motifId = motif.getId();
            //Add motif to list
            this.tune.addMotif(motif);

            //Restore motif instances
            for (int i = 0; i < tune.trackCount(); i++)
            {
                Track t = tune.getTrack(i);
                foreach (Duration d in motifInstances[i])
                    t.insert(motifId, d);
            }

            //Update childs
            foreach (KeyValuePair<int, Variation> child in childVariationList)
            {
                Motif m = this.getMotif(child.Key);
                m.setVariation(child.Value, motifId);
            }

            //Update tree
            this.treeview_restoreMotif(motif.getName(), motif.getParentId(), childVariationList);
        }
        public void deleteMotif(int id)
        {
            Motif motif = this.getMotif(id);
            int parentId = motif.getParentId();

            //Delete all instances of Motif in TrackList
            for (int i = 0; i < tune.trackCount(); i++)
            {
                Track t = tune.getTrack(i);
                int j = 0;
                while (j < t.motifCount())
                {
                    if (t.getMotifIdentificator(j) == id)
                    {
                        t.remove(j);
                    }
                    else j++;
                }
            }

            //Update childs
            for (int i = 0; i < this.motifCount(); i++)
            {
                Motif m = this.tune.getMotifFromList(i);
                if (parentId == id)
                {
                    if (parentId == -1) m.setVariation(new NullVariation(), -1);
                    else m.setVariation(new CompositeVariation(motif.getVariation(), m.getVariation()), parentId);
                }
            }

            //Update tree
            this.treeview_deleteMotif(this.getMotif(id).getName());

            //Delete motif from list
            this.tune.deleteMotif(id);
        }
        public void removeMotif(int track, Duration start)
        {
            Track t = this.tune.getTrack(track);
            this.tune.getTrack(track).remove(t.getMotifIndex(start));
        }
        public void insertMotif(int motifId, int track, Duration start){
            this.tune.getTrack(track).insert(motifId, start);
        }
        public void moveMotif(int motifId, KeyValuePair<int, Duration> initialPosition, KeyValuePair<int, Duration> finalPosition)
        {
            this.removeMotif(initialPosition.Key, initialPosition.Value);
            this.insertMotif(motifId, finalPosition.Key, finalPosition.Value);
        }
        public void addTrack()
        {
            this.tune.addTrack();
        }
        public void addTrack(int index, Track track)
        {
            this.tune.addTrack(index, track);
        }
        public void deleteTrack(int track)
        {
            this.tune.deleteTrack(track);
        }
        public void renameMotif(int motifId, string newName)
        {
            this.treeview_renameMotif(this.getMotif(motifId).getName(), newName);
            this.getMotif(motifId).setName(newName);
        }
        public void setTempo(int tempo)
        {
            this.tune.setTempo(tempo);
            this.mainWindow.setTempo(tempo);
        }
        public List<Motif> splitMotif(Motif motif)
        {
            List<Motif> motifList = motif.split();
            string motifName = motif.getName();

            for (int i = 0; i < motifList.Count; i++)
            {
                string name = this.generateDefaultMotifName(motifName + "_" + (i+1));
                motifList[i].setName(name);
            }
            return motifList;
        }
        public void executeAction(ProgramAction action)
        {
            this.executingAction = true;

            if (actionIndex == MAX_ACTIONS)
            {
                actionList.RemoveAt(0);
                actionIndex--;
            }
            while (actionIndex < actionList.Count())
            {
                actionList.RemoveAt(actionList.Count - 1);
            }
            actionList.Add(action);
            action.execute();
            actionIndex++;

            this.mainWindow.setUndoButton(true);
            this.mainWindow.setRedoButton(false);

            this.executingAction = false;
        }
        public void undoAction()
        {
            if (this.actionIndex <= 0)
            {
                mainWindow.setUndoButton(false);
                return;
            }

            this.executingAction = true;

            this.actionIndex--;
            this.actionList.ElementAt(actionIndex).undo();

            this.executingAction = false;

            if (this.actionIndex == 0)
            {
                mainWindow.setUndoButton(false);
            }
            mainWindow.setRedoButton(true);
        }
        public void redoAction()
        {
            if (this.actionIndex >= this.actionList.Count())
            {
                mainWindow.setRedoButton(false);
                return;
            }

            this.executingAction = true;

            this.actionList.ElementAt(actionIndex).execute();
            this.actionIndex++;

            this.executingAction = false;

            if (this.actionIndex >= this.actionList.Count())
            {
                mainWindow.setRedoButton(false);
            }
            mainWindow.setUndoButton(true);
        }
        public void resetActionList()
        {
            while (this.actionList.Count() > 0)
                this.actionList.RemoveAt(0);
            this.actionIndex = 0;
        }
        public bool isExecutingAction()
        {
            return this.executingAction;
        }
        #endregion

        #region MotifTreeView
        private void treeview_addMotif(string motifName, int parentId)
        {
            if (parentId == -1) motifTreeView.addMotif(motifName);
            else if (getMotif(parentId) == null) motifTreeView.addMotif(motifName);
            else motifTreeView.addMotif(motifName, getMotif(parentId).getName());
        }
        private void treeview_deleteMotif(string motifName)
        {
            motifTreeView.deleteMotif(motifName);
        }
        private void treeview_restoreMotif(string motifName, int parentId, Dictionary<int, Variation> childVariationList)
        {
            treeview_addMotif(motifName, parentId);
            List<string> childNames = new List<string>();
            foreach (KeyValuePair<int, Objects.Music.Variations.Variation> child in childVariationList)
            {
                childNames.Add(this.getMotif(child.Key).getName()); 
            }
            motifTreeView.restoreChilds(motifName, childNames);
        }
        public void treeview_selectNode(Motif m)
        {
            this.motifTreeView.SelectedNode = motifTreeView.Nodes.Find(m.getName(), true)[0];
        }
        public void treeview_renameMotif(string oldName, string newName)
        {
            motifTreeView.renameMotif(oldName, newName);
        }
        public void selectMotifOnTreeView(string motifName)
        {
            motifTreeView.selectMotif(motifName);
        }
        private void motifTreeView_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            if (motifTreeView.SelectedNode == null || motifTreeView.SelectedNode.Text == null)
                playlist.selectMotif("");
            else
                playlist.selectMotif(motifTreeView.SelectedNode.Text);
            this.updatePlaylist();
        }
        #endregion

        #region Update
        public void updatePlaylist()
        {
            this.playlist.updateHeader();
            this.playlist.Invalidate();
        }
        public void updateMainWindow(){
            this.mainWindow.setTempo(this.getTempo());
            this.mainWindow.Invalidate();
        }
        #endregion

        public int trackElementCount(int track)
        {
            return this.tune.getTrack(track).motifCount();
        }

        public int getMotifIndexFromTrack(int track, Duration selected)
        {
            if (track < 0 || track >= this.trackCount()) return -1;
            if (this.motifCount(track) == 0) return -1;

            int index = this.tune.getTrack(track).getMotifIndex(selected);
            if (index < 0) return -1;

            //Check if you are actually clicking a Motif
            Duration start = this.tune.getTrack(track).getStartTime(index);
            Duration duration = this.tune.getMotif(this.tune.getTrack(track).getMotifIdentificator(index)).getDuration();

            if (start + duration > selected) return index;
            else return -1;
        }

        public bool fitsMotif(Motif motif, int track, Duration start)
        {
            return fitsMotif(motif, track, start, -1);
        }

        public bool fitsMotif(Motif motif, int track, Duration start, int selectedIndex)
        {
            if (track < 0 || track >= this.trackCount()) return false;

            int indexLeft = this.tune.getTrack(track).getMotifIndex(start);
            int indexRight = indexLeft + 1;

            //Ignore my own Motif id
            if (indexLeft == selectedIndex) indexLeft--;
            if (indexRight == selectedIndex) indexRight++;
            
            //Check if it fits after left Motif
            if (indexLeft >= 0)
            {
                Duration sleft = this.tune.getTrack(track).getStartTime(indexLeft);
                Duration dleft = this.tune.getMotif(this.tune.getTrack(track).getMotifIdentificator(indexLeft)).getDuration();
                if (sleft + dleft > start) return false;                
            }

            //Check if it fits after right Motif
            if (indexRight < this.tune.getTrack(track).motifCount())
            {
                Duration sright = this.tune.getTrack(track).getStartTime(indexRight);
                if (start + motif.getDuration() > sright) return false;
            }

            return true;
        }

        public Motif getMotifFromTrack(int track, int i)
        {
            return this.tune.getMotif(this.tune.getTrack(track).getMotifIdentificator(i));
        }

        public Duration getStartTimeFromTrack(int track, int i)
        {
            return this.tune.getTrack(track).getStartTime(i);
        }

        public Motif getMotif(int id)
        {
            return this.tune.getMotif(id);
        }

        public Track getTrack(int index)
        {
            return this.tune.getTrack(index);
        }

        public int getVolume(int track)
        {
            return this.tune.getTrack(track).getVolume();
        }

        public void setVolume(int volume, int track)
        {
            this.tune.getTrack(track).setVolume(volume);
        }

        public Timbre getTimbre(int track)
        {
            return this.tune.getTrack(track).getTimbre();
        }

        public void setTimbre(Timbre timbre, int track)
        {
            this.tune.getTrack(track).setTimbre(timbre);
        }

        public int trackCount()
        {
            return this.tune.trackCount();
        }

        public int motifCount()
        {
            return this.tune.motifCount();
        }

        public int motifCount(int track)
        {
            return this.tune.getTrack(track).motifCount();
        }

        public string generateDefaultMotifName(string root)
        {
            if (getMotifFromName(root) == null) return root;
            int count = 1;
            while (true)
            {
                string motifName = root + " (" + count + ")";
                if (getMotifFromName(motifName) == null) return motifName;
                count++;
            }
        }

        public Motif getMotifFromName(string motifName)
        {
            for (int i = 0; i < tune.motifCount(); i++)
            {
                Motif m = tune.getMotifFromList(i);
                if (m.getName() == motifName) return m;
            }
            return null;
        }

        public int getTempo()
        {
            return this.tune.getTempo();
        }

        public void Dispose()
        {
            this.mediaPlayer.Dispose();
        }

        public bool isTunePlaying()
        {
            return this.mediaPlayer.isTunePlaying();
        }

        public void play()
        {
            mediaPlayer.Reset();
            AbcFileWriter.saveToFile(this.tune, StringConstants.TEMP_ABC, getTempo(), true);
            saveMidi(StringConstants.TEMP_ABC, StringConstants.TEMP_MIDI);
            mediaPlayer.SetTuneURL(StringConstants.TEMP_MIDI);
            this.playlist.setTempo(this.getTempo());
            mediaPlayer.PlayTune(this.playlist.getCurrentPlayerTime());
        }

        public void stop()
        {
            mediaPlayer.StopTune();
        }

        public void playMotif(Motif motif)
        {
            mediaPlayer.Reset();
            AbcFileWriter.saveToFile(motif, StringConstants.TEMP_ABC, true);
            saveMidi(StringConstants.TEMP_ABC, StringConstants.TEMP_MIDI);
            mediaPlayer.SetMotifURL(StringConstants.TEMP_MIDI);
            mediaPlayer.PlayMotif();
        }

        public void stopMotif()
        {
            mediaPlayer.StopMotif();
        }

        public void saveABC(string abcpath)
        {
            AbcFileWriter.saveToFile(this.tune, abcpath, getTempo(), false);
        }

        public void saveABC(Motif m, string abcpath)
        {
            AbcFileWriter.saveToFile(m, abcpath, false);
        }

        public void saveMidi(string midipath)
        {
            AbcFileWriter.saveToFile(this.tune, StringConstants.TEMP_ABC, getTempo(), true);
            saveMidi(StringConstants.TEMP_ABC, midipath);
        }

        private void saveMidi(string abcpath, string midipath)
        {
            string a = ConsoleParser.ExecuteCommand(StringConstants.ABC2MIDI, "\"" + abcpath + "\" -o \"" + midipath + "\"");
        }

        public void savePDF(string pdfPath)
        {
            AbcFileWriter.saveToFile(this.tune, StringConstants.TEMP_ABC, getTempo(), false);
            ConsoleParser.ExecuteCommand("\"" + StringConstants.ABCM2PS + "\"", "-c -g \"" + StringConstants.TEMP_ABC + "\" -E -O \"" + StringConstants.TEMP_EPS_WRITE + "\"");
            ConsoleParser.ExecuteCommand("\"" + StringConstants.EPSTOPDF + "\"", "--outfile=\"" + pdfPath + "\" \"" + StringConstants.TEMP_EPS_READ + "\"");
        }

        public void newProject()
        {
            this.resetActionList();
            this.motifTreeView.Nodes.Clear();
            this.tune = new Tune();
            this.updatePlaylist();
            this.updateMainWindow();
            this.mainWindow.setUndoButton(false);
            this.mainWindow.setRedoButton(false);
        }

        public void saveProject(string filename)
        {
            this.projectUtil.save(filename, this.tune, this.motifTreeView);
        }

        public void loadProject(string filename)
        {
            this.projectUtil.load(filename, out this.tune, this.motifTreeView);
            this.updatePlaylist();
            this.resetActionList();
            this.tune.recalculateNextId();
            this.updatePlaylist();
            this.updateMainWindow();
            this.mainWindow.setUndoButton(false);
            this.mainWindow.setRedoButton(false);
        }
    }
}
