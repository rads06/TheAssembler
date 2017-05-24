using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMCTerminal;
using LogicGates;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace demo
{
    public static class GameHelper
    {
        public static int ActiveModule;
        public static MusicPlayer player;
        public static IntroScreen intro;
        public static Terminal term;
        public static PowerBoard power;
        public static VentBoard vent;
        public static ControlConsole main;

        public static void Initialize()
        {
            ActiveModule = 0;
        }

        public static void Load()
        {
            player.Play(0);
        }

        public static void ChangeActiveModule(int num)
        {
            switch (num)
            {
                case 0:
                    ActiveModule = 0;
                    player.Play(0);
                    break;
                case 1:
                    ActiveModule = 1;
                    player.Play(1);
                    break;
                case 2:
                    ActiveModule = 2;
                    player.Play(1);
                    break;
                case 3:
                    ActiveModule = 3;
                    player.Play(1);
                    break;
                default:
                    ActiveModule = 4;
                    player.Play(1);
                    break;
            }
        }

        public static void UpdateActive(GameTime gt)
        {
            if (KeyboardExt.IsKeyJustPressed(Microsoft.Xna.Framework.Input.Keys.Escape))
            {
                ChangeActiveModule(4);
            }

            switch (ActiveModule)
            {
                case 0:
                    intro.Update(gt);
                    break;
                case 1:
                    term.Update();
                    break;
                case 2:
                    power.Update(gt);
                    break;
                case 3:
                    vent.Update(gt);
                    break;
                default:
                    main.Update();
                    break;
            }
        }

        public static void DrawActive(SpriteBatch sb)
        {
            switch (ActiveModule)
            {
                case 0:
                    intro.Draw(sb);
                    break;
                case 1:
                    term.Draw(sb);
                    break;
                case 2:
                    power.Draw(sb);
                    break;
                case 3:
                    vent.Draw(sb);
                    break;
                default:
                    main.Draw(sb);
                    break;
            }
        }
    }
}
