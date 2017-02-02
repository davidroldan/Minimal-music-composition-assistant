namespace MusicaMinimalista
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            this.controller.Dispose();
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.mainPanel = new System.Windows.Forms.Panel();
            this.playListPanel = new System.Windows.Forms.Panel();
            this.playlist = new MusicaMinimalista.Controls.PlayList();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tsbAddTrack = new System.Windows.Forms.ToolStripButton();
            this.tsbZoomIn = new System.Windows.Forms.ToolStripButton();
            this.tsbZoomOut = new System.Windows.Forms.ToolStripButton();
            this.tsbPlay = new System.Windows.Forms.ToolStripButton();
            this.tsbStop = new System.Windows.Forms.ToolStripButton();
            this.tsbTempo = new System.Windows.Forms.ToolStripTextBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnNewProject = new System.Windows.Forms.ToolStripMenuItem();
            this.mnOpenProject = new System.Windows.Forms.ToolStripMenuItem();
            this.mnSaveProject = new System.Windows.Forms.ToolStripMenuItem();
            this.mnSaveProjectAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnNewMotif = new System.Windows.Forms.ToolStripMenuItem();
            this.mnLoadMotif = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnExportMidi = new System.Windows.Forms.ToolStripMenuItem();
            this.mnExportABC = new System.Windows.Forms.ToolStripMenuItem();
            this.mnExportMusicSheet = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnExit = new System.Windows.Forms.ToolStripMenuItem();
            this.editarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.treeviewImageList = new System.Windows.Forms.ImageList(this.components);
            this.treeviewContextMenu = new System.Windows.Forms.ContextMenu();
            this.mcmPlay = new System.Windows.Forms.MenuItem();
            this.mcmRename = new System.Windows.Forms.MenuItem();
            this.mcmEditMotif = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.mcmTransport = new System.Windows.Forms.MenuItem();
            this.mcmDelay = new System.Windows.Forms.MenuItem();
            this.mcmChangeNoteDuration = new System.Windows.Forms.MenuItem();
            this.mcmRetrogradation = new System.Windows.Forms.MenuItem();
            this.mcmInversion = new System.Windows.Forms.MenuItem();
            this.mcmRachmaninoffInversion = new System.Windows.Forms.MenuItem();
            this.mcmTonalTransport = new System.Windows.Forms.MenuItem();
            this.mcmModulate = new System.Windows.Forms.MenuItem();
            this.mcmPermutate = new System.Windows.Forms.MenuItem();
            this.mcmInterpolation = new System.Windows.Forms.MenuItem();
            this.mcmElision = new System.Windows.Forms.MenuItem();
            this.mcmOrnamentate = new System.Windows.Forms.MenuItem();
            this.mcmCanonizate = new System.Windows.Forms.MenuItem();
            this.mcmHarmonizate = new System.Windows.Forms.MenuItem();
            this.mcmSplit = new System.Windows.Forms.MenuItem();
            this.mcmDuplicate = new System.Windows.Forms.MenuItem();
            this.mcmExport = new System.Windows.Forms.MenuItem();
            this.mcmDelete = new System.Windows.Forms.MenuItem();
            this.playlistContextMenu = new System.Windows.Forms.ContextMenu();
            this.pcmCopy = new System.Windows.Forms.MenuItem();
            this.pcmPaste = new System.Windows.Forms.MenuItem();
            this.pcmUntie = new System.Windows.Forms.MenuItem();
            this.pcmSplit = new System.Windows.Forms.MenuItem();
            this.pcmDelete = new System.Windows.Forms.MenuItem();
            this.wmpMotif = new AxWMPLib.AxWindowsMediaPlayer();
            this.wmpTune = new AxWMPLib.AxWindowsMediaPlayer();
            this.motifTreeView = new MusicaMinimalista.Controls.MotifTreeView();
            this.mainPanel.SuspendLayout();
            this.playListPanel.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wmpMotif)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wmpTune)).BeginInit();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.playListPanel);
            this.mainPanel.Controls.Add(this.toolStrip);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.mainPanel.Location = new System.Drawing.Point(274, 24);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(510, 439);
            this.mainPanel.TabIndex = 0;
            // 
            // playListPanel
            // 
            this.playListPanel.AutoScroll = true;
            this.playListPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.playListPanel.Controls.Add(this.playlist);
            this.playListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.playListPanel.Location = new System.Drawing.Point(0, 25);
            this.playListPanel.Name = "playListPanel";
            this.playListPanel.Size = new System.Drawing.Size(510, 414);
            this.playListPanel.TabIndex = 3;
            // 
            // playlist
            // 
            this.playlist.AllowDrop = true;
            this.playlist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(30)))));
            this.playlist.Location = new System.Drawing.Point(0, 0);
            this.playlist.Name = "playlist";
            this.playlist.Size = new System.Drawing.Size(2400, 428);
            this.playlist.TabIndex = 0;
            this.playlist.DragDrop += new System.Windows.Forms.DragEventHandler(this.playlist_DragDrop);
            this.playlist.DragEnter += new System.Windows.Forms.DragEventHandler(this.playlist_DragEnter);
            this.playlist.MouseDown += new System.Windows.Forms.MouseEventHandler(this.playlist_MouseDown);
            this.playlist.MouseMove += new System.Windows.Forms.MouseEventHandler(this.playlist_MouseMove);
            this.playlist.MouseUp += new System.Windows.Forms.MouseEventHandler(this.playlist_MouseUp);
            this.playlist.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.playlist_MouseWheel);
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAddTrack,
            this.tsbZoomIn,
            this.tsbZoomOut,
            this.tsbPlay,
            this.tsbStop,
            this.tsbTempo});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(510, 25);
            this.toolStrip.TabIndex = 2;
            this.toolStrip.Text = "toolStrip1";
            // 
            // tsbAddTrack
            // 
            this.tsbAddTrack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddTrack.Image = global::MusicaMinimalista.Properties.Resources.add_icon;
            this.tsbAddTrack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddTrack.Name = "tsbAddTrack";
            this.tsbAddTrack.Size = new System.Drawing.Size(23, 22);
            this.tsbAddTrack.Text = "Añadir track";
            this.tsbAddTrack.Click += new System.EventHandler(this.tsbAddTrack_Click);
            // 
            // tsbZoomIn
            // 
            this.tsbZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbZoomIn.Image = global::MusicaMinimalista.Properties.Resources.zoom_in_icon;
            this.tsbZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbZoomIn.Name = "tsbZoomIn";
            this.tsbZoomIn.Size = new System.Drawing.Size(23, 22);
            this.tsbZoomIn.Text = "Aumentar zoom";
            this.tsbZoomIn.Click += new System.EventHandler(this.tsbZoomIn_Click);
            // 
            // tsbZoomOut
            // 
            this.tsbZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbZoomOut.Image = global::MusicaMinimalista.Properties.Resources.zoom_out_icon;
            this.tsbZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbZoomOut.Name = "tsbZoomOut";
            this.tsbZoomOut.Size = new System.Drawing.Size(23, 22);
            this.tsbZoomOut.Text = "Disminuir zoom";
            this.tsbZoomOut.Click += new System.EventHandler(this.tsbZoomOut_Click);
            // 
            // tsbPlay
            // 
            this.tsbPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPlay.Image = global::MusicaMinimalista.Properties.Resources.play_icon;
            this.tsbPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPlay.Name = "tsbPlay";
            this.tsbPlay.Size = new System.Drawing.Size(23, 22);
            this.tsbPlay.Text = "Reproducir";
            this.tsbPlay.Click += new System.EventHandler(this.tsbPlay_Click);
            // 
            // tsbStop
            // 
            this.tsbStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbStop.Enabled = false;
            this.tsbStop.Image = global::MusicaMinimalista.Properties.Resources.stop_icon;
            this.tsbStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbStop.Name = "tsbStop";
            this.tsbStop.Size = new System.Drawing.Size(23, 22);
            this.tsbStop.Text = "Parar";
            this.tsbStop.Click += new System.EventHandler(this.tsbStop_Click);
            // 
            // tsbTempo
            // 
            this.tsbTempo.BackColor = System.Drawing.Color.Black;
            this.tsbTempo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tsbTempo.Font = new System.Drawing.Font("Courier New", 11F, System.Drawing.FontStyle.Bold);
            this.tsbTempo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.tsbTempo.Name = "tsbTempo";
            this.tsbTempo.Size = new System.Drawing.Size(35, 25);
            this.tsbTempo.Text = "120";
            this.tsbTempo.TextChanged += new System.EventHandler(this.tsbTempo_TextChanged);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.editarToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(784, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnNewProject,
            this.mnOpenProject,
            this.mnSaveProject,
            this.mnSaveProjectAs,
            this.toolStripSeparator2,
            this.mnNewMotif,
            this.mnLoadMotif,
            this.toolStripSeparator1,
            this.mnExportMidi,
            this.mnExportABC,
            this.mnExportMusicSheet,
            this.toolStripSeparator3,
            this.mnExit});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // mnNewProject
            // 
            this.mnNewProject.Name = "mnNewProject";
            this.mnNewProject.Size = new System.Drawing.Size(209, 22);
            this.mnNewProject.Text = "Nuevo proyecto";
            this.mnNewProject.Click += new System.EventHandler(this.mnNewProject_Click);
            // 
            // mnOpenProject
            // 
            this.mnOpenProject.Name = "mnOpenProject";
            this.mnOpenProject.Size = new System.Drawing.Size(209, 22);
            this.mnOpenProject.Text = "Abrir proyecto...";
            this.mnOpenProject.Click += new System.EventHandler(this.mnOpenProject_Click);
            // 
            // mnSaveProject
            // 
            this.mnSaveProject.Enabled = false;
            this.mnSaveProject.Name = "mnSaveProject";
            this.mnSaveProject.Size = new System.Drawing.Size(209, 22);
            this.mnSaveProject.Text = "Guardar proyecto";
            this.mnSaveProject.Click += new System.EventHandler(this.mnSaveProject_Click);
            // 
            // mnSaveProjectAs
            // 
            this.mnSaveProjectAs.Name = "mnSaveProjectAs";
            this.mnSaveProjectAs.Size = new System.Drawing.Size(209, 22);
            this.mnSaveProjectAs.Text = "Guardar proyecto como...";
            this.mnSaveProjectAs.Click += new System.EventHandler(this.mnSaveProjectAs_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(206, 6);
            // 
            // mnNewMotif
            // 
            this.mnNewMotif.Name = "mnNewMotif";
            this.mnNewMotif.Size = new System.Drawing.Size(209, 22);
            this.mnNewMotif.Text = "Nuevo motivo...";
            this.mnNewMotif.Click += new System.EventHandler(this.mnNewMotif_Click);
            // 
            // mnLoadMotif
            // 
            this.mnLoadMotif.Name = "mnLoadMotif";
            this.mnLoadMotif.Size = new System.Drawing.Size(209, 22);
            this.mnLoadMotif.Text = "Cargar motivo...";
            this.mnLoadMotif.Click += new System.EventHandler(this.mnLoadMotif_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(206, 6);
            // 
            // mnExportMidi
            // 
            this.mnExportMidi.Name = "mnExportMidi";
            this.mnExportMidi.Size = new System.Drawing.Size(209, 22);
            this.mnExportMidi.Text = "Exportar MIDI...";
            this.mnExportMidi.Click += new System.EventHandler(this.mnExportMidi_Click);
            // 
            // mnExportABC
            // 
            this.mnExportABC.Name = "mnExportABC";
            this.mnExportABC.Size = new System.Drawing.Size(209, 22);
            this.mnExportABC.Text = "Exportar ABC...";
            this.mnExportABC.Click += new System.EventHandler(this.mnExportABC_Click);
            // 
            // mnExportMusicSheet
            // 
            this.mnExportMusicSheet.Name = "mnExportMusicSheet";
            this.mnExportMusicSheet.Size = new System.Drawing.Size(209, 22);
            this.mnExportMusicSheet.Text = "Exportar partitura...";
            this.mnExportMusicSheet.Click += new System.EventHandler(this.mnExportMusicSheet_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(206, 6);
            // 
            // mnExit
            // 
            this.mnExit.Name = "mnExit";
            this.mnExit.Size = new System.Drawing.Size(209, 22);
            this.mnExit.Text = "Salir";
            this.mnExit.Click += new System.EventHandler(this.mnSalir_Click);
            // 
            // editarToolStripMenuItem
            // 
            this.editarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnUndo,
            this.mnRedo});
            this.editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            this.editarToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.editarToolStripMenuItem.Text = "Editar";
            // 
            // mnUndo
            // 
            this.mnUndo.Enabled = false;
            this.mnUndo.Name = "mnUndo";
            this.mnUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.mnUndo.Size = new System.Drawing.Size(163, 22);
            this.mnUndo.Text = "Deshacer";
            this.mnUndo.Click += new System.EventHandler(this.mnUndo_Click);
            // 
            // mnRedo
            // 
            this.mnRedo.Enabled = false;
            this.mnRedo.Name = "mnRedo";
            this.mnRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.mnRedo.Size = new System.Drawing.Size(163, 22);
            this.mnRedo.Text = "Rehacer";
            this.mnRedo.Click += new System.EventHandler(this.mnRedo_Click);
            // 
            // treeviewImageList
            // 
            this.treeviewImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("treeviewImageList.ImageStream")));
            this.treeviewImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.treeviewImageList.Images.SetKeyName(0, "motif_abc.png");
            // 
            // treeviewContextMenu
            // 
            this.treeviewContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mcmPlay,
            this.mcmRename,
            this.mcmEditMotif,
            this.menuItem2,
            this.mcmSplit,
            this.mcmDuplicate,
            this.mcmExport,
            this.mcmDelete});
            // 
            // mcmPlay
            // 
            this.mcmPlay.Index = 0;
            this.mcmPlay.Text = "Reproducir";
            this.mcmPlay.Click += new System.EventHandler(this.mcmPlay_Click);
            // 
            // mcmRename
            // 
            this.mcmRename.Index = 1;
            this.mcmRename.Text = "Renombrar";
            this.mcmRename.Click += new System.EventHandler(this.mcmRename_Click);
            // 
            // mcmEditMotif
            // 
            this.mcmEditMotif.Index = 2;
            this.mcmEditMotif.Text = "Editar motivo...";
            this.mcmEditMotif.Click += new System.EventHandler(this.mcmEditMotif_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 3;
            this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mcmTransport,
            this.mcmDelay,
            this.mcmChangeNoteDuration,
            this.mcmRetrogradation,
            this.mcmInversion,
            this.mcmRachmaninoffInversion,
            this.mcmTonalTransport,
            this.mcmModulate,
            this.mcmPermutate,
            this.mcmInterpolation,
            this.mcmElision,
            this.mcmOrnamentate,
            this.mcmCanonizate,
            this.mcmHarmonizate});
            this.menuItem2.Text = "Variar";
            // 
            // mcmTransport
            // 
            this.mcmTransport.Index = 0;
            this.mcmTransport.Text = "Transporte...";
            this.mcmTransport.Click += new System.EventHandler(this.mcmTransport_Click);
            // 
            // mcmDelay
            // 
            this.mcmDelay.Index = 1;
            this.mcmDelay.Text = "Retardo...";
            this.mcmDelay.Click += new System.EventHandler(this.mcmDelay_Click);
            // 
            // mcmChangeNoteDuration
            // 
            this.mcmChangeNoteDuration.Index = 2;
            this.mcmChangeNoteDuration.Text = "Aumentación / Disminución...";
            this.mcmChangeNoteDuration.Click += new System.EventHandler(this.mcmChangeNoteDuration_Click);
            // 
            // mcmRetrogradation
            // 
            this.mcmRetrogradation.Index = 3;
            this.mcmRetrogradation.Text = "Retrogradación";
            this.mcmRetrogradation.Click += new System.EventHandler(this.mcmRetrogradation_Click);
            // 
            // mcmInversion
            // 
            this.mcmInversion.Index = 4;
            this.mcmInversion.Text = "Inversión";
            this.mcmInversion.Click += new System.EventHandler(this.mcmInversion_Click);
            // 
            // mcmRachmaninoffInversion
            // 
            this.mcmRachmaninoffInversion.Index = 5;
            this.mcmRachmaninoffInversion.Text = "Inversión Rachmaninoff";
            this.mcmRachmaninoffInversion.Click += new System.EventHandler(this.mcmRachmaninoffInversion_Click);
            // 
            // mcmTonalTransport
            // 
            this.mcmTonalTransport.Index = 6;
            this.mcmTonalTransport.Text = "Transporte Tonal...";
            this.mcmTonalTransport.Click += new System.EventHandler(this.mcmTonalTransport_Click);
            // 
            // mcmModulate
            // 
            this.mcmModulate.Index = 7;
            this.mcmModulate.Text = "Modulación estática...";
            this.mcmModulate.Click += new System.EventHandler(this.mcmModulate_Click);
            // 
            // mcmPermutate
            // 
            this.mcmPermutate.Index = 8;
            this.mcmPermutate.Text = "Permutación de triada";
            this.mcmPermutate.Click += new System.EventHandler(this.mcmPermutate_Click);
            // 
            // mcmInterpolation
            // 
            this.mcmInterpolation.Index = 9;
            this.mcmInterpolation.Text = "Interpolación";
            this.mcmInterpolation.Click += new System.EventHandler(this.mcmInterpolation_Click);
            // 
            // mcmElision
            // 
            this.mcmElision.Index = 10;
            this.mcmElision.Text = "Elipsis";
            this.mcmElision.Click += new System.EventHandler(this.mcmElision_Click);
            // 
            // mcmOrnamentate
            // 
            this.mcmOrnamentate.Index = 11;
            this.mcmOrnamentate.Text = "Adorno";
            this.mcmOrnamentate.Click += new System.EventHandler(this.mcmOrnamentate_Click);
            // 
            // mcmCanonizate
            // 
            this.mcmCanonizate.Index = 12;
            this.mcmCanonizate.Text = "Canonización";
            this.mcmCanonizate.Click += new System.EventHandler(this.mcmCanonizate_Click);
            // 
            // mcmHarmonizate
            // 
            this.mcmHarmonizate.Index = 13;
            this.mcmHarmonizate.Text = "Armonización";
            this.mcmHarmonizate.Click += new System.EventHandler(this.mcmHarmonizate_Click);
            // 
            // mcmSplit
            // 
            this.mcmSplit.Index = 4;
            this.mcmSplit.Text = "Dividir";
            this.mcmSplit.Click += new System.EventHandler(this.mcmSplit_Click);
            // 
            // mcmDuplicate
            // 
            this.mcmDuplicate.Index = 5;
            this.mcmDuplicate.Text = "Duplicar";
            this.mcmDuplicate.Click += new System.EventHandler(this.mcmDuplicate_Click);
            // 
            // mcmExport
            // 
            this.mcmExport.Index = 6;
            this.mcmExport.Text = "Exportar ABC...";
            this.mcmExport.Click += new System.EventHandler(this.mcmExport_Click);
            // 
            // mcmDelete
            // 
            this.mcmDelete.Index = 7;
            this.mcmDelete.Text = "Eliminar";
            this.mcmDelete.Click += new System.EventHandler(this.mcmDelete_Click);
            // 
            // playlistContextMenu
            // 
            this.playlistContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.pcmCopy,
            this.pcmPaste,
            this.pcmUntie,
            this.pcmSplit,
            this.pcmDelete});
            // 
            // pcmCopy
            // 
            this.pcmCopy.Index = 0;
            this.pcmCopy.Text = "Copiar";
            this.pcmCopy.Click += new System.EventHandler(this.pcmCopy_Click);
            // 
            // pcmPaste
            // 
            this.pcmPaste.Index = 1;
            this.pcmPaste.Text = "Pegar";
            this.pcmPaste.Click += new System.EventHandler(this.pcmPaste_Click);
            // 
            // pcmUntie
            // 
            this.pcmUntie.Index = 2;
            this.pcmUntie.Text = "Desligar";
            this.pcmUntie.Click += new System.EventHandler(this.pcmUntie_Click);
            // 
            // pcmSplit
            // 
            this.pcmSplit.Index = 3;
            this.pcmSplit.Text = "Dividir";
            this.pcmSplit.Click += new System.EventHandler(this.pcmSplit_Click);
            // 
            // pcmDelete
            // 
            this.pcmDelete.Index = 4;
            this.pcmDelete.Text = "Eliminar";
            this.pcmDelete.Click += new System.EventHandler(this.pcmDelete_Click);
            // 
            // wmpMotif
            // 
            this.wmpMotif.Enabled = true;
            this.wmpMotif.Location = new System.Drawing.Point(25, 341);
            this.wmpMotif.Name = "wmpMotif";
            this.wmpMotif.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("wmpMotif.OcxState")));
            this.wmpMotif.Size = new System.Drawing.Size(213, 49);
            this.wmpMotif.TabIndex = 4;
            // 
            // wmpTune
            // 
            this.wmpTune.Enabled = true;
            this.wmpTune.Location = new System.Drawing.Point(25, 404);
            this.wmpTune.Name = "wmpTune";
            this.wmpTune.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("wmpTune.OcxState")));
            this.wmpTune.Size = new System.Drawing.Size(213, 47);
            this.wmpTune.TabIndex = 3;
            // 
            // motifTreeView
            // 
            this.motifTreeView.ImageIndex = 0;
            this.motifTreeView.ImageList = this.treeviewImageList;
            this.motifTreeView.Location = new System.Drawing.Point(3, 49);
            this.motifTreeView.Name = "motifTreeView";
            this.motifTreeView.SelectedImageIndex = 0;
            this.motifTreeView.Size = new System.Drawing.Size(268, 410);
            this.motifTreeView.TabIndex = 2;
            this.motifTreeView.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.motifTreeView_AfterLabelEdit);
            this.motifTreeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.motifTreeView_MouseDown);
            this.motifTreeView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.motifTreeView_MouseUp);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 463);
            this.Controls.Add(this.wmpMotif);
            this.Controls.Add(this.wmpTune);
            this.Controls.Add(this.motifTreeView);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainWindow";
            this.Text = "Música Minimalista";
            this.SizeChanged += new System.EventHandler(this.MainWindow_SizeChanged);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.playListPanel.ResumeLayout(false);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wmpMotif)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wmpTune)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton tsbAddTrack;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnLoadMotif;
        private System.Windows.Forms.Panel playListPanel;
        private Controls.MotifTreeView motifTreeView;
        private Controls.PlayList playlist;
        private System.Windows.Forms.ToolStripButton tsbZoomIn;
        private System.Windows.Forms.ToolStripButton tsbZoomOut;
        private System.Windows.Forms.ContextMenu treeviewContextMenu;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.ImageList treeviewImageList;
        private System.Windows.Forms.MenuItem mcmTransport;
        private System.Windows.Forms.MenuItem mcmDelay;
        private System.Windows.Forms.MenuItem mcmRename;
        private System.Windows.Forms.ToolStripButton tsbPlay;
        private System.Windows.Forms.MenuItem mcmDelete;
        private System.Windows.Forms.ToolStripMenuItem editarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnUndo;
        private System.Windows.Forms.ToolStripMenuItem mnRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnExit;
        private System.Windows.Forms.MenuItem mcmPlay;
        private System.Windows.Forms.ContextMenu playlistContextMenu;
        private System.Windows.Forms.MenuItem pcmCopy;
        private System.Windows.Forms.MenuItem pcmPaste;
        private System.Windows.Forms.MenuItem pcmDelete;
        private System.Windows.Forms.ToolStripMenuItem mnOpenProject;
        private System.Windows.Forms.ToolStripMenuItem mnSaveProject;
        private System.Windows.Forms.ToolStripMenuItem mnSaveProjectAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.MenuItem mcmEditMotif;
        private System.Windows.Forms.ToolStripMenuItem mnExportMidi;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.MenuItem pcmUntie;
        private System.Windows.Forms.MenuItem mcmDuplicate;
        private System.Windows.Forms.ToolStripMenuItem mnExportMusicSheet;
        private System.Windows.Forms.ToolStripMenuItem mnNewProject;
        private System.Windows.Forms.ToolStripMenuItem mnExportABC;
        private System.Windows.Forms.MenuItem mcmExport;
        private System.Windows.Forms.MenuItem mcmRetrogradation;
        private System.Windows.Forms.MenuItem mcmInversion;
        private System.Windows.Forms.MenuItem mcmTonalTransport;
        private System.Windows.Forms.ToolStripButton tsbStop;
        private AxWMPLib.AxWindowsMediaPlayer wmpTune;
        private AxWMPLib.AxWindowsMediaPlayer wmpMotif;
        private System.Windows.Forms.ToolStripTextBox tsbTempo;
        private System.Windows.Forms.MenuItem mcmCanonizate;
        private System.Windows.Forms.MenuItem mcmModulate;
        private System.Windows.Forms.MenuItem mcmPermutate;
        private System.Windows.Forms.MenuItem mcmHarmonizate;
        private System.Windows.Forms.MenuItem mcmElision;
        private System.Windows.Forms.MenuItem mcmInterpolation;
        private System.Windows.Forms.ToolStripMenuItem mnNewMotif;
        private System.Windows.Forms.MenuItem mcmOrnamentate;
        private System.Windows.Forms.MenuItem mcmRachmaninoffInversion;
        private System.Windows.Forms.MenuItem mcmSplit;
        private System.Windows.Forms.MenuItem pcmSplit;
        private System.Windows.Forms.MenuItem mcmChangeNoteDuration;
    }
}

