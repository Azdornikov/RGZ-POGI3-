using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RGZ_POGI3_
{
    public class CSound
    {
        MediaPlayer sound = new MediaPlayer();
        MediaPlayer music = new MediaPlayer();

        public CSound()
        {

        }

        public void play(string name) // проигрывание звука
        {
            if (Path.GetExtension(name) != ".mp3") // сравниваем расширение (формат)
            {
                throw new Exception("музыку в студию"); // вызываем ошибку
            }

            sound.Open(new Uri(name, UriKind.Relative));
            sound.Play();
        }

        public void repeat(string name) // повтор
        {
            music.Open(new Uri(name, UriKind.Relative));
            music.MediaEnded += new EventHandler(Media_Ended);
            music.Play();
        }

        private void Media_Ended(object sender, EventArgs e) // определение конца музыки
        {
            music.Position = TimeSpan.Zero;
            music.Play();
        }

        public void stop() // остановка
        {
            music.Stop();
        }
    }
}
