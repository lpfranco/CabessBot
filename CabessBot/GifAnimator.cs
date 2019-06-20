using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace CabessBot
{
    public class GifAnimator
    {
        private Form Animator { get; set; }
        private PictureBox PictureBox { get; set; }
        private int Width { get; set; }
        private int Height { get; set; }
        private string RunningPath { get; set; }
        private string ResourcePath { get; set; }
        private Timer Timer {get;set;}

        public GifAnimator()
        {
            RunningPath = AppDomain.CurrentDomain.BaseDirectory;
            ResourcePath = Path.GetFullPath(Path.Combine(RunningPath, @"..\..\Gifs\"));

            PictureBox = new PictureBox();
            PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

   
            Animator = new Form()
            {
                FormBorderStyle = FormBorderStyle.None,
                StartPosition = FormStartPosition.CenterScreen,
                TopMost = true
            };

            Timer = new Timer()
            {
                Interval = 5000,
            };

            Timer.Tick += Tick;
        }

        public void Animate(string gifFile)
        {
            var image = Image.FromFile($@"{ResourcePath}\{gifFile}.gif");

            Width = image.Width;
            Height = image.Height;

            PictureBox.Image = image;
            PictureBox.Width = image.Width;
            PictureBox.Height = image.Height;

            Animator.Width = image.Width;
            Animator.Height = image.Height;
            Animator.Controls.Add(PictureBox);

            Timer.Start();
            Animator.ShowDialog();
        }

        private void Tick(object sender, EventArgs e)
        {
            Animator.Close();
            Timer.Stop();
            Timer.Interval = 5000;
        }
    }
}

