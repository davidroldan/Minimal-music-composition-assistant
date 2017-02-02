using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using MusicaMinimalista.Objects;
using MusicaMinimalista.Objects.Music;
using MusicaMinimalista.Objects.Utils;
using MusicaMinimalista.Objects.Actions;

namespace MusicaMinimalista.Controls
{
    public partial class PlayList : Control
    {
        private Controller controller;
        private Duration step;
        private int beat;
        private int bar;
        private int n_bars;

        private int zoom;
        private int track_height;
        private int track_header;
        private Pen trackPen, horizontalBarPen, progressiveBarPen;
        private Font headerFont, motifFont;
        private const int HEADER_SIZE = 15;

        private int PanelHeight;

        private double currentPlayerTime;
        private int tempo;

        private List<TrackHeader> trackHeaderList;
        private bool updatingHeader;

        private class TrackHeader{
            public ComboBox timbreBox;
            public Button deleteButton;
            public TrackBar volumeBar;
            
            public TrackHeader(ComboBox timbreBox, Button deleteButton, TrackBar volumeBar)
            {
                this.timbreBox = timbreBox;
                this.deleteButton = deleteButton;
                this.volumeBar = volumeBar;
            }

            public void removeFromControl(ControlCollection control)
            {
                control.Remove(timbreBox);
                control.Remove(deleteButton);
                control.Remove(volumeBar);
            }

            public void addToControl(ControlCollection control)
            {
                control.Add(timbreBox);
                control.Add(deleteButton);
                control.Add(volumeBar);
            }
        }

        public PlayList()
        {
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer,
                true
            );
            this.AllowDrop = true;
            this.trackHeaderList = new List<TrackHeader>();
            InitializeComponent();
            SetDefaultValues();
        }

        private void SetDefaultValues()
        {
            this.step = 1;
            this.beat = 4;
            this.bar = 4; 
            this.n_bars = 30;

            this.zoom = 5;
            this.track_height = 50;
            this.track_header = 150;
            this.trackPen = new Pen(Color.FromArgb(255, 255, 255, 255), 1);
            this.horizontalBarPen = new Pen(Color.FromArgb(255, 205, 205, 205), 4);
            this.progressiveBarPen = new Pen(Color.FromArgb(255, 0, 205, 0), 2);
            this.headerFont = new Font("Calibri", 10, FontStyle.Regular, GraphicsUnit.Point);
            this.motifFont = new Font("Calibri", 10, FontStyle.Regular, GraphicsUnit.Point);

            this.selectedMotifIndex = -1;
            this.currentPlayerTime = 0;
            this.tempo = 120;
            this.selectedMotif = null;

            this.updatingHeader = false;
        }

        public void setController(Controller c)
        {
            this.controller = c;
            this.Invalidate();
        }

        public void OnResizePanel(int width, int height)
        {
            this.PanelHeight = height;
            this.Invalidate();
        }

        public void zoomOut()
        {
            if (this.zoom > 3) this.zoom--;
            this.Invalidate();
        }

        public void zoomIn()
        {
            if (this.zoom < 40) this.zoom++;
            this.Invalidate();
        }

        public Duration getTrackDuration()
        {
            return step * beat * bar * n_bars;
        }

        public double getCurrentPlayerTime()
        {
            return this.currentPlayerTime;
        }

        public void setCurrentPlayerTime(double playerTime)
        {
            this.currentPlayerTime = playerTime;
        }

        public void setTempo(int tempo)
        {
            this.currentPlayerTime *= this.tempo / tempo; //Fix currentPlayerTime
            this.tempo = tempo;
        }

        #region Mouse Related Functions
        public KeyValuePair<int, Duration> getClickPosition(int x, int y)
        {
            int track = (y - HEADER_SIZE) / track_height;
            Duration selectedStart = (x - this.track_header) / zoom;
            return new KeyValuePair<int,Duration>(track, selectedStart);
        }

        public KeyValuePair<int, Duration> getActiveMotifPosition()
        {
            return new KeyValuePair<int, Duration>(this.selectedTrack, this.currentPosition);
        }

        public Motif getActiveMotif()
        {
            if (selectedMotifIndex == -1) return null;
            return this.controller.getMotifFromTrack(selectedTrack, selectedMotifIndex);
        }

