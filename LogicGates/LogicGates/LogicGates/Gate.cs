using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LogicGates
{
    public abstract class Gate : IGamePiece
    {
        public Texture2D Texture { get; protected set; }
        public int Height { get { return this.Texture.Height; } }
        public int Width { get { return this.Texture.Width; } }
        
        //The below X and Y coords reflect the top left of the sprite
        public int X { get { return Convert.ToInt32(this.Loc.X - offset.X); } }
        public int Y { get { return Convert.ToInt32(this.Loc.Y - offset.Y); } }

        public Vector2 Loc { get; set; }

        //A Vector pointing from the topleft of the sprite to the center.
        Vector2 offset;
        
        public Rectangle Area 
        { 
            get 
            {
                return new Rectangle(X, Y, Width, Height);
            } 
        }

        public Gate(Texture2D Texture, Vector2 Loc)
        {
            this.Texture = Texture;
            this.Loc = Loc;

            //Calculate offset
            offset = new Vector2(88, 85);
        }

        public void Update(GameTime gt)
        {
            //Not implemented yet
        }

        /*public void SnapToOrigin()
        {
            this.Loc = this.origin;
        }*/

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Loc, null, Color.White, 0f, offset, 1f, SpriteEffects.None, 0f);
        }

        //This function needs to be overloaded.  By default it returns high voltage.
        public virtual Voltage GetOutput(List<ILogicInput> inputs, string namedOutput)
        {
            inputs.ForEach(delegate(ILogicInput l) { l.UpdateOutputs(); });
            return Voltage.None;
        }

        //Explicit implementation to be called by Hand when object is grabbed
        void IGamePiece.Update(Hand h)
        {
            this.Loc = h.Loc;
        }

        Rectangle IGamePiece.Area
        {
            get { return this.Area; }
        }
    }
}
