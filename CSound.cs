using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGZ_POGI3_
{
    public class CSound
    {
        MediaPlayer sound = new MediaPlayer();
        MediaPlayer music = new MediaPlayer();

        public CSound()
        {

        }

        public void play(string name)
        {
            sound.Open(new Uri(name, UriKind.Relative));
            sound.Play();
        }

        public void repeat(string name)
        {
            music.Open(new Uri(name, UriKind.Relative));
            music.MediaEnded += new EventHandler(Media_Ended);
            music.Play();
        }

        private void Media_Ended(object sender, EventArgs e)
        {
            music.Position = TimeSpan.Zero;
            music.Play();
        }

        public void stop()
        {
            music.Stop();
        }
    }
}
