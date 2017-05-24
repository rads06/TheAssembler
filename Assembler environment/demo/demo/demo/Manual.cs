using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace demo
{
    public class Manual : IPage
    {
        Stack<Texture2D> frontPages, backPages;
        Rectangle advRect, retRect;

        public Manual(ContentManager Content)
        {
            frontPages = new Stack<Texture2D>();
            backPages = new Stack<Texture2D>();
            backPages.Push(Content.Load<Texture2D>("Manual - P3"));
            backPages.Push(Content.Load<Texture2D>("Manual - P2"));
            backPages.Push(Content.Load<Texture2D>("Manual - P1"));

            advRect = new Rectangle(400, 0, 400, 600);
            retRect = new Rectangle(0, 0, 400, 600); 
        }

        void AdvancePage()
        {
            if (backPages.Count <= 1) return;
            frontPages.Push(backPages.Pop());
        }

        void ReturnPage()
        {
            if (frontPages.Count <= 0) return;
            backPages.Push(frontPages.Pop());
        }

        public string Name
        {
            get { return "Manual"; }
        }

        public void Update(Microsoft.Xna.Framework.GameTime gt)
        {
            if (MouseExt.IsLeftButtonJustPressed())
                if (advRect.Contains(MouseExt.State.X, MouseExt.State.Y)) AdvancePage();
                else if (retRect.Contains(MouseExt.State.X, MouseExt.State.Y)) ReturnPage();
        }

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch sb)
        {
            sb.Draw(backPages.Peek(), new Rectangle(0, 0, 800, 600), Color.White);
        }

        string IPage.Name { get { return this.Name; } }
        void IPage.Update(Microsoft.Xna.Framework.GameTime gt) { this.Update(gt); }
        void IPage.Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch sb) { this.Draw(sb); }
    }
}
