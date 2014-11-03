using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Music
    {
        public static System.Media.SoundPlayer BMusic = new System.Media.SoundPlayer(WindowsFormsApplication1.Properties.Resources.pipa);
        public bool flag = true;
        public void StartMusic()
        {
            BMusic.PlayLooping();
        }
        public void StopMusic()
        {
            BMusic.Stop();
        }
        
    }
}