        public void selectMotif(string name)
        {
            this.selectedMotif = controller.getMotifFromName(name);
        }

        public bool headerClicked(int x, int y)
        {
            if (y > HEADER_SIZE) return false;
            this.currentPlayerTime = (x - this.track_header) * 60.0 / zoom / tempo;
            this.Invalidate();
            return true;
        }

        public bool pickElement(int x, int y)
        {
            this.relativeStart = 0;

            //Get selected track
            int track = (y - HEADER_SIZE) / track_height;
            Duration selectedStart = (x - this.track_header) / zoom;
            int motifIndex = controller.getMotifIndexFromTrack(track, selectedStart);

            if (motifIndex < 0)
            {
                this.selectedMotifIndex = -1;
                this.selectedMotif = null;
                return false;
            }
            else
            {
                this.currentPosition = this.controller.getStartTimeFromTrack(track, motifIndex);
                this.relativeStart = selectedStart - this.currentPosition;
                this.selectedMotifIndex = motifIndex;
                this.selectedTrack = track;
                Motif m = controller.getMotifFromTrack(selectedTrack, selectedMotifIndex);
                this.selectedMotif = m;
                controller.selectMotifOnTreeView(m.getName());
                return true;
            }            
        }

        public void moveElement(int x, int y)
        {
            //Get selected track
            int track = (y - HEADER_SIZE) / track_height;
            Motif motif = getActiveMotif();

            //Get target position
            Duration targetPosition = (x - this.track_header) / zoom - relativeStart;
            Duration trackDuration = getTrackDuration();
            if (trackDuration < targetPosition + motif.getDuration())
            {
                targetPosition = trackDuration - motif.getDuration();
            }
            if (targetPosition < 0) targetPosition = 0;

            if (track != this.selectedTrack && this.controller.fitsMotif(motif, track, targetPosition))
            {
                //Update Motif's position and update selectedMotif index
                this.controller.removeMotif(selectedTrack, currentPosition);
                this.controller.insertMotif(motif.getId(), track, targetPosition);
                this.selectedMotifIndex = this.controller.getMotifIndexFromTrack(track, targetPosition);
                this.selectedTrack = track;
                this.currentPosition = targetPosition;
                this.Invalidate();
            }
            else if (this.controller.fitsMotif(motif, selectedTrack, targetPosition, this.selectedMotifIndex))
            {
                //Update Motif's position and update selectedMotif index
                this.controller.removeMotif(selectedTrack, currentPosition);
                this.controller.insertMotif(motif.getId(), selectedTrack, targetPosition);
                this.selectedMotifIndex = this.controller.getMotifIndexFromTrack(selectedTrack, targetPosition);
                this.currentPosition = targetPosition;
                this.Invalidate();
            }
        }

        private Duration relativeStart;
        private Duration currentPosition;
        private int selectedMotifIndex;
        private int selectedTrack;
        private Motif selectedMotif;
        #endregion

        private void debugPaint(PaintEventArgs e)
        {
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Near;
            stringFormat.LineAlignment = StringAlignment.Near;
            Rectangle r = new Rectangle(0, 250, 250, 50);
            e.Graphics.DrawString("Selected Motif: " + this.selectedMotifIndex, headerFont, Brushes.White, r, stringFormat);
            r = new Rectangle(0, 260, 250, 50);
            e.Graphics.DrawString("Selected Track: " + this.selectedTrack, headerFont, Brushes.White, r, stringFormat);
            r = new Rectangle(0, 270, 250, 50);
            e.Graphics.DrawString("Current pos: " + this.currentPosition, headerFont, Brushes.White, r, stringFormat);
            r = new Rectangle(0, 280, 250, 50);
            e.Graphics.DrawString("Zoom: " + this.zoom, headerFont, Brushes.White, r, stringFormat);
            r = new Rectangle(0, 290, 250, 50);
            e.Graphics.DrawString("Cur.point: " + this.currentPlayerTime, headerFont, Brushes.White, r, stringFormat);
        }

