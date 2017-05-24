using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace demo
{
    public class IntroScreen : IPage
    {
        Game1 game;
        Vector2 versionVector;
        Texture2D titleImage, blkImage;
        Vector2 dPos = new Vector2(310, 410);
        Rectangle dRect = new Rectangle(321, 426, (481 - 321), (492 - 426));
        Color transColor = new Color(255, 255, 255, 0);
        byte aLevel = 255;
        Boolean aChange;
        SpriteFont font;
        string name;

        public IntroScreen(ContentManager Content, Game1 Game)
        {
            game = Game;
            name = "Intro";
            aChange = true;
            versionVector = new Vector2(700, 575);
            titleImage = Content.Load<Texture2D>("Title Image");
            blkImage = Content.Load<Texture2D>("Darkness");
            font = Content.Load<SpriteFont>("TerminalFont");
        }

        public void Update(GameTime gt)
        {

            if (aLevel > 254)
            {
                aChange = true;
            }
            if (aChange)
            {
                aLevel -= 5;
            }
            if (aLevel < 1)
            {
                aChange = false;
            }
            if (!aChange)
            {
                aLevel += 5;
            }
            transColor.A = aLevel;

            //Mouse controls
            if (MouseExt.IsLeftButtonJustPressed())
                if (dRect.Contains(MouseExt.State.X, MouseExt.State.Y))
                    game.ChangeActivePageTo("Mainframe");
        }

        public void Draw(SpriteBatch sb)
        {
            //GraphicsDevice.Clear(Color.DarkGray);
            sb.Draw(titleImage, Vector2.Zero, Color.White);
            sb.Draw(blkImage, dPos, transColor);
            sb.DrawString(font, "V. Log(0)", versionVector, Color.White);
        }

        #region Explicit Interface Implementation
        string IPage.Name
        {
            get { return name; }
        }

        void IPage.Update(GameTime gt)
        {
            this.Update(gt);
        }

        void IPage.Draw(SpriteBatch sb)
        {
            this.Draw(sb);
        }
        #endregion
    }
}
