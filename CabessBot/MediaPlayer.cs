using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMPLib;

namespace CabessBot
{
    public class MediaPlayer
    {
        private WindowsMediaPlayer Player { get; set; }
        private string RunningPath { get; set; }
        private string ResourcePath { get; set; }
        public MediaPlayer()
        {
            Player = new WindowsMediaPlayer();
            RunningPath = AppDomain.CurrentDomain.BaseDirectory;
            ResourcePath = Path.GetFullPath(Path.Combine(RunningPath, @"..\..\MemesSounds\"));
        }

        public void Play(string memeSong)
        {
            Player.URL = ResourcePath + memeSong + ".mp3";
            Player.controls.play();
        }
    }
}
