using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace demo
{
    public class MusicPlayer
    {
        private int c = 0;
        protected Song titleSong, mainSong, dieSong;
        public MusicPlayer(ContentManager Content)
        {
            MediaPlayer.IsRepeating = true;
            titleSong = Content.Load<Song>("TitleScreen");
            mainSong = Content.Load<Song>("Main Game");
            dieSong = Content.Load<Song>("Time Running Out");
        }
        public void Play(int s)
        {
            if (c != 1 && s == 0)
            {
                MediaPlayer.Play(titleSong);
                c = 1;
            }
            if (c != 2 && s == 1)
            {
                MediaPlayer.Play(mainSong);
                c = 2;
            }
            if (c != 3 && s == 2)
            {
                MediaPlayer.Play(dieSong);
                c = 3;
            }
        }
    }
}
