using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MusicaMinimalista.Controls;
using AxWMPLib;

namespace MusicaMinimalista.Objects
{
    public class MediaPlayer
    {
        private readonly SynchronizationContext synchronizationContext;
        private AxWindowsMediaPlayer wmpTune;
        private AxWindowsMediaPlayer wmpMotif;
        private PlayList playlist;
        private double currentTime;
        private bool abort;

        public MediaPlayer(AxWMPLib.AxWindowsMediaPlayer wmpTune, AxWMPLib.AxWindowsMediaPlayer wmpMotif, PlayList playlist)
        {
            this.wmpTune = wmpTune;
            this.wmpMotif = wmpMotif;
            this.wmpTune.PlayStateChange += wmpTune_PlayStateChange;
            this.playlist = playlist;
            this.synchronizationContext = SynchronizationContext.Current;
            this.currentTime = 0;
            this.abort = false;
        }

        void wmpTune_PlayStateChange(object sender, _WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (e.newState == (int)WMPLib.WMPPlayState.wmppsPlaying)
            {
                abort = false;
                RunLoop();
            }
            else{
                abort = true;
            }
        }

        private void LoopRoutine()
        {
            double newcurTime = 0;
            while (true)
            {
                if (abort)
                {
                    synchronizationContext.Post(new SendOrPostCallback(o =>
                    {
                        playlist.setCurrentPlayerTime(0);
                        playlist.Invalidate();
                    }), null);
                    return;
                }

                newcurTime = wmpTune.Ctlcontrols.currentPosition;

                if (newcurTime != currentTime)
                {
                    currentTime = newcurTime;
                    synchronizationContext.Post(new SendOrPostCallback(o =>
                    {
                        playlist.setCurrentPlayerTime(currentTime);
                        playlist.Invalidate();
                    }), null);
                }
                Thread.Sleep(200);
            }
        }

        private async void RunLoop()
        {
            Action action = () => { LoopRoutine(); };
            await Task.Run(action);
        }

        public void Reset()
        {
            wmpTune.Ctlcontrols.stop();
            wmpTune.currentPlaylist.clear();
            wmpMotif.Ctlcontrols.stop();
            wmpMotif.currentPlaylist.clear();
        }

        public void SetTuneURL(string url)
        {
            wmpTune.URL = url;
        }

        public void SetMotifURL(string url)
        {
            wmpMotif.URL = url;
        }

        public void PlayTune(double startPosition)
        {
            wmpTune.Ctlcontrols.currentPosition = startPosition;
            wmpTune.Ctlcontrols.play();
        }

        public void StopTune()
        {
            wmpTune.Ctlcontrols.stop();
            wmpTune.currentPlaylist.clear();
            abort = true;
        }

        public void Dispose()
        {
            //TODO
        }

        public void PlayMotif()
        {
            wmpMotif.Ctlcontrols.play();
        }

        public void StopMotif()
        {
            wmpMotif.Ctlcontrols.stop();
        }

        public bool isTunePlaying()
        {
            return wmpTune.playState == WMPLib.WMPPlayState.wmppsPlaying;
        }
    }
}
