using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MusicaMinimalista.Objects;
using MusicaMinimalista.Objects.Music;
using MusicaMinimalista.Objects.Music.Variations;
using MusicaMinimalista.Objects.Actions;
using MusicaMinimalista.Objects.Utils;

namespace MusicaMinimalista
{
    public partial class MainWindow : Form
    {

        private Controller controller;
        private bool isUpdatingTempo;

        public MainWindow()
        {
            InitializeComponent();
            this.initializeMediaPlayers();
            controller = new Controller(new Tune(), wmpTune, wmpMotif, motifTreeView, playlist, this);
            tsbTempo.Text = Convert.ToString(controller.getTempo());
            playlist.setController(controller);
            this.updateComponentSizes();
            createFolders();
            projectFilePath = "";
            isUpdatingTempo = false;
            this.controller.updatePlaylist();
        }

        private void initializeMediaPlayers()
        {
            wmpTune.uiMode = "invisible";
            wmpTune.Location = new System.Drawing.Point(-5, -5);
            wmpTune.Size = new System.Drawing.Size(2, 2);
            wmpMotif.uiMode = "invisible";
            wmpMotif.Location = new System.Drawing.Point(-5, -5);
            wmpMotif.Size = new System.Drawing.Size(2, 2);
            wmpTune.PlayStateChange += wmpTune_PlayStateChange;
        }

        private void createFolders()
        {
            if (!System.IO.Directory.Exists(StringConstants.ABC_DIR))
                System.IO.Directory.CreateDirectory(StringConstants.ABC_DIR);
            if (!System.IO.Directory.Exists(StringConstants.MIDI_DIR))
                System.IO.Directory.CreateDirectory(StringConstants.MIDI_DIR);
            if (!System.IO.Directory.Exists(StringConstants.TEMP_DIR))
                System.IO.Directory.CreateDirectory(StringConstants.TEMP_DIR);
            if (!System.IO.Directory.Exists(StringConstants.PDF_DIR))
                System.IO.Directory.CreateDirectory(StringConstants.PDF_DIR);
            if (!System.IO.Directory.Exists(StringConstants.PROJECT_DIR))
                System.IO.Directory.CreateDirectory(StringConstants.PROJECT_DIR);
        }

        #region MainWindow Object Updaters
        public void setUndoButton(bool enabled)
        {
            mnUndo.Enabled = enabled;
        }
        public void setRedoButton(bool enabled)
        {
            mnRedo.Enabled = enabled;
        }
        public void setTempo(int tempo)
        {
            isUpdatingTempo = true;
            this.tsbTempo.Text = Convert.ToString(tempo);
            isUpdatingTempo = false;
        }
        #endregion

        #region Menu Strip
        private string projectFilePath;
        private void mnNewProject_Click(object sender, EventArgs e)
        {
            projectFilePath = "";
            mnSaveProject.Enabled = false;
            controller.newProject();
        }
        private void mnOpenProject_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = StringConstants.PROJECT_DIR;
            ofd.Filter = "Minimalist Music Project file (*.mm)|*.mm|All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() != DialogResult.OK) return;

            projectFilePath = ofd.FileName;
            mnSaveProject.Enabled = true;