        public void updateHeader()
        {
            if (controller == null) return;
            this.updatingHeader = true;

            while (this.trackHeaderList.Count > this.controller.trackCount())
            {
                TrackHeader header = trackHeaderList.ElementAt(this.trackHeaderList.Count - 1);
                header.removeFromControl(this.Controls);
                this.trackHeaderList.Remove(header);
            }
            for (int i = 0; i < this.trackHeaderList.Count; i++)
            {
                Timbre t = controller.getTimbre(i);
                this.trackHeaderList[i].timbreBox.SelectedIndex = (int)t;
                int volume = controller.getVolume(i);
                this.trackHeaderList[i].volumeBar.Value = volume;
            }
            while (this.trackHeaderList.Count < this.controller.trackCount())
            {
                ComboBox cb = new ComboBox();
                cb.Location = new System.Drawing.Point(0, HEADER_SIZE + this.track_height * this.trackHeaderList.Count);
                cb.Width = this.track_header - 2;
                cb.BackColor = Color.FromArgb(255, 0, 0, 0);
                cb.ForeColor = Color.FromArgb(255, 255, 255, 255);
                cb.FlatStyle = FlatStyle.Flat;
                cb.DataSource = Enum.GetNames(typeof(Timbre)).Select(s => s.Replace("_", " ")).ToArray();
                cb.SelectedValue = controller.getTimbre(this.trackHeaderList.Count);
                cb.Name = Convert.ToString(this.trackHeaderList.Count);
                cb.SelectedIndexChanged += combobox_indexChanged;

                Button b = new Button();
                b.Location = new System.Drawing.Point(5, HEADER_SIZE + this.track_height * this.trackHeaderList.Count + 26);
                b.Size = new System.Drawing.Size(18, 18);
                b.BackColor = System.Drawing.Color.Black;
                b.BackgroundImage = global::MusicaMinimalista.Properties.Resources.remove_icon;
                b.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                b.Name = Convert.ToString(this.trackHeaderList.Count);
                b.Click += button_Click;

                TrackBar tb = new TrackBar();
                tb.BackColor = System.Drawing.Color.Black;
                tb.Location = new System.Drawing.Point(55, HEADER_SIZE + this.track_height * this.trackHeaderList.Count + 26);
                System.Drawing.Size size = new System.Drawing.Size(80, 20);
                tb.MaximumSize = size;
                tb.MinimumSize = size;
                tb.Size = size;
                tb.Maximum = Track.MAX_VOLUME;
                tb.Value = Track.MAX_VOLUME / 2;
                tb.Name = Convert.ToString(this.trackHeaderList.Count);
                tb.TickStyle = System.Windows.Forms.TickStyle.None;
                tb.ValueChanged += trackBar_valueChanged;

                TrackHeader header = new TrackHeader(cb, b, tb);
                this.trackHeaderList.Add(header);
                header.addToControl(this.Controls);
            }

            this.updatingHeader = false;
        }

        void button_Click(object sender, EventArgs e)
        {
            if (controller.trackCount() == 1) return;

            Button b = (Button)sender;
            int track = int.Parse(b.Name);

            ProgramAction action = new DeleteTrackAction(this.controller, track, this.controller.getTrack(track));
            this.controller.executeAction(action);
        }

        void trackBar_valueChanged(object sender, EventArgs e)
        {
            if (controller.isExecutingAction() || this.updatingHeader) return;

            TrackBar t = (TrackBar)sender;
            int track = int.Parse(t.Name);

            ProgramAction action = new SetVolumeAction(this.controller, track, this.controller.getVolume(track), t.Value);
            this.controller.executeAction(action);
        }

        void combobox_indexChanged(object sender, EventArgs e)
        {
            if (controller.isExecutingAction() || this.updatingHeader) return;

            ComboBox cb = (ComboBox)sender;
            int track = int.Parse(cb.Name);

            ProgramAction action = new SetTimbreAction(
                this.controller,
                track,
                this.controller.getTimbre(track),
                (Timbre)Enum.Parse(typeof(Timbre), ((string)cb.SelectedValue).Replace(" ", "_"))
            );
            this.controller.executeAction(action);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (controller != null)
            {
                this.Width = bar * beat * zoom * n_bars + this.track_header;
                this.Height = Math.Max(controller.trackCount() * track_height + HEADER_SIZE + 5, this.PanelHeight - 21);
            }

            base.OnPaint(e);
            if (controller == null) return;

            int trackCount = this.controller.trackCount();
            int stepsize = zoom;
            int beatsize = beat * zoom;
            int barsize = beatsize * bar;
            
            // Top Header
            e.Graphics.FillRectangle(Brushes.Blue, 0, 0, this.Width, HEADER_SIZE);
            e.Graphics.DrawLine(this.trackPen, 0, 0, this.Width, 0);
            for (int i = 0; i < n_bars; i++)
            {
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Near;
                stringFormat.LineAlignment = StringAlignment.Near;
                Rectangle r = new Rectangle(i * barsize + this.track_header + 5, 0, barsize, HEADER_SIZE);
                e.Graphics.DrawString(Convert.ToString(i + 1), headerFont, Brushes.White, r, stringFormat);
            }

            // Track Headers
            for (int i = 0; i < trackCount; i++)
            {
                e.Graphics.FillRectangle(Brushes.Black, 0, HEADER_SIZE + track_height * i, this.track_header, track_height);
            }

            // Horizontal lines (Track)
            for (int i = 0; i < trackCount + 1; i++)
            {
                e.Graphics.DrawLine(this.trackPen, 0, HEADER_SIZE + track_height * i, this.Width, HEADER_SIZE + track_height * i);
            }

            // Vertical lines (Track)
            for (int i = 0; i <= n_bars * bar; i++)
            {
                if (i == 0) e.Graphics.DrawLine(this.horizontalBarPen, this.track_header + beatsize * i, 0, this.track_header + beatsize * i, HEADER_SIZE + trackCount * track_height);
                else if (i % bar == 0) e.Graphics.DrawLine(this.horizontalBarPen, this.track_header + beatsize * i, HEADER_SIZE, this.track_header + beatsize * i, HEADER_SIZE + trackCount * track_height);
                else e.Graphics.DrawLine(this.trackPen, this.track_header + beatsize * i, HEADER_SIZE, this.track_header + beatsize * i, HEADER_SIZE + trackCount * track_height);
            }

            // Paint Non-Selected Motifs
            for (int i = 0; i < trackCount; i++)
            {
                for (int j = 0; j < controller.trackElementCount(i); j++)
                {
                    Motif m = controller.getMotifFromTrack(i, j);
                    if (m != this.selectedMotif)
                    {
                        Duration start = controller.getStartTimeFromTrack(i, j);
                        this.paintMotif(e, m, start, i, j);
                    }
                }
            }

            // Repaint Selected Motifs
            for (int i = 0; i < trackCount; i++)
            {
                for (int j = 0; j < controller.trackElementCount(i); j++)
                {
                    Motif m = controller.getMotifFromTrack(i, j);
                    if (m == this.selectedMotif)
                    {
                        Duration start = controller.getStartTimeFromTrack(i, j);
                        this.paintMotif(e, m, start, i, j);
                    }
                }
            }

            // Paint Temporal Line
            int paintPosition = (int)(this.currentPlayerTime * zoom * tempo / 60) + this.track_header;
            e.Graphics.DrawLine(this.progressiveBarPen, paintPosition, 0, paintPosition, HEADER_SIZE + trackCount * track_height);

            //debugPaint(e);
        }

        private void paintMotif(PaintEventArgs e, Motif motif, Duration start, int track, int motifPosition)
        {
            Duration length = motif.getDuration();
            string name = motif.getName();
            Rectangle rect = new Rectangle(this.track_header + (zoom * start).ToInt(), track * track_height + HEADER_SIZE, (zoom * length).ToInt(), track_height);

            Color color = getMotifBaseColor(motif);

            SolidBrush br = new SolidBrush(ColorUtil.decreaseBrightness(color, 70));
            Pen pen = new Pen(color, 2);

            if (motifPosition == this.selectedMotifIndex && start == this.currentPosition && track == this.selectedTrack)
            {
                br = new SolidBrush(ColorUtil.increaseBrightness(color, 40));
            }
            if (motif == this.selectedMotif)
            {
                pen = new Pen(Color.Yellow, 3);
            }

            e.Graphics.FillRectangle(br, rect);
            e.Graphics.DrawRectangle(pen, rect);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            e.Graphics.DrawString(name, this.motifFont, Brushes.White, rect, stringFormat);
        }

        private Color getMotifBaseColor(Motif motif)
        {
            return Color.FromArgb(255, 255, 0, 0);
        }
    }
}