            controller.loadProject(projectFilePath);
        }
        private void mnSaveProject_Click(object sender, EventArgs e)
        {
            controller.saveProject(projectFilePath);
        }
        private void mnSaveProjectAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = StringConstants.PROJECT_DIR;
            sfd.Filter = "Minimalist Music Project file (*.mm)|*.mm|All files (*.*)|*.*";
            sfd.FilterIndex = 1;
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() != DialogResult.OK) return;

            projectFilePath = sfd.FileName;
            mnSaveProject.Enabled = true;

            controller.saveProject(projectFilePath);
        }
        private void mnExportMidi_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = StringConstants.MIDI_DIR;
            sfd.Filter = "MIDI file (*.midi)|*.midi|All files (*.*)|*.*";
            sfd.FilterIndex = 1;
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() != DialogResult.OK) return;

            controller.saveMidi(sfd.FileName);
        }
        private void mnExportABC_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = StringConstants.ABC_DIR;
            sfd.Filter = "ABC file (*.abc)|*.abc|All files (*.*)|*.*";
            sfd.FilterIndex = 1;
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() != DialogResult.OK) return;

            controller.saveABC(sfd.FileName);
        }
        private void mnExportMusicSheet_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = StringConstants.PDF_DIR;
            sfd.Filter = "PDF file (*.pdf)|*.pdf|All files (*.*)|*.*";
            sfd.FilterIndex = 1;
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() != DialogResult.OK) return;

            controller.savePDF(sfd.FileName);
        }
        private void mnNewMotif_Click(object sender, EventArgs e)
        {
            Forms.EditMotifForm emf = new Forms.EditMotifForm(new Motif(), this.controller);
            if (emf.ShowDialog() == DialogResult.OK)
            {
                Motif m2 = emf.editedMotif;
                string name = controller.generateDefaultMotifName("motif");
                m2.setName(name);

                ProgramAction action = new CreateMotifAction(this.controller, m2);
                this.controller.executeAction(action);

                controller.treeview_selectNode(m2);
            }
        }
        private void mnLoadMotif_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = StringConstants.ABC_DIR;
            ofd.Filter = "ABC file (*.abc)|*.abc|All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;

            if(ofd.ShowDialog() != DialogResult.OK) return;

            string name = controller.generateDefaultMotifName(System.IO.Path.GetFileNameWithoutExtension(ofd.FileName));
            Motif m = AbcFileReader.readFromFile(ofd.FileName);
            m.setName(name);

            ProgramAction action = new CreateMotifAction(this.controller, m);
            this.controller.executeAction(action);

            controller.treeview_selectNode(m);
        }
        private void mnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void mnUndo_Click(object sender, EventArgs e)
        {
            controller.undoAction();
        }
        private void mnRedo_Click(object sender, EventArgs e)
        {
            controller.redoAction();
        }
        #endregion
        
        #region Tool Strip
        private void tsbAddTrack_Click(object sender, EventArgs e)
        {
            ProgramAction action = new AddTrackAction(this.controller, this.controller.trackCount());
            this.controller.executeAction(action);
        }

        private void tsbZoomIn_Click(object sender, EventArgs e)
        {
            this.playlist.zoomIn();
        }

        private void tsbZoomOut_Click(object sender, EventArgs e)
        {
            this.playlist.zoomOut();
        }

        private void tsbPlay_Click(object sender, EventArgs e)
        {
            controller.play();
        }

        private void tsbStop_Click(object sender, EventArgs e)
        {
            controller.stop();
        }

        private void tsbTempo_TextChanged(object sender, EventArgs e)
        {
            if (isUpdatingTempo) return;

            int tempo = 0;
            try
            {
                tempo = Int32.Parse(tsbTempo.Text);
            }
            catch (Exception)
            {
                return;
            }
            if (tempo <= 0) return;
            
            ProgramAction action = new ChangeTempoAction(this.controller, this.controller.getTempo(), tempo);
            this.controller.executeAction(action);
        }
        #endregion

        #region Motif Tree Context Menu
        private void mcmPlay_Click(object sender, EventArgs e)
        {
            Motif m = controller.getMotifFromName(this.motifTreeView.SelectedNode.Text);
            this.controller.playMotif(m);
        }

        private void mcmRename_Click(object sender, EventArgs e)
        {
            this.motifTreeView.LabelEdit = true;
            if (!this.motifTreeView.SelectedNode.IsEditing)
            {
                this.motifTreeView.SelectedNode.BeginEdit();
            }
        }

        private void mcmEditMotif_Click(object sender, EventArgs e)
        {
            Motif m = controller.getMotifFromName(this.motifTreeView.SelectedNode.Text);
            Forms.EditMotifForm emf = new Forms.EditMotifForm(m, this.controller);
            if (emf.ShowDialog() == DialogResult.OK)
            {
                Motif m2 = emf.editedMotif;
                m2.setVariation(new NullVariation(), m.getId());
                string name = controller.generateDefaultMotifName(m.getName() + "_edited");
                m2.setName(name);

                ProgramAction action = new CreateMotifAction(this.controller, m2);
                this.controller.executeAction(action);

                controller.treeview_selectNode(m2);
            }
        }

        private void mcmTransport_Click(object sender, EventArgs e)
        {
            Motif m = controller.getMotifFromName(this.motifTreeView.SelectedNode.Text);
            Forms.TransportVariationForm tvf = new Forms.TransportVariationForm();
            if (tvf.ShowDialog() == DialogResult.OK)
            {
                TransportVariation transportVariation = new TransportVariation(tvf.transport);
                Motif m2 = transportVariation.variate(m);
                m2.setVariation(transportVariation, m.getId());
                string name = controller.generateDefaultMotifName(m.getName() + "_transport");
                m2.setName(name);

                ProgramAction action = new CreateMotifAction(this.controller, m2);
                this.controller.executeAction(action);

                controller.treeview_selectNode(m2);
            }
        }

        private void mcmDelay_Click(object sender, EventArgs e)
        {
            Motif m = controller.getMotifFromName(this.motifTreeView.SelectedNode.Text);
            Forms.DelayVariationForm dvf = new Forms.DelayVariationForm();
            if (dvf.ShowDialog() == DialogResult.OK)
            {
                DelayVariation transportVariation = new DelayVariation(dvf.delay);
                Motif m2 = transportVariation.variate(m);
                m2.setVariation(transportVariation, m.getId());
                string name = controller.generateDefaultMotifName(m.getName() + "_delay");
                m2.setName(name);

                ProgramAction action = new CreateMotifAction(this.controller, m2);
                this.controller.executeAction(action);

                controller.treeview_selectNode(m2);
            }
        }

        private void mcmChangeNoteDuration_Click(object sender, EventArgs e)
        {
            Motif m = controller.getMotifFromName(this.motifTreeView.SelectedNode.Text);
            Forms.NoteValueVariationForm nvv = new Forms.NoteValueVariationForm();
            if (nvv.ShowDialog() == DialogResult.OK)
            {
                NoteValueVariation nvVariation = new NoteValueVariation(nvv.multiplier);
                Motif m2 = nvVariation.variate(m);
                m2.setVariation(nvVariation, m.getId());
                string name = controller.generateDefaultMotifName(m.getName() + "_changeNoteDuration");
                m2.setName(name);

                ProgramAction action = new CreateMotifAction(this.controller, m2);
                this.controller.executeAction(action);

                controller.treeview_selectNode(m2);
            }
        }

        private void mcmRetrogradation_Click(object sender, EventArgs e)
        {
            Motif m = controller.getMotifFromName(this.motifTreeView.SelectedNode.Text);

            RetrogradeVariation retrogradeVariation = new RetrogradeVariation();
            Motif m2 = retrogradeVariation.variate(m);
            m2.setVariation(retrogradeVariation, m.getId());
            string name = controller.generateDefaultMotifName(m.getName() + "_retrograde");
            m2.setName(name);
            ProgramAction action = new CreateMotifAction(this.controller, m2);
            this.controller.executeAction(action);

            controller.treeview_selectNode(m2);
        }

        private void mcmInversion_Click(object sender, EventArgs e)
        {
            Motif m = controller.getMotifFromName(this.motifTreeView.SelectedNode.Text);

            InversionVariation inversionVariation = new InversionVariation();
            Motif m2 = inversionVariation.variate(m);
            m2.setVariation(inversionVariation, m.getId());
            string name = controller.generateDefaultMotifName(m.getName() + "_inversion");
            m2.setName(name);
            ProgramAction action = new CreateMotifAction(this.controller, m2);
            this.controller.executeAction(action);

            controller.treeview_selectNode(m2);
        }

        private void mcmRachmaninoffInversion_Click(object sender, EventArgs e)
        {
            Motif m = controller.getMotifFromName(this.motifTreeView.SelectedNode.Text);

            RachmaninoffInversionVariation rachmaninoffInversionVariation = new RachmaninoffInversionVariation();
            Motif m2 = rachmaninoffInversionVariation.variate(m);
            m2.setVariation(rachmaninoffInversionVariation, m.getId());
            string name = controller.generateDefaultMotifName(m.getName() + "_inversion_Rachmaninoff");
            m2.setName(name);
            ProgramAction action = new CreateMotifAction(this.controller, m2);
            this.controller.executeAction(action);

            controller.treeview_selectNode(m2);
        }

        private void mcmCanonizate_Click(object sender, EventArgs e)
        {
            Motif m = controller.getMotifFromName(this.motifTreeView.SelectedNode.Text);

            CanonizationVariation canonizationVariation = new CanonizationVariation();
            Motif m2 = canonizationVariation.variate(m);
            m2.setVariation(canonizationVariation, m.getId());
            string name = controller.generateDefaultMotifName(m.getName() + "_canonizate");
            m2.setName(name);
            ProgramAction action = new CreateMotifAction(this.controller, m2);
            this.controller.executeAction(action);
        }

        private void mcmModulate_Click(object sender, EventArgs e)
        {
            Motif m = controller.getMotifFromName(this.motifTreeView.SelectedNode.Text);
            Forms.ModulationVariationForm mvf = new Forms.ModulationVariationForm();
            if (mvf.ShowDialog() == DialogResult.OK)
            {
                ModulationVariation modulationVariation = new ModulationVariation(mvf.newTonality);
                Motif m2 = modulationVariation.variate(m);
                m2.setVariation(modulationVariation, m.getId());
                string name = controller.generateDefaultMotifName(m.getName() + "_modulation");
                m2.setName(name);

                ProgramAction action = new CreateMotifAction(this.controller, m2);
                this.controller.executeAction(action);

                controller.treeview_selectNode(m2);
            }
        }

        private void mcmPermutate_Click(object sender, EventArgs e)
        {
            Motif m = controller.getMotifFromName(this.motifTreeView.SelectedNode.Text);

            PermutationVariation permutationVariation = new PermutationVariation();
            Motif m2 = permutationVariation.variate(m);
            m2.setVariation(permutationVariation, m.getId());
            string name = controller.generateDefaultMotifName(m.getName() + "_permutate");
            m2.setName(name);
            ProgramAction action = new CreateMotifAction(this.controller, m2);
            this.controller.executeAction(action);
        }

        private void mcmTonalTransport_Click(object sender, EventArgs e)
        {

            Motif m = controller.getMotifFromName(this.motifTreeView.SelectedNode.Text);
            Forms.TonalTransportVariationForm ttvf = new Forms.TonalTransportVariationForm();
            if (ttvf.ShowDialog() == DialogResult.OK)
            {
                TonalTransportVariation tonalTransport = new TonalTransportVariation(ttvf.orig, ttvf.dest);
                Motif m2 = tonalTransport.variate(m);
                m2.setVariation(tonalTransport, m.getId());
                string name = controller.generateDefaultMotifName(m.getName() + "_tonaltransp");
                m2.setName(name);

                ProgramAction action = new CreateMotifAction(this.controller, m2);
                this.controller.executeAction(action);

                controller.treeview_selectNode(m2);
            }
        }

        private void mcmHarmonizate_Click(object sender, EventArgs e)
        {
            Motif m = controller.getMotifFromName(this.motifTreeView.SelectedNode.Text);

            HarmonizationVariation harmonizationVariation = new HarmonizationVariation();
            Motif m2 = harmonizationVariation.variate(m);
            m2.setVariation(harmonizationVariation, m.getId());
            string name = controller.generateDefaultMotifName(m.getName() + "_harmonizate");
            m2.setName(name);
            ProgramAction action = new CreateMotifAction(this.controller, m2);
            this.controller.executeAction(action);
        }

        private void mcmElision_Click(object sender, EventArgs e)
        {
            Motif m = controller.getMotifFromName(this.motifTreeView.SelectedNode.Text);

            ElisionVariation elisionVariation = new ElisionVariation();
            Motif m2 = elisionVariation.variate(m);
            m2.setVariation(elisionVariation, m.getId());
            string name = controller.generateDefaultMotifName(m.getName() + "_elision");
            m2.setName(name);
            ProgramAction action = new CreateMotifAction(this.controller, m2);
            this.controller.executeAction(action);
        }

        private void mcmInterpolation_Click(object sender, EventArgs e)
        {
            Motif m = controller.getMotifFromName(this.motifTreeView.SelectedNode.Text);

            InterpolationVariation interpolationVariation = new InterpolationVariation();
            Motif m2 = interpolationVariation.variate(m);
            m2.setVariation(interpolationVariation, m.getId());
            string name = controller.generateDefaultMotifName(m.getName() + "_interpolate");
            m2.setName(name);
            ProgramAction action = new CreateMotifAction(this.controller, m2);
            this.controller.executeAction(action);
        }

        private void mcmOrnamentate_Click(object sender, EventArgs e)
        {
            Motif m = controller.getMotifFromName(this.motifTreeView.SelectedNode.Text);

            OrnamentationVariation ornamentationVariation = new OrnamentationVariation(-1);
            Motif m2 = ornamentationVariation.variate(m);
            m2.setVariation(ornamentationVariation, m.getId());
            string name = controller.generateDefaultMotifName(m.getName() + "_ornament");
            m2.setName(name);
            ProgramAction action = new CreateMotifAction(this.controller, m2);
            this.controller.executeAction(action);
        }

        private void mcmSplit_Click(object sender, EventArgs e)
        {
            Motif m = controller.getMotifFromName(this.motifTreeView.SelectedNode.Text);

            ProgramAction action = new SplitMotifOnTreeViewAction(controller, m);
            this.controller.executeAction(action);
        }

        private void mcmDuplicate_Click(object sender, EventArgs e)
        {
            Motif m = controller.getMotifFromName(this.motifTreeView.SelectedNode.Text);
            Motif clonedMotif = m.Clone();
            string name = controller.generateDefaultMotifName(m.getName() + "_copy");
            clonedMotif.setName(name);

            ProgramAction action = new CreateMotifAction(controller, clonedMotif);
            this.controller.executeAction(action);
        }

        private void mcmExport_Click(object sender, EventArgs e)
        {
            Motif m = controller.getMotifFromName(this.motifTreeView.SelectedNode.Text);

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = StringConstants.ABC_DIR;
            sfd.Filter = "ABC file (*.abc)|*.abc|All files (*.*)|*.*";
            sfd.FilterIndex = 1;
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() != DialogResult.OK) return;

            controller.saveABC(m, sfd.FileName);
        }

        private void mcmDelete_Click(object sender, EventArgs e)
        {
            TreeNode node = this.motifTreeView.SelectedNode;
            DialogResult result = MessageBox.Show(
                "¿Esta seguro que quiere eliminar el motivo "
                + node.Text + "?",
                "Eliminar motivo",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                Motif motif = controller.getMotifFromName(this.motifTreeView.SelectedNode.Text);
                ProgramAction action = new DeleteMotifAction(this.controller, motif);
                this.controller.executeAction(action);
            }
        }
        #endregion

        #region Playlist Context Menu
        private void pcmCopy_Click(object sender, EventArgs e)
        {
            Motif motif = this.playlist.getActiveMotif();
            this.selectedCopyMotifId = motif.getId();
        }
        private void pcmPaste_Click(object sender, EventArgs e)
        {
            int track = selectedPastePosition.Key;
            Duration startTime = selectedPastePosition.Value;
            ProgramAction action = new InsertMotifAction(this.controller, this.selectedCopyMotifId, track, startTime);
            this.controller.executeAction(action);
        }
        private void pcmUntie_Click(object sender, EventArgs e)
        {
            int motifId = this.playlist.getActiveMotif().getId();

            KeyValuePair<int, Duration> activePosition = this.playlist.getActiveMotifPosition();
            int track = activePosition.Key;
            Duration startTime = activePosition.Value;

            Motif originalMotif = controller.getMotif(motifId);
            Motif clonedMotif = originalMotif.Clone();
            string name = controller.generateDefaultMotifName(originalMotif.getName() + "_copy");
            clonedMotif.setName(name);

            ProgramAction action = new UntieMotifAction(this.controller, motifId, clonedMotif, track, startTime);
            this.controller.executeAction(action);
        }
        private void pcmSplit_Click(object sender, EventArgs e)
        {
            int motifId = this.playlist.getActiveMotif().getId();
            KeyValuePair<int, Duration> activePosition = this.playlist.getActiveMotifPosition();
            int track = activePosition.Key;
            Duration startTime = activePosition.Value;


            ProgramAction action = new SplitMotifOnTrackAction(this.controller, motifId, track, startTime);
            this.controller.executeAction(action);
        }
        private void pcmDelete_Click(object sender, EventArgs e)
        {
            int motifId = this.playlist.getActiveMotif().getId();
            KeyValuePair<int, Duration> activePosition = this.playlist.getActiveMotifPosition();
            int track = activePosition.Key;
            Duration startTime = activePosition.Value;
            ProgramAction action = new RemoveMotifAction(this.controller, motifId, track, startTime);
            this.controller.executeAction(action);
        }
        #endregion

        #region Playlist Events
        private void playlist_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }
        private void playlist_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Effect == DragDropEffects.Copy)
            {
                Motif motif = this.controller.getMotifFromName(this.motifTreeView.SelectedNode.Text);
                Point relativePoint = playlist.PointToClient(new Point(e.X, e.Y));
                KeyValuePair<int, Duration> position = this.playlist.getClickPosition(relativePoint.X, relativePoint.Y);
                int track = position.Key;
                Duration startTime = position.Value;
                if (track >= controller.trackCount()) return; //off range
                if (!controller.fitsMotif(motif, track, startTime)) return;
                ProgramAction action = new InsertMotifAction(this.controller, motif.getId(), track, startTime);
                this.controller.executeAction(action);
                mnUndo.Enabled = true;
                mnRedo.Enabled = false;
            }
        }
        private void playlist_MouseDown(object sender, MouseEventArgs e)
        {
            if (!this.controller.isTunePlaying() && this.playlist.headerClicked(e.X, e.Y))
                return;

            this.playlist.pickElement(e.X, e.Y);
            this.motifTreeView.SelectedNode = null;
            this.controller.updatePlaylist();
            if (e.Button != MouseButtons.Left) return;

            playlistMoveMotif = this.playlist.pickElement(e.X, e.Y);
            if (playlistMoveMotif)
            {
                this.initialPosition = this.playlist.getActiveMotifPosition();
                this.finalPosition = this.playlist.getActiveMotifPosition();
            }
        }
        private void playlist_MouseMove(object sender, MouseEventArgs e)
        {
            if (playlistMoveMotif)
            {
                this.playlist.moveElement(e.X, e.Y);
                this.finalPosition = this.playlist.getActiveMotifPosition();
            }
        }
        private void playlist_MouseUp(object sender, MouseEventArgs e)
        {
            if (playlistMoveMotif)
            {
                playlistMoveMotif = false;
                if (initialPosition.Key == finalPosition.Key && initialPosition.Value == finalPosition.Value) return;

                int motifId = this.playlist.getActiveMotif().getId();
                this.controller.moveMotif(motifId, finalPosition, initialPosition);
                ProgramAction action = new MoveMotifAction(this.controller, motifId, this.initialPosition, this.finalPosition);
                this.controller.executeAction(action);
                mnUndo.Enabled = true;
                mnRedo.Enabled = false;
            }
            else if (e.Button == MouseButtons.Right)
            {
                Point p = new Point(e.X, e.Y);
                pcmCopy.Enabled = false;
                pcmPaste.Enabled = false;
                pcmSplit.Enabled = false;
                pcmDelete.Enabled = false;
                pcmUntie.Enabled = false;
                selectedPastePosition = this.playlist.getClickPosition(e.X, e.Y);
                if (this.playlist.pickElement(e.X, e.Y))
                {
                    pcmCopy.Enabled = true;
                    pcmSplit.Enabled = true;
                    pcmDelete.Enabled = true;
                    pcmUntie.Enabled = true;
                }
                else if (this.selectedCopyMotifId >= 0)
                {
                    if (this.controller.fitsMotif(
                        this.controller.getMotif(this.selectedCopyMotifId),
                        selectedPastePosition.Key,
                        selectedPastePosition.Value)
                        )
                    {
                        pcmPaste.Enabled = true;
                    }
                }
                playlistContextMenu.Show(playlist, p);
            }
        }
        private void playlist_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0) playlist.zoomOut();
            else playlist.zoomIn();
        }
        bool playlistMoveMotif = false;
        int selectedCopyMotifId = -1;
        KeyValuePair<int, Duration> initialPosition;
        KeyValuePair<int, Duration> finalPosition;
        KeyValuePair<int, Duration> selectedPastePosition;
        #endregion

        #region Motif Tree Events
        private void motifTreeView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            
            Point p = new Point(e.X, e.Y);
            TreeNode node = motifTreeView.GetNodeAt(p);
            if (node == null) return;
            motifTreeView.SelectedNode = node;

            treeviewContextMenu.Show(motifTreeView, p);
        }
        private void motifTreeView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            Point p = new Point(e.X, e.Y);
            TreeNode node = motifTreeView.GetNodeAt(p);
            if (node == null) return;
            motifTreeView.SelectedNode = node;

            motifTreeView.DoDragDrop(node, DragDropEffects.Copy);
        }
        private void motifTreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            e.CancelEdit = true; //RenameMotifAction will edit the name later
            this.motifTreeView.LabelEdit = false;

            string newtext = e.Label;
            string oldtext = e.Node.Text;

            if (newtext == null || newtext == "") return;

            //Eliminate all spaces at the beginning of the newtext
            while (newtext[0] == ' ')
            {
                newtext = newtext.Substring(1);
            }
              
            //If no changes in name or new name is empty, cancel edit
            if (newtext == oldtext || newtext == "") return;
            

            if (controller.getMotifFromName(newtext) != null)
            {
                MessageBox.Show(
                    "Ya existe un motivo con el nombre " + e.Label + ".",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            Motif motif = controller.getMotifFromName(e.Node.Text);
            ProgramAction action = new RenameMotifAction(this.controller, motif, e.Label);
            this.controller.executeAction(action);
            mnUndo.Enabled = true;
            mnRedo.Enabled = false;
        }
        #endregion

        #region Main Window Events
        private void MainWindow_SizeChanged(object sender, EventArgs e)
        {
            this.updateComponentSizes();
        }
        private void updateComponentSizes()
        {
            this.mainPanel.Width = this.Width - 290;
            this.motifTreeView.Height = this.Height - 91;
            this.playlist.OnResizePanel(this.playListPanel.Width, this.playListPanel.Height);
        }
        #endregion      

        #region Windows Media Player Events
        void wmpTune_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (e.newState == (int)WMPLib.WMPPlayState.wmppsPlaying)
            {
                this.tsbPlay.Image = global::MusicaMinimalista.Properties.Resources.pause_icon;
                this.tsbStop.Enabled = true;
                this.tsbPlay.Enabled = false;
            }
            else
            {
                this.tsbPlay.Image = global::MusicaMinimalista.Properties.Resources.play_icon;
                this.tsbStop.Enabled = false;
                this.tsbPlay.Enabled = true;
            }
        }
        #endregion       

    }
}
